using Depo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class Ozellestirme_Maanger : MonoBehaviour
{
    public Text PuanText;
    
    public GameObject[] islemPanelleri;
    public GameObject islemCanvasi;
    public GameObject[] GenelPaneller;
    public Button[] islemButonlari;
    int aktifIslemPaneliIndex;
    private int SapkaIndex = -1;
    private int SilahIndex = -1;
    private int KalkanIndex = -1;


    [Header("-----------SAPKALAR")]
    public GameObject[] Sapkalar;
    public Button[] SapkaButonlari;
    public TMP_Text SapkaText;
    [Header("-----------SILAH")]
    public GameObject[] Silahlar;
    public Button[] SilahButonlari;
    public TMP_Text SilahText;
    [Header("-----------KALKANLAR")]
    public GameObject[] Kalkanlar;
    public Button[] KalkanButonlari;
    public TMP_Text KalkanText;


    BellekYonetim _BellekYönetimi = new BellekYonetim();
    VeriYönetimi _VeriYönetimi = new VeriYönetimi();
    [Header("-----------GENEL VERÝLER")]
    public AudioSource[] Sesler;
    public Animator Kaydedildi_Animator;
    public List<ItemBilgileri> _Itembilgileri = new List<ItemBilgileri>();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TMP_Text[] TextObjeleri;

    string SatinAlmaText;
    string ItemText;
    


    void Start()
    {
        PuanText.text = _BellekYönetimi.VeriOku_i("Puan").ToString();

        _VeriYönetimi.Load();
        _Itembilgileri = _VeriYönetimi.ListeyiAktar();

       //_BellekYönetimi.VeriKaydet_string("Dil", "TR");

        DurumuKontrolEt(0, true);
        DurumuKontrolEt(1, true);
        DurumuKontrolEt(2, true);

        foreach (var item in Sesler)
        {
            item.volume = _BellekYönetimi.VeriOku_f("MenuFx");
        }

        _VeriYönetimi.DilLoad();
        _DilOkunanVeriler = _VeriYönetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[1]);
        DilTercihiYonetimi();

    }
    void DilTercihiYonetimi()
    {
        if (_BellekYönetimi.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerieri_TR[i].Metin;
            }
            SatinAlmaText= _DilVerileriAnaObje[0]._DilVerieri_TR[4].Metin;
            ItemText = _DilVerileriAnaObje[0]._DilVerieri_TR[3].Metin;

           
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerieri_EN[i].Metin;
            }
            SatinAlmaText = _DilVerileriAnaObje[0]._DilVerieri_EN[4].Metin;
            ItemText = _DilVerileriAnaObje[0]._DilVerieri_EN[3].Metin;
           
        }
    }

    void DurumuKontrolEt(int Bolum,bool islem=false)
    {
        if (Bolum == 0)
        {           
            if (_BellekYönetimi.VeriOku_i("AktifSapka") == -1)
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }
            TextObjeleri[4].text = SatinAlmaText;
            islemButonlari[0].interactable = false;
            islemButonlari[1].interactable = false;

                if (!islem)
                {
                    SapkaIndex = -1;
                    SapkaText.text = ItemText;
                }               
            }
            else
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);

                }
                SapkaIndex = _BellekYönetimi.VeriOku_i("AktifSapka");
                Sapkalar[SapkaIndex].SetActive(true); 

                SapkaText.text = _Itembilgileri[SapkaIndex].Item_Ad;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
            
        }       
        else if (Bolum == 1)
        {            
            if (_BellekYönetimi.VeriOku_i("AktifSilah") == -1)
            {
                foreach (var item in Silahlar)
                {
                    item.SetActive(false);
                }
            TextObjeleri[4].text = SatinAlmaText;
            islemButonlari[0].interactable = false;
            islemButonlari[1].interactable = false;

                if (!islem)
                {
                    SilahIndex = -1;
                    SilahText.text = ItemText;
                }                
            }
            else
            {
                foreach (var item in Silahlar)
                {
                    item.SetActive(false);

                }
                SilahIndex = _BellekYönetimi.VeriOku_i("AktifSilah");
                Silahlar[SilahIndex].SetActive(true);

                SilahText.text = _Itembilgileri[SilahIndex+9].Item_Ad;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
        }

        else if(Bolum ==2)
        {
            
            if (_BellekYönetimi.VeriOku_i("AktifKalkan") == -1)
            {
                foreach (var item in Kalkanlar)
                {
                    item.SetActive(false);
                }
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = false;

                if (!islem)
                {
                    KalkanIndex = -1;
                    KalkanText.text = ItemText;
                }
               
            }
            else
            {
                foreach (var item in Kalkanlar)
                {
                    item.SetActive(false);

                }
                KalkanIndex = _BellekYönetimi.VeriOku_i("AktifKalkan");
                Kalkanlar[KalkanIndex].SetActive(true);

                KalkanText.text = _Itembilgileri[KalkanIndex+18].Item_Ad;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
            
        }


    }

  

    public void SatinAl()
    {
        Sesler[1].Play();
        if (aktifIslemPaneliIndex !=-1)
        {
            switch (aktifIslemPaneliIndex)
            {
                case 0:
                    _Itembilgileri[SapkaIndex].SatinAlmaDurumu = true;
                    _BellekYönetimi.VeriKaydet_int("Puan", _BellekYönetimi.VeriOku_i("Puan") - _Itembilgileri[SapkaIndex].Puan);
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                    PuanText.text = _BellekYönetimi.VeriOku_i("Puan").ToString();
                    break;
                case 1:
                    _Itembilgileri[SilahIndex + 9].SatinAlmaDurumu = true;
                    _BellekYönetimi.VeriKaydet_int("Puan", _BellekYönetimi.VeriOku_i("Puan") - _Itembilgileri[SilahIndex + 9].Puan);
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                    PuanText.text = _BellekYönetimi.VeriOku_i("Puan").ToString();
                    break;
                case 2:
                    _Itembilgileri[KalkanIndex + 18].SatinAlmaDurumu = true;
                    _BellekYönetimi.VeriKaydet_int("Puan", _BellekYönetimi.VeriOku_i("Puan") - _Itembilgileri[KalkanIndex + 18].Puan);
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                    PuanText.text = _BellekYönetimi.VeriOku_i("Puan").ToString();
                    break;
            }
        }
       
    }

    public void Kaydet()
    {
        Sesler[2].Play();
        if (aktifIslemPaneliIndex != -1)
        {
            switch (aktifIslemPaneliIndex)
            {
                case 0:
                    _BellekYönetimi.VeriKaydet_int("AktifSapka", SapkaIndex);
                    islemButonlari[1].interactable = false;
                    if (!Kaydedildi_Animator.GetBool("ok"))
                        Kaydedildi_Animator.SetBool("ok", true);             
                    break;
                case 1:
                    _BellekYönetimi.VeriKaydet_int("AktifSilah", SilahIndex);
                    islemButonlari[1].interactable = false;
                    if (!Kaydedildi_Animator.GetBool("ok"))
                        Kaydedildi_Animator.SetBool("ok", true);
                    break;
                case 2:
                    _BellekYönetimi.VeriKaydet_int("AktifKalkan", KalkanIndex);
                    islemButonlari[1].interactable = false;
                    if (!Kaydedildi_Animator.GetBool("ok"))
                        Kaydedildi_Animator.SetBool("ok", true);
                    break;
            }
        }
    }



    public void Sapka_YonButonlari(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri")
        {
            if (SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text= _Itembilgileri[SapkaIndex].Item_Ad;

                if (!_Itembilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[SapkaIndex].Puan + " - " + SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _Itembilgileri[SapkaIndex].Item_Ad;

                if (!_Itembilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[SapkaIndex].Puan + " - " + SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            if (SapkaIndex==Sapkalar.Length-1)         
                SapkaButonlari[1].interactable = false;
            else      
                SapkaButonlari[1].interactable = true;
                
                        
            if (SapkaIndex !=-1)
            {
                SapkaButonlari[0].interactable = true;

            }




        }
        else
        {
            if (SapkaIndex != -1)
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex--;

                if (SapkaIndex != -1)
                {
                    Sapkalar[SapkaIndex].SetActive(true);
                    SapkaButonlari[0].interactable = true;
                    SapkaText.text = _Itembilgileri[SapkaIndex].Item_Ad;

                    if (!_Itembilgileri[SapkaIndex].SatinAlmaDurumu)
                    {
                        TextObjeleri[4].text = _Itembilgileri[SapkaIndex].Puan + " - " + SatinAlmaText;
                        islemButonlari[1].interactable = false;

                        if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SapkaIndex].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        TextObjeleri[4].text = SatinAlmaText;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SapkaButonlari[0].interactable = false;
                    SapkaText.text = ItemText;
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                }
            }
            else
            {
                SapkaButonlari[0].interactable = false;
                SapkaText.text = ItemText;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;

            }
             if (SapkaIndex != Sapkalar.Length - 1)
                 SapkaButonlari[1].interactable = true;
            }

            
        }
    public void Silah_YonButonlari(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri")
        {
            if (SilahIndex == -1)
            {
                SilahIndex = 0;
                Silahlar[SilahIndex].SetActive(true);
                SilahText.text = _Itembilgileri[SilahIndex + 9].Item_Ad;

                if (!_Itembilgileri[SilahIndex + 9].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[SilahIndex + 9].Puan + " - " + SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SilahIndex + 9].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Silahlar[SilahIndex].SetActive(false);
                SilahIndex++;
                Silahlar[SilahIndex].SetActive(true);
                SilahText.text = _Itembilgileri[SilahIndex + 9].Item_Ad;

                if (!_Itembilgileri[SilahIndex + 9].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[SilahIndex + 9].Puan + " - " + SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SilahIndex + 9].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            if (SilahIndex == Silahlar.Length - 1)
                SilahButonlari[1].interactable = false;
            else
                SilahButonlari[1].interactable = true;


            if (SilahIndex != -1)
            {
                SilahButonlari[0].interactable = true;

            }




        }
        else
        {
            if (SilahIndex != -1)
            {
                Silahlar[SilahIndex].SetActive(false);
                SilahIndex--;

                if (SilahIndex != -1)
                {
                    Silahlar[SilahIndex].SetActive(true);
                    SilahButonlari[0].interactable = true;
                    SilahText.text = _Itembilgileri[SilahIndex + 9].Item_Ad;

                    if (!_Itembilgileri[SilahIndex + 9].SatinAlmaDurumu)
                    {
                        TextObjeleri[4].text = _Itembilgileri[SilahIndex + 9].Puan + " - " + SatinAlmaText;
                        islemButonlari[1].interactable = false;

                        if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[SilahIndex + 9].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        TextObjeleri[4].text = SatinAlmaText;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SilahButonlari[0].interactable = false;
                    SilahText.text = ItemText;
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                }
            }
            else
            {
                SilahButonlari[0].interactable = false;
                SilahText.text = ItemText;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;

            }
            if (SilahIndex != Silahlar.Length - 1)
                SilahButonlari[1].interactable = true;
        }


    }
    public void Kalkan_YonButonlari(string islem)
    {
        Sesler[0].Play();
        if (islem == "ileri")
        {
            if (KalkanIndex == -1)
            {
                KalkanIndex = 0;
                Kalkanlar[KalkanIndex].SetActive(true);
                KalkanText.text = _Itembilgileri[KalkanIndex+18].Item_Ad;

                if (!_Itembilgileri[KalkanIndex + 18].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[KalkanIndex + 18].Puan + " - " + SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[KalkanIndex + 18].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Kalkanlar[KalkanIndex].SetActive(false);
                KalkanIndex++;
                Kalkanlar[KalkanIndex].SetActive(true);
                KalkanText.text = _Itembilgileri[KalkanIndex+18].Item_Ad;

                if (!_Itembilgileri[KalkanIndex + 18].SatinAlmaDurumu)
                {
                    TextObjeleri[4].text = _Itembilgileri[KalkanIndex + 18].Puan + " - "+ SatinAlmaText;
                    islemButonlari[1].interactable = false;

                    if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[KalkanIndex + 18].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }

            if (KalkanIndex == Kalkanlar.Length - 1)
                KalkanButonlari[1].interactable = false;
            else
                KalkanButonlari[1].interactable = true;


            if (KalkanIndex != -1)
            {
                KalkanButonlari[0].interactable = true;

            }




        }
        else
        {
            if (KalkanIndex != -1)
            {
                Kalkanlar[KalkanIndex].SetActive(false);
                KalkanIndex--;

                if (KalkanIndex != -1)
                {
                    Kalkanlar[KalkanIndex].SetActive(true);
                    KalkanButonlari[0].interactable = true;
                    KalkanText.text = _Itembilgileri[KalkanIndex+18].Item_Ad;

                    if (!_Itembilgileri[KalkanIndex + 18].SatinAlmaDurumu)
                    {
                        TextObjeleri[4].text = _Itembilgileri[KalkanIndex + 18].Puan + " - " + SatinAlmaText;
                        islemButonlari[1].interactable = false;

                        if (_BellekYönetimi.VeriOku_i("Puan") < _Itembilgileri[KalkanIndex + 18].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        TextObjeleri[4].text = SatinAlmaText;
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    KalkanButonlari[0].interactable = false;
                    KalkanText.text = ItemText;
                    TextObjeleri[4].text = SatinAlmaText;
                    islemButonlari[0].interactable = false;
                }
            }
            else
            {
                KalkanButonlari[0].interactable = false;
                KalkanText.text = ItemText;
                TextObjeleri[4].text = SatinAlmaText;
                islemButonlari[0].interactable = false;

            }
            if (KalkanIndex != Kalkanlar.Length - 1)
                KalkanButonlari[1].interactable = true;
        }


    }
    public void islemPanaeliCýkart(int Index)
    {
        Sesler[0].Play();
        DurumuKontrolEt(Index);
        GenelPaneller[0].SetActive(true);
        aktifIslemPaneliIndex = Index;
        islemPanelleri[Index].SetActive(true);
        GenelPaneller[1].SetActive(true);
        islemCanvasi.SetActive(false);
    }

    public void GeriDon()
    {
        Sesler[0].Play();
        GenelPaneller[0].SetActive(false);
        islemCanvasi.SetActive(true);
        GenelPaneller[1].SetActive(false);
        islemPanelleri[aktifIslemPaneliIndex].SetActive(false);
        DurumuKontrolEt(aktifIslemPaneliIndex,true);
        aktifIslemPaneliIndex = -1;
    }

    public void AnaMenuyeDon()
    {
        Sesler[0].Play();
        _VeriYönetimi.Save(_Itembilgileri);
        SceneManager.LoadScene(0);
    }
}