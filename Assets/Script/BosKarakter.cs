using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{

    public SkinnedMeshRenderer _Renderer;
    public Material AtanacakOlanMateryal;
    public NavMeshAgent _Navmash;
    public Animator _Animator;
    public GameObject Target;
    bool Temas_Var;
    public GameManager _GameManager;
    private void LateUpdate()
    {
        if (Temas_Var)       
            _Navmash.SetDestination(Target.transform.position);             
    }
    Vector3 PozisyonVer()
    {

        return new Vector3(transform.position.x, 60f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Altkarakterler") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                MatarialDeðistirVeAnimasyonTetikle();
                GetComponent<AudioSource>().Play();
                Temas_Var = true;
            }         
        }
        else if (other.CompareTag("Sag_igneK") || other.CompareTag("Sol_igneK"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Testere"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Sag_Pervane_igne") || other.CompareTag("Sol_Pervane_igne"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

       else if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }
       else if (other.CompareTag("Dusman"))
        {       
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
    }

    void MatarialDeðistirVeAnimasyonTetikle()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = AtanacakOlanMateryal;
        _Renderer.materials = mats;
        _Animator.SetBool("Saldir", true);

        gameObject.tag = "Altkarakterler";
        GameManager.AnlikKaraterSayisi++;
        
    }

    
}
