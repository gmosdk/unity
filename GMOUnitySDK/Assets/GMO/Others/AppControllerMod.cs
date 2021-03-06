﻿using UnityEngine;
using System.IO;

public class AppControllerMod 
{
    public static void UpdateUnityAppController(string path)
    {
        const string filePath = "Classes/UnityAppController.mm";
        string fullPath = Path.Combine(path, filePath);
        
        var reader = new StreamReader(fullPath);
        string AppControllerSource = reader.ReadToEnd();
        reader.Close();
        
		// Add header import
		AppControllerSource = AddHeaderImportFramework(AppControllerSource);

		// Add SDK Handler Callback
		AppControllerSource = AddSDKHandlerCallback(AppControllerSource);

		// Add callback register Push Notification
		AppControllerSource = AddCallbackRegisterPushNotifications(AppControllerSource);

		// Call Facebook activeApp() inside applicationDidBecomeActive function 
		AppControllerSource = AddFacebookAppEvents(AppControllerSource);

		// Add callback register Push Notification with Token data
		AppControllerSource = AddCallbackRegisterPushNotificationWithTokenData(AppControllerSource);

        var writer = new StreamWriter(fullPath, false);
		writer.Write(AppControllerSource);
        writer.Close();
        
    }

	private static string AddSDKHandlerCallback(string AppControllerSource) {
		int fixupStart = AppControllerSource.IndexOf("openURL:", System.StringComparison.Ordinal);
		if(fixupStart <= 0)
			return AppControllerSource;
		int fixupMid = AppControllerSource.IndexOf("return", fixupStart);
		if(fixupMid <= 0)
			return AppControllerSource;
		int fixupEnd = AppControllerSource.IndexOf(';', fixupMid);
		if(fixupEnd <= 0)
			return AppControllerSource;
		
		string fixedAppController = AppControllerSource.Substring(0, fixupMid);
		
		if (AppControllerSource.IndexOf("GMOGameSDK handleOpenURL", System.StringComparison.Ordinal) <= 0){
			// Base on kind of SDK
			if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "[GMOGameSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];\n[OCSDKConfigure handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];\nreturn true;";
			}
			else if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "return [GMOGameSDK application:application handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];";
			}
			else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				fixedAppController += "return [OCSDKConfigure handleOpenURL:url sourceApplication:sourceApplication annotation:annotation];";
			}
		}
		fixedAppController += AppControllerSource.Substring(fixupEnd+1);
		return fixedAppController;
	}

	private static string AddHeaderImportFramework(string AppControllerSource) {
		int finalFixupStart = AppControllerSource.IndexOf("#import <OpenGLES/EAGL.h", System.StringComparison.Ordinal);
		if(finalFixupStart <= 0)
			return AppControllerSource;
		int finalFixupEnd = AppControllerSource.IndexOf(">", finalFixupStart);
		if(finalFixupEnd <= 0)
			return AppControllerSource;
		
		string finalFixedAppController = AppControllerSource.Substring(0, finalFixupStart);
		
		if ( AppControllerSource.IndexOf("GMOSDK.h", System.StringComparison.Ordinal) <= 0 || AppControllerSource.IndexOf("OnClanSDK.h", System.StringComparison.Ordinal) <= 0){
			// Base on kind of SDK
			if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <GMOSDK/GMOSDK.h>\n#import <OnClanSDK/OCSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
			else if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <GMOSDK/GMOSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
			else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				finalFixedAppController += "#import <OnClanSDK/OCSDK.h>\n#import <OpenGLES/EAGL.h>";
			}
			
			// Import Facebook header
			finalFixedAppController += "\n#import <FBSDKCoreKit/FBSDKCoreKit.h> \n#import <FBSDKLoginKit/FBSDKLoginKit.h> \n#import <FBSDKShareKit/FBSDKShareKit.h>";
		}
		
		finalFixedAppController += AppControllerSource.Substring(finalFixupEnd+1);
		return finalFixedAppController;
	}

	private static string AddCallbackRegisterPushNotifications(string AppControllerSource) {
		// Add callback register Push Notification
		int notiPosStart = AppControllerSource.IndexOf("didReceiveRemoteNotification", System.StringComparison.Ordinal);
		if (notiPosStart <= 0)
			return AppControllerSource;
		int notiPosEnd = AppControllerSource.IndexOf("{", notiPosStart);
		if (notiPosEnd <= 0)
			return AppControllerSource;
		
		string pushNotiString = AppControllerSource.Substring(0, notiPosEnd);
		
		if (AppControllerSource.IndexOf("GMOGameSDK handlePushNotification", System.StringComparison.Ordinal) <= 0){
			pushNotiString += "{\n\t[GMOGameSDK handlePushNotification:userInfo];";
		}
		
		pushNotiString += AppControllerSource.Substring(notiPosEnd+1);
		return pushNotiString;
	}

	private static string AddCallbackRegisterPushNotificationWithTokenData(string AppControllerSource) {
		int regPosStart = AppControllerSource.IndexOf("didRegisterForRemoteNotificationsWithDeviceToken", System.StringComparison.Ordinal);
		if (regPosStart <= 0)
			return AppControllerSource;
		int regPosEnd = AppControllerSource.IndexOf("{", regPosStart);
		if (regPosEnd <= 0)
			return AppControllerSource;
		
		string tempString = AppControllerSource.Substring(0, regPosEnd);
		
		if (AppControllerSource.IndexOf("GMOGameSDK configurePushNotificationWithTokenData", System.StringComparison.Ordinal) <= 0){
			tempString += "{\n\t[GMOGameSDK configurePushNotificationWithTokenData:deviceToken];";
		}
		
		tempString += AppControllerSource.Substring(regPosEnd+1);
		return tempString;
	}

	private static string AddFacebookAppEvents(string AppControllerSource) {
		int fbActivePosStart = AppControllerSource.IndexOf("applicationDidBecomeActive", System.StringComparison.Ordinal);
		if (fbActivePosStart <= 0)
			return AppControllerSource;
		int fbActivePosEnd = AppControllerSource.IndexOf("{", fbActivePosStart);
		if (fbActivePosEnd <= 0)
			return AppControllerSource;
		
		string fbActiveString = AppControllerSource.Substring(0, fbActivePosEnd);
		
		if (AppControllerSource.IndexOf("FBSDKAppEvents activateApp", System.StringComparison.Ordinal) <= 0){
			fbActiveString += "{\n\t[FBSDKAppEvents activateApp];";
		}
		
		fbActiveString += AppControllerSource.Substring(fbActivePosEnd+1);
		return fbActiveString;
	}
}
