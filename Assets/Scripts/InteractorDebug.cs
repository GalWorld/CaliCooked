using System;
using UnityEngine;

public class InteractorDebug : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Esta tocando el objeto");
    }
}
