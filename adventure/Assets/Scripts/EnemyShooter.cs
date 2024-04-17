using UnityEngine;

public class EnemyShooter : Entity {
    public GameObject bulletPrefab;
    public Rigidbody2D player;
    public AudioClip shootSound; // Reference to the shooting sound clip
    public float shootingVolume = 0.5f; // Public variable to control the shooting volume

    private AudioSource audioSource; // This will reference the AudioSource component
    private System.Random rand = new System.Random();

    void Start() {
        SetHP(30f);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Use a random delay before the first shot and then shoot every 3 seconds
        InvokeRepeating("Shoot", rand.Next(1, 4), 3.0f);
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
    }

    public override void handleDeath() {
        Destroy(gameObject);
    }

    void Shoot() {
        Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, fixedRotation);
        bullet.GetComponent<Bullet>().SetSelf("Enemy");
        bullet.GetComponent<Bullet>().SetTarget("Player");

        // Determine the direction to shoot the bullet based on the player's position
        if (transform.position.x < player.position.x) {
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }

        // Play the shooting sound at the specified volume
        if (audioSource != null && shootSound != null) {
            audioSource.PlayOneShot(shootSound, shootingVolume);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject gameObject = collision.gameObject;

        if (gameObject.CompareTag("Player")) {
            float verticalOffset = gameObject.GetComponent<Entity>().transform.position.y - transform.position.y;
            if (verticalOffset < 0.5) {
                gameObject.GetComponent<Entity>().TakeDamage(5);
            } else {
                TakeDamage((int)GetHP());
            }
        }
    }
}
