using UnityEngine;


public sealed class EnemyAudioController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] _dieAudioClips;
    [SerializeField] private AudioClip[] _attackAudioClips;
    [SerializeField] private AudioClip[] _walkAudioClips;
    [SerializeField] private AudioClip[] _hurtAudioClips;

    private AudioSource _dieAudioSource;
    private AudioSource _attackAudioSource;
    private AudioSource _walkAudioSource;
    private AudioSource _hurtAudioSource;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _attackAudioSource = gameObject.AddComponent<AudioSource>();
        _dieAudioSource = gameObject.AddComponent<AudioSource>();
        _walkAudioSource = gameObject.AddComponent<AudioSource>();
        _hurtAudioSource = gameObject.AddComponent<AudioSource>();

        AudioSource[] audioSources = { _attackAudioSource, _dieAudioSource, _walkAudioSource, _hurtAudioSource };
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

    public void PlayAttackSound()
    {
        PlayRandomSound(_attackAudioClips, _attackAudioSource);
    }

    public void PlayDieSound()
    {
        PlayRandomSound(_dieAudioClips, _dieAudioSource);
    }

    public void PlayWalkSound()
    {
        PlayRandomSound(_walkAudioClips, _walkAudioSource);
    }

    public void PlayHurtSound()
    {
        PlayRandomSound(_hurtAudioClips, _hurtAudioSource);
    }

    #endregion
}
