using UnityEngine;


public class DoorButton : MonoBehaviour
{
    #region Fields

    [SerializeField] private Door _door;

    private AudioSource _audioSourcePressed;
    private Collider2D _collider;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _collider = gameObject.GetComponent<Collider2D>();
        _audioSourcePressed = gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            _door.Open();
            _audioSourcePressed.Play();
            _collider.enabled = false;
        }
    }

    #endregion
}
