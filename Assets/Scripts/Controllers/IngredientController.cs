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
        if (ingredient.States.Count == 0) return;

        currentStates.Add(new StationDictionary(ingredient.States[0], false));
    }
    public bool CheckIngredientIsPlatable()
    {
        if (currentStates.FindIndex(state => state.id == StateEnum.plate) == -1) return false;
        return currentStates.TrueForAll(states => states.id != StateEnum.plate ? states.value : true);
    }

    private void CheckIngredientIsComplete()
    {
        if(currentStates.Count < ingredient.States.Count)
        {
            currentStates.Add(new StationDictionary(ingredient.States[currentStates.Count], false));
            isComplete = false;
            return;
        }
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
