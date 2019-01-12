using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    public DialogManager manager;

    public string[] dialog;
    public string title;

    public bool triggered;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!triggered)
            {
                triggered = true;
                manager.SetTitle(title);
                manager.ShowDialog(dialog);
            }
        }
    }
}
