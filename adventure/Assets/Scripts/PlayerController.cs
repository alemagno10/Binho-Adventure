using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity {

    public Rigidbody2D body;
    public float speed;
    public float jumpForce;
    public Vector2 jump;
    public bool isGrounded = true;
    public AudioClip damageSound; // AudioClip for damage sound
    public float damageSoundVolume = 0.5f; // Volume regulator for the damage sound

    private AudioSource audioSource; // AudioSource component to play the sound

    void Start() {
        speed = 7;
        jumpForce = 7;
        jump = new Vector2(0.0f, 2.0f);

        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            // Add an AudioSource component if not already present
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isGrounded) {
            body.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;  // Marks as on the ground
        }
    
        if (collision.gameObject.CompareTag("Die")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;  // Marks as not on the ground
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);

        // Play the damage sound at the specified volume
        if (audioSource != null && damageSound != null) {
            audioSource.PlayOneShot(damageSound, damageSoundVolume);
        }
    }

    public override void handleDeath() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene on death
    }
}
