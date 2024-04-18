using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    void Awake()
    {
        // Check if instance already exists and if it's not this one
        if (instance != null && instance != this)
        {
            // Destroy this instance because it's a duplicate
            Destroy(gameObject);
        }
        else
        {
            // Set the instance to this
            instance = this;
            // Don't destroy this object when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Get the AudioSource component and play the music
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
