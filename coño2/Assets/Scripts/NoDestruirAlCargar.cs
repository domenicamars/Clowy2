using UnityEngine;

public class NoDestruirAlCargar : MonoBehaviour
{
    void Start()
    {
        // Asegúrate de que este script esté adjunto al objeto de inventario que quieres conservar.
        DontDestroyOnLoad(this.gameObject);
    }
}