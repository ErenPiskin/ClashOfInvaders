using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class karakter : MonoBehaviour
{
   
    public GameManager _GameManager;
    public Kamera _Kamera;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;
    public Slider _Slider;
    public GameObject GecisNoktasý;

    private void FixedUpdate()
    {
        if (!SonaGeldikmi)
            transform.Translate(Vector3.forward * 13f * Time.deltaTime);
    }

    private void Start()
    {
        float Fark = Vector3.Distance(transform.position, GecisNoktasý.transform.position);
        _Slider.maxValue = Fark;
    }
    void Update()
    {
        if (Time.timeScale != 0)
        {

            if (SonaGeldikmi)
            {
                transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, .004f);
                if (_Slider.value != 0)
                    _Slider.value -= .05f;
            }
            else
            {
                float Fark = Vector3.Distance(transform.position, GecisNoktasý.transform.position);
                _Slider.value = Fark;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                            transform.position.y, transform.position.z + .7f), .14f);
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x
                            , transform.position.y, transform.position.z - .7f), .14f);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikartma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _GameManager.AdamYonetim(other.tag, sayi, other.transform);
        }
        else if (other.CompareTag("sontetikleyici"))
        {
            _Kamera.SonaGeldikmi = true;
            _GameManager.DusmanlariTetikle();
            SonaGeldikmi = true;
        }
        else if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);           
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ODirek"))
        {
            if (transform.position.z > 60.80)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
            else if (transform.position.z <= 60.80)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
        }
        if (collision.gameObject.CompareTag("SaDirek") || collision.gameObject.CompareTag("Sag_igneK"))
        {
            if (transform.position.z > 51)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);

        }
        if (collision.gameObject.CompareTag("SDirek") || collision.gameObject.CompareTag("Sol_igneK"))
        {
            if (transform.position.z > 67)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);
            else if (transform.position.z > 63)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3f);


        }
        if (collision.gameObject.CompareTag("Sag_Pervane_igne") || collision.gameObject.CompareTag("Sol_Pervane_igne"))
        {
            if (transform.position.z < 55)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f);
            else if (transform.position.z > 66)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f);
        }
    }



}
