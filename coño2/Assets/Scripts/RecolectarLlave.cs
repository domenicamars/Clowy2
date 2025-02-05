using UnityEngine;

public class RecolectarLlave : MonoBehaviour
{
    private Inventario inventarioScript; // Referencia al script del inventario
    private bool puedeRecoger = false;  // Indica si el jugador está cerca del objeto

    private void Start()
    {
        // Buscar el inventario si no está asignado
        if (inventarioScript == null)
        {
            GameObject inventarioObj = GameObject.FindWithTag("Inventario");
            if (inventarioObj != null)
            {
                inventarioScript = inventarioObj.GetComponent<Inventario>();
            }
        }
    }

    private void Update()
    {
        // Si el jugador está cerca y presiona E, recoger el objeto
        if (puedeRecoger && Input.GetKeyDown(KeyCode.E) && inventarioScript != null)
        {
            Recoger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador entra en el área del objeto
        if (other.CompareTag("Player"))
        {
            puedeRecoger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el jugador sale del área del objeto
        if (other.CompareTag("Player"))
        {
            puedeRecoger = false;
        }
    }

    private void Recoger()
    {
        // Agregar el objeto al inventario
        inventarioScript.AddItem(gameObject);

        // Desactivar el objeto del mundo
        gameObject.SetActive(false);

        // Reiniciar el estado
        puedeRecoger = false;
    }
}
