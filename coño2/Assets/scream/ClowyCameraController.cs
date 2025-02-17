using UnityEngine;
using UnityEngine.SceneManagement; // Importar SceneManager para cambiar escenas
using System.Collections;

public class ClowyCameraController : MonoBehaviour
{
    // ======== VARIABLES P�BLICAS ========
    public Transform clowy;                // Referencia al transform de Clowy
    public Camera clowyCamera;            // C�mara de Clowy
    public Animator clowyAnimator;        // Animator de Clowy
    public AudioSource screamAudio;       // AudioSource para el sonido del grito

    // ======== VARIABLES PRIVADAS ========
    private GameObject playerCapsule;      // C�psula del jugador
    private CharacterController playerController; // CharacterController del jugador
    private Camera mainCamera;             // C�mara principal
    private bool hasPlayedScream = false;  // Control para evitar repetir el sonido
    private bool isShaking = false;        // Control para el shake

    // ======== CONFIGURACIONES PARA EL SHAKE ========
    public float shakeDuration = 0.5f;     // Duraci�n del shake
    public float shakeMagnitude = 0.2f;    // Intensidad del shake

    // ======== M�TODO START ========
    void Start()
    {
        // Desactivar la c�mara de Clowy al inicio
        clowyCamera.enabled = false;

        // Buscar la c�psula del jugador
        playerCapsule = GameObject.Find("PlayerCapsule");
        if (playerCapsule != null)
        {
            playerController = playerCapsule.GetComponent<CharacterController>();
        }

        // Buscar la c�mara principal por su tag
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
    }

    // ======== M�TODO UPDATE ========
    void Update()
    {
        // Hacer que la c�mara de Clowy siga su posici�n
        if (clowy != null)
        {
            clowyCamera.transform.position = clowy.position + new Vector3(0, 1.5f, -3f);
            clowyCamera.transform.LookAt(clowy);
        }

        // Obtener el estado de la animaci�n actual
        var stateInfo = clowyAnimator.GetCurrentAnimatorStateInfo(0);

        // ======== Si est� en la animaci�n "Scream" ========
        if (stateInfo.IsName("Scream"))
        {
            // 1. Reproducir sonido una sola vez
            if (!hasPlayedScream && screamAudio != null)
            {
                screamAudio.Play();
                hasPlayedScream = true;
            }

            // 2. Hacer el shake si no est� en curso
            if (!isShaking)
            {
                StartCoroutine(CameraShake());
            }

            // 3. Cambiar de c�mara y desactivar jugador
            clowyCamera.enabled = true;
            if (mainCamera != null) mainCamera.enabled = false;
            if (playerController != null) playerController.enabled = false;
            if (playerCapsule != null) playerCapsule.SetActive(false);
        }

        // ======== Si TERMIN� la animaci�n "Scream" ========
        if (stateInfo.IsName("Scream") && stateInfo.normalizedTime >= 1f)
        {
            StartCoroutine(ChangeSceneAfterDelay(1f)); // Espera 1 segundo y cambia a GAME OVER
        }
    }

    // ======== M�TODO PARA EL SHAKE ========
    IEnumerator CameraShake()
    {
        isShaking = true;
        Vector3 originalPosition = clowyCamera.transform.localPosition;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            clowyCamera.transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        clowyCamera.transform.localPosition = originalPosition;
        isShaking = false;
    }

    // ======== M�TODO PARA CAMBIAR DE ESCENA ========
    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GAME OVER");
    }
}
