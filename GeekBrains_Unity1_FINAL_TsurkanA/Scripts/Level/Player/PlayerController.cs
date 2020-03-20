using UnityStandardAssets.CrossPlatformInput;
using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class PlayerController : MonoBehaviour, IKillable, IDamageable, IHealable
{
    #region Fields

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _lightPrefab;
    [SerializeField] private Transform _startBullet;
    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private PlayerAudioController _playerAudioController;

    [SerializeField] private int _healthMax = 10;
    [SerializeField] private int _healthMin = 0;
    [SerializeField] private float _speedMax = 6.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _reloadShootTime = 0.3f;
    [SerializeField] private float _reloadBombTime = 2.5f;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private CapsuleCollider2D _collider;
    private AudioSource _audioSourceShoot;
    private RaycastHit2D[] _hits;
    private Quaternion _rotationLookForward = Quaternion.identity;
    private Quaternion _rotationLookBackward = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    private Vector3 _boxCastSize;
    private Vector2 _curSpeed;
    private Vector2 _startPosition;
    private HealthBarController _healthBarController;

    private int _health;
    private float _inputHorizontal = 0.0f;
    private float _boxCastExtraHeight = 0.2f;
    private float _boxCastSizeRatio = 0.9f;
    private bool _isForward = true;
    private bool _canBomb = true;
    private bool _canShoot = true;
    private bool _isGrounded = false;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.SetTarget(gameObject.transform);

        _startPosition = gameObject.transform.position;
        _health = _healthMax;

        GameObject LevelControllerObject = GameObject.FindGameObjectWithTag("LevelController");
        _healthBarController = LevelControllerObject.GetComponent<HealthBarController>();
        _healthBarController.SetHealth(_health, _healthMax);

        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _audioSourceShoot = gameObject.GetComponent<AudioSource>();
        _animator = gameObject.GetComponent<Animator>();

        _collider = gameObject.GetComponent<CapsuleCollider2D>();
        _boxCastSize = _collider.bounds.size * _boxCastSizeRatio;

        GameObject Light = Instantiate(_lightPrefab);
        LightFollower _lightFollower = Light.GetComponent<LightFollower>();
        _lightFollower.SetTarget(transform);
    }

    private void Update()
    {
        _inputHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        if (_canShoot && CrossPlatformInputManager.GetButton("Fire1")) Fire();
        if (_canBomb && CrossPlatformInputManager.GetButton("Fire2")) FireBomb();
        if (CrossPlatformInputManager.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
            _animator.SetBool("IsJumping", true);
            _playerAudioController.PlayJumpSound();
        }
    }

    private void FixedUpdate()
    {
        ChangeLookDirection(_inputHorizontal);
        Move(_inputHorizontal);
        _animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = IsGrounded();
        _animator.SetBool("IsJumping", !_isGrounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = IsGrounded();
    }

    #endregion


    #region Methods

    private bool IsGrounded()
    {
        _hits = Physics2D.BoxCastAll(_collider.bounds.center, _boxCastSize, 0.0f, Vector2.down, _boxCastExtraHeight, _platformLayerMask);
        foreach (var hit in _hits)
        {
            if (hit.collider != null && !hit.collider.isTrigger)
                return true;
        }
        return false;
    }

    private void Move(float _inputHorizontal)
    {
        _curSpeed = _rigidBody.velocity;
        _curSpeed.x = _inputHorizontal * _speedMax;
        _rigidBody.velocity = _curSpeed;
    }

    private void Jump()
    {
        _isGrounded = false;
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
   
    private void ChangeLookDirection(float _inputHorizontal)
    {
        if (_inputHorizontal > 0.0f && !_isForward) LookForward();
        else if (_inputHorizontal < 0.0f && _isForward) LookBackward();      
    }

    private void LookForward()
    {
        _isForward = true;
        transform.rotation = _rotationLookForward;
    }

    private void LookBackward()
    {
        _isForward = false;
        transform.rotation = _rotationLookBackward;
    }

    private void Fire()
    {
        _canShoot = false;

        _playerAudioController.PlayShootSound();

        Instantiate(_bullet, _startBullet.position, transform.rotation);

        Invoke("ReloadShooting", _reloadShootTime);
    }

    private void ReloadShooting()
    {
        _canShoot = true;
    }

    private void FireBomb()
    {
        _canBomb = false;

        Instantiate(_bomb, _startBullet.position, transform.rotation);

        Invoke("ReloadBombing", _reloadBombTime);
    }

    private void ReloadBombing()
    {
        _canBomb = true;
    }

    private void Respawn()
    {
        _health = _healthMax;
        _healthBarController.SetHealth(_health, _healthMax);
        transform.position = _startPosition;
        _isForward = true;
        transform.rotation = _rotationLookForward;
    }

    public void SetStartPosition(Vector2 position)
    {
        _startPosition = position;
    }

    #endregion


    #region IKillable

    public void Kill()
    {
        Respawn();
    }

    #endregion


    #region IDamageable

    public int Damage(int damage)
    {
        _health = Mathf.Max(_health - damage, _healthMin);
        _healthBarController.SetHealth(_health, _healthMax);
        _playerAudioController.PlayHurtSound();
        if (_health == _healthMin) Kill();
        return _health;
    }

    #endregion


    #region IHealable

    public int Heal(int healing)
    {
        _health = Mathf.Min(_health + healing, _healthMax);
        _healthBarController.SetHealth(_health, _healthMax);
        return _health;
    }

    #endregion
}
