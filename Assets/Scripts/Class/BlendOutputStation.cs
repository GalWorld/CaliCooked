using System.Collections;
using UnityEngine;

public class BlendOutputStation : OutputStation
{
    [Header("Shader Settings")]
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] private float blendLerpTime = 0.15f;

    private string speedProperty = "_Speed";
    private string playProperty = "_Play";
    private string colorProperty = "_Color";
    private Material liquidMaterial;
    private float targetSpeed;
    private Coroutine blendRoutine;
    private GameObject currentObject;

    void Awake()
    {

        if (liquidRenderer != null)
        {
            liquidMaterial = liquidRenderer.material;
            if (liquidMaterial.HasProperty(speedProperty))
                targetSpeed = liquidMaterial.GetFloat(speedProperty);
        }
    }

    public override void Generated(GameObject ingredientProccesed)
    {
        base.Generated(ingredientProccesed);
        currentObject = ingredientProccesed;

        if (ingredientProccesed.TryGetComponent(out IngredientController controller))
            if (liquidMaterial.HasProperty(colorProperty))
                liquidMaterial.SetColor(colorProperty, controller.GetColorIngredient());

        StartBlend(true);
    }

    public override void Degenerated(GameObject ingredientProccesed)
    {
        base.Degenerated(ingredientProccesed);
        currentObject = ingredientProccesed;
        StartBlend(false);
    }

    private void StartBlend(bool play)
    {
        if (liquidMaterial == null)
            return;

        if (blendRoutine != null)
            StopCoroutine(blendRoutine);

        blendRoutine = StartCoroutine(AnimateBlend(play));
    }

    private IEnumerator AnimateBlend(bool play)
    {
        float startSpeed;
        float endSpeed;

        if (play)
        {
            startSpeed = 0f;
            endSpeed = targetSpeed;

            if (liquidMaterial.HasProperty(playProperty))
                liquidMaterial.SetFloat(playProperty, 1f);
        }
        else
        {
            startSpeed = liquidMaterial.GetFloat(speedProperty);
            endSpeed = 0f;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / blendLerpTime;
            float k = Mathf.Clamp01(t);

            float s = Mathf.Lerp(startSpeed, endSpeed, k);
            liquidMaterial.SetFloat(speedProperty, s);

            yield return null;
        }

        liquidMaterial.SetFloat(speedProperty, endSpeed);

        if (!play)
        {
            if (liquidMaterial.HasProperty(playProperty))
                liquidMaterial.SetFloat(playProperty, 0f);

            if(currentObject != null)
            {
                currentObject.SetActive(true);
                if (currentObject.TryGetComponent(out IngredientState ingredient))
                    ingredient.ChangeMesh(StateEnum.blend);
            }
        }

        blendRoutine = null;
    }
}
