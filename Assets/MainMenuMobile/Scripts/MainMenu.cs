using UnityEngine;
using AsyncSceneTransition;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private Transform _playerTransform;

    // Various 
    private Canvas _touchCanvas;
    private GameObject _pauseScreen;

    private bool _paused = false;



    void Awake()
    {
        _touchCanvas = GameObject.FindWithTag("UICanvas").GetComponent<Canvas>();
        //ScreenFade.fadedOut += transform.GetChild(0).gameObject.SetActive(true);
            
    }


    /// <summary>
    /// Toggles pause setting and will turn the UI off/on
    /// </summary>
    public void PauseGame()
    {
        _paused = !_paused;
        // Stop time
        Time.timeScale = _paused ? 0f : 1f;
        _pauseScreen.SetActive(_paused);
    }

    /// <summary>
    /// Returns to the main menu scene and tidies up variables/state
    /// </summary>
    public void ReturnToMenu()
    {

        // Do we need to tidy up any variables or state?
        _pauseScreen.SetActive(false);
        PauseGame();
        SceneTransitionManager.Instance.LoadTargetLevel(0);
    }

    public void ReloadToCheckpoint()
    {
        _pauseScreen.SetActive(false);
        PauseGame();
        SceneTransitionManager.Instance.ReloadCurrentLevel();
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void CloseGame()
    {
        // Does anything need to happen? Save data to playerprefs or whatever?

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
        {
            PauseGame();
        }
        Application.Quit();
    }
}
