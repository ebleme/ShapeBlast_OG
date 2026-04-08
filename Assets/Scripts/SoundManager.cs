using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Muzikle ilgili işlemler için müziğin AuidioSource'unu al
    // SFX AudioSource referans olarak al
    
    // Item ların yere ve kendilerine dokunduğu anda çıkacak olan ses
    // Item ların yok olacağı zaman çıkacak olan ses


    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;


    [SerializeField] private AudioClip touchSFX;
    [SerializeField] private AudioClip successSFX;
    
    
    public void PlayTouchSound()
    {
        soundSource.PlayOneShot(touchSFX);
    }

    public void PlaySuccessSound()
    {
        soundSource.PlayOneShot(successSFX);
    }
}
