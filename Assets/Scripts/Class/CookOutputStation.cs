using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class CookOutputStation : OutputStation
{
    [Header("Animation")]
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float waitForAnim = 0.5f;
    [SerializeField] private Transform cook;
    [SerializeField] private float scaleFactor = 0.7f;

    private GameObject currentObject;
    private Vector3 oldScale;

    public override void Generated(GameObject ingredientProccesed)
    {
        base.Generated(ingredientProccesed);
        currentObject = ingredientProccesed;
        currentObject.SetActive(true);
        currentObject.layer = 2;
        oldScale = currentObject.transform.localScale;
        currentObject.transform.SetParent(cook);
        currentObject.transform.localScale *= scaleFactor;
        currentObject.transform.position = cook.position;

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
        {
            currentObject.transform.position = ingredientPosition.position;
            currentObject.transform.SetParent(null);
            currentObject.transform.localScale = oldScale;
            currentObject.layer = 0;
            currentObject.SetActive(true);
            if (currentObject.TryGetComponent(out IngredientState ingredient))
                ingredient.ChangeMesh(StateEnum.cook);
        }
            
    }
}


