using UnityEngine;

public class Entity : MonoBehaviour {
    public float hp = 30f;
    public AudioClip damageSound; // AudioClip for when taking damage

    private AudioSource audioSource; // AudioSource component to play the sound

    void Start() {
        // Get the AudioSource component on the same GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();
        // If there isn't one, add it dynamically
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void TakeDamage(int damage) {
        this.hp -= damage;
        // Play the damage sound
        if (audioSource != null && damageSound != null) {
            audioSource.PlayOneShot(damageSound);
        }

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
}
