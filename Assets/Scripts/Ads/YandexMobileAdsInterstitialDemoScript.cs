/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsInterstitialDemoScript : MonoBehaviour
{
    private String message = "";

    private InterstitialAdLoader interstitialAdLoader;

    private const String CountResetsFromAddPref = "CountResetsFromAdd";

    public void Awake()
    {
        this.interstitialAdLoader = new InterstitialAdLoader();
        // DontDestroyOnLoad(gameObject);
    }

    /*public void OnGUI()
    {
        var fontSize = (int)(0.05f * Math.Min(Screen.width, Screen.height));

        var labelStyle = GUI.skin.GetStyle("label");
        labelStyle.fontSize = fontSize;

        var buttonStyle = GUI.skin.GetStyle("button");
        buttonStyle.fontSize = fontSize;

#if UNITY_EDITOR
        this.message = "Mobile ads SDK is not available in editor. Only Android and iOS environments are supported";
#else
            if (GUILayout.Button("Request Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.RequestInterstitial();
            }

            if (this.interstitial != null)
            {
                if (GUILayout.Button("Show Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.ShowInterstitial();
                }
            }
            if(this.interstitial != null)
            {
                if (GUILayout.Button("Destroy Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.interstitial.Destroy();
                }
            }
#endif

        GUILayout.Label(this.message, labelStyle);
    }*/

    public void RequestInterstitial()
    {
        //Sets COPPA restriction for user age under 13
        YandexAds.SetAgeRestricted(true);

        // Replace demo Unit ID 'demo-interstitial-yandex' with actual Ad Unit ID
        string adUnitId = "demo-interstitial-yandex"; // TODO поменять на реальное значение 


        int countResetsFromAdd = PlayerPrefs.GetInt(CountResetsFromAddPref);
        Debug.Log("Показов после рекламы : " + countResetsFromAdd);
        countResetsFromAdd = countResetsFromAdd + 1;
        PlayerPrefs.SetInt(CountResetsFromAddPref, countResetsFromAdd);

        /*if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }*/

        if (AddManager.interstitial == null)
        {
            this.interstitialAdLoader.LoadAd(
            this.CreateAdRequest(adUnitId),
            onLoaded: this.HandleAdLoaded,
            onFailed: this.HandleAdFailedToLoad);
            this.DisplayMessage("Interstitial is requested");
        }

        if (countResetsFromAdd > 3)
        {
            ShowInterstitial();
            PlayerPrefs.SetInt(CountResetsFromAddPref, 0);
        }
        PlayerPrefs.Save();
    }

    public void ShowInterstitial()
    {
        if (AddManager.interstitial == null)
        {
            this.DisplayMessage("Interstitial is not ready yet");
            return;
        }

        AddManager.interstitial.OnAdClicked += this.HandleAdClicked;
        AddManager.interstitial.OnAdShown += this.HandleAdShown;
        AddManager.interstitial.OnAdFailedToShow += this.HandleAdFailedToShow;
        AddManager.interstitial.OnAdImpression += this.HandleImpression;
        AddManager.interstitial.OnAdDismissed += this.HandleAdDismissed;

        AddManager.interstitial.Show();
        Debug.Log("Реклама показана!");
        AddManager.interstitial = null;
    }

    private AdRequest CreateAdRequest(string adUnitId)
    {
        return new AdRequest(adUnitId);
    }

    private void DisplayMessage(String message)
    {
        this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
        MonoBehaviour.print(message);
    }

    #region Interstitial callback handlers

    public void HandleAdLoaded(Interstitial interstitial)
    {
        this.DisplayMessage("HandleAdLoaded event received");

        AddManager.interstitial = interstitial;
    }

    public void HandleAdFailedToLoad(AdFailedToLoadEventArgs args)
    {
        this.DisplayMessage($"HandleAdFailedToLoad event received with message: {args.Message}");
    }
    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdShown event received");
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdDismissed event received");

        AddManager.interstitial.Destroy();
        AddManager.interstitial = null;
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage($"HandleImpression event received with data: {data}");
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage($"HandleAdFailedToShow event received with message: {args.Message}");
    }

    #endregion
}
