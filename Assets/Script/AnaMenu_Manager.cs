using Depo;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnaMenu_Manager : MonoBehaviour
{

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYönetimi _VeriYonetimi = new VeriYönetimi();
    public GameObject CikisPaneli;
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();

    [Header("---------GENEL VERÝLERÝ")]
    public List<DilVerileriAnaObje> _Varsayýlan_DilVerileri = new List<DilVerileriAnaObje>();
    public AudioSource ButonSes;
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    public List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TMP_Text[] TextObjeleri;

    [Header("---------LOADING VERÝLERÝ")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    void Start()
    {
        _BellekYonetim.KontrolEtveTanimla();
        _VeriYonetimi.IlkKurulumDosyasýOlusturma(_ItemBilgileri, _Varsayýlan_DilVerileri);
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");     

        _VeriYonetimi.DilLoad();
        _DilOkunanVeriler= _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[0]);
        DilTercihiYonetimi();
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
    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }
    public void Oyna()
    {
        ButonSes.Play();
        StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_i("SonLevel")));
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

    public void Cikis()
    {

    }
    public void CikisButonislem(string durum)
    {
        ButonSes.Play();
        if (durum == "Cikis")
            CikisPaneli.SetActive(true);
        else if (durum == "Evet")
            Application.Quit();
        else
            CikisPaneli.SetActive(false);
    }
}
