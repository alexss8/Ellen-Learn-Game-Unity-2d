using UnityEngine;


public sealed class HealthPickupOnDestroySpawner : MonoBehaviour
{
    #region

    [SerializeField] GameObject _healthPickup;

    private Vector3 _positionOffset = new Vector2(0.0f, 1.0f);

    #endregion


    #region UnityMethods

    private void OnDestroy()
    {
        _healthPickup = Instantiate(
            _healthPickup, 
            transform.position + _positionOffset, 
            transform.rotation);
    }

    #endregion
}
