using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reklamlar : MonoBehaviour
{
    BannerView banner;
    public InterstitialAd gecis;
    string APP_ID = "your-admob-app-id";


    void Start()
    {
        MobileAds.Initialize(APP_ID);
        BannerSorgu();
        GecisSorgu();
    }


    public void BannerSorgu()
    {
        string bannerId = "your-admob-banner-id";
        banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            AdRequest reklamSorgu = new AdRequest.Builder().Build();
            banner.LoadAd(reklamSorgu);
        }
    }


    public void GecisSorgu()
    {
        string adUnitId = "your-admob-interstitial-ad-id";
        gecis = new InterstitialAd(adUnitId);

        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            AdRequest gecisSorgusu = new AdRequest.Builder().Build();
            gecis.LoadAd(gecisSorgusu);
        }
    }
}
