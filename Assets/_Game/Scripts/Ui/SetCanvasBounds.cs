using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SetCanvasBounds : MonoBehaviour {
  public static float scaleCanvas = 0f;
  public static float scaleBackground = 0f;
  public static float minWidth = 0f;
  public static float expectHeight = 0f;

  public static float defaultWidth = 768f;
  public static float defaultHeight = 1024f;
  public static float defaultScreenWidth = 570f;
  const float scaleCamera = 0.91f;

#if UNITY_IOS
  [DllImport("__Internal")]
  private extern static void GetSafeAreaImpl(out float x, out float y, out float w, out float h);
  [DllImport("__Internal")]
  private extern static void SetStatusBarHidden(bool hidden);
#endif

  //Check iphoneX
  private static bool isCheckDevice = false;
  private static bool _isIphoneX = false;

  public static bool isIPhoneX {
    get {
      if (isCheckDevice) {
        return _isIphoneX;
      }
      else {
        return IsIphoneX();
      }
    }
  }

  Rect lastSafeArea = new Rect(0, 0, 0, 0);
  public bool isAutoSetup = true;
  public bool isBackground = false;

  private Rect GetSafeArea() {
    float x, y, w, h;
#if UNITY_IOS && !UNITY_EDITOR
		GetSafeAreaImpl(out x, out y, out w, out h);
#else
    x = 0;
    y = 0;
    w = Screen.width;
    h = Screen.height;
#endif

    return new Rect(x, y, w, h);
  }

  // Use this for initialization
  void Awake() {
    if (isIPhoneX) {
      if (isAutoSetup) {
        Setup();
      }
    }
  }

  public void Setup() {
    //Set Canvas Scale
    CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();
    if (scaler != null) {
      if (isBackground) {
        scaler.matchWidthOrHeight = scaleBackground;
      }
      else {
        scaler.matchWidthOrHeight = scaleCanvas;
      }
    }
  }

  private static bool IsIphoneX() {
    bool result = false;
    _isIphoneX = false;
    isCheckDevice = true;
#if GEARINC_TEST_IPHONEX
    _isIphoneX = true;
    return true;
#endif

    if (((float)Screen.width / Screen.height) < 0.52f) {
      result = true;

      //show status bar for iphoneX
#if !UNITY_EDITOR && UNITY_IOS
      SetStatusBarHidden(false);
#endif
    }

    _isIphoneX = result;

    if (result) {
      CalculateNewScale();
    }

    return result;
  }

  //CuongNP R33
  private static void CalculateNewScale() {
    minWidth = Screen.width * defaultHeight / Screen.height;
    expectHeight = Screen.height * defaultScreenWidth / Screen.width * scaleCamera;

    scaleCanvas = (1 - ((defaultScreenWidth - minWidth) / (defaultWidth - minWidth))) * scaleCamera;

    //TODO need calculate for Background
    scaleBackground = 0.82f;
  }
}