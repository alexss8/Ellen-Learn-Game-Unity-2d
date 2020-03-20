using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _healing = 1;

    private SceneAudioController _sceneAudioController;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        GameObject LevelController = GameObject.FindGameObjectWithTag("LevelController");
        _sceneAudioController = LevelController.GetComponent<SceneAudioController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHealable Health = collision.gameObject.GetComponent<IHealable>();
        if (Health != null)
        {
            _sceneAudioController.PlayHealthPickUpSound();
            Health.Heal(_healing);
            Destroy(gameObject);
        }
    }

    #endregion
}
