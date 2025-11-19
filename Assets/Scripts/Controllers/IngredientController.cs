using UnityEngine;
using System.Collections.Generic;

public class IngredientController : MonoBehaviour
{
    public bool isComplete;
    [SerializeField] private SOIngredient ingredient;
    [SerializeField] private List<StationDictionary> currentStates;

    private void Start()
    {
        currentStates = new();
        foreach (var state in ingredient.States)
        {
            currentStates.Add(new StationDictionary(state, false));
        }
    }
    public bool CheckIngredientIsPlatable()
    {
        return currentStates.TrueForAll(states => states.id != StateEnum.plate ? states.value : true);
    }

    private void CheckIngredientIsComplete()
    {
        isComplete = currentStates.TrueForAll(states => states.value);
    }

    public string GetIngredientId()
    {
        return ingredient.id;
    }

    public bool? GetStateValue(StateEnum state)
    {
        if (currentStates == null) return null;

        if (currentStates.Exists(states => states.id == state))
            return currentStates.Find(states => states.id == state).value;
        else
            return null;
    }

    public void SetStateValue(StateEnum state, bool value)
    {
        if (currentStates == null) return;

        if (currentStates.Exists(states => states.id == state))
            currentStates.Find(states => states.id == state).value = value;

        CheckIngredientIsComplete();
    }

}
