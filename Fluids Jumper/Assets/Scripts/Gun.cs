using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;

    [Header("Projectile")]
    public GameObject projectile;
    public GameObject projectileSpawn;
    public float velocity;

    [Header("Player")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.RegisterAction(InputManager.Keys.mouse0, Fire, GetInstanceID());
    }

    public void Fire()
    {
        var bullet = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * velocity);
    }
}
