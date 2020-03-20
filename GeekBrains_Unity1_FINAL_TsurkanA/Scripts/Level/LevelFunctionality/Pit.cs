using Assets.MyAssets.EllenGame.Scripts.Intefaces;
using UnityEngine;


public sealed class Pit : MonoBehaviour
{
    #region Fields

    private IKillable _killable;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _killable = collision.gameObject.GetComponent<IKillable>();
        if (_killable != null) _killable.Kill();
    }

    #endregion
}
