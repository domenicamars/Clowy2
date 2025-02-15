using UnityEngine;

public class ClowyCameraController : MonoBehaviour
{
    public Transform clowy; // Referencia a Clowy
    public Camera clowyCamera; // Referencia a la cámara de Clowy
    public Animator clowyAnimator; // Referencia al Animator de Clowy
    private GameObject playerCapsule; // Referencia a la cápsula del jugador
    private CharacterController playerController; // Referencia al CharacterController del jugador
    private Camera mainCamera; // Referencia a la cámara principal

    void Start()
    {
        // Desactivar la cámara de Clowy al inicio
        clowyCamera.enabled = false;

        // Buscar la cápsula del jugador por su nombre
        playerCapsule = GameObject.Find("PlayerCapsule");

        // Obtener la referencia al CharacterController del jugador
        if (playerCapsule != null)
        {
            playerController = playerCapsule.GetComponent<CharacterController>();
        }

        // Buscar la cámara principal por su tag
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Hacer que la cámara de Clowy apunte siempre a él
        if (clowy != null)
        {
            clowyCamera.transform.position = clowy.position + new Vector3(0, 1.5f, -3f); // Ajustar la posición de la cámara
            clowyCamera.transform.LookAt(clowy); // Hacer que la cámara mire a Clowy
        }

        // Activar la cámara de Clowy y desactivar la cámara principal y el movimiento del jugador cuando se inicie la animación "Scream"
        if (clowyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Scream"))
        {
            clowyCamera.enabled = true;
            if (mainCamera != null)
            {
                mainCamera.enabled = false; // Desactivar la cámara principal
            }
            if (playerController != null)
            {
                playerController.enabled = false; // Desactivar el movimiento del jugador
            }
            if (playerCapsule != null)
            {
                playerCapsule.SetActive(false); // Ocultar la cápsula del jugador
            }
        }
        else
        {
            clowyCamera.enabled = false;
            if (mainCamera != null)
            {
                mainCamera.enabled = true; // Activar la cámara principal
            }
            if (playerController != null)
            {
                playerController.enabled = true; // Activar el movimiento del jugador
            }
            if (playerCapsule != null)
            {
                playerCapsule.SetActive(true); // Mostrar la cápsula del jugador
            }
        }
    }
}