using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  // Needed for IEnumerator

public class PlayerController : Entity {
    public Rigidbody2D body;
    public float speed;
    public float jumpForce;
    public Vector2 jump;
    public bool isGrounded = true;
    public AudioClip damageSound; // AudioClip for damage sound
    public AudioClip jumpSound; // AudioClip for jump sound
    public AudioClip deathSound; // AudioClip for death sound
    public float damageSoundVolume = 0.5f; // Volume regulator for the damage sound
    public float jumpSoundVolume = 0.5f; // Volume regulator for the jump sound
    public float deathSoundVolume = 0.5f; // Volume regulator for the death sound

    private AudioSource audioSource; // AudioSource component to play the sound
    private SceneChanger sceneChanger; 

    void Start() {
        speed = 7;
        jumpForce = 7;
        jump = new Vector2(0.0f, 2.0f);
        sceneChanger = FindObjectOfType<SceneChanger>();

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
            // Play the jump sound at the specified volume
            if (audioSource != null && jumpSound != null) {
                audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;  // Marks as on the ground
        } else if (collision.gameObject.CompareTag("Die")) {
            handleDeath(); // Handle death when colliding with deadly objects
        }   
    } 

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Portal")) {
            sceneChanger.Next("Level2-Onca"); 
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
        // Play the death sound at the specified volume
        if (audioSource != null && deathSound != null) {
            // Pause the game by setting time scale to 0
            Time.timeScale = 0;
            audioSource.PlayOneShot(deathSound, deathSoundVolume);
            StartCoroutine(ReloadSceneAfterDeathSound(deathSound.length));
        } else {
            // If no sound is assigned, reload immediately
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator ReloadSceneAfterDeathSound(float delay) {
        // Wait for the death sound to play while everything is paused
        yield return new WaitForSecondsRealtime(delay);  // Use WaitForSecondsRealtime since timeScale is 0
        Time.timeScale = 1;  // Resume the game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
