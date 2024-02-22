using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Dusman : MonoBehaviour
{

    public GameObject Saldiri_Hedefi;
    public NavMeshAgent _NavMash;
    public Animator _Animator;
    bool Saldiri_Basladimi;
    public GameManager _GameManager;
    
    public void AnimasyonTetikle()
    {
        _Animator.SetBool("Saldir", true);
        Saldiri_Basladimi = true;     
    }
    void LateUpdate()
    {
        if (Saldiri_Basladimi)
        {         
            _NavMash.SetDestination(Saldiri_Hedefi.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Altkarakterler"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 50f, transform.position.z);
            _GameManager.YokOlmaEfektiOlustur(yeniPoz,false,true);
            gameObject.SetActive(false);
        }
    }
}
