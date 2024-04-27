using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : MonoBehaviour, IStoreListener
{
    IStoreController controller;
    public string[] product;
    public GameObject reklamKaldirBtn;
    public Reklamlar reklamlar;

    void Start()
    {
        //PlayerPrefs.DeleteKey("ReklamKaldir");

        IAPStart();

        if (PlayerPrefs.GetInt("ReklamKaldir") == 1)
        {
            reklamKaldirBtn.SetActive(false);
        }
    }


    void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        foreach (string item in product)
        {
            builder.AddProduct(item, ProductType.NonConsumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Error " + error.ToString());
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (string.Equals(e.purchasedProduct.definition.id, product[0], StringComparison.Ordinal))
        {
            ReklamKaldir();
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;
        }
    }


    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Error while buying " + p.ToString());
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }


    void ReklamKaldir()
    {
        PlayerPrefs.SetInt("ReklamKaldir", 1);
        reklamKaldirBtn.SetActive(false);
        reklamlar.BannerSorgu();
        reklamlar.GecisSorgu();
    }


    public void IAPButton(string id)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("İnternet yok");
        }
        else
        {
            Product proc = controller.products.WithID(id);

            if (proc != null && proc.availableToPurchase)
            {
                Debug.Log("Buying");
                controller.InitiatePurchase(proc);
            }
            else
            {
                Debug.Log("Not");
            }
        }
    }
}
