using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	float ScreenWidth;
	float ScreenHeight;
	string text = "UserID: ";

	// Use this for initialization
	void Start () {
		#if UNITY_IPHONE
		GMOSDKHandler.Instance.Init();
		GMOSDKHandler.Instance.SetAutoShowLoginDialog(false);
		#endif

		#if UNITY_ANDROID
		GMOSDKHandler.Instance.Init();
		GMOSDKHandler.Instance.SetAutoShowLoginDialog(true);
		GMOSDKHandler.Instance.UseSmallSDKButton();
		GMOSDKHandler.Instance.SetKeepLoginSession(true);
		GMOSDKHandler.Instance.SetKeepCardPaymentPackageID(true);
		#endif

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	void OnApplicationQuit(){
		#if UNITY_ANDROID
		GMOSDKHandler.Instance.FinishSDK();
		#endif
	}

	void OnGUI () {

		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;

		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 30;
		
		if(GUI.Button(new Rect(ScreenWidth / 3, 40,ScreenWidth / 3,ScreenHeight / 10), "Login", customButton)) {
			GMOSDKHandler.Instance.ShowLoginView ();
		}
		
		if(GUI.Button(new Rect(ScreenWidth / 3, 50 + ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Logout", customButton)) {
			GMOSDKHandler.Instance.Logout ();
		}

		if(GUI.Button(new Rect(ScreenWidth / 3, 60 + 2 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Switch Account", customButton)) {
			GMOSDKHandler.Instance.SwitchAccount ();
		}

		GMOSession gmoSession = GMOSDKHandler.Instance.GetGMOSession();
		text = "UserID: " + gmoSession.UserID;
		GUI.TextArea(new Rect(10, 40, ScreenWidth / 4, ScreenHeight / 10), text, 200, customButton);

		if(GMOSDKHandler.Instance.IsUserLoggedIn()){	
			if(GUI.Button(new Rect(ScreenWidth / 3, 70 + 3 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "User Info", customButton)) {
				GMOSDKHandler.Instance.ShowUserInfoView ();
			}

			if(GUI.Button(new Rect(ScreenWidth / 3, 80 + 4 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Payment", customButton)) {
				GMOSDKHandler.Instance.ShowPaymentView ();
			}

			if(GUI.Button(new Rect(ScreenWidth / 3, 90 + 5 * ScreenHeight / 10,ScreenWidth / 3,ScreenHeight / 10), "Payment With Package", customButton)) {
				GMOSDKHandler.Instance.ShowPaymentViewWithPackageID ("app.pkid.tym4K");
			}

		}
	}
}
