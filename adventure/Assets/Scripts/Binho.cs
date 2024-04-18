using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binho : MonoBehaviour {
    private Transform player; 
    public float followSpeed = 2.0f;
    public float followDistance = 5.0f; 

    void Start(){
        player = GameObject.Find("Player").transform;
    }

    void Update(){
        if (player.position.x > transform.position.x){
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceToPlayer > followDistance){
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x - followDistance, transform.position.y), followSpeed * Time.deltaTime);
            }
        }
    }
}
