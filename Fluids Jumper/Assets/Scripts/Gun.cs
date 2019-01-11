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

    [Header("Objects")]
    public GameObject player;
    public PlayerMovement playerMovement;

    [Header("Variables")]
    public float velocity;
    public float cooldown;
    public float counter;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.RegisterAction(InputManager.Keys.mouse0, Fire, GetInstanceID());
    }

    public void Update()
    {
        counter += Time.deltaTime;
        
    }

    public void Fire()
    {
        if (counter >= cooldown && !playerMovement.sprint)
        {
            var bullet = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * velocity);
            counter = 0f;
        }
    }
}
