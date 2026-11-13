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

public class YandexMobileAdsBannerDemoScript : MonoBehaviour
{
    private String message = "";

    private Banner banner;

    /*public void OnGUI()
    {
        var fontSize = (int) (0.05f * Math.Min(Screen.width, Screen.height));

        var labelStyle = GUI.skin.GetStyle("label");
        labelStyle.fontSize = fontSize;

        var buttonStyle = GUI.skin.GetStyle("button");
        buttonStyle.fontSize = fontSize;

        #if UNITY_EDITOR
            this.message = "Mobile ads SDK is not available in editor. Only Android and iOS environments are supported";
        #else
            if (GUILayout.Button("Request Banner", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.RequestBanner();
            }

            if (GUILayout.Button("Destroy Banner", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.banner.Destroy();
            }
        #endif

        GUILayout.Label(this.message, labelStyle);
    }*/

    public void RequestBanner(RectTransform rectTransform)
    {

        //Sets COPPA restriction for user age under 13
        YandexAds.SetAgeRestricted(true);

        // Replace demo Unit ID 'demo-banner-yandex' with actual Ad Unit ID
        string adUnitId = "demo-banner-yandex"; // TODO поменять на реальное значение 

        if (this.banner != null)
        {
            this.banner.Destroy();
        }
        // Set sticky banner width
        // BannerAdSize bannerSize = BannerAdSize.Sticky(GetScreenWidthDp());
        // Or set inline banner maximum width and height
        //int maxHeight = CalcMaxBannerHeight(rectTransform);
        BannerAdSize bannerSize = BannerAdSize.Inline(1000, 50); // 50 - минимум
        this.banner = new Banner(bannerSize, AdPosition.BottomCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnAdClicked += this.HandleAdClicked;
        this.banner.OnImpression += this.HandleImpression;

        this.banner.LoadAd(this.CreateAdRequest(adUnitId));
        this.DisplayMessage("Banner is requested");
    }

    private int CalcMaxBannerHeight(RectTransform rectTransform)
    {
        // Нижний край кнопки в пикселях экрана (работает для Overlay и Camera канвасов)
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var canvas = rectTransform.GetComponentInParent<Canvas>();
        Camera cam = (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            ? canvas.worldCamera : null;
        float buttonBottomPx = RectTransformUtility.WorldToScreenPoint(cam, corners[0]).y;

        // Свободное место под кнопкой: от низа экрана до нижнего края кнопки
        float availablePx = Mathf.Max(0f, buttonBottomPx);

        // SDK не допускает высоту больше 15% экрана
        availablePx = Mathf.Min(availablePx, Screen.height * 0.15f);

        return Mathf.FloorToInt(availablePx / (Screen.dpi / 160f)); // px -> dp
    }


    // Example how to get screen width for request. The larger horizontal safe-area inset is
    // subtracted on both sides, so a centered banner stays centered and clear of the display cutout.
    private int GetScreenWidthDp()
    {
        Rect safeArea = Screen.safeArea;
        float largerInset = Mathf.Max(safeArea.xMin, Screen.width - safeArea.xMax);
        return ScreenUtils.ConvertPixelsToDp((int)(Screen.width - 2 * largerInset));
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

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdLoaded event received");
        this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage("HandleAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage("HandleImpression event received with data: " + data);
    }

    #endregion
}
