using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;

    [Header("Button Sounds")]
    [SerializeField] private AudioClip buttonClickSFX;

    private void Awake()
    {
        // Singleton básico
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Reproduce el sonido principal de los botones.
    /// Llamarlo desde el evento OnClick del botón.
    /// </summary>
    public void PlayButtonSFX()
    {
        if (buttonClickSFX != null)
            audioSource.PlayOneShot(buttonClickSFX);
    }

    /// <summary>
    /// Reproducir cualquier sonido SFX que quieras.
    /// </summary>
    public void PlayCustomSFX(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
