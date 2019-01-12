using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    [Header("Important")]
    public InputManager inputManager;

    [Header("Gameobjects")]
    public GameObject dialogBox;
    public Text dialogText;
    public Text titleText;

    [Header("Buttons")]
    public Button next;
    public Button close;
    public Button previous;

    [Header("Text variables")]
    public string[] dialogs;
    public int currentLine;

    [Header("Player")]
    [SerializeField]
    private PlayerMovement player;

    // Use this for initialization
    void Start () {
        dialogText.text = dialogs[0];

        close.onClick.AddListener(CloseBox);
        next.onClick.AddListener(NextLine);
        previous.onClick.AddListener(PreviousLine);
	}


    void Update()
    {
        if (currentLine < dialogs.Length - 1)
        {
            next.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
        }
        else
        {
            next.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
        }

        if (currentLine >= 1)
        {
            previous.gameObject.SetActive(true);
        }
        else
        {
            previous.gameObject.SetActive(false);
        }
    }
    

    public void CloseBox()
    {
        // Resets buttons and values when closing dialog box
        currentLine = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Enable player to move and rotate again
        player.move = true;
        player.view = true;
        player.gunDown = false;

        dialogBox.SetActive(false);
    }

    void NextLine()
    {
        dialogText.text = dialogs[++currentLine];
    }

    void PreviousLine()
    {
        dialogText.text = dialogs[--currentLine];
    }

    public void ShowDialog(string[] text)
    {
        // Stops player from moving and rotating while in a dialog box
        player.view = false;
        player.move = false;
        player.gunDown = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Starts displaying text
        dialogs = text;
        dialogText.text = dialogs[0];
        dialogBox.SetActive(true);
    }

    public void SetTitle(string text)
    {
        titleText.text = text;
    }
}
