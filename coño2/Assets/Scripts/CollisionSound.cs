using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource automáticamente
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
