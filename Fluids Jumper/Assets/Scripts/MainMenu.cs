using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Groupings")]
    public GameObject mainMenu;
    public GameObject levelSelect;

    [Header("Buttons")]
    public Button levelSelectButton;
    public Button quitGameButton;
    public Button tutorialButton;
    public Button backButton;

    [Header("Levels")]
    public string tutorialLevel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        levelSelectButton.onClick.AddListener(SelectLevelMenu);
        quitGameButton.onClick.AddListener(QuitGame);
        tutorialButton.onClick.AddListener(SelectTutorial);
        backButton.onClick.AddListener(Back);

        FindObjectOfType<SoundManager>().PlayBGM(FindObjectOfType<SoundManager>().defaultClip);
    }

    public void SelectLevelMenu()
    {
        levelSelect.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SelectTutorial()
    {
        SceneManager.LoadScene(tutorialLevel);
    }

    public void Back()
    {
        levelSelect.SetActive(false);
        mainMenu.SetActive(true);
    }
}
