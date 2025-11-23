using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ingridient") || other.CompareTag("Dish"))
        {
            Destroy(other.gameObject);
        }
    }
}
