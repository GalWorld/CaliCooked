using UnityEngine;
using System.Collections;
public class CutOutputStation : OutputStation
{
    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private float waitForAnim = 1f;
    [SerializeField] private Transform cut;

    private GameObject currentObject;

    protected override void Start()
    {
        base.Start();
        if(anim == null)
            TryGetComponent(out anim);
    }

    public override void Generated(GameObject ingredientProccesed)
    {
        base.Generated(ingredientProccesed);
        currentObject = ingredientProccesed;
        currentObject.SetActive(true);
        currentObject.layer = 2;
        currentObject.transform.position = cut.position;

        if (anim != null)
        {
            anim.SetTrigger("Play");
        }
    }

    public override void Degenerated(GameObject ingredientProccesed)
    {
        base.Degenerated(currentObject);
        currentObject = ingredientProccesed;

        if (anim != null)
        {
            anim.SetTrigger("Stop");
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitForAnim);
        if (currentObject != null)
            currentObject.transform.position = ingredientPosition.position;
            currentObject.layer = 0;
            currentObject.SetActive(true);
            if (currentObject.TryGetComponent(out IngredientState ingredient))
                ingredient.ChangeMesh(StateEnum.cut);

    }
}


