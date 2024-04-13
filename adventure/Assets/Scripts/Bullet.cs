using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 18f;
    
    void Start(){
        rb.velocity = speed * transform.right;
    }
    
    void OnTriggerEnter2D(Collider2D hit){
        if(!hit.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
