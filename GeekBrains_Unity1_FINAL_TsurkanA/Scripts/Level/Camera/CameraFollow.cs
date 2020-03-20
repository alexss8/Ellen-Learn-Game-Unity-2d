using UnityEngine;


public sealed class CameraFollow : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 1.0f, -10.0f);

    [SerializeField] private float _smoothSpeed = 0.125f;

    private Transform _target;

    #endregion


    #region UnityMethods

    private void FixedUpdate()
    {
        FollowCameraSmooth();
    }

    #endregion


    #region Methods

    private void FollowCameraSmooth()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    #endregion
}
