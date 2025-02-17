using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public GameObject objetoAparecer;  // El objeto que aparecerá al recoger la llave
    public GameObject panelNotas;      // Panel UI donde se mostrarán las notas
    public Image imagenNota;           // Imagen que cambiará para mostrar las notas

    public Sprite[] notas;             // Array de imágenes de las notas
    private int notaActual = 0;        // Controlador para cambiar de nota
    private bool mostrandoNota = false;

    private void Start()
    {
        // Al inicio, desactiva el panel de notas
        panelNotas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa el objeto que aparece al recoger la llave
            if (objetoAparecer != null) objetoAparecer.SetActive(true);
            
            // Muestra la primera nota
            panelNotas.SetActive(true);
            imagenNota.sprite = notas[0];
            mostrandoNota = true;
            
            // Destruye la llave
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        // Si se está mostrando la nota y el jugador presiona espacio
        if (mostrandoNota && Input.GetKeyDown(KeyCode.Space))
        {
            notaActual++;  // Avanza a la siguiente nota

            if (notaActual < notas.Length)
            {
                // Cambia la imagen de la nota
                imagenNota.sprite = notas[notaActual];
            }
            else
            {
                // Cuando no hay más notas, oculta el panel
                panelNotas.SetActive(false);
                mostrandoNota = false;
            }
        }
    }
}
