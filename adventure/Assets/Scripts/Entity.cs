using UnityEngine;

public class Entity : MonoBehaviour {
    public float hp = 30f;
    public float totalHp = 30f;

    void Start() {
        
    }

    public virtual void TakeDamage(int damage) {
        this.hp -= damage;


        if (this.hp <= 0) {
            handleDeath();
        }
    }

    public virtual void handleDeath() {
        Destroy(gameObject);
    }

    protected void SetHP(float value) {
        hp = value;
    }

    public float GetHP() {
        return hp;
    }

    public float GetTotalHp() {
        return totalHp;
    }
}
