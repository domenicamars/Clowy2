using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public string playerTag = "Player"; // Tag del jugador
    public string cameraName = "MainCamera"; // Nombre de la c�mara
    public float detectionRange = 5f; // Rango de detecci�n
    public Animator animator; // Referencia al Animator
    private Camera mainCamera; // Referencia a la c�mara principal

    void Start()
    {
        // Buscar la c�mara por su nombre
        GameObject cameraObject = GameObject.Find(cameraName);
        if (cameraObject != null)
        {
            mainCamera = cameraObject.GetComponent<Camera>();
        }
    }

    void Update()
    {
        // Buscar al jugador por su tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Si el jugador est� dentro del rango de detecci�n, activar la animaci�n
            if (distanceToPlayer <= detectionRange)
            {
                animator.SetTrigger("Scream");
                PointCameraAtClown();
            }
        }
    }

    void PointCameraAtClown()
    {
        // Mover la c�mara para que apunte a la cara del payaso
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z - 2f);
            mainCamera.transform.LookAt(transform);
        }
    }
}