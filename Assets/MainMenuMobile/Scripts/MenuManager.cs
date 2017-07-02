using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private static MenuManager instance;

    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindWithTag("GlobalGameManager").GetComponent<MenuManager>();
            }

            return instance;
        }
    }
    private GameObject optionsMenuPanel;
    private GameObject pauseMenuPanel;
    private Text musicOnText;
    private Text sfxOnText;

    // Use this for initialization
    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetUpMainMenu();
        }
        else
        {
            SetUpPauseMenu();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenuPanel != null)
        {
            if(pauseMenuPanel.activeSelf)
            {
                SetMusicStrings();
            }
        }

        if(optionsMenuPanel != null)
        {
            if(optionsMenuPanel.activeSelf)
            {
                SetMusicStrings();
            }
        }
    }

    void SetUpMainMenu()
    {
        optionsMenuPanel = GameObject.Find("Options Panel");
        musicOnText = GameObject.Find("MusicText").GetComponent<Text>();
        sfxOnText = GameObject.Find("SFXText").GetComponent<Text>();
        optionsMenuPanel.SetActive(false);
        // Maybe make sure the pause screen is off, instead of relying on the rpfab to be setup correct
        // this is Matt's fault, blame him!
    }

    void SetUpPauseMenu()
    {
        pauseMenuPanel = GameObject.Find("PauseScreen");
        musicOnText = GameObject.Find("MusicText").GetComponent<Text>();
        sfxOnText = GameObject.Find("SFXText").GetComponent<Text>();
        pauseMenuPanel.SetActive(false);
    }

    public void SetMusicStrings()
    {
        if(SoundManager.Instance.MusicOn)
        {
            musicOnText.text = "Music: On";
        }
        else
        {
            musicOnText.text = "Music: Off";
        }

        if (SoundManager.Instance.SfxOn)
        {
            sfxOnText.text = "SFX: On";
        }
        else
        {
            sfxOnText.text = "SFX: Off";
        }
    }
}
