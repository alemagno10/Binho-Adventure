using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 18f;

    private float lifeTime = 1.5f;
    private float timer = 0f;
    private float damage = 10f;
    private string self = "Player";
    private string target = "Enemy";
    
    void Start(){
        rb.velocity = speed * transform.right;
    }

    void Update(){
        timer += Time.deltaTime;
        if(timer >= lifeTime){
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D hit){
        if(!hit.gameObject.CompareTag(self)){
            if(hit.gameObject.CompareTag(target)){
                hit.GetComponent<Entity>().TakeDamage((int)damage);
            }
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage){
        this.damage = damage;
    }

    public void SetSelf(string self){
        this.self = self;
    }

    public void SetTarget(string target){
        this.target = target;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }
}
