using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : Entity {
    public int speed = 5;
    public int damage = 30;
    public float flipTime;
    public float flipDelta;
    
    private System.Random rand = new System.Random();

    void Start(){
        flipDelta = rand.Next(1, 4);
        InvokeRepeating("Flip", 1.0f, flipDelta);
        SetHP(20f);
    }

    void Update(){
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void Flip(){
        speed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision){
        GameObject gameObject = collision.gameObject;
        
        if (gameObject.CompareTag("Player")){
            if((gameObject.GetComponent<Entity>().transform.position.y - transform.position.y) < 0.5){
                gameObject.GetComponent<Entity>().TakeDamage(damage);
            } else {
                TakeDamage((int)GetHP());
            }
        }

        if (gameObject.CompareTag("Wall") || gameObject.CompareTag("Enemy")){
            Flip();
        }
    }

    public override void TakeDamage(int damage){
        base.TakeDamage(damage);
    }

    public override void handleDeath(){
        Destroy(gameObject);
    }
}