using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Entity {

    public GameObject bulletPrefab;
    public Rigidbody2D player;

    void Start(){
        SetHP(30f);
        InvokeRepeating("Shoot", 3.0f, 3.0f);
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
}
