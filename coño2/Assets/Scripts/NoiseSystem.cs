using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para trabajar con la barra de UI

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
    public float micSensitivity = 5f; // Sensibilidad del micrófono
    public float micThreshold = 0.1f; // Umbral de ruido

    // Referencia a la barra de ruido
    public Image noiseBarImage;  // La barra de ruido que vamos a actualizar

    // Reducción del ruido con el tiempo
    public float decreaseAmount = 5f; // Qué tanto disminuirá el ruido
    public float decreaseInterval = 2f; // Intervalo de tiempo para disminuir

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
            noiseBarImage.fillAmount = noiseLevel / maxNoise; // Llena la barra de ruido
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión fue con un objeto de tipo "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddNoise();
        }
    }

    private void AddNoise()
    {
        noiseLevel += noiseIncrease;
        noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

        // Si el nivel de ruido alcanza el máximo, activar al enemigo
        if (noiseLevel >= maxNoise)
        {
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

    // Corutina para disminuir el ruido con el tiempo
    private IEnumerator DecreaseNoiseOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseInterval);

            // Reducir el ruido gradualmente
            noiseLevel -= decreaseAmount;
            noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

            if (noiseBarImage != null)
            {
                noiseBarImage.fillAmount = noiseLevel / maxNoise;
            }
        }
    }
}
