using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Transform MainCamera; // Se busca dinámicamente
    public Transform enemyFace;     // Referencia a la cara del enemigo
    public Animator enemyAnimator;  // Animator del enemigo
    public float lookSpeed = 2f;    // Velocidad de la transición de la cámara

    private bool isLooking = false;

    private void Start()
    {
        // Buscar la cámara en la escena persistente
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        if (cameraObject != null)
        {
            MainCamera = cameraObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró la cámara en la escena persistente.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en el rango
        {
            enemyAnimator.SetTrigger("Attack"); // Activa la animación del enemigo
            isLooking = true;
        }
    }

    private void Update()
    {
        if (isLooking && MainCamera != null)
        {
            // Suaviza la rotación de la cámara hacia la cara del enemigo
            Vector3 direction = enemyFace.position - MainCamera.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            MainCamera.rotation = Quaternion.Slerp(MainCamera.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }
}
