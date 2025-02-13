using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivoDeGuardado = Path.Combine(Application.persistentDataPath, "datosJuego.json");

        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");
        }

        if (jugador == null)
        {
            Debug.LogError("❌ No se encontró el jugador. Asegúrate de que tiene la etiqueta 'Player'.");
            return;
        }

        if (!File.Exists(archivoDeGuardado))
        {
            Debug.Log("📂 No se encontró un archivo de guardado, creando uno nuevo...");
            GuardarDatos();
        }
        else
        {
            CargarDatos();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("📥 Cargando datos...");
            Debug.Log("📌 Posición guardada: " + datosJuego.posicion);
            Debug.Log("🔊 Ruido guardado: " + datosJuego.ruido);

            // Desactivar CharacterController antes de mover al jugador
            CharacterController cc = jugador.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            jugador.transform.position = datosJuego.posicion;

            if (cc != null) cc.enabled = true; // Reactivar CharacterController

            // Cargar el ruido en el NoiseSystem
            NoiseSystem noiseSystem = jugador.GetComponent<NoiseSystem>();
            if (noiseSystem != null)
            {
                noiseSystem.noiseLevel = datosJuego.ruido;
                Debug.Log("🔊 Ruido cargado en NoiseSystem: " + noiseSystem.noiseLevel);
            }
        }
        else
        {
            Debug.LogWarning("⚠️ El archivo de guardado no existe.");
        }
    }

    private void GuardarDatos()
    {
        NoiseSystem noiseSystem = jugador.GetComponent<NoiseSystem>();

        if (noiseSystem == null)
        {
            Debug.LogError("❌ No se encontró el NoiseSystem en el jugador.");
            return;
        }

        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position,
            ruido = noiseSystem.noiseLevel
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos, true);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);
        Debug.Log("✅ Archivo guardado: " + cadenaJSON);
    }
}
