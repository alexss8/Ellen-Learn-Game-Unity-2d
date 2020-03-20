using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public class MyEnemy : MonoBehaviour, IKillable, IDamageable
{
    #region Fields

    [SerializeField] private Color _damagedColor = Color.red;
    [SerializeField] private LayerMask _targetLeayerMask;
    [SerializeField] private EnemyAudioController _enemyAudioController;

    [SerializeField] private int _healthMin = 0;
    [SerializeField] private int _health = 3;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _atackCouldown = 2.0f;
    [SerializeField] private float _speedMax = 4.0f;
    [SerializeField] private float _damagedTime = 0.3f;
    [SerializeField] private float _minDistance = 1.5f;
    [SerializeField] private float _aggroZoneRadius = 4.0f;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Color _initColor;
    private PlayerController _target;
    private Collider2D _hit;
    private CapsuleCollider2D _collider;
    private Quaternion _rotationLookForward = Quaternion.identity;
    private Quaternion _rotationLookBackward = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    private Vector2 _curSpeed;

    private Vector2 _distanceToTarget;
    private bool _isForward = true;
    private bool _canAttack = true;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<CapsuleCollider2D>();
        _initColor = _spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        if (IsAggroed())
        {
            _distanceToTarget = _target.transform.position - transform.position;
            ChangeLookDirection(_distanceToTarget.x);
            if (_distanceToTarget.sqrMagnitude > _minDistance * _minDistance) Move(_distanceToTarget.x);
            else if (_canAttack) Attack();
        }
    }

    #endregion


    #region Methods

    private void Attack()
    {
        _canAttack = false;
        _animator.SetBool("Attacking", true);
        _animator.Play("Attack");
        Invoke("ResetAtackCouldown", _atackCouldown);
    }

    private void DamageTarget()
    {
        _target.Damage(_damage);
    }

    private void ResetAtackCouldown()
    {
        _animator.SetBool("Attacking", false);
        _canAttack = true;
    }

    private bool IsAggroed()
    {
        _hit = Physics2D.OverlapCircle(_collider.bounds.center, _aggroZoneRadius, _targetLeayerMask);
        if (_hit != null)
        {
            _target = _hit.gameObject.GetComponent<PlayerController>();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeLookDirection(float direction)
    {
        if (Mathf.Sign(direction) > 0.0f && !_isForward) LookForward();
        else if (Mathf.Sign(direction) <= 0.0f && _isForward) LookBackward();
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

    private void Move(float _distanceToTarget)
    {
        _curSpeed = _rigidBody.velocity;
        _curSpeed.x = Mathf.Sign(_distanceToTarget) * _speedMax;
        _rigidBody.velocity = _curSpeed;
    }

    private void ColorBack()
    {
        _spriteRenderer.color = _initColor;
    }

    #endregion


    #region IKillable

    public void Kill()
    {
        _animator.SetBool("Dead", true);
}

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion


    #region IDamageable

    public int Damage(int damage)
    {
        _health = Mathf.Max(_health - damage, _healthMin);
        if (_health == _healthMin)
        {
            Kill();
        }
        else
        {
            _enemyAudioController.PlayHurtSound();
            _spriteRenderer.color = _damagedColor;
            CancelInvoke("ColorBack");
            Invoke("ColorBack", _damagedTime);
        }
        return _health;
    }

    #endregion
}
