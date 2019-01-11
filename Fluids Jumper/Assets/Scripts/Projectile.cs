using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float duration = 5f;
    public float lifeTime;
    // Start is called before the first frame update
    void Awake()
    {
        lifeTime = 0f;
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
