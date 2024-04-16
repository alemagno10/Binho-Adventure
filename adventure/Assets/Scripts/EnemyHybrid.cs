using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHybrid : Entity {
    public int speed = 5;
    public int damage = 30;
    public float flipTime;
    public float flipDelta;

    public GameObject bulletPrefab;
    public Rigidbody2D player;
    private System.Random rand = new System.Random();

    void Start(){
        flipDelta = rand.Next(1, 4);
        InvokeRepeating("Flip", 1.0f, flipDelta);
        InvokeRepeating("Shoot", rand.Next(1,4), 3.0f);
        SetHP(20f);
    }

    void Update(){
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void Shoot(){
        Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, fixedRotation);
        bullet.GetComponent<Bullet>().SetSelf("Enemy");
        bullet.GetComponent<Bullet>().SetTarget("Player");
        if(transform.position.x < player.position.x){
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }
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

        if (gameObject.CompareTag("Wall")){
            Flip();
        }
    }

    void Flip(){
        speed *= -1;
    }

    public override void TakeDamage(int damage){
        base.TakeDamage(damage);
    }

    public override void handleDeath(){
        Destroy(gameObject);
    }

}