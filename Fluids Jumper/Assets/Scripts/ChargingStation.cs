using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    [Header("Objects")]
    public FluidTanks data;
    public PlayerMovement player;

    [Header("GameObjects")]
    public GameObject gun;

    [Header("Variables")]
    public float cooldown;
    public float counter;
    public bool charging;

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            counter += Time.deltaTime;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            charging = true;
            player.gunDown = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.gunDown = true;
            if (counter >= cooldown)
            {
                if (data.waterAmount < data.maxStorage)
                    data.waterAmount += 1;
                if (data.gelAmount < data.maxStorage)
                    data.gelAmount += 1;
                if (data.superFluidAmount < data.maxStorage)
                    data.superFluidAmount += 1;

                counter = 0f;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.gunDown = false;
            charging = false;
            
        }
    }
}
