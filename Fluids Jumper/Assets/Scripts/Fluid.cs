using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    public enum FluidType
    {
        Water,
        Gel,
        SuperFluid
    };

    public FluidType fluid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            transform.position = transform.position + new Vector3(0f, 0.1f, 0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wood")
        {
            other.transform.position = new Vector3(other.transform.position.x, transform.position.y - 0.5f, other.transform.position.z);
        }
    }
}
