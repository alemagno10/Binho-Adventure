using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHybrid : Entity {
    public int speed = 5;
    public int damage = 30;
    public float flipTime;
    public float flipDelta;

    public GameObject bulletPrefab;
    public Rigidbody2D player;
    private System.Random rand = new System.Random();

    // Audio properties
    public AudioClip shootSound; // Reference to the shooting sound clip
    public AudioClip damageSound; // AudioClip for when taking damage
    public AudioClip deathSound; // AudioClip for when dying
    public float shootingVolume = 0.5f; // Public variable to control the shooting volume
    public float damageVolume = 0.5f; // Public variable to control the damage sound volume
    public float deathVolume = 0.5f; // Public variable to control the death sound volume

    private AudioSource audioSource; // This will reference the AudioSource component

    void Start() {
        flipDelta = rand.Next(1, 4);
        InvokeRepeating("Flip", 1.0f, flipDelta);
        InvokeRepeating("Shoot", rand.Next(1,4), 3.0f);
        SetHP(20f);

        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            // Add an AudioSource component if not already present
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update() {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void Shoot() {
        Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, fixedRotation);
        bullet.GetComponent<Bullet>().SetSelf("Enemy");
        bullet.GetComponent<Bullet>().SetTarget("Player");
        if (transform.position.x < player.position.x) {
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }

        // Play the shooting sound with 3D audio effects
        Play3DSound(shootSound, transform.position, shootingVolume);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject gameObject = collision.gameObject;
        
        if (gameObject.CompareTag("Player")) {
            if ((gameObject.GetComponent<Entity>().transform.position.y - transform.position.y) < 0.5) {
                gameObject.GetComponent<Entity>().TakeDamage(damage);
                PlaySoundAndDestroy(damageSound, damageVolume);
            } else {
                TakeDamage((int)GetHP());
            }
        }

        if (gameObject.CompareTag("Wall") || gameObject.CompareTag("Enemy")) {
            Flip();
        }
    }

    void Flip() {
        speed *= -1;
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        PlaySoundAndDestroy(damageSound, damageVolume);
    }

    public override void handleDeath() {
        PlaySoundAndDestroy(deathSound, deathVolume);
        Destroy(gameObject);
    }

    // Plays a sound with 3D audio at the source position
    void Play3DSound(AudioClip clip, Vector3 position, float volume) {
        GameObject tempAudioSource = new GameObject("TempAudio");
        tempAudioSource.transform.position = position;  // Position the source at the bullet's location
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

        // Configure AudioSource for 3D sound
        audioSource.spatialBlend = 1.0f;  // Fully 3D sound
        audioSource.rolloffMode = AudioRolloffMode.Linear;  // Linear rolloff is more intuitive
        audioSource.minDistance = 1;  // Close range
        audioSource.maxDistance = 50;  // Max range where the sound is audible

        audioSource.PlayOneShot(clip, volume);
        Destroy(tempAudioSource, clip.length);
    }

    void PlaySoundAndDestroy(AudioClip clip, float volume) {
    if (clip != null) {
        // Create a temporary GameObject for the AudioSource
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = transform.position; // Set its position

        // Add an AudioSource to the temp GameObject
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
        // Configure the AudioSource for 3D sound
        tempAudioSource.spatialBlend = 1.0f; // Set to 3D sound
        tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
        tempAudioSource.minDistance = 1.0f;
        tempAudioSource.maxDistance = 50.0f;

        // Play the clip at the specified volume
        tempAudioSource.PlayOneShot(clip, volume);

        // Destroy the temporary AudioSource after the clip has finished playing
        Destroy(tempAudioObject, clip.length);
    }
}

}
