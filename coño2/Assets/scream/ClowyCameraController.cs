using UnityEngine;

public class ClowyCameraController : MonoBehaviour
{
    public Transform clowy; // Referencia a Clowy
    public Camera clowyCamera; // Referencia a la c�mara de Clowy
    public Animator clowyAnimator; // Referencia al Animator de Clowy
    private GameObject playerCapsule; // Referencia a la c�psula del jugador
    private CharacterController playerController; // Referencia al CharacterController del jugador
    private Camera mainCamera; // Referencia a la c�mara principal

    void Start()
    {
        // Desactivar la c�mara de Clowy al inicio
        clowyCamera.enabled = false;

        // Buscar la c�psula del jugador por su nombre
        playerCapsule = GameObject.Find("PlayerCapsule");

        // Obtener la referencia al CharacterController del jugador
        if (playerCapsule != null)
        {
            playerController = playerCapsule.GetComponent<CharacterController>();
        }

        // Buscar la c�mara principal por su tag
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Hacer que la c�mara de Clowy apunte siempre a �l
        if (clowy != null)
        {
            clowyCamera.transform.position = clowy.position + new Vector3(0, 1.5f, -3f); // Ajustar la posici�n de la c�mara
            clowyCamera.transform.LookAt(clowy); // Hacer que la c�mara mire a Clowy
        }

        // Activar la c�mara de Clowy y desactivar la c�mara principal y el movimiento del jugador cuando se inicie la animaci�n "Scream"
        if (clowyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Scream"))
        {
            clowyCamera.enabled = true;
            if (mainCamera != null)
            {
                mainCamera.enabled = false; // Desactivar la c�mara principal
            }
            if (playerController != null)
            {
                playerController.enabled = false; // Desactivar el movimiento del jugador
            }
            if (playerCapsule != null)
            {
                playerCapsule.SetActive(false); // Ocultar la c�psula del jugador
            }
        }
        else
        {
            clowyCamera.enabled = false;
            if (mainCamera != null)
            {
                mainCamera.enabled = true; // Activar la c�mara principal
            }
            if (playerController != null)
            {
                playerController.enabled = true; // Activar el movimiento del jugador
            }
            if (playerCapsule != null)
            {
                playerCapsule.SetActive(true); // Mostrar la c�psula del jugador
            }
        }
    }
}