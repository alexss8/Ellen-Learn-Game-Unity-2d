using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class Spikes : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _damage;
    [SerializeField] private float _reboundYVelocity;

    private Rigidbody2D _targetRigidBody;
    private Vector2 _curSpeed;
    private IDamageable _damageable;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _damageable = collision.gameObject.GetComponent<IDamageable>();
        if (_damageable != null)
        {
            _damageable.Damage(_damage);
            _targetRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (_targetRigidBody != null)
            {
                _curSpeed = _targetRigidBody.velocity;
                _curSpeed.y = _reboundYVelocity;
                _curSpeed.x *= -1.0f;
                _targetRigidBody.velocity = _curSpeed;
            }
        }
    }

    #endregion
}
