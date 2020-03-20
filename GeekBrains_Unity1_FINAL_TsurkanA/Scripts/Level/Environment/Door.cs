using UnityEngine;


public sealed class Door : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _journeyLength = 3.4f;

    private AudioSource _audioSourceOpening;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float _fractionOfJourney;
    private float _distanceCovered = 0.0f;
    private bool _opening = false;
    private bool _isClosed = true;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _audioSourceOpening = gameObject.GetComponent<AudioSource>();
        _startPosition = transform.position;
        _endPosition = _startPosition - new Vector3(0.0f, _journeyLength);
    }

    private void FixedUpdate()
    {
        if (_isClosed && _opening) Move();
    }

    #endregion


    #region Methods

    private void Move()
    {
        _distanceCovered += _speed * Time.deltaTime;
        if (_distanceCovered >= _journeyLength)
        {
            transform.position = _endPosition;
            _opening = false;
            _isClosed = false;
        }
        else
        {
            _fractionOfJourney = _distanceCovered / _journeyLength;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _fractionOfJourney);
        }
    }

    public void Open()
    {
        if (_isClosed) _audioSourceOpening.Play();
        _opening = true;
    }

    #endregion
}
