using UnityEngine;

public class EnemyRunner : Entity {
    public int speed = 5;
    public int damage = 30;
    public float flipDelta;

    // Audio properties
    public AudioClip damageSound; // AudioClip for when taking damage
    public AudioClip deathSound; // AudioClip for when dying
    public float damageVolume = 0.5f; // Volume regulator for the damage sound
    public float deathVolume = 0.5f; // Volume regulator for the death sound

    private AudioSource audioSource;
    private bool isDead = false; // Flag to ensure death is handled only once
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
        if (!isDead) {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    void Flip() {
        speed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (isDead) {
            return; // Ignore collisions if already dead
        }

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
        if (isDead) {
            return; // Ignore if already dead
        }

        base.TakeDamage(damage);
        if (hp <= 0) {
            handleDeath();
        } else {
            PlaySound(damageSound, damageVolume);
        }
    }

    public override void handleDeath() {
        if (!isDead) {
            isDead = true; // Set the flag to prevent multiple death handling
            PlaySound(deathSound, deathVolume);
            // Optionally, disable the collider here if the object has one
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null) {
                collider.enabled = false;
            }
            Destroy(gameObject, deathSound.length); // Delay destruction to allow sound to play
        }
    }

    // Helper method to play a sound
    void PlaySound(AudioClip clip, float volume) {
        if (audioSource != null && clip != null) {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
