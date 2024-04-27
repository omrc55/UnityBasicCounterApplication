using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Yonetim : MonoBehaviour
{
    /*
     * 
     * PlayerPrefs: Aciklama, Sayac, Hedef, Zaman, ArkaPlan
     * 
     */

    public Camera kamera;

    public Reklamlar reklamlar;

    public List<ArkaPlanlar> arkaPlanlar = new List<ArkaPlanlar>();

    public GameObject sayacBtn;
    public GameObject hedef;
    public GameObject sayac;
    public GameObject sayacPanel;
    public GameObject duzenleBtn;
    public GameObject sifirlaBtn;
    public GameObject eksiltBtn;
    public GameObject sayfaBtn;
    public GameObject sayfaPanel;
    public GameObject sagaGitBtn;
    public GameObject solaGitBtn;
    public GameObject onayBtn;
    public GameObject arkaPlanBtn;
    public GameObject reklamiKaldirBtn;
    public GameObject aciklama;
    public GameObject devamEt;
    public GameObject acik;
    public GameObject kapali;
    public GameObject arkaPlan;
    public GameObject zamanlayici;
    public GameObject sayfaSayisiTabela;
    public GameObject sayfaSayisiYazi;

    bool sayacPanelAcik = false;
    bool sayacBtnBasildi = false;

    int sayi;
    int hedefSayi;
    int arkaPlanSayi;
    int kayit;

    float zaman;
    int zamanSaat;
    int zamanDakika;
    int zamanSaniye;
    int saattenKalan;
    int dakikadanKalan;

    string saniyeYazi;
    string dakikaYazi;
    string saatYazi;


    void Start()
    {
        //PlayerPrefs.SetFloat("Zaman", 30000);
        //PlayerPrefs.SetInt("ArkaPlan", 0);
        //PlayerPrefs.SetInt("Kayit", 12);
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("Hedef"))
        {
            PlayerPrefs.SetInt("Hedef0", PlayerPrefs.GetInt("Hedef"));
            PlayerPrefs.SetString("Aciklama0", PlayerPrefs.GetString("Aciklama"));
            PlayerPrefs.SetInt("Sayac0", PlayerPrefs.GetInt("Sayac"));
            PlayerPrefs.SetFloat("Zaman0", PlayerPrefs.GetFloat("Zaman"));

            PlayerPrefs.DeleteKey("Hedef");
            PlayerPrefs.DeleteKey("Aciklama");
            PlayerPrefs.DeleteKey("Sayac");
            PlayerPrefs.DeleteKey("Zaman");
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        arkaPlanSayi = PlayerPrefs.GetInt("ArkaPlan");

        arkaPlan.GetComponent<Image>().sprite = arkaPlanlar[arkaPlanSayi].arkaPlan;
        arkaPlan.GetComponent<Image>().color = arkaPlanlar[arkaPlanSayi].renk;

        Yenile();
    }


    public void Yenile()
    {
        kayit = PlayerPrefs.GetInt("Kayit");

        sayfaSayisiYazi.GetComponent<Text>().text = kayit + 1 + "/20";
        aciklama.GetComponent<InputField>().text = PlayerPrefs.GetString("Aciklama" + kayit);

        if (PlayerPrefs.GetInt("Hedef" + kayit) > 0)
        {
            hedef.GetComponent<InputField>().text = PlayerPrefs.GetInt("Hedef" + kayit).ToString();
            hedefSayi = PlayerPrefs.GetInt("Hedef" + kayit);
        }
        else
        {
            hedef.GetComponent<InputField>().text = "";
            hedefSayi = 0;
        }

        if (PlayerPrefs.GetInt("Sayac" + kayit) > 0)
        {
            sayac.GetComponent<Text>().text = PlayerPrefs.GetInt("Sayac" + kayit).ToString();

            ZamanIsleyici();

            sifirlaBtn.SetActive(true);
            eksiltBtn.SetActive(true);
            devamEt.SetActive(true);
            zamanlayici.SetActive(true);
        }
        else
        {
            sayac.GetComponent<Text>().text = "START";

            sifirlaBtn.SetActive(false);
            eksiltBtn.SetActive(false);
            devamEt.SetActive(false);
            zamanlayici.SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (sayac.GetComponent<Text>().text != "START")
        {
            sayi = int.Parse(sayac.GetComponent<Text>().text);
        }

        if (sayacBtnBasildi)
        {
            sayi++;
            PlayerPrefs.SetInt("Sayac" + kayit, sayi);
            sayac.GetComponent<Text>().text = sayi.ToString();
            sayacBtnBasildi = false;
        }

        if (sayacBtn.activeSelf)
        {
            zaman = PlayerPrefs.GetFloat("Zaman" + kayit);

            if (hedefSayi == 0 || sayi < hedefSayi)
            {
                zaman = zaman + Time.deltaTime;
                PlayerPrefs.SetFloat("Zaman" + kayit, zaman);

                ZamanIsleyici();
            }

            ZamanIsleyici();
        }

        if (sayacPanelAcik)
        {
            sayacPanel.transform.position = Vector3.Lerp(sayacPanel.transform.position, acik.transform.position, Time.deltaTime * 10);
        }
        else
        {
            sayacPanel.transform.position = Vector3.Lerp(sayacPanel.transform.position, kapali.transform.position, Time.deltaTime * 10);
        }
    }


    public void SayacBtnAc()
    {
        if (sayac.GetComponent<Text>().text == "START")
        {
            sayac.GetComponent<Text>().text = "0";
        }
        sayacBtn.SetActive(true);
        duzenleBtn.SetActive(true);
        arkaPlanBtn.SetActive(false);
        reklamiKaldirBtn.SetActive(false);
        zamanlayici.SetActive(true);
        sifirlaBtn.SetActive(false);
        eksiltBtn.SetActive(false);
        sayfaBtn.SetActive(false);
        devamEt.SetActive(false);
        hedef.SetActive(true);
        sayfaSayisiTabela.SetActive(false);
        sagaGitBtn.SetActive(false);
        solaGitBtn.SetActive(false);
        onayBtn.SetActive(false);
    }


    public void SayacBtnCalistir()
    {
        if (hedefSayi != 0 && sayi >= hedefSayi)
        {
            Vibration.Vibrate(1000);
        }
        else
        {
            sayacBtnBasildi = true;
            Vibration.Vibrate(75);
        }
    }


    public void SayacBtnKapat()
    {
        if (reklamlar.gecis.IsLoaded())
        {
            reklamlar.gecis.Show();
        }
        reklamlar.GecisSorgu();

        devamEt.SetActive(true);
        sifirlaBtn.SetActive(true);
        eksiltBtn.SetActive(true);
        sayfaBtn.SetActive(true);
        sayacBtn.SetActive(false);
        duzenleBtn.SetActive(false);
        arkaPlanBtn.SetActive(true);
        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            reklamiKaldirBtn.SetActive(true);
        }
    }


    public void Sifirla()
    {
        if (reklamlar.gecis.IsLoaded())
        {
            reklamlar.gecis.Show();
        }
        reklamlar.GecisSorgu();

        devamEt.SetActive(false);
        sayac.GetComponent<Text>().text = "START";
        sayi = 0;
        PlayerPrefs.SetInt("Sayac" + kayit, 0);
        zaman = 0.0f;
        PlayerPrefs.SetFloat("Zaman" + kayit, 0.0f);
        zamanlayici.SetActive(false);
        sifirlaBtn.SetActive(false);
        eksiltBtn.SetActive(false);
    }


    public void MetinGirisi()
    {
        PlayerPrefs.SetString("Aciklama" + kayit, aciklama.GetComponent<InputField>().text);
    }


    public void HedefGirisi()
    {
        if (hedef.GetComponent<InputField>().text == "")
        {
            PlayerPrefs.SetInt("Hedef" + kayit, 0);
            hedefSayi = 0;
        }
        else
        {
            PlayerPrefs.SetInt("Hedef" + kayit, int.Parse(hedef.GetComponent<InputField>().text));
            hedefSayi = PlayerPrefs.GetInt("Hedef" + kayit);
        }
    }


    public void Eksilt()
    {
        if (reklamlar.gecis.IsLoaded())
        {
            reklamlar.gecis.Show();
        }
        reklamlar.GecisSorgu();

        if (sayi > 0)
        {
            sayi--;
            PlayerPrefs.SetInt("Sayac" + kayit, sayi);
            sayac.GetComponent<Text>().text = sayi.ToString();
        }
    }


    public void AciklamaTiklandi()
    {
        sayacPanelAcik = true;
    }


    public void AciklamaKapatildi()
    {
        sayacPanelAcik = false;
    }


    void ZamanIsleyici()
    {
        zamanSaat = Mathf.RoundToInt(PlayerPrefs.GetFloat("Zaman" + kayit)) / 3600;
        saattenKalan = Mathf.RoundToInt(PlayerPrefs.GetFloat("Zaman" + kayit)) - (3600 * zamanSaat);
        zamanDakika = saattenKalan / 60;
        dakikadanKalan = saattenKalan - (60 * zamanDakika);
        zamanSaniye = dakikadanKalan;

        if (zamanSaat > 9)
        {
            saatYazi = zamanSaat + ":";
        }
        else if (zamanSaat > 0)
        {
            saatYazi = "0" + zamanSaat + ":";
        }
        else
        {
            saatYazi = "";
        }

        if (zamanDakika > 9)
        {
            dakikaYazi = zamanDakika + ":";
        }
        else if (zamanDakika > 0 || zamanSaat > 0)
        {
            dakikaYazi = "0" + zamanDakika + ":";
        }
        else
        {
            dakikaYazi = "";
        }

        if (zamanSaniye < 10)
        {
            saniyeYazi = "0" + zamanSaniye;
        }
        else
        {
            saniyeYazi = zamanSaniye.ToString();
        }

        zamanlayici.GetComponent<Text>().text = saatYazi + dakikaYazi + saniyeYazi;
    }


    public void ArkaPlanDegistir()
    {
        if (arkaPlanSayi < arkaPlanlar.Count - 1)
        {
            arkaPlanSayi++;
        }
        else
        {
            arkaPlanSayi = 0;
        }

        PlayerPrefs.SetInt("ArkaPlan", arkaPlanSayi);

        arkaPlan.GetComponent<Image>().sprite = arkaPlanlar[arkaPlanSayi].arkaPlan;
        arkaPlan.GetComponent<Image>().color = arkaPlanlar[arkaPlanSayi].renk;

        Debug.Log(arkaPlanSayi);
    }


    public void SayfaBtnBas()
    {
        sifirlaBtn.SetActive(false);
        eksiltBtn.SetActive(false);
        arkaPlanBtn.SetActive(false);
        reklamiKaldirBtn.SetActive(false);
        sayfaBtn.SetActive(false);
        hedef.SetActive(false);
        sayfaPanel.SetActive(true);
        sayfaSayisiTabela.SetActive(true);

        if (kayit > 0)
        {
            solaGitBtn.SetActive(true);
        }

        if (kayit < 19)
        {
            sagaGitBtn.SetActive(true);
        }

        onayBtn.SetActive(true);
    }


    public void SayfaBtnOnay()
    {
        if (reklamlar.gecis.IsLoaded())
        {
            reklamlar.gecis.Show();
        }
        reklamlar.GecisSorgu();

        arkaPlanBtn.SetActive(true);
        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            reklamiKaldirBtn.SetActive(true);
        }
        sayfaBtn.SetActive(true);
        hedef.SetActive(true);
        sayfaPanel.SetActive(false);
        sayfaSayisiTabela.SetActive(false);
        solaGitBtn.SetActive(false);
        sagaGitBtn.SetActive(false);
        onayBtn.SetActive(false);

        Yenile();
    }


    public void SayfaDegistir(bool buSagBtn)
    {
        if (buSagBtn)
        {
            kayit++;
        }
        else
        {
            kayit--;
        }

        if (kayit > 0)
        {
            solaGitBtn.SetActive(true);
        }
        else
        {
            solaGitBtn.SetActive(false);
        }

        if (kayit < 19)
        {
            sagaGitBtn.SetActive(true);
        }
        else
        {
            sagaGitBtn.SetActive(false);
        }

        PlayerPrefs.SetInt("Kayit", kayit);
        Yenile();

        sifirlaBtn.SetActive(false);
        eksiltBtn.SetActive(false);
        arkaPlanBtn.SetActive(false);
        reklamiKaldirBtn.SetActive(false);
        sayfaBtn.SetActive(false);
        hedef.SetActive(false);
    }
}
