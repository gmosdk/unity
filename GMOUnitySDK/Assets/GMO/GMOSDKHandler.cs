using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Text;

public class GMOSDKHandler {
	
	private static GMOSDKHandler _instance;
	public static string GMO_VERSION = "1.0.1a";

	private bool closeViewAfterSuccessPayment = false;
	
	// Singleton for SDK handler
	public static GMOSDKHandler Instance
	{
		get
		{
			if(_instance == null) _instance = new GMOSDKHandler();
			return _instance;
		}
	}
	
	#if UNITY_IPHONE
	
	// SDK functions
	[DllImport("__Internal")]
	private static extern void init();
	
	[DllImport("__Internal")]
	private static extern void setSDKButtonVisibility(bool isVisible);
	
	[DllImport("__Internal")]
	private static extern void setKeepLoginSession(bool isKeepLoginSession);
	
	[DllImport("__Internal")]
	private static extern bool setHideWelcomeView(bool isHideWelcomeView);

	[DllImport("__Internal")]
	private static extern bool setHidePaymentView(bool isHidePaymentView);
	
	[DllImport("__Internal")]
	private static extern void inviteFacebookFriends();
	
	// User Functions
	[DllImport("__Internal")]
	private static extern void setAutoShowLogin(bool autoShowLogin);
	
	[DllImport("__Internal")]
	private static extern void showUserInfoView();

	[DllImport("__Internal")]
	private static extern void showRegisterView();

	[DllImport("__Internal")]
	private static extern void showTransactionHistory();

	[DllImport("__Internal")]
	private static extern void showLoginView();
	
	[DllImport("__Internal")]
	private static extern void showGoogleLogin();
	
	[DllImport("__Internal")]
	private static extern void showFacebookLogin();

	[DllImport("__Internal")]
	private static extern void showFacebookLoginWithPermissions(string permissions);
	
	[DllImport("__Internal")]
	private static extern void showTwitterLogin();
	
	[DllImport("__Internal")]
	private static extern void switchAccount();
	
	[DllImport("__Internal")]
	private static extern void logout();
	
	[DllImport("__Internal")]
	private static extern bool isUserLoggedIn();
	
	// Payment functions
	[DllImport("__Internal")]
	private static extern void showPaymentView();
	
	[DllImport("__Internal")]
	private static extern void showPaymentViewWithPackageID(string packageID);
	
	[DllImport("__Internal")]
	private static extern void sendStateToWrapper(string state);
	
	[DllImport("__Internal")]
	private static extern void setCharacter(string characterName, string characterID, string serverName, string serverID);
	
	[DllImport("__Internal")]
	private static extern void closePaymentView();
	
	// Track functions
	[DllImport("__Internal")]
	private static extern void sendEventWithCategoryWithValue(string category, string action, string label, int value);

	[DllImport("__Internal")]
	private static extern void sendEventWithCategory(string category, string action, string label);
	
	[DllImport("__Internal")]
	private static extern void sendViewWithName(string name);
	
	// Notification functions
	[DllImport("__Internal")]
	private static extern bool registerPushNotificationWithGroupName(string name);

	// Facebook App Event functions 
	[DllImport("__Internal")]
	private static extern void fbLogEvent(string name);

	[DllImport("__Internal")]
	private static extern void fbLogEventWithParameter(string name, double value, string parameters);

	#region SDK functions
	/*
	 * Call this function in your first scene or when you want user to login
	 * */
	public void Init(){
		GMOSDKReceiver.InitializeGameObjects ();

		init();
		Debug.Log("Called init iOS ");
		Debug.Log("GMOSDK-Unity Version: " + GMO_VERSION);
	}
	
	/*
	 * Call this function to setting hide or show SDK floating button
	 * */
	public void SetSDKButtonVisibility(bool isVisible){
		setSDKButtonVisibility(isVisible);
	}
	
	/*
	 * This function will control the GMO Login Session will be kept or removed at app lauching
	 * (when session's removed user has to login again when app start). 
	 * */
	public void SetKeepLoginSession(bool isKeepLoginSession){
		setKeepLoginSession(isKeepLoginSession);
	}
	
	/*
	 * This function will control the GMO Login View will be automatically show at app lauching (when user's not logged in)
	 * Or you have to call [ShowLoginView](#show-login-view) function to show the LoginView.
	 * */
	public void SetAutoShowLoginDialog(bool autoShowLogin) {
		setAutoShowLogin(autoShowLogin);
	}

	/*
	 * Call this function in `OnApplicationQuit` script
	 * */
	public void FinishSDK()
	{
		
	}
	#endregion
	
	#region User functions
	
	/*
	 * Show login view function (when user's not logged in)
	 * */
	public void ShowLoginView()
	{
		showLoginView();
	}
	
	/*
	 * Logout function
	 * */
	public void Logout() {
		Debug.Log ("Start logout");
		logout();
	}
	
	/*
	 * Call this function when you want to switch logged in user to other GMO User. 
	 * Remember to check callback after switching successful (it'll be called in OnLoginSucceed again)
	 * */
	public void SwitchAccount()
	{
		switchAccount();
	}
	
	/*
	 * This function will show register view  
	 * */
	public void ShowRegisterView()
	{
		showRegisterView();
	}

	/*
	 * This function will show transaction history view  
	 * */
	public void ShowTransactionHistory()
	{
		showTransactionHistory();
	}

	/*
	 * This function will show user profile view  
	 * */
	public void ShowUserInfoView()
	{
		showUserInfoView();
	}
	
	/*
	 * This function will return user logged in state
	 * */
	public bool IsUserLoggedIn() {
		return isUserLoggedIn();
	}
	
	/*
	 * Return GMOSession if logged in
	 * */
	public GMOSession GetGMOSession() {
		return GMOSession.Instance;
	}
	
	/*
	 * Set character function to support character management on web
	 * */
	public void SetCharacter(string characterName, string characterID, string serverName, string serverID) {
		setCharacter(characterName, characterID, serverName, serverID);
	}
	
	public void ShowFacebookLogin()
	{
		showFacebookLogin();
	}

	public void ShowFacebookLoginWithPermissions(string[] permissions)
	{
		StringBuilder builder = new StringBuilder();

		foreach(string permission in permissions)
		{
			builder.Append(permission).Append("|");
		}

		string result = builder.ToString();
		
		// Remove the final delimiter
		result = result.TrimEnd('|');
        
        showFacebookLoginWithPermissions(result);
    }
	
	public void ShowGoogleLogin()
	{
		showGoogleLogin();
	}
	
	public void ShowTwitterLogin()
	{
		showTwitterLogin();
	}
	
	public void InviteFacebookFriends()
	{
		inviteFacebookFriends();
	}

	public void SetHideWelcomeView(bool autoHideWelcomeView) {
		setHideWelcomeView (autoHideWelcomeView);
	}

	public void SetHidePaymentView(bool isHidePaymentView) {
		setHidePaymentView (isHidePaymentView);
    }

	#endregion
	
	#region Payment functions
	/*
	 * Show payment view with default list payment packages
	 * */
	public void ShowPaymentView()
	{
		showPaymentView();
	}
	
	/*
	 * Show a specific package depends on your in-game mechanism
	 * */
	public void ShowPaymentViewWithPackageID(string packageID)
	{
		showPaymentViewWithPackageID(packageID);
	}
	
	public void SendStateToWrapper(string state) {
		sendStateToWrapper(state);
	}

	/*
	 * This function will close payment view  
	 * */
	public void ClosePaymentView()
	{
		closePaymentView();
	}
	#endregion
	
	#region Analytics functions
	public void SendView(string name) {
		sendViewWithName(name);
	}
	
	public void SendEvent(string category,string action,string label) {
		sendEventWithCategory(category, action, label);
	}
	
	public void SendEvent(string category,string action,string label, int value) {
		sendEventWithCategoryWithValue(category, action, label, value);
	}
	#endregion
	
	#region Notification functions
	public void SetPushGroup(string groupName) {
		registerPushNotificationWithGroupName(groupName);
	}
	#endregion

	#region Facebook App Events functions
	public void FBLogEvent(string name) {
		fbLogEvent(name);
	}

	public void FBLogEventWithParameter(string name, double value, Dictionary<string, string> dictionary) {
		fbLogEventWithParameter(name, value, ConvertDictionaryToString(dictionary));
	}
	#endregion
	#endif

	#if UNITY_ANDROID
	private AndroidJavaClass cls_GMOUnityHandler;
	
	#region SDK functions
	public void Init(){
		GMOSDKReceiver.InitializeGameObjects ();
		AndroidJNI.AttachCurrentThread ();
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		
		Debug.Log("Start init Android");
		
		cls_GMOUnityHandler.CallStatic("Init");
		
		Debug.Log("Called init Android ");
		Debug.Log("GMOSDK-Unity Version: " + GMO_VERSION);

		// Configure Adwords and AppFlyer 
		if (GMOSetting.UsingAdWords)
			GMOSDKHandler.Instance.ConfigureAdwords();

		if (GMOSetting.UsingAppFlyer)
			GMOSDKHandler.Instance.ConfigureAppFlyer();
	}
	
	public void SetAutoShowLoginDialog(bool autoShowLogin) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = autoShowLogin;
		cls_GMOUnityHandler.CallStatic("SetAutoShowLoginDialog", args);
	}
	
	public void SetKeepLoginSession(bool isKeepLoginSession){
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isKeepLoginSession;
		cls_GMOUnityHandler.CallStatic("SetKeepLoginSession", args);
	}
	
	// Have to call this function before Init() function.
	public void SetHideWelcomeView(bool autoHideWelcomeView) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = autoHideWelcomeView;
		cls_GMOUnityHandler.CallStatic("SetHideWelcomeView", args);
	}

	public void SetSDKButtonVisibility(bool isVisibility) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isVisibility;
		cls_GMOUnityHandler.CallStatic("SetSDKButtonVisibility", args);
    }

	public void SetKeepCardPaymentPackageID(bool isKeepCardPackageID) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isKeepCardPackageID;
		cls_GMOUnityHandler.CallStatic("SetKeepCardPaymentPackageID", args);
	}
	
	public void UseSmallSDKButton()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("UseSmallSDKButton");
	}

	public void FinishSDK()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("FinishSDK");
	}

	public bool CloseViewAfterSuccessPayment{
		get{
			return this.closeViewAfterSuccessPayment;
		}
		set{
			this.closeViewAfterSuccessPayment = value;
		}
	}

	#endregion
	
	#region User functions
	public void Logout() {
		Debug.Log ("Start logout");
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");

		cls_GMOUnityHandler.CallStatic("Logout");
	}
	
	public void SwitchAccount()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("SwitchAccount");
	}
	
	public void ShowUserInfoView()
	{
		GMOThreadHandler.Instance.Start();

		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowUserInfoView");
	}

	public void ShowRegisterView()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowRegisterView");
	}

	public void ShowTransactionHistory()
	{
		GMOThreadHandler.Instance.Start();

		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowTransactionHistory");
	}
	
	public void ShowLoginView()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowLoginView");
	}
	
	public void ShowFacebookLogin()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowLoginFacebook");
	}
	
	public void ShowGoogleLogin()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowLoginGoogle");
	}
	
	public void ShowTwitterLogin()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowLoginTwitter");
	}

	public void SetHidePaymentView(bool isHidePaymentView) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = isHidePaymentView;
		cls_GMOUnityHandler.CallStatic("SetHidePaymentView", args);
	}
	
	public void InviteFacebookFriends()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("InviteFacebookFriend");
	}
	
	public bool IsUserLoggedIn() {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		return cls_GMOUnityHandler.CallStatic<bool>("IsUserLoggedIn");
	}

	public void SetCharacter(string characterName, string characterID, string serverName, string serverID) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[4];
		args [0] = characterName;
		args [1] = characterID;
		args [2] = serverName;
		args [3] = serverID;
		cls_GMOUnityHandler.CallStatic("SetCharacter", args);
	}
	
	/*
	 * Return GMOSession if logged in
	 * */
	public GMOSession GetGMOSession() {
		return GMOSession.Instance;
	}
	#endregion
	
	#region Payment functions
	public void ShowPaymentView()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ShowPaymentView");
	}

	public void ShowPaymentViewWithPackageID(string packageID)
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		
		object[] args = new object[1];
		args [0] = packageID;
		
		cls_GMOUnityHandler.CallStatic("ShowPaymentViewWithPackageID", args);
	}

	
	public void ClosePaymentView()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ClosePaymentView");
	}

	public void SendStateToWrapper(string state) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = state;
		cls_GMOUnityHandler.CallStatic("SendStateToWrapper", args);
	}
	#endregion
	
	#region Analytics functions
	public void SendView(string activityName) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = activityName;
		cls_GMOUnityHandler.CallStatic("SendView", args);
	}
	
	public void SendEvent(string category,string action,string label) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[3];
		args [0] = category;
		args [1] = action;
		args [2] = label;
		cls_GMOUnityHandler.CallStatic("SendEvent", args);
	}
	
	public void SendEvent(string category,string action,string label, int value) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[4];
		args [0] = category;
		args [1] = action;
		args [2] = label;
		args [3] = value;
		cls_GMOUnityHandler.CallStatic("SendEvent", args);
	}
	#endregion
	
	#region Notification functions
	public void SetPushGroup(string groupName) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = groupName;
		cls_GMOUnityHandler.CallStatic("SetPushGroup", args);
	}
	#endregion

	#region Facebook App Events functions
	public void ActivateFBAppEvent()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("ActivateFBAppEvent");
	}
	
	public void DeactivateFBAppEvent()
	{
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		cls_GMOUnityHandler.CallStatic("DeactivateFBAppEvent");
	}

	public void FBLogEvent(string name) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[1];
		args [0] = name;
		cls_GMOUnityHandler.CallStatic("FBLogEvent", args);
	}
	
	public void FBLogEventWithParameter(string name, double value, Dictionary<string, string> dictionary) {
		cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
		object[] args = new object[3];
		args [0] = name;
		args [1] = value;
		args [2] = ConvertDictionaryToString(dictionary);
		cls_GMOUnityHandler.CallStatic("FBLogEventWithParameter", args);
	}
	#endregion

	#region Other Functions
	public void ConfigureAppFlyer() {
		if (GMOSetting.UsingAppFlyer) {
			cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
			object[] args = new object[1];
			args [0] = GMOSetting.AppFlyerKey;
			cls_GMOUnityHandler.CallStatic("ConfigureAppFlyer", args);
		}
	}

	public void ConfigureAdwords() {
		if (GMOSetting.UsingAdWords) {
			cls_GMOUnityHandler = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityHandler");
			object[] args = new object[4];
			args [0] = GMOSetting.AdWordsConversionID;
			args [1] = GMOSetting.AdWordsLabel;
			args [2] = GMOSetting.AdWordsValue;
			args [3] = GMOSetting.AdWordsIsRepeatable;
			cls_GMOUnityHandler.CallStatic("ConfigureAdwords", args);
		}
	}

	#endregion
	#endif

	private string ConvertDictionaryToString(Dictionary<string, string> dictionary) {
		StringBuilder builder = new StringBuilder();
		foreach (KeyValuePair<string, string> pair in dictionary)
		{
			builder.Append(pair.Key).Append(":").Append(pair.Value).Append(';');
		}
		string result = builder.ToString();
		
		// Remove the final delimiter
		result = result.TrimEnd(';');
		
		return result;
	}
}
