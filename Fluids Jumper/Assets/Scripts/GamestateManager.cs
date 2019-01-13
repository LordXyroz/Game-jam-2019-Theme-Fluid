using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
{
    [Header("Managers")]
    public SoundManager soundManager;
    public InputManager inputManager;
    public DialogManager dialogManager;

    [Header("GameObjects")]
    public GameObject hud;
    public GameObject pauseMenu;
    public GameObject player;
    public GameObject spawnPoint;

    [Header("Scripts")]
    public PlayerMovement playerMovement;

    [Header("Buttons")]
    public Button quitLevelButton;

    [Header("Scenes")]
    public string mainMenuScene;

    [Header("BGM")]
    public AudioClip bgm;

    [Header("Variables")]
    public bool dead = false;
    public bool paused = false;


    // Start is called before the first frame update
    void Start()
    {
        quitLevelButton.onClick.AddListener(QuitLevel);

        soundManager = FindObjectOfType<SoundManager>();

        soundManager.PlayBGM(bgm);

        inputManager.RegisterAction(InputManager.Keys.escape, ToggleMenu, GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            player.transform.position = spawnPoint.transform.position;
            dead = false; //duuuuuhhhhhh
        }
    }

    public void ToggleMenu()
    {
        if (!dialogManager.dialogBox.activeSelf)
        {
            paused = !paused;
            playerMovement.view = !paused;
            playerMovement.move = !paused;
            playerMovement.gunDown = paused;

            Cursor.visible = paused;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;

            pauseMenu.SetActive(paused);
        }
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
