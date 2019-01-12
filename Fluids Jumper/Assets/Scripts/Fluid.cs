using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FluidType
{
    Water,
    Gel,
    SuperFluid,
    None
};

public class Fluid : MonoBehaviour
{
    [Header("Managers")]
    public GamestateManager gamestateManager;

    [Header("Main")]
    public FluidType fluid;
    public SphereCollider collider;

    [Header("Vectors")]
    public Vector3 currentHeight;
    public Vector3 targetHeight;

    [Header("Variables")]
    public float timer;
    public float heightLimit;
    public bool limitReached;

    [Header("Materials")]
    public Material waterMaterial;
    public Material gelMaterial;
    public Material superFluidMaterial;

    // Start is called before the first frame update
    void Start()
    {
        currentHeight = transform.position;
        targetHeight = currentHeight;

        switch (fluid)
        {
            case FluidType.Water:
                GetComponent<Renderer>().material = waterMaterial;
                break;
            case FluidType.Gel:
                GetComponent<Renderer>().material = gelMaterial;
                break;
            case FluidType.SuperFluid:
                GetComponent<Renderer>().material = superFluidMaterial;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            collider.center = new Vector3(0f, 5f, 0f);
        }
        else
            timer -= Time.deltaTime;

        if (targetHeight.y >= heightLimit)
            limitReached = true;
        else
            limitReached = false;


        transform.position = Vector3.Lerp(currentHeight, targetHeight, Time.deltaTime * 2f);
        currentHeight = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            if (fluid == FluidType.None)
            {
                fluid = other.GetComponent<Projectile>().fluidType;

                switch (fluid)
                {
                    case FluidType.Water:
                        GetComponent<Renderer>().material = waterMaterial;
                        break;
                    case FluidType.Gel:
                        GetComponent<Renderer>().material = gelMaterial;
                        break;
                    case FluidType.SuperFluid:
                        GetComponent<Renderer>().material = superFluidMaterial;
                        break;
                }
            }
            if (other.GetComponent<Projectile>().fluidType == fluid)
            {
                collider.center = transform.InverseTransformPoint(new Vector3(other.transform.position.x, 0f, other.transform.position.z));
                Destroy(other.gameObject);
                if (targetHeight.y < heightLimit)
                {
                    targetHeight += new Vector3(0f, 0.1f, 0f);
                    timer = 0.05f;
                }
            }

            
        }
        if (other.gameObject.tag == "PlayerTrigger")
        {
            gamestateManager.dead = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (fluid)
        {
            case FluidType.Water:
                if (other.gameObject.tag == "Wood")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                break;
            case FluidType.Gel:
                if (other.gameObject.tag == "Wood")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                if (other.gameObject.tag == "Heavy")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                break;
            case FluidType.SuperFluid:
                if (other.gameObject.tag == "Wood")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                if (other.gameObject.tag == "Heavy")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                if (other.gameObject.tag == "SuperHeavy")
                {
                    other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.3f, other.transform.position.z);
                }
                break;
        }
    }
}
