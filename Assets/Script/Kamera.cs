using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{

    public Transform target;
    public Vector3 target_offset;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;
    void Start()
    {
        target_offset = transform.position - target.position;
    }

  
    private void LateUpdate()
    {
        if (!SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .55f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, .010f);
        }
    }
}
