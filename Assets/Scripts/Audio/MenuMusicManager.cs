using UnityEngine;

public class MenuMusicSingleAudioSource : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introClip;  
    public AudioClip loopClip;  

    void Start()
    {
        audioSource.loop = false;        
        audioSource.clip = introClip;
        audioSource.Play();

        // Cuando termine la intro, cambiamos al loop
        Invoke(nameof(PlayLoopMusic), introClip.length-1);
    }

    void PlayLoopMusic()
    {
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
