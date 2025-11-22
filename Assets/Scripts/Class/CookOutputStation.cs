using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class CookOutputStation : OutputStation
{
    [Header("Animation")]
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float waitForAnim = 0.5f;

    private GameObject currentObject;

    public override void Generated(GameObject ingredientProccesed)
    {
        base.Generated(ingredientProccesed);
        currentObject = ingredientProccesed;

        if (vfx != null && particle != null)
        {
            vfx.SendEvent("OnPlay");
            particle.Play();
        }

    }

    public override void Degenerated(GameObject ingredientProccesed)
    {
        StartCoroutine(Wait());
        base.Degenerated(ingredientProccesed);
        currentObject = ingredientProccesed;

        if (vfx != null && particle != null)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            vfx.Stop();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitForAnim);
        if (currentObject != null)
            currentObject.SetActive(true);
            if(currentObject.TryGetComponent(out IngredientState ingredient))
                ingredient.ChangeMesh(StateEnum.cook);
    }
}


