using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Depo;
public class AyarlarManager : MonoBehaviour
{

    public AudioSource Butonses;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetimi = new BellekYonetim();


    
    VeriYönetimi _VeriYonetimi = new VeriYönetimi();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    public List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TMP_Text[] TextObjeleri;
    
    [Header("---------DÝL TERCÝHÝ OBJELERÝ")]
    public TMP_Text DilText;
    public Button[] DilButonlari;
#pragma warning disable IDE0052 
    int AktifDilIndex;
#pragma warning restore IDE0052 

    void Start()
    {
        _VeriYonetimi.DilLoad();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[4]);
        DilTercihiYonetimi();
        DilDurumunuKonrtolEt();

        Butonses.volume = _BellekYonetimi.VeriOku_f("MenuFx");

        MenuSes.value = _BellekYonetimi.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYonetimi.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetimi.VeriOku_f("OyunSes");
    }
    void DilTercihiYonetimi()
    {
        if (_BellekYonetimi.VeriOku_s("Dil") == "TR")
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



    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {

            case "menuses":
                _BellekYonetimi.VeriKaydet_float("MenuSes", MenuSes.value);
                break;

            case "menufx":
                _BellekYonetimi.VeriKaydet_float("MenuFx", MenuFx.value);
                break;

            case "oyunses":
                _BellekYonetimi.VeriKaydet_float("OyunSes", OyunSes.value);
                break;

        }
    }
    public void GeriDon()
    {
        Butonses.Play();
        SceneManager.LoadScene(0);
       
    }


    void DilDurumunuKonrtolEt()
    {
        if (_BellekYonetimi.VeriOku_s("Dil") == "TR") 
        {
            AktifDilIndex = 0;
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
        }else
        {
            AktifDilIndex = 1;
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
        }
    }
    public void DilDegistir(string Yon)
    {
        if (Yon=="ileri")
        {
            AktifDilIndex = 1;
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
            DilButonlari[0].interactable = true;
            _BellekYonetimi.VeriKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
        }
        else
        {
            AktifDilIndex = 0;
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
            DilButonlari[1].interactable = true;
            _BellekYonetimi.VeriKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
        }

        Butonses.Play();
    }
}
