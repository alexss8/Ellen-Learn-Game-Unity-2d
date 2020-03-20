using UnityEngine;


public sealed class GameFinisher : MonoBehaviour
{
    #region Fields

    private GameObject _cameraGameObject;
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private Color _initialColor;
    private Vector3 _newPosition;

    private float _opacityVisible = 1.0f;
    private float _opacityInVisible = 0.0f;
    private float _smoothingColorVisibility = 0.01f;
    private bool _isGameEnded = false;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
        _spriteRenderer.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _opacityInVisible);
    }

    private void FixedUpdate()
    {
        if (_isGameEnded && _spriteRenderer.color.a != _opacityVisible)
        {
            if (_spriteRenderer.color.a != _opacityVisible)
            {
                _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _initialColor, _smoothingColorVisibility);
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }
    }

    #endregion


    #region Methods

    public void EndGame()
    {
        if (!_isGameEnded)
        {
            _isGameEnded = true;
            _playerController.enabled = false;
            _newPosition = _cameraGameObject.transform.position;
            _newPosition.z = 0.0f;
            transform.position = _newPosition;
        }
    }

    #endregion
}
