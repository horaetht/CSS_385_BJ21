using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource sfxSource;

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public AudioSource longMusicSource;

    public void PlayLongMusic()
    {
        if (longMusicSource != null)
        {
            longMusicSource.Play();
        }
    }

}
