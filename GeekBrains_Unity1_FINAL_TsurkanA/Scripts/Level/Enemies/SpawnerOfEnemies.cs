using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class SpawnerOfEnemies : MonoBehaviour, IKillable, IDamageable
{

    #region Fields

    [SerializeField] private Color _damagedColor = Color.red;
    [SerializeField] private GameObject _particlesDestroy;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _enemySpawnPlace;

    [SerializeField] private int _healthMin = 0;
    [SerializeField] private int _health = 10;
    [SerializeField] private float _spawnColdown = 3.0f;
    [SerializeField] private float _damagedTime = 0.3f;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSourceDamaged;
    private Color _initColor;

    private const string TAG_NAME_PLAYER = "Player";

    private bool _canSpawn = true;
    private bool _isPlayerInZone = false;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _audioSourceDamaged = gameObject.GetComponent<AudioSource>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _initColor = _spriteRenderer.color;
    }

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
        Instantiate(_enemy, _enemySpawnPlace.position, transform.rotation);
        _canSpawn = false;
        Invoke("ReloadSpawn", _spawnColdown);
    }

    private void ReloadSpawn()
    {
        _canSpawn = true;
    }

    private void ColorBack()
    {
        _spriteRenderer.color = _initColor;
    }

    #endregion


    #region IKillable

    public void Kill()
    {
        Instantiate(_particlesDestroy, transform.position, transform.rotation);
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
            _audioSourceDamaged.Play();
            _spriteRenderer.color = _damagedColor;
            CancelInvoke("ColorBack");
            Invoke("ColorBack", _damagedTime);
        }
        return _health;
    }

    #endregion
}
