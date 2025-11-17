using UnityEngine;


public enum StateEnum
{
    cut, blend, cook, plate
}

[System.Serializable]
public class StationDictionary
{
    public StateEnum id;
    public bool value;

    public StationDictionary(StateEnum id, bool value)
    {
        this.id = id;
        this.value = value;
    }
}
