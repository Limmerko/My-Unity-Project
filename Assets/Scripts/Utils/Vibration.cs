using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

using System.Collections;
using System.Runtime.InteropServices;
#if UNITY_IOS
using TapticPlugin;
#endif

public static class Vibration
{
    
#if UNITY_ANDROID
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
    public static AndroidJavaObject context;

    public static AndroidJavaClass vibrationEffect;
#endif

    private static bool initialized = false;
    public static void Init ()
    {
        if ( initialized ) return;

#if UNITY_ANDROID
        if ( Application.isMobilePlatform ) {

            unityPlayer = new AndroidJavaClass ( "com.unity3d.player.UnityPlayer" );
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ( "currentActivity" );
            vibrator = currentActivity.Call<AndroidJavaObject> ( "getSystemService", "vibrator" );
            context = currentActivity.Call<AndroidJavaObject> ( "getApplicationContext" );

            if ( AndroidVersion >= 26 ) {
                vibrationEffect = new AndroidJavaClass ( "android.os.VibrationEffect" );
            }

        }
#endif

        initialized = true;
    }

    public static void VibrateImpact()
    {
#if UNITY_ANDROID
        Vibration.VibrateAndroid(1);
#endif
#if UNITY_IOS
        TapticManager.Impact(ImpactFeedback.Medium);
#endif
    }


#if UNITY_ANDROID
    ///<summary>
    /// Only on Android
    /// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
    ///</summary>
    public static void VibrateAndroid ( long milliseconds )
    {

        if ( Application.isMobilePlatform ) {
            if ( AndroidVersion >= 26 ) {
                AndroidJavaObject createOneShot = vibrationEffect.CallStatic<AndroidJavaObject> ( "createOneShot", milliseconds, -1 );
                vibrator.Call ( "vibrate", createOneShot );

            } else {
                vibrator.Call ( "vibrate", milliseconds );
            }
        }
    }
#endif
    
    public static int AndroidVersion {
        get {
            int iVersionNumber = 0;
            if ( Application.platform == RuntimePlatform.Android ) {
                string androidVersion = SystemInfo.operatingSystem;
                int sdkPos = androidVersion.IndexOf ( "API-" );
                iVersionNumber = int.Parse ( androidVersion.Substring ( sdkPos + 4, 2 ).ToString () );
            }
            return iVersionNumber;
        }
    }
    
    public static void Vibrate ()
    {
#if UNITY_ANDROID
        if ( Application.isMobilePlatform ) {
            Handheld.Vibrate ();
        }
#endif
    }
}