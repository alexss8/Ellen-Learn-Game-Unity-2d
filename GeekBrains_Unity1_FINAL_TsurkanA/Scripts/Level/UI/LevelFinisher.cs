using UnityEngine;


public sealed class LevelFinisher : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _nextSceneNumber;

    private LevelController _levelController;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        GameObject LevelControllerGameObj = GameObject.FindGameObjectWithTag("LevelController");
        _levelController = LevelControllerGameObj.GetComponent<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _levelController.LoadScene(_nextSceneNumber);
        }
    }

    #endregion
}
