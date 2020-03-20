using UnityEngine;


public sealed class PlayerAudioController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] _walkAudioClips;
    [SerializeField] private AudioClip[] _runAudioClips;
    [SerializeField] private AudioClip[] _shootAudioClips;
    [SerializeField] private AudioClip[] _hurtAudioClips;
    [SerializeField] private AudioClip[] _jumpAudioClips;

    private AudioSource _walkAudioSource;
    private AudioSource _runAudioSource;
    private AudioSource _shootAudioSource;
    private AudioSource _hurtAudioSource;
    private AudioSource _jumpAudioSource;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _walkAudioSource = gameObject.AddComponent<AudioSource>();
        _runAudioSource = gameObject.AddComponent<AudioSource>();
        _shootAudioSource = gameObject.AddComponent<AudioSource>();
        _jumpAudioSource = gameObject.AddComponent<AudioSource>();
        _hurtAudioSource = gameObject.AddComponent<AudioSource>();

        AudioSource[] audioSources = { _walkAudioSource, _runAudioSource, _shootAudioSource, _hurtAudioSource, _jumpAudioSource };
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

    public void PlayWalkSound()
    {
        PlayRandomSound(_walkAudioClips, _walkAudioSource);
    }

    public void PlayRunSound()
    {
        PlayRandomSound(_runAudioClips, _runAudioSource);
    }

    public void PlayShootSound()
    {
        PlayRandomSound(_shootAudioClips, _shootAudioSource);
    }

    public void PlayHurtSound()
    {
        PlayRandomSound(_hurtAudioClips, _hurtAudioSource);
    }

    public void PlayJumpSound()
    {
        PlayRandomSound(_jumpAudioClips, _jumpAudioSource);
    }

    #endregion
}
