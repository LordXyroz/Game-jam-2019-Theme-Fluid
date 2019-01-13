using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;
    public SoundManager soundManager;

    [Header("Projectile")]
    public GameObject projectile;
    public GameObject projectileSpawn;

    [Header("Objects")]
    public GameObject player;
    public PlayerMovement playerMovement;
    public ParticleSystem particleSystem;
    public FluidSelector fluidSelector;
    public FluidTanks fluidTanks;
    
    [Header("Materials")]
    public Material waterMaterial;
    public Material gelMaterial;
    public Material superFluidMaterial;

    [Header("Audio")]
    public AudioClip shootEFX;
    public AudioClip suck1EFX;
    public AudioClip suck2EFX;
    public AudioClip suck3EFX;

    [Header("Variables")]
    public float velocity;
    public float cooldown;
    public float counter;
    public bool charging;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        
        fluidTanks.waterAmount = fluidTanks.maxStorage;
        fluidTanks.gelAmount = fluidTanks.maxStorage;
        fluidTanks.superFluidAmount = fluidTanks.maxStorage;

        inputManager.RegisterAction(InputManager.Keys.mouse0, Fire, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.mouse1, AlternateFire, GetInstanceID());
        inputManager.RegisterAction(InputManager.Keys.mouse1Up, StopAlternateFire, GetInstanceID());
    }

    public void Update()
    {
        counter += Time.deltaTime;
        
    }

    public void Fire()
    {
        if (counter >= cooldown && !playerMovement.sprint && !playerMovement.gunDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(projectileSpawn.transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == "Fluid")
                {
                    if (hit.collider.GetComponent<Fluid>().limitReached)
                    {
                        return;
                    }
                }
            }
            switch (fluidSelector.fluidType)
            {
                case FluidType.Water:
                    if (fluidTanks.waterAmount > 0)
                    {
                        fluidTanks.waterAmount -= 1;
                        break;
                    }
                    else return;
                case FluidType.Gel:
                    if (fluidTanks.gelAmount > 0)
                    {
                        fluidTanks.gelAmount -= 1;
                        break;
                    }
                    else return;
                case FluidType.SuperFluid:
                    if (fluidTanks.superFluidAmount > 0)
                    {
                        fluidTanks.superFluidAmount -= 1;
                        break;
                    }
                    else return;
            }

            soundManager.PlayEfx(shootEFX);
            var bullet = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * velocity);

            counter = 0f;
            
        }
    }

    public void AlternateFire()
    {
        if (!playerMovement.sprint && !playerMovement.gunDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(projectileSpawn.transform.position, transform.forward, out hit))
            {
                if (hit.collider.gameObject.tag == "Fluid" && counter >= cooldown)
                {
                    counter = 0f;

                    if (hit.collider.gameObject.GetComponent<Fluid>().targetHeight.y >= -0.05f)
                    {
                        if (hit.collider.gameObject.GetComponent<Fluid>().fluid == FluidType.Water && fluidTanks.waterAmount >= fluidTanks.maxStorage)
                            return;
                        if (hit.collider.gameObject.GetComponent<Fluid>().fluid == FluidType.Gel && fluidTanks.gelAmount >= fluidTanks.maxStorage)
                            return;
                        if (hit.collider.gameObject.GetComponent<Fluid>().fluid == FluidType.SuperFluid && fluidTanks.superFluidAmount >= fluidTanks.maxStorage)
                            return;

                        hit.collider.gameObject.GetComponent<Fluid>().targetHeight.y -= 0.1f;
                        particleSystem.transform.position = hit.point;

                        particleSystem.transform.forward = projectileSpawn.transform.position - particleSystem.transform.position;

                        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();

                        switch (hit.collider.gameObject.GetComponent<Fluid>().fluid)
                        {
                            case FluidType.Water:
                                renderer.material = waterMaterial;
                                renderer.trailMaterial = waterMaterial;
                                fluidTanks.waterAmount += 1;
                                break;
                            case FluidType.Gel:
                                renderer.material = gelMaterial;
                                renderer.trailMaterial = gelMaterial;
                                fluidTanks.gelAmount += 1;
                                break;
                            case FluidType.SuperFluid:
                                renderer.material = superFluidMaterial;
                                renderer.trailMaterial = superFluidMaterial;
                                fluidTanks.superFluidAmount += 1;
                                break;
                        }

                        if (particleSystem.isStopped)
                            particleSystem.Play();

                        int rand = Random.Range(0, 3);
                        Debug.Log(rand);
                        switch (rand)
                        {
                            case 0:
                                soundManager.PlayEfx(suck1EFX);
                                break;
                            case 1:
                                soundManager.PlayEfx(suck2EFX);
                                break;
                            case 2:
                                soundManager.PlayEfx(suck3EFX);
                                break;
                        }

                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<Fluid>().fluid = FluidType.None;
                        particleSystem.Stop();
                    }
                }
            }

        }
        else
            StopAlternateFire();
    }

    public void StopAlternateFire()
    {
        particleSystem.Stop();
    }
}
