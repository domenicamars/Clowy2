using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player; // El jugador al que el enemigo perseguirá
    private NavMeshAgent agent; // Para mover al enemigo
    private Animator animator; // Para animaciones
    private bool isChasing = false; // Estado de persecución

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;


        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Desactivar la persecución al inicio
        isChasing = false;
    }

    void Update()
    {
        // Si el enemigo está persiguiendo
        if (isChasing && player != null)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isChasing", true); // Cambia la animación a perseguidor
        }
    }

    // Método que se llama cuando el ruido alcanza el máximo
    public void StartChasing()
    {
        isChasing = true;
        Debug.Log("¡El enemigo te está persiguiendo!");
    }

    // Método para asignar el jugador si no está asignado
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
