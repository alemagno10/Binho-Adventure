using UnityEngine;

public class EnemyShooter : Entity {
    public GameObject bulletPrefab;
    public Rigidbody2D player;
    public AudioClip shootSound;  // Reference to the shooting sound clip
    public AudioClip damageSound;  // AudioClip for when taking damage
    public AudioClip deathSound;  // AudioClip for when dying
    public float shootingVolume = 0.05f;  // Public variable to control the shooting volume
    public float damageVolume = 0.5f;  // Public variable to control the damage sound volume
    public float deathVolume = 1.5f;  // Public variable to control the death sound volume

    private AudioSource audioSource;

    void Start() {
        SetHP(30f);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        InvokeRepeating("Shoot", Random.Range(1, 4), 3.0f);
    }

    public override void handleDeath() {
        PlaySoundAndDestroy(deathSound, deathVolume);
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        PlaySoundAndDestroy(damageSound, damageVolume);
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Bullet>().SetSelf("Enemy");
        bullet.GetComponent<Bullet>().SetTarget("Player");

        if (transform.position.x < player.position.x) {
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }

        Play3DSound(shootSound, transform.position, shootingVolume);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            float verticalOffset = collision.gameObject.GetComponent<Entity>().transform.position.y - transform.position.y;
            if (verticalOffset < 0.5) {
                collision.gameObject.GetComponent<Entity>().TakeDamage(5);
            } else {
                TakeDamage((int)GetHP());
            }
        }
    }

    // Plays a sound at the source position with 3D audio settings
    void Play3DSound(AudioClip clip, Vector3 position, float volume) {
        GameObject tempAudioSource = new GameObject("TempAudio");
        tempAudioSource.transform.position = position;  // Position the source at the bullet's location
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

        // Configure AudioSource for 3D sound
        audioSource.spatialBlend = 1.0f;  // Fully 3D sound
        audioSource.rolloffMode = AudioRolloffMode.Linear;  // Linear rolloff is more intuitive in most cases
        audioSource.minDistance = 1;  // Close range
        audioSource.maxDistance = 50;  // Max range where the sound is audible

        audioSource.PlayOneShot(clip, volume);
        Destroy(tempAudioSource, clip.length);
    }

    // Plays a sound and destroys the temporary AudioSource object after playing
    void PlaySoundAndDestroy(AudioClip clip, float volume) {
        if (clip != null) {
            GameObject tempAudioSource = new GameObject("TempAudio");
            AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();
            audioSource.PlayOneShot(clip, volume);
            Destroy(tempAudioSource, clip.length);
        }
    }
}
