using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ads_Native : MonoBehaviour
{
    private void RequestNativeAd()
    {
            var unitId = AdDisplayer.get_unit_id_native();
            AdLoader adLoader = new AdLoader.Builder(unitId)
                .ForUnifiedNativeAd()
                .Build();
            adLoader.OnUnifiedNativeAdLoaded += HandleUnifiedNativeAdLoaded;
            adLoader.OnAdFailedToLoad += AdLoader_OnAdFailedToLoad;
            var rq = new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("954EEF7C5310E3782526624C14668FCA")
                .Build();
            adLoader.LoadAd(rq);

        
    }




    void init()
    {
        RequestNativeAd();
        gameObject.SetActive(false);
        if (Application.isEditor)
        {
            gameObject.SetActive(true);
            double stars = 3.5;
            check_rate(stars);
        }
    }

    private void Start()
    {
        init();
    }
    void check_rate(double stars)
    {
        if (stars >= 0)
        {
            rateText.text = stars + "";
            rateText.gameObject.SetActive(true);
            starRating.SetActive(true);
            Store.gameObject.SetActive(true);
            //BodyText.gameObject.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                var star = this.starRating.transform.GetChild(i).gameObject;
                star.SetActive(false);
            }
            if (stars > 1) { this.starRating.transform.GetChild(0).gameObject.SetActive(true); }
            if (stars > 2) { this.starRating.transform.GetChild(1).gameObject.SetActive(true); }
            if (stars > 3) { this.starRating.transform.GetChild(2).gameObject.SetActive(true); }
            if (stars > 4 && stars < 5) { this.starRating.transform.GetChild(3).gameObject.SetActive(true); }
            if (stars >= 5) { this.starRating.transform.GetChild(4).gameObject.SetActive(true); }
        }
        else
        {
            rateText.gameObject.SetActive(false);
            starRating.SetActive(false);
            Store.gameObject.SetActive(false);
            //BodyText.gameObject.SetActive(true);
        }
    }
    void set_ad()
    {
        var ad_choise_Texture = nativeAd.GetAdChoicesLogoTexture();
        if (ad_choise_Texture != null)
        {

            AdChoices.texture = ad_choise_Texture;
            if (!nativeAd.RegisterAdChoicesLogoGameObject(AdChoices.gameObject))
            {
                Debug.Log("***************RegisterAdChoicesLogoGameObject failed!");
            }
        }
        else
            AdChoices.gameObject.SetActive(false);
        string headline = nativeAd.GetHeadlineText();
        if (!string.IsNullOrEmpty(headline))
        {
            HeadlineText.text = headline;
            if (!nativeAd.RegisterHeadlineTextGameObject(HeadlineText.gameObject))
            {
                Debug.Log("***************RegisterHeadlineTextGameObject failed!");
            }
        }
        else
        {
            HeadlineText.gameObject.SetActive(false);
        }
        string body = nativeAd.GetBodyText();
        if (!string.IsNullOrEmpty(headline))
        {
            BodyText.text = body;
            if (!nativeAd.RegisterBodyTextGameObject(BodyText.gameObject))
            {
                Debug.Log("***************RegisterBodyTextGameObject failed!");
            }
        }
        else
        {
            BodyText.gameObject.SetActive(false);
        }


        List<Texture2D> imgs = nativeAd.GetImageTextures();
        if (imgs != null && imgs.Count > 0)
        {

            bigImage.GetComponent<RawImage>().texture = imgs[0];
            List<GameObject> imageObjs = new List<GameObject>();
            imageObjs.Add(bigImage.gameObject);
            nativeAd.RegisterImageGameObjects(imageObjs);
        }
        if (con != null)
        {
            if (!nativeAd.RegisterCallToActionGameObject(con))
            {
                Debug.Log("***************RegisterCallToActionGameObject failed!");
            }
        }

        string call_to = nativeAd.GetCallToActionText();

        if (!string.IsNullOrEmpty(call_to))
        {
            //CallToAction.AddComponent<BoxCollider2D>();
            CallToAction.text = call_to;
            //CallToAction.gameObject.AddComponent<BoxCollider>();
            if (!nativeAd.RegisterCallToActionGameObject(CallToAction.gameObject))
            {
                Debug.Log("***************RegisterCallToActionGameObject failed!");
            }
        }
        else
        {
            CallToAction.gameObject.SetActive(false);
        }

        double stars = nativeAd.GetStarRating();

        check_rate(stars);

        Texture2D iconTexture = nativeAd.GetIconTexture();
        if (iconTexture != null)
        {

            IconImage.texture = iconTexture;

            if (!nativeAd.RegisterIconImageGameObject(IconImage.gameObject))
            {
                Debug.Log("***************RegisterIconImageGameObject failed!");
            }
        }
        else
            IconImage.gameObject.SetActive(false);
        string store = nativeAd.GetStore();

        if (!string.IsNullOrEmpty(headline))
        {
            Store.text = store;
            if (!nativeAd.RegisterStoreGameObject(Store.gameObject))
            {
                Debug.Log("***************RegisterStoreGameObject failed!");
            }
        }
        else
        {
            Store.gameObject.SetActive(false);
        }

        string price = nativeAd.GetPrice();

        if (!string.IsNullOrEmpty(headline))
        {

            Price.text = price;
            if (!nativeAd.RegisterPriceGameObject(Price.gameObject))
            {
                Debug.Log("***************RegisterPriceGameObject failed!");
            }
        }
        else
        {
            Price.gameObject.SetActive(false);
        }
        string adv_text = nativeAd.GetAdvertiserText();
        if (!string.IsNullOrEmpty(adv_text))
        {
            AdvertiserText.text = adv_text;
            if (!nativeAd.RegisterAdvertiserTextGameObject(AdvertiserText.gameObject))
            {
                Debug.Log("***************RegisterAdvertiserTextGameObject failed!");
            }
        }
        else
        {
            AdvertiserText.gameObject.SetActive(false);
        }





























    }


    private void AdLoader_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("***********AdLoader_OnAdFailedToLoad : " + e.Message);
    }

    private void HandleUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
    {
        gameObject.SetActive(true);
        //MonoBehaviour.print("Unified native ad loaded.");
        nativeAd = args.nativeAd;
        unifiedNativeAdLoaded = true;
        //set_ad();
        //Debug.Log("***********HandleUnifiedNativeAdLoaded : ");


    }
    private bool unifiedNativeAdLoaded;
    private UnifiedNativeAd nativeAd;
    public RawImage AdChoices, IconImage, bigImage;
    public Text AdvertiserText, BodyText, CallToAction, HeadlineText, Price, Store, rateText;
    public GameObject starRating, con;
    void Update()
    {

        if (unifiedNativeAdLoaded)
        {

            unifiedNativeAdLoaded = false;

            set_ad();


        }
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    //Called when there is an exception
    void LogCallback(string condition, string stackTrace, LogType type)
    {
        
        Debug.Log("******l4m: type: " + type + "-----stackTrace" + stackTrace + "-----condition" + condition);
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
}
