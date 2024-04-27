using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diller : MonoBehaviour
{
    public enum DilSecenekleri { Türkçe, İngilizce }
    public DilSecenekleri dilSecenekleriAyari;

    public enum Kelimeler { Merhaba, Hoşçakal, Naber }
    public Kelimeler kelimeAyarlari;

    string[,] kelimeler = new string[2, 3] {
        {
            "Merhaba",
            "Hoşçakal",
            "Naber?"
        },

        {
            "Hello",
            "Good By",
            "How are you?"
        }
    };

    void Update()
    {
        GetComponent<Text>().text = kelimeler[(int)dilSecenekleriAyari, (int)kelimeAyarlari];
    }
}
