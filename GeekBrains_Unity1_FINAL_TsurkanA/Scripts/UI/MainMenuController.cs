using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public sealed class MainMenuController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _loadingProgress;
    [SerializeField] private Text _textHelper;

    [SerializeField] private string _helpText = "Press Any Key to continue...";

    private GameObject _levelController;

    private const float _PROGRESS_MAX = 1.0f;

    private float _progressStartAskingFrom = 0.9f;
    // Value left for full progress fill.
    private float _progressFillerValue;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _progressFillerValue = _PROGRESS_MAX - _progressStartAskingFrom;
        _levelController = GameObject.FindGameObjectWithTag("LevelController");
        _levelController.SetActive(false);
    }

    #endregion


    #region Methods

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int screneBuildIndex)
    {
        StartCoroutine(LoadSceneCoroutine(screneBuildIndex));
    }

    IEnumerator LoadSceneCoroutine(int screneBuildIndex)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(screneBuildIndex);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            _loadingProgress.fillAmount = asyncOperation.progress + _progressFillerValue;

            if (asyncOperation.progress >= _progressStartAskingFrom)
            {
                _textHelper.text = _helpText;
                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                    _levelController.SetActive(true);
                }
            }

            yield return null;
        }
    }

    #endregion
}

