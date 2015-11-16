using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using SimpleJSON;

public class GMOSDKReceiver : MonoBehaviour {
	private static GameObject playGameObject;
	private static bool initialized;
	
	private static GMOSDKReceiver _instance;
	
	// Singleton for SDK handler
	public static GMOSDKReceiver Instance
	{
		get
		{
			if(_instance == null) _instance = new GMOSDKReceiver();
			return _instance;
		}
	}
	
	public static void InitializeGameObjects()
	{
		if(!initialized)
		{
			playGameObject = new GameObject("GMOSDKReceiver");
			playGameObject.AddComponent(typeof(GMOSDKReceiver));
			//keep this game object around for all scenes
			DontDestroyOnLoad(playGameObject);		
			initialized = true;
		}	
	}
	
	public void OnLoginSuccess(string gmoSession)
	{
		// Get User info from GMOSession
		GMOSession gmoSessionObj = new GMOSession(gmoSession);
		GMOSession.Instance.UpdateInstance(gmoSessionObj);

		Debug.Log ("GMOSDK: Did login");
	}
	
	public void OnLoginError(string error)
	{
		Debug.Log ("GMOSDK: Login Error: " + error);
	}
	
	public void OnLogoutSuccess(string userName)
	{ 
		Debug.Log ("GMOSDK: Did logout " + userName);
	}
	
	public void OnPaymentSuccess(string transactionResult)
	{
		// Parse Transaction result into class GMOPaymentResult.cs
		GMOPaymentResult paymentResult = new GMOPaymentResult(transactionResult);
		Debug.Log ("GMOSDK Currency: " + paymentResult.Currency);

		// Parse amount, packageID, in AppPaymentResult
		Debug.Log ("GMOSDK: Did payment");
		Debug.Log("GMOSDK: " + transactionResult);
	}
	
	public void OnPaymentError(string error)
    {
		Debug.Log ("GMOSDK: Payment Error: " + error);
    }
    
    public void OnCloseLoginView()
    {
		Debug.Log ("GMOSDK: Close Login View");
    }

	public void OnOpenPaymentView()
	{
		#if UNITY_ANDROID
		// Must call this function to start Callback Thread
		GMOThreadHandler.Instance.Start();
		#endif
		
		Debug.Log ("GMOSDK: Open Payment View");
	}
    
    public void OnClosePaymentView()
    {
        #if UNITY_ANDROID
		// Must call this function to stop Callback Thread
		GMOThreadHandler.Instance.Stop();
        #endif
        
		Debug.Log ("GMOSDK: Close Payment View");
    }
    
    public void GetPaymentState(string packageID)
    {
		Debug.Log ("GMOSDK: PackageID: " + packageID);
		string paymentState = packageID;
        
		// Game info can be set and change during your game play via GlobalGameVariables
		string gameServerID = GlobalGameVariables.Instance.gameServerID;
		string gameUserID = GlobalGameVariables.Instance.gameUserID;
		string gameInfo = GlobalGameVariables.Instance.gameInfo;
		
		paymentState += "|" + gameInfo + "|" + gameServerID + "|" + gameUserID;

#if UNITY_ANDROID || UNITY_IPHONE
        GMOSDKHandler.Instance.SendStateToWrapper(paymentState);
#endif
	}
	
}
