using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class GMOThreadHandler : MonoBehaviour {

	#if UNITY_ANDROID
	private Thread callbackThread;
	private AndroidJavaClass cls_MessageFromSDK;
	private AndroidJavaObject activityContext = null;
	private string lastPidTime = "";
	private string lastCallbackTime = "";
	private static bool canCallbackThreadRun = false;

	private static GMOThreadHandler _instance;
	
	// Singleton for GMO Thread handler
	public static GMOThreadHandler Instance
	{
		get
		{
			if(_instance == null) _instance = new GMOThreadHandler();
			return _instance;
		}
	}

	// When GMOSDK has been started, Android will start a new activity and Unity activity would be paused. 
	// So we could not callback data to Unity activity. New thread and runOnUiThread will solve this problem. 
	public void Start() 
	{
		Debug.Log("GMOSDK: Starting callback thread");
		AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		activityContext = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
		canCallbackThreadRun = true;
		
		callbackThread = new Thread(new ThreadStart(LoopCallbackThread));
		callbackThread.Start();
	}
	
	// Make a loop on UI Thread to call AndroidJNI functions
	void LoopCallbackThread() {
		int attachThread = AndroidJNI.AttachCurrentThread();
		if (attachThread != 0){
			Debug.Log("GMOSDK: Attach thread failed");
			return;
		}
		
		while (true) {
			if (!canCallbackThreadRun){
				AndroidJNI.DetachCurrentThread();
				break;
			}
			
			try {
				activityContext.Call("runOnUiThread", new AndroidJavaRunnable(WorkThread));
			}
			catch (AndroidJavaException e) {
				Debug.Log("GMOSDK: " + e.ToString());
			} 
			catch (Exception e) {
				Debug.Log("GMOSDK: " + e.ToString());
			}
			
			Thread.Sleep(300);
		}
	}

	void WorkThread(){
		GetPaymentStateFromThread ();

		// Call this when we need close paymentView after success payment
		if (GMOSDKHandler.Instance.CloseViewAfterSuccessPayment)
			ActiveCallbackIDFromThread ();
	}
	
	// Receive packageID and send PaymentState to GMOSDK
	void GetPaymentStateFromThread() {
		cls_MessageFromSDK = new AndroidJavaClass("com.appota.gamesdk.v4.commons.Messages"); 
		string pid = cls_MessageFromSDK.GetStatic<string>("pid");
		if (pid.Equals("") || pid == null)
			return;
		
		// @pid: "packageID" + "@" + "unix_time"
		string packageID = pid.Split('@')[0];
		string pidTime = pid.Split('@')[1];
		
		if (!pidTime.Equals(lastPidTime)) {
			lastPidTime = pidTime;
			GMOSDKReceiver.Instance.GetPaymentState(packageID);
		}
	}

	void ActiveCallbackIDFromThread() {
		cls_MessageFromSDK = new AndroidJavaClass("com.appota.gamesdk.v4.unity.UnityReceiver"); 
		string callbackID = cls_MessageFromSDK.GetStatic<string>("callbackID");
		if (callbackID.Equals("") || callbackID == null)
			return;

		// @callbackID: "message" + "@" + "unix_time"
		string message = callbackID.Split('@')[0];
		string unixTime = callbackID.Split('@')[1];

		if (!unixTime.Equals(lastCallbackTime)) {
			lastCallbackTime = unixTime;
			GMOSDKHandler.Instance.ClosePaymentView();
		}
	}
	
	public void Stop() 
	{
		canCallbackThreadRun = false;
	}
	#endif
}
