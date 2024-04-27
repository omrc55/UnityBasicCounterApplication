using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EkranGoruntusu : MonoBehaviour
{

    public Camera kamera;
    public int genislik;
    public int yukseklik;
    bool goruntuCek;
    static EkranGoruntusu ornek;
    public string kayitYeri;


    private void Awake()
    {
        ornek = this;
    }


    private void OnPostRender()
    {
        if (goruntuCek)
        {
            goruntuCek = false;
            RenderTexture resimOlustur = kamera.targetTexture;

            Texture2D resimSorgu = new Texture2D(resimOlustur.width, resimOlustur.height, TextureFormat.ARGB32, false);
            Rect rekt = new Rect(0, 0, resimOlustur.width, resimOlustur.height);
            resimSorgu.ReadPixels(rekt, 0, 0);

            byte[] byteArray = resimSorgu.EncodeToPNG();
            System.IO.File.WriteAllBytes(kayitYeri + "/goruntu" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".png", byteArray);
            Debug.Log("Ekran görüntüsü alındı.");

            RenderTexture.ReleaseTemporary(resimOlustur);
            kamera.targetTexture = null;
        }
    }


    public void EkranGoruntusuCek(int genislik, int yukseklik)
    {
        kamera.targetTexture = RenderTexture.GetTemporary(genislik, yukseklik, 16);
        goruntuCek = true;
    }


    static void EkranGoruntusuAl(int genislik, int yukseklik)
    {
        ornek.EkranGoruntusuCek(genislik, yukseklik);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            EkranGoruntusuAl(genislik, yukseklik);
        }
    }
}
