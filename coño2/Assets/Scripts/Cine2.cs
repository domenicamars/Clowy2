using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cine2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndVideo;
            Debug.Log("Evento loopPointReached asignado correctamente.");
        }
        else
        {
            Debug.LogError("No se encontró el VideoPlayer en este objeto.");
        }
    }

    void Update()
    {
        if (!videoPlayer.isPlaying) // Si el video termina, cambia de escena
        {
            Debug.Log("El video terminó (comprobado con isPlaying). Cambiando de escena...");
            SceneManager.LoadScene("pantalla inicial");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Tecla SPACE presionada, cambiando de escena...");
            SceneManager.LoadScene("pantalla inicial");
        }
    }

    void EndVideo(VideoPlayer vp)
    {
        Debug.Log("El video terminó, intentando cambiar de escena...");
        SceneManager.LoadScene("pantalla inicial");
    }
}
