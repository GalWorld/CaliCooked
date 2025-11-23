using UnityEngine;

public class DisableVFXOnWebGL : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false); // o desactiva solo el componente
        }
    }
}
