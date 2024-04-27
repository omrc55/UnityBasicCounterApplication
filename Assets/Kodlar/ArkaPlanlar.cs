using UnityEngine;

[System.Serializable]
public class ArkaPlanlar
{
    public Sprite arkaPlan;
    public Color32 renk;

    public ArkaPlanlar(Sprite yeniArkaPlan, Color32 yeniRenk)
    {
        arkaPlan = yeniArkaPlan;
        renk = yeniRenk;
    }
}
