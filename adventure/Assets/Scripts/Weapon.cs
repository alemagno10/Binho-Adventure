using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject bulletPrefab;
    public AudioClip shootSound; // Reference to the shooting sound clip
    public float shootingVolume = 0.5f; // Adjustable volume for the shooting sound

    private AudioSource audioSource; // This will reference the AudioSource component

    void Start() {
        // Get the AudioSource component on the same GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();
        // If there isn't one, add it dynamically
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, fixedRotation);

        // Determine the speed based on the mouse position
        if (Input.mousePosition.x > 175) {
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }

        // Play the shooting sound at the specified volume
        if (audioSource != null && shootSound != null) {
            audioSource.PlayOneShot(shootSound, shootingVolume);
        }
    }
}
