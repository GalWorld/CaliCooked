using System.Collections;
using UnityEngine;

public class BlendOutputStation : OutputStation
{
    [SerializeField] private Transform ingredientPosition;
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] private float blendLerpTime = 0.15f;

    private string speedProperty = "_Speed";
    private string playProperty = "_Play";
    private string colorProperty = "_Color";
    private AudioSource audioSource;
    private Material liquidMaterial;
    private float targetSpeed;
    private float targetVolume;
    private Coroutine blendRoutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            targetVolume = audioSource.volume;

        if (liquidRenderer != null)
        {
            liquidMaterial = liquidRenderer.material;
            if (liquidMaterial.HasProperty(speedProperty))
                targetSpeed = liquidMaterial.GetFloat(speedProperty);
        }
    }

    public override void Generated(GameObject ingredientProccesed)
    {
        ingredientProccesed.transform.position = ingredientPosition.position;

        if (ingredientProccesed.TryGetComponent(out IngredientState ingredient))
            ingredient.ChangeMesh(StateEnum.blend);

        if (ingredientProccesed.TryGetComponent(out IngredientController controller))
            if (liquidMaterial.HasProperty(colorProperty))
                liquidMaterial.SetColor(colorProperty, controller.GetColorIngredient());

        StartBlend(true);
    }

    public override void Degenerated()
    {
        StartBlend(false);
    }

    private void StartBlend(bool play)
    {
        if (liquidMaterial == null || audioSource == null)
            return;

        if (blendRoutine != null)
            StopCoroutine(blendRoutine);

        blendRoutine = StartCoroutine(AnimateBlend(play));
    }

    private IEnumerator AnimateBlend(bool play)
    {
        float startSpeed;
        float endSpeed;
        float startVolume = audioSource.volume;
        float endVolume;

        if (play)
        {
            startSpeed = 0f;
            endSpeed = targetSpeed;
            endVolume = targetVolume;

            if (liquidMaterial.HasProperty(playProperty))
                liquidMaterial.SetFloat(playProperty, 1f);

            audioSource.volume = 0f;
            audioSource.Play();
        }
        else
        {
            startSpeed = liquidMaterial.GetFloat(speedProperty);
            endSpeed = 0f;
            endVolume = 0f;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / blendLerpTime;
            float k = Mathf.Clamp01(t);

            float s = Mathf.Lerp(startSpeed, endSpeed, k);
            liquidMaterial.SetFloat(speedProperty, s);

            float v = Mathf.Lerp(startVolume, endVolume, k);
            audioSource.volume = v;

            yield return null;
        }

        liquidMaterial.SetFloat(speedProperty, endSpeed);

        if (!play)
        {
            if (liquidMaterial.HasProperty(playProperty))
                liquidMaterial.SetFloat(playProperty, 0f);

            audioSource.volume = 0f;
            audioSource.Stop();
        }

        blendRoutine = null;
    }
}
