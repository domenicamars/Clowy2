using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSystem : MonoBehaviour
{
    public float noiseLevel = 0f;
    public float maxNoise = 100f;
    public float noiseIncrease = 10f;
    public float runningNoiseIncreaseRate = 25f; // Aumento ligeramente más rápido
    public bool isRunning = false;
    public EnemyAI enemy;

    // Variables del micrófono
    private AudioClip micClip;
    private string micDevice;
    public float micSensitivity = 5f;
    public float micThreshold = 0.1f;

    // Referencia a la barra de ruido
    public Image noiseBarImage;

    // Reducción del ruido con el tiempo
    public float decreaseAmount = 5f;
    public float decreaseInterval = 2f;

    private void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            micClip = Microphone.Start(micDevice, true, 1, 44100);
        }

        StartCoroutine(DecreaseNoiseOverTime());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            AddNoise(runningNoiseIncreaseRate * Time.deltaTime); // Aumenta progresivamente
        }
        else
        {
            isRunning = false;
        }

        float micLoudness = GetMicrophoneLoudness();
        if (micLoudness > micThreshold)
        {
            AddNoise(noiseIncrease);
        }

        if (noiseBarImage != null)
        {
            noiseBarImage.fillAmount = noiseLevel / maxNoise;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddNoise(noiseIncrease);
        }
    }

    private void AddNoise(float amount)
    {
        noiseLevel += amount;
        noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

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

    private IEnumerator DecreaseNoiseOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseInterval);

            noiseLevel -= decreaseAmount;
            noiseLevel = Mathf.Clamp(noiseLevel, 0, maxNoise);

            if (noiseBarImage != null)
            {
                noiseBarImage.fillAmount = noiseLevel / maxNoise;
            }
        }
    }
}
