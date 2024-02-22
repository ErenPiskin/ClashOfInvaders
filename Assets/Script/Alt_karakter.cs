using UnityEngine;
using UnityEngine.AI;

public class Alt_karakter : MonoBehaviour
{

    NavMeshAgent _Navmash;
    public GameManager _GameManager;
    public GameObject Target;
    private Rigidbody characterRigidbody;
    void Start()
    {
        _Navmash = GetComponent<NavMeshAgent>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, .001f, transform.position.z);
    }
    private void LateUpdate()
    {
        _Navmash.SetDestination(Target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sag_igneK") || other.CompareTag("Sol_igneK"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Testere"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Sag_Pervane_igne") || other.CompareTag("Sol_Pervane_igne"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            ResetCharacterVelocity();
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);
        }
    }
    private void ResetCharacterVelocity()
    {
        characterRigidbody.velocity = Vector3.zero;
        characterRigidbody.angularVelocity = Vector3.zero;
    }
}
