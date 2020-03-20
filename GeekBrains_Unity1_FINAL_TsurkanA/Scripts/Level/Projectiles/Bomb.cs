using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class Bomb : MonoBehaviour
{
    #region Fields

    [SerializeField] private LayerMask _damagableLayerMask;
    [SerializeField] private GameObject _particlesBoom;

    [SerializeField] private int _damage = 2;
    [SerializeField] private float _forceOfThrowX = 8.0f;
    [SerializeField] private float _forceOfThrowY = 5.0f;
    [SerializeField] private float _lifeTime = 4.0f;
    [SerializeField] private float _boomRadius = 3.0f;
    [SerializeField] private float _boomForce = 5.0f;

    private GameObject _player;
    private Rigidbody2D _rigidBody;
    private Vector2 _force;
    private Vector2 _boomForceDirection;
    private Vector2 _boomForceAtrophy;
    private CircleCollider2D _collider;
    private Collider2D[] _hit;
    private IDamageable _damagable;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<CircleCollider2D>();

        SetForceOnStart();

        Invoke("Explode", _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _damagable = collision.gameObject.GetComponent<IDamageable>();
        if (_damagable != null && collision.gameObject.tag != "Player") Explode();
    }

    #endregion


    #region Methods

    private void Explode()
    {
        _hit = Physics2D.OverlapCircleAll(_collider.bounds.center, _boomRadius, _damagableLayerMask);
        if (_hit != null)
        {
            foreach (Collider2D _hitCollider in _hit)
            {
                if (_hitCollider.attachedRigidbody != null)
                {
                    _boomForceDirection = _hitCollider.transform.position - transform.position;
                    // Atrophy end power of boom force.
                    _boomForceAtrophy = _boomForceDirection / _boomRadius;
                    _boomForceDirection = _boomForceDirection.normalized - _boomForceAtrophy;
                    _force = _boomForce * _boomForceDirection;
                    _hitCollider.attachedRigidbody.AddForce(_force, ForceMode2D.Impulse);
                }
                _damagable = _hitCollider.gameObject.GetComponent<IDamageable>();
                if (_hitCollider != null) _damagable.Damage(_damage);
            }
            Instantiate(_particlesBoom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void SetForceOnStart()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _force.x = _player.transform.right.x * _forceOfThrowX;
        _force.y = _forceOfThrowY;
        _rigidBody.AddForce(_force, ForceMode2D.Impulse);
    }

    #endregion
}
