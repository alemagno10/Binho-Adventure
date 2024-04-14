using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {  
    public float hp = 100f;  

    public virtual void TakeDamage(int damage){
        this.hp -= damage;
        if(this.hp <= 0){
            Destroy(gameObject);
        }
    }

    protected void SetHP(float value) {
        hp = value;
    }

    protected float GetHP() {
        return hp;
    }
}