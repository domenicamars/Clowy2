using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // M�todo para reiniciar el juego
    public void Restart()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
