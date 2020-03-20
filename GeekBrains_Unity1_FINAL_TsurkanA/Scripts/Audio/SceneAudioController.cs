using UnityEngine;


public sealed class SceneAudioController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] _HealthPickUpAudioClips;

    private AudioSource _HealthPickUpAudioSource;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _HealthPickUpAudioSource = gameObject.AddComponent<AudioSource>();

        AudioSource[] audioSources = { _HealthPickUpAudioSource };
        foreach (AudioSource source in audioSources)
        {
            source.enabled = true;
            source.volume = 1.0f;
            source.playOnAwake = false;
            source.loop = false;
        }
    }

    #endregion


    #region Methods

    private void PlayRandomSound(AudioClip[] audioClips, AudioSource audioSource)
    {
        int randomSoundIdx = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomSoundIdx];
        audioSource.Play();
    }

    public void PlayHealthPickUpSound()
    {
        PlayRandomSound(_HealthPickUpAudioClips, _HealthPickUpAudioSource);
    }

    #endregion
}
