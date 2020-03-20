using UnityEngine;
using UnityEngine.UI;


public sealed class HealthBarController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Text _healthBarTextInfo;

    private float _filledAmount;

    #endregion


    #region Methods

    public void SetHealth(int currentHealth, int maxHealth)
    {
        _filledAmount = (float)currentHealth / maxHealth;
        _healthBarImage.fillAmount = _filledAmount;
        _healthBarTextInfo.text = currentHealth.ToString();
    }

    #endregion
}
