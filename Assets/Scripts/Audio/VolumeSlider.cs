using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;      
    [SerializeField] string parameterName;      

    public void SetVolume(float value)
    {
        mixer.SetFloat(parameterName, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }
}
