using UnityEngine;

public class EnemyShooter : Entity {
    public GameObject bulletPrefab;
    public Rigidbody2D player;
    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public float shootingVolume = 0.5f;
    public float damageVolume = 0.5f;
    public float deathVolume = 0.5f;

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

        PlaySoundAndDestroy(shootSound, shootingVolume);
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
