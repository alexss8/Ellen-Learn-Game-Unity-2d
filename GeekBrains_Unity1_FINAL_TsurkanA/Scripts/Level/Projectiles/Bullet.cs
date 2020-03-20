using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class Bullet : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 9.0f;
    [SerializeField] private float _lifeTime = 4.0f;

    private GameObject _player;
    private Rigidbody2D _rigidBody;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        SetSpeedOnStart();
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null) damageable.Damage(_damage);
        Destroy(gameObject);
    }

    #endregion


    #region Methods

    private void SetSpeedOnStart()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidBody.velocity = _player.transform.right * _speed;
    }

    #endregion
}
