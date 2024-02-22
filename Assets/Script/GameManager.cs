using Depo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int AnlikKaraterSayisi = 1;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfekti;

    [Header("LEVEL VERÝLERÝ")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;

    [Header("-----------ÖZELLEÞTÝRME")]
    public GameObject[] Sapkalar;
    public GameObject[] Silahlar;
    public GameObject[] Kalkanlar;

    Matamatiksel_Islemler _Matamatiksel_Islemler = new Matamatiksel_Islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYönetimi _VeriYonetimi = new VeriYönetimi();

    Scene _Scene;
    [Header("---------GENEL VERÝLERÝ")]
    public AudioSource[] Sesler;
    public GameObject[] islemPanelleri;
    public Slider OyunSesiAyar;
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    public List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TMP_Text[] TextObjeleri;

    [Header("---------LOADING VERÝLERÝ")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;

    private void Awake()
    {
        Sesler[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        OyunSesiAyar.value = _BellekYonetim.VeriOku_f("OyunSes");
        Sesler[1].volume = _BellekYonetim.VeriOku_f("MenuFx");
        Destroy(GameObject.FindWithTag("MenuMuzik"));
        ItemleriKontrolEt();
    }
    private void Start()
    {
        DusmanlariOlustur();
        _Scene = SceneManager.GetActiveScene();

        _VeriYonetimi.DilLoad();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[5]);
        DilTercihiYonetimi();
        AnlikKaraterSayisi = 1;
       
    }
    void DilTercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerieri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerieri_EN[i].Metin;
            }
        }
    }
    public void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
    }
    public void SavasDurumu()
    {
        if (SonaGeldikmi)
        {
            if (AnlikKaraterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKaraterSayisi < KacDusmanOlsun || AnlikKaraterSayisi == KacDusmanOlsun)
                {
                    islemPanelleri[3].SetActive(true);
                }
                else
                {
                    if (AnlikKaraterSayisi > 5)
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 300);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    else
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 100);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    islemPanelleri[2].SetActive(true);
                }
            }
        }
    }
    public void AdamYonetim(string islemturu, int GelenSayi, Transform Pozisyon)
    {
        switch (islemturu)
        {
            case "Carpma":
                _Matamatiksel_Islemler.Carpma(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Toplama":
                _Matamatiksel_Islemler.Toplama(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Cikartma":
                _Matamatiksel_Islemler.Cikartma(GelenSayi, Karakterler, YokOlmaEfektleri, AdamLekesiEfekti);
                break;
            case "Bolme":
                _Matamatiksel_Islemler.Bolme(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }
    }
    public void YokOlmaEfektiOlustur(Vector3 Pozisyon, bool Balyoz = false, bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                Vector3 yeniPoz = new Vector3(Pozisyon.x, 30f, Pozisyon.z);
                item.SetActive(true);
                item.transform.position = yeniPoz;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();

                if (!Durum)
                    AnlikKaraterSayisi--;
                else
                    KacDusmanOlsun--;
                break;
            }
        }
        if (Balyoz)
        {
            Vector3 yeniPoz = new Vector3(Pozisyon.x, 26.65f, Pozisyon.z);
            foreach (var item in AdamLekesiEfekti)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPoz;
                    break;
                }
            }
        }

        if (!OyunBittimi)
            SavasDurumu();
    }
    public void ItemleriKontrolEt()
    {
        if (_BellekYonetim.VeriOku_i("AktifSapka") != -1)
            Sapkalar[_BellekYonetim.VeriOku_i("AktifSapka")].SetActive(true);

        if (_BellekYonetim.VeriOku_i("AktifSilah") != -1)
            Silahlar[_BellekYonetim.VeriOku_i("AktifSilah")].SetActive(true);

        if (_BellekYonetim.VeriOku_i("AktifKalkan") != -1)
            Kalkanlar[_BellekYonetim.VeriOku_i("AktifKalkan")].SetActive(true);
    }
    public void CikisButonislem(string durum)
    {
        Sesler[1].Play();
        Time.timeScale = 0;
        if (durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (durum == "tekrar")
        {
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1;
        }
        else if (durum == "Anasayfa")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void Ayarlar(string durum)
    {
        if (durum == "ayarla")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SesiAyarla()
    {
        _BellekYonetim.VeriKaydet_float("OyunSes", OyunSesiAyar.value);
        Sesler[0].volume = OyunSesiAyar.value;
    }

    public void SonrakiLevel()
    {

        if (_Scene.buildIndex < 5)
        {
            StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
        }
        else
        {

        }
    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        YuklemeEkrani.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            YuklemeSlider.value = progress;
            yield return null;
        }
    }
}
