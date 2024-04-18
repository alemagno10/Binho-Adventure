using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : Entity {
    public int speed = 5;
    public int damage = 30;
    public float flipTime;
    public float flipDelta;

    // Audio properties
    public AudioClip damageSound; // AudioClip for when taking damage
    public AudioClip deathSound; // AudioClip for when dying
    public float damageVolume = 0.5f; // Volume regulator for the damage sound
    public float deathVolume = 0.5f; // Volume regulator for the death sound

    private AudioSource audioSource;
    private System.Random rand = new System.Random();

    void Start() {
        flipDelta = rand.Next(1, 4);
        InvokeRepeating("Flip", 1.0f, flipDelta);
        SetHP(20f);
        
        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update() {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void Flip() {
        speed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject gameObject = collision.gameObject;

        if (gameObject.CompareTag("Player")) {
            if((gameObject.GetComponent<Entity>().transform.position.y - transform.position.y) < 0.5) {
                gameObject.GetComponent<Entity>().TakeDamage(damage);
                PlaySound(damageSound, damageVolume);
            } else {
                TakeDamage((int)GetHP());
            }
        }

        if (gameObject.CompareTag("Wall") || gameObject.CompareTag("Enemy")) {
            Flip();
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        PlaySound(damageSound, damageVolume);
    }

    public override void handleDeath() {
        PlaySound(deathSound, deathVolume);
        Destroy(gameObject);
    }

    // Helper method to play a sound
    void PlaySound(AudioClip clip, float volume) {
        if (audioSource != null && clip != null) {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
