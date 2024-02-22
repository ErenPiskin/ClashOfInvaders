using Depo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    [Header("---------BUTONLAR")]
    public Button[] Butonlar;
    public int Level;
    public Sprite KilitButon;

    BellekYonetim _BellekYonetim = new BellekYonetim();

    [Header("---------GENEL VERÝLERÝ")]
    public AudioSource ButonSes;
    VeriYönetimi _VeriYonetimi = new VeriYönetimi();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    public List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TMP_Text[] TextObjeleri;

    [Header("---------LOADING VERÝLERÝ")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    void Start()
    {
        _VeriYonetimi.DilLoad();
        _DilOkunanVeriler = _VeriYonetimi.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[2]);
        DilTercihiYonetimi();

        //_BellekYonetim.VeriKaydet_int("SonLevel", Level);
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
        int MevcutLevel = _BellekYonetim.VeriOku_i("SonLevel") - 4;
        int Index = 1;

        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (Index <= MevcutLevel)
            {
                Butonlar[i].GetComponentInChildren<Text>().text = Index.ToString();
                int SahneIndex = Index + 4;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(SahneIndex); });
            }
            else
            {
                Butonlar[i].GetComponent<Image>().sprite = KilitButon;
                Butonlar[i].enabled = false;
            }
            Index++;
        }
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

        StartCoroutine(LoadAsync(Index));
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

    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
}
