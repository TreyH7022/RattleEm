using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    public AudioClip music;
    private AudioSource audioSource;

    void Start()
    {
        Music.instance.PlayMusic(music);
    }

    void Awake()
    {
        // Prevents duplicates when scene restarts
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}