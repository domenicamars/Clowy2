using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using System.Collections; // Para usar corutinas

public class NoiseSystem : MonoBehaviour
{
    public float noiseLevel = 0f;  
    public float maxNoise = 100f;  
    public float noiseIncrease = 10f;  
    public bool isRunning = false;  
    public EnemyAI enemy;  

    // Variables del micrófono
    private AudioClip micClip;
    private string micDevice;
    public float micSensitivity = 5f; // Ajusta la sensibilidad del micrófono
    public float micThreshold = 0.1f; // Nivel mínimo de ruido detectable

    // Referencia a la imagen que será usada como barra de progreso
    public Image noiseBarImage;  // Arrastra aquí la imagen que será la barra

    // Parámetros para la reducción de ruido
    public float decreaseAmount = 5f; // Cantidad que disminuirá el ruido
    public float decreaseInterval = 2f; // Intervalo en segundos para disminuir el ruido

    private void Start()
    {
        // Iniciar el micrófono
        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0]; // Usa el primer micrófono disponible
            micClip = Microphone.Start(micDevice, true, 1, 44100);
        }

        // Iniciar la corutina para disminuir el ruido con el tiempo
        StartCoroutine(DecreaseNoiseOverTime());
    }

    private void Update()
    {
        // Detectar si el jugador está corriendo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            AddNoise();
        }
        else
        {
            isRunning = false;
        }

        // Detectar ruido del micrófono
        float micLoudness = GetMicrophoneLoudness();
        if (micLoudness > micThreshold)
        {
            AddNoise();
        }

        // Actualizar la imagen de la barra de ruido
        if (noiseBarImage != null)
        {
            // Actualizar la cantidad de llenado de la imagen según el ruido
            noiseBarImage.fillAmount = noiseLevel / maxNoise; // Esto llena la barra según el nivel de ruido
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto con el que colisiona tiene el tag "Obstacle", agrega ruido
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddNoise();
        }
    }

    private void AddNoise()
    {
        noiseLevel += noiseIncrease;
        noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

        if (noiseLevel >= maxNoise)
        {
            // Si el enemigo no está asignado, buscarlo por su tag
            if (enemy == null)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<EnemyAI>();
            }

            if (enemy != null)
            {
                enemy.StartChasing();
            }
            else
            {
                Debug.LogWarning("No se encontró el enemigo en la escena.");
            }
        }
    }

    private float GetMicrophoneLoudness()
    {
        if (micClip == null) return 0f;

        float[] data = new float[256];
        int micPosition = Microphone.GetPosition(micDevice) - (data.Length + 1);
        if (micPosition < 0) return 0f;

        micClip.GetData(data, micPosition);
        float sum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            sum += Mathf.Abs(data[i]);
        }
        return sum / data.Length * micSensitivity;
    }

    // Corutina para reducir el ruido con el tiempo
    private IEnumerator DecreaseNoiseOverTime()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo
            yield return new WaitForSeconds(decreaseInterval);

            // Reducir el nivel de ruido gradualmente
            noiseLevel -= decreaseAmount;
            noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

            // Actualizar la barra
            if (noiseBarImage != null)
            {
                noiseBarImage.fillAmount = noiseLevel / maxNoise;
            }
        }
    }
}
