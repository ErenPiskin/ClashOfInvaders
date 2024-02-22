using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruzgar : MonoBehaviour
{
    public float solPervaneKuvvet = 15f;  
    public float sagPervaneKuvvet = -15f;

    private bool isInsideArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Altkarakterler"))
        {
            isInsideArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Altkarakterler"))
        {
            isInsideArea = false;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Altkarakterler") && isInsideArea)
        {
            float kuvvet = (gameObject.CompareTag("Sol_pervane")) ? solPervaneKuvvet : sagPervaneKuvvet;
            Vector3 force = new Vector3(0, 0, kuvvet);

            other.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}