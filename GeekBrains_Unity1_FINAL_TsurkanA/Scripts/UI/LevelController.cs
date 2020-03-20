using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class LevelController : Singleton<LevelController>
{
    #region Fields

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mobileSingleStickControlPrefab;

    private GameObject _mobileSingleStickControl;

    private bool _isGameOnPause = false;

    #endregion


    #region UnityMethods

    override protected void Awake()
    {
        base.Awake();
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGameOnPause) ResumeGame();
            else PauseGame();
        }
    }

    private void OnDisable()
    {
#if UNITY_ANDROID
        Destroy(_mobileSingleStickControl);
#endif
    }

    private void OnEnable()
    {
#if UNITY_ANDROID
        SetUpMobileEnvironment();
#endif
    }

    #endregion


    #region Methods

    private void SetUpMobileEnvironment()
    {
        _mobileSingleStickControl = Instantiate(
            _mobileSingleStickControlPrefab,
            _mobileSingleStickControlPrefab.transform.position,
            _mobileSingleStickControlPrefab.transform.rotation
            );
        _mobileSingleStickControl.transform.SetParent(transform.parent, false);
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        _isGameOnPause = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int screneBuildIndex)
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        _isGameOnPause = false;
        SceneManager.LoadScene(screneBuildIndex);
    }

    private void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        _isGameOnPause = true;
    }
    
    #endregion
}

