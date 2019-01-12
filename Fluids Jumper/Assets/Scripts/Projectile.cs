using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Main")]
    public FluidSelector fluidSelector;
    public FluidType fluidType;

    [Header("Variables")]
    public float duration = 5f;
    public float lifeTime;

    [Header("Materials")]
    public Material myMaterial;

    public Material waterMaterial;
    public Material gelMaterial;
    public Material superFluidMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        lifeTime = 0f;
        fluidSelector = FindObjectOfType<FluidSelector>();
        fluidType = fluidSelector.fluidType;

        switch (fluidType)
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
        lifeTime += Time.deltaTime; 
        if (lifeTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
