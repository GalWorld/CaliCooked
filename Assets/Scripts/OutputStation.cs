using System.Collections;
using UnityEngine;

public class OutputStation : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] protected Transform ingredientPosition;
    [SerializeField] private float volumeLerpTime = 0.15f;

    public AudioSource aud;
    private float targetVolume;
    private Coroutine audRoutine;

    protected virtual void Start()
    {
        if (aud != null)
            targetVolume = aud.volume;
    }

    public virtual void Generated(GameObject ingredientProccesed) {
        ingredientProccesed.transform.position = ingredientPosition.position;
        StartSound(true);
    }

    public virtual void Degenerated(GameObject ingredientProccesed)
    {
        StartSound(false);
    }

    private void StartSound(bool play)
    {
        if (aud == null)
            return;

        if (audRoutine != null)
            StopCoroutine(audRoutine);

        audRoutine = StartCoroutine(AnimateBlend(play));
    }


    private IEnumerator AnimateBlend(bool play)
    {
        float startVolume = aud.volume;
        float endVolume;

        if (play)
        {
            endVolume = targetVolume;
            aud.volume = 0f;
            aud.Play();
        }
        else
        {
            endVolume = 0f;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / volumeLerpTime;
            float k = Mathf.Clamp01(t);

            float v = Mathf.Lerp(startVolume, endVolume, k);
            aud.volume = v;

            yield return null;
        }


        if (!play)
        {
            aud.volume = 0f;
            aud.Stop();
        }

        audRoutine = null;
    }
}
