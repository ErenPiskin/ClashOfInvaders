using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Depo
{
    public class Matamatiksel_Islemler
    {
        public void Carpma(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {

            int DonguSayisi = (GameManager.AnlikKaraterSayisi * GelenSayi) - GameManager.AnlikKaraterSayisi;
            int sayi = 0;
            foreach (var item in Karakterler)
            {
                if (sayi < DonguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusturmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.AnlikKaraterSayisi *= GelenSayi;
        }

        public void Toplama(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi2 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi2 < GelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusturmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.AnlikKaraterSayisi += GelenSayi;
        }

        public void Cikartma(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri, List<GameObject> AdamLekesi)
        {
            if (GameManager.AnlikKaraterSayisi < GelenSayi)
            {
                foreach (var item in Karakterler)
                {

                    foreach (var item2 in YokOlmaEfektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 28f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            if (GameManager.AnlikKaraterSayisi > 1)
                                item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKaraterSayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != GelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {

                            foreach (var item2 in YokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {

                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, 28f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = yeniPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    if (GameManager.AnlikKaraterSayisi > 1)
                                        item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.AnlikKaraterSayisi -= GelenSayi;
            }
        }

        public void Bolme(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKaraterSayisi <= GelenSayi)
            {
                foreach (var item in Karakterler)
                {

                    foreach (var item2 in YokOlmaEfektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {

                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 28f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            if (GameManager.AnlikKaraterSayisi >1)
                                item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKaraterSayisi = 1;
            }
            else
            {
                int bolen = GameManager.AnlikKaraterSayisi / GelenSayi;
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {

                            foreach (var item2 in YokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {

                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, 28f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = yeniPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    if (GameManager.AnlikKaraterSayisi > 1)
                                        item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                if (GameManager.AnlikKaraterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKaraterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKaraterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKaraterSayisi /= GelenSayi;
                    GameManager.AnlikKaraterSayisi++;
                }
                else if (GameManager.AnlikKaraterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKaraterSayisi /= GelenSayi;
                    GameManager.AnlikKaraterSayisi+=2;
                }             
            }
        }
    }

    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
        public string VeriOku_s(string Key)
        {
           return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float VeriOku_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }
        public void KontrolEtveTanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSilah", -1);
                PlayerPrefs.SetInt("AktifKalkan", -1);
                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");
            }
        }     
    }
    public class Verilerimiz
    {
        public static List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int Item_Index;
        public string Item_Ad;
        public int Puan;
        public bool SatinAlmaDurumu;
    }
    public class VeriYönetimi
    {
        List<ItemBilgileri> _ItemIcbilgileri;
        public void Save(List<ItemBilgileri> _Itembilgileri)
        {            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _Itembilgileri);
            file.Close();
        }
        
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemIcbilgileri = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();

                

            }
        }

        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemIcbilgileri;
            
        }

        public void IlkKurulumDosyasýOlusturma(List<ItemBilgileri> _Itembilgileri, List<DilVerileriAnaObje> _DilVerileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _Itembilgileri);
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, _DilVerileri);
                file.Close();
            }


        }


        
        List<DilVerileriAnaObje> _DilVerileriIcListe;
        public void DilLoad()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _DilVerileriIcListe = (List<DilVerileriAnaObje>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<DilVerileriAnaObje> DilVerileriListeyiAktar()
        {
            return _DilVerileriIcListe;

        }
    }

    [Serializable]
    public class DilVerileriAnaObje
    {
        
        public List<DilVerileri_TR> _DilVerieri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> _DilVerieri_EN = new List<DilVerileri_TR>();
    }
    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }
}
