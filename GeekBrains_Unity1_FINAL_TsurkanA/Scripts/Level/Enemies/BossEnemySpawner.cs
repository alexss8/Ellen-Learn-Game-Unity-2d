using UnityEngine;


public sealed class BossEnemySpawner : MonoBehaviour
{

    #region Fields

    [SerializeField] private GameObject _enemy;
    [SerializeField] private Vector3 _spawnPosition;

    [SerializeField] private float _spawnColdown = 5.0f;

    private const string TAG_NAME_PLAYER = "Player";

    private bool _canSpawn = true;
    private bool _isPlayerInZone = false;

    #endregion


    #region UnityMethods

    private void FixedUpdate()
    {
        if (_isPlayerInZone && _canSpawn) SpawnEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TAG_NAME_PLAYER)) _isPlayerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TAG_NAME_PLAYER)) _isPlayerInZone = false;
    }

    #endregion


    #region Methods

    private void SpawnEnemy()
    {
        Instantiate(_enemy, transform.position + _spawnPosition, transform.rotation);
        _canSpawn = false;
        Invoke("ReloadSpawn", _spawnColdown);
    }

    private void ReloadSpawn()
    {
        _canSpawn = true;
    }

    #endregion
}
