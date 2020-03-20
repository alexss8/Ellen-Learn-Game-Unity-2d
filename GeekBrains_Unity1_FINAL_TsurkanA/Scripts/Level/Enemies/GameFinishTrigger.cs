using UnityEngine;


public sealed class GameFinishTrigger : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameFinisher _gameFinisher;

    #endregion


    #region UnityMethods

    private void OnDestroy()
    {
        _gameFinisher.EndGame();
    }

    #endregion
}
