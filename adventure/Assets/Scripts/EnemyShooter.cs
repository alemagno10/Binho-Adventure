using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Entity {

    public GameObject bulletPrefab;
    public Rigidbody2D player;
    private System.Random rand = new System.Random();

    void Start(){
        SetHP(30f);
        InvokeRepeating("Shoot", rand.Next(1,4), 3.0f);
    }

    public override void TakeDamage(int damage){
        base.TakeDamage(damage);
    }

    public override void handleDeath(){
        Destroy(gameObject);
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
                gameObject.GetComponent<Entity>().TakeDamage(5);
            } else {
                TakeDamage((int)GetHP());
            }
        }
    }
}
