using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip bgm;
    public AudioClip fallDamage;

    private void Start()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }


}
