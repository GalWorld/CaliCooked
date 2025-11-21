using UnityEngine;


public abstract class OutputStation : MonoBehaviour
{
  public abstract void Generated(GameObject ingredientProccesed);
  public abstract void Degenerated();

}
