using UnityEngine;


public sealed class LightFollower : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 1.0f, -1.0f);

    private Transform _target;

    #endregion


    #region UnityMethods

    private void LateUpdate()
    {
        FollowTarget();
    }

    #endregion


    #region Methods

    private void FollowTarget()
    {
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = desiredPosition;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    #endregion
}
