using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject gameoverText;
    public GameObject player;
    public GameObject spawnPoint;

    [Header("Variables")]
    public bool dead = false;


    // Start is called before the first frame update
    void Start()
    {
        
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
}
