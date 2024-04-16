using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject bulletPrefab;
    public AudioClip shootSound; // Add this line to reference your shooting sound

    private AudioSource audioSource; // This will reference the AudioSource component

    void Start() {
        // Get the AudioSource component on the same GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();
        // If there isn't one, add it dynamically (optional)
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

        if (Input.mousePosition.x > 175) {
            bullet.GetComponent<Bullet>().SetSpeed(18f);
        } else {
            bullet.GetComponent<Bullet>().SetSpeed(-18f);
        }

        // Play the shooting sound
        if (audioSource != null && shootSound != null) {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
