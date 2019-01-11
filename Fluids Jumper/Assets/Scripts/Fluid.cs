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
    public SphereCollider collider;
    public float timer;

    public Vector3 currentHeight;
    public Vector3 targetHeight;


    // Start is called before the first frame update
    void Start()
    {
        currentHeight = transform.position;
        targetHeight = currentHeight;
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

        transform.position = Vector3.Lerp(currentHeight, targetHeight, Time.deltaTime * 2f);
        currentHeight = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            collider.center = transform.worldToLocalMatrix * new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            Destroy(other.gameObject);
            targetHeight += new Vector3(0f, 0.1f, 0f);
            timer = 0.05f;
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
