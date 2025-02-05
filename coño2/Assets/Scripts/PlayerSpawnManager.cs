using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform player; // Arrastra el jugador en el inspector

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            // ✅ Mover al jugador a la última posición guardada
            player.position = new Vector3(x, y, z);
        }
    }
}
