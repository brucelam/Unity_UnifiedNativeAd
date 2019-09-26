using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
#if EASY_MOBILE
using EasyMobile;
#endif
public enum GameState
{
    Prepare,
    Playing,
    Paused,
    PreGameOver,
    GameOver
}

public class AdDisplayer : MonoBehaviour
{
    public static string get_unit_id_reward()
    {
#if UNITY_ANDROID
        return app_id_android_rw;
#endif
        return app_id_ios_rw;
    }
    public static string get_unit_id_inter()
    {
#if UNITY_ANDROID
        return app_id_android_inter;
#endif
        return app_id_ios_inter;
    }
    public static string get_unit_id_native()
    {
#if UNITY_ANDROID
        return app_id_android_native;
#endif
        return app_id_ios_native;
    }
    public static string get_unit_id_banner()
    {
#if UNITY_ANDROID
        return app_id_android_banner;
#endif
        return app_id_ios_banner;
    }
    public static string get_app_id()
    {
#if UNITY_ANDROID
        return app_id_android;
#endif
        return app_id_ios;
    }

    public const string app_id_ios = "ca-app-pub-4585119275656037~3658295529";
    public const string app_id_android = "ca-app-pub-4585119275656037~2486921834";

    public const string app_id_ios_banner = "ca-app-pub-4585119275656037/3801943360";
    public const string app_id_ios_inter = "ca-app-pub-4585119275656037/3466723839";
    public const string app_id_ios_native = "ca-app-pub-4585119275656037/7945076071";
    public const string app_id_ios_rw = "ca-app-pub-4585119275656037/3742233611";

    public const string app_id_android_banner = "ca-app-pub-4585119275656037/4472164763";
    public const string app_id_android_inter = "ca-app-pub-4585119275656037/8669186805";
    public const string app_id_android_native = "ca-app-pub-4585119275656037/2306519438";
    public const string app_id_android_rw = "ca-app-pub-4585119275656037/2384853557";


    public static AdDisplayer Instance { get; private set; }
    void Awake()
    {

        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    void init()
    {

        string appId = get_app_id();

        MobileAds.SetiOSAppPauseOnBackground(true);
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

    }
    public void Start()
    {
        init();
    }
}

