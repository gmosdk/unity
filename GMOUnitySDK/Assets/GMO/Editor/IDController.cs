using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;
using GMO;

[CustomEditor(typeof(GMOSetting))]
public class IDController : EditorWindow {

	static string _facebookID;
	static string _facebookSecretID;
	static string _facebookAppLinkUrl;
	static string _twitterKey;
	static string _twitterSecret;
	static string _googleID;
	static string _googleSecretID;
	static string _gameID;
	static string _inAppApiKey;
	// AppFlyer key
	static bool _usingAppFlyer;
	static string _appleAppID;
	static string _appFlyerKey;
	// AdWords key
	static bool _usingAdWords;
	static string _adWordsConversionID;
	static string _adWordsLabel;
	static string _adWordsValue;
	static bool _adWordsIsRepeatable;

	private GMOSetting instance;
	static bool isUsingOnClanSDK = false;
	//static bool isUsingGMOSDK = false;
	static int minHeight;
	static int minWidth;

	public Texture2D gmoLogo;
	
	private static IDController windows;
	void OnEnable()
	{
		gmoLogo = AssetDatabase.LoadAssetAtPath("Assets/GMO/Resources/gmo_logo.png", typeof(Texture2D)) as Texture2D;
		
	}
	
	[MenuItem ("GMO/Configurations")]
	static void Init(){
		isUsingOnClanSDK = System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null;
		//isUsingGMOSDK = System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null;
		
		windows = GetWindow(typeof (IDController), false, "GMO", true) as IDController;

		windows.minSize = new Vector2(400, 400);
		windows.maxSize = new Vector2(600, 600);

		windows.Show();
		
		EditorWindow.GetWindow(typeof (IDController)).Show();

		_facebookID = GMOSetting.FacebookAppID;
		_facebookSecretID = GMOSetting.FacebookAppSecretID;
		_facebookAppLinkUrl = GMOSetting.FacebookAppLinkUrl;
		_twitterKey = GMOSetting.TwitterConsumerKey;
		_twitterSecret = GMOSetting.TwitterConsumerSecret;
		_googleID = GMOSetting.GoogleClientId;
		_googleSecretID = GMOSetting.GoogleClientSecretId;
		_gameID = GMOSetting.GameID;
		_inAppApiKey = GMOSetting.InAppApiKey;

		_usingAppFlyer = GMOSetting.UsingAppFlyer;
		_appleAppID = GMOSetting.AppleAppID;
		_appFlyerKey = GMOSetting.AppFlyerKey;

		_usingAdWords = GMOSetting.UsingAdWords;
		_adWordsConversionID = GMOSetting.AdWordsConversionID;
		_adWordsLabel = GMOSetting.AdWordsLabel;
		_adWordsValue = GMOSetting.AdWordsValue;
		_adWordsIsRepeatable = GMOSetting.AdWordsIsRepeatable;
	}
	
	void OnGUI()
	{
		//GUILayout.Label(gmoLogo,GUILayout.MaxHeight(120), GUILayout.MaxWidth(400));
		
		GUILayout.Space(10);
		GUILayout.Label ("Version: " + GMOSDKHandler.GMO_VERSION, EditorStyles.label);
		
		EditorGUILayout.BeginVertical();
		
		if (PenaltyEditorTools.DrawHeader("GMO Settings"))
		{
			_inAppApiKey = EditorGUILayout.TextField("API Key", _inAppApiKey);

			if (isUsingOnClanSDK) {
				_gameID = EditorGUILayout.TextField("Game ID", _gameID);
			}
		}
		
		GUILayout.Space(10);
		
		if (PenaltyEditorTools.DrawHeader("Social Settings"))
		{
			_facebookID = EditorGUILayout.TextField("Facebook ID", _facebookID);
			#if UNITY_WP8
			_facebookSecretID = EditorGUILayout.TextField("Facebook Secret ID", _facebookSecretID);
			#elif UNITY_IOS
			_facebookAppLinkUrl = EditorGUILayout.TextField("Facebook App Link URL", _facebookAppLinkUrl);
			#endif
			_twitterKey = EditorGUILayout.TextField("Twitter Key", _twitterKey);
			_twitterSecret = EditorGUILayout.TextField("Twitter Secret", _twitterSecret);
			_googleID = EditorGUILayout.TextField("Google Client ID", _googleID);
			_googleSecretID = EditorGUILayout.TextField("Google Client Secret ID", _googleSecretID);
		}
		
		GUILayout.Space(10);

		if (PenaltyEditorTools.DrawHeader("Other Settings"))
		{
			// AppFlyer Configure
			if (_usingAppFlyer) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
			_usingAppFlyer = EditorGUILayout.Toggle("Using AppFlyer",_usingAppFlyer);
			GUI.backgroundColor = Color.white;
			if (_usingAppFlyer) {
				_appleAppID = EditorGUILayout.TextField("Apple AppID", _appleAppID);
				_appFlyerKey = EditorGUILayout.TextField("AppFlyer Key", _appFlyerKey);
				GUILayout.Space(20);
			}

			// AdWords Configure
			if (_usingAdWords) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
			_usingAdWords = EditorGUILayout.Toggle("Using AdWords",_usingAdWords);
			GUI.backgroundColor = Color.white;
			if (_usingAdWords) {
				_adWordsConversionID = EditorGUILayout.TextField("ConversionID", _adWordsConversionID);
				_adWordsLabel = EditorGUILayout.TextField("Label", _adWordsLabel);
				_adWordsValue = EditorGUILayout.TextField("Value", _adWordsValue);
				if (_adWordsIsRepeatable) GUI.backgroundColor = Color.green; else GUI.backgroundColor = Color.white;
				_adWordsIsRepeatable = EditorGUILayout.Toggle("IsRepeatable",_adWordsIsRepeatable);
				GUI.backgroundColor = Color.white;
			}


		}
		
		GUILayout.Space(10);

		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginHorizontal();
		Color myStyleColor = new Color(190f / 255, 240f / 255, 143f / 255);
		GUI.backgroundColor = myStyleColor;
		
		if (GUILayout.Button("Update Setting!", GUILayout.ExpandWidth(true), GUILayout.MinWidth(250), GUILayout.MinHeight(50)))
		{
			SaveSetting();
		}
		GUILayout.Space(10);
		GUI.backgroundColor = Color.white;
		
		if (GUILayout.Button("Cancel!", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
		{
			windows.Close();
			//Cancel Setting;
		}
		
		EditorGUILayout.EndHorizontal();
		
		GUILayout.Space(10);

	}
	
	//Save or Update Settings Data
	
	void SaveSetting()
	{
		GMOSetting.FacebookAppID = _facebookID;
		GMOSetting.FacebookAppSecretID = _facebookSecretID;
		GMOSetting.FacebookAppLinkUrl = _facebookAppLinkUrl;
		GMOSetting.TwitterConsumerKey = _twitterKey;
		GMOSetting.TwitterConsumerSecret = _twitterSecret;
		GMOSetting.GoogleClientId = _googleID;
		GMOSetting.GoogleClientSecretId = _googleSecretID;
		GMOSetting.InAppApiKey = _inAppApiKey;
		GMOSetting.GameID = _gameID;

		GMOSetting.UsingAppFlyer = _usingAppFlyer;
		GMOSetting.AppleAppID = _appleAppID;
		GMOSetting.AppFlyerKey = _appFlyerKey;
		
		GMOSetting.UsingAdWords = _usingAdWords;
		GMOSetting.AdWordsConversionID = _adWordsConversionID;
		GMOSetting.AdWordsLabel = _adWordsLabel;
		GMOSetting.AdWordsValue = _adWordsValue;
		GMOSetting.AdWordsIsRepeatable = _adWordsIsRepeatable;
		ManifestMod.GenerateManifest();
		Debug.Log("Complete setting!!!");
		
	}
	
}
