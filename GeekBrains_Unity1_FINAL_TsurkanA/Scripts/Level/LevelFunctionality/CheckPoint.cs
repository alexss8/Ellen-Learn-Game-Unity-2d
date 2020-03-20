using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _pointOfStart;

    private bool _isSet = false;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isSet && collision.gameObject.tag.Equals("Player"))
        {
            _isSet = true;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.SetStartPosition(_pointOfStart.position);
        }
    }

    #endregion
}
