using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GMO 
{
	public class ManifestMod
	{
		// GMO Meta Data Element 
		public const string GMOApikeyMetaDataName = "com.gmo.apiKey";
		
		// Social Elements Name
		public const string FacebookActivityName = "com.gmo.facebook.FacebookActivity";
		public const string ApplicationLinkMetaDataName = "FacebookAppLinkUrl";
		public const string ApplicationIdMetaDataName = "com.facebook.sdk.ApplicationId";
		public const string TwitterKeyMetaDataName = "com.gmo.twitter.consumer.key";
		public const string TwitterSecretMetaDataName = "com.gmo.twitter.consumer.secret";
		
		// OnClan Elements Name
		public const string OnClanActivityName = "com.onclan.android.core.OnClanActivity";
		public const string OnClanLoginActivityName = "com.onclan.android.home.LoginActivity";
		public const string OnClanChatServiceName = "com.onclan.android.chat.mqtt.ChatService";
		
		// GMO Elements Name
		public const string GMOBaseActivityName = "com.gmo.gamesdk.v4.ui.BaseSDKActivty";

		public const string AppsFlyerReceiverName = "com.appsflyer.MultipleInstallBroadcastReceiver";
		
		public static void GenerateManifest()
		{
			var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
			
			// only copy over a fresh copy of the AndroidManifest if one does not exist
			if (!File.Exists(outputFile))
			{
				Debug.LogError("AndroidManifest.xml missing!!! Please put it in Assets/Plugin/Android.");
			}
			UpdateManifest(outputFile);
		}
		
		private static XmlNode FindChildNode(XmlNode parent, string name)
		{
			XmlNode curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name))
				{
					return curr;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
		private static XmlNode FindNodeWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
		{
			XmlNode curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
				{
					return curr;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
		private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
		{
			var curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
				{
					return curr as XmlElement;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		
		public static void UpdateManifest(string fullPath)
		{
			
			XmlDocument doc = new XmlDocument();
			doc.Load(fullPath);
			
			if (doc == null)
			{
				Debug.LogError("Couldn't load " + fullPath);
				return;
			}
			
			XmlNode manNode = FindChildNode(doc, "manifest");
			XmlNode dict = FindChildNode(manNode, "application");
			
			if (dict == null)
			{
				Debug.LogError("Error parsing " + fullPath);
				return;
			}
			
			string ns = dict.GetNamespaceOfPrefix("android");
			
			#region Modify Social Elements 
			//add the facebook activity
			XmlElement facebookActivityElement = FindElementWithAndroidName("activity", "name", ns, FacebookActivityName, dict);
			if (facebookActivityElement == null)
			{
				facebookActivityElement = CreateFBActivityElement(doc, ns);
				dict.AppendChild(facebookActivityElement);
			}
			
			//add the app id
			//<meta-data android:name="com.facebook.sdk.ApplicationId" android:value="\ 409682555812308" />
			XmlElement appIdElement = FindElementWithAndroidName("meta-data", "name", ns, ApplicationIdMetaDataName, dict);
			if (appIdElement == null)
			{
				appIdElement = doc.CreateElement("meta-data");
				appIdElement.SetAttribute("name", ns, ApplicationIdMetaDataName);
				dict.AppendChild(appIdElement);
			}
			appIdElement.SetAttribute("value", ns, "\\ " + GMOSetting.FacebookAppID); // stupid hack to be string format

			//add the fb app link
			//<meta-data android:name="FacebookAppLinkUrl" android:value="your_facebook_app_link" />
			XmlElement appLinkElement = FindElementWithAndroidName("meta-data", "name", ns, ApplicationLinkMetaDataName, dict);
			if (appLinkElement == null)
			{
				appLinkElement = doc.CreateElement("meta-data");
				appLinkElement.SetAttribute("name", ns, ApplicationLinkMetaDataName);
				dict.AppendChild(appLinkElement);
			}
			appLinkElement.SetAttribute("value", ns, GMOSetting.FacebookAppLinkUrl); 
			
			//add the TwitterConsumerKey
			//<meta-data android:name="com.gmo.gamesdk.twitter.consumer.key" android:value="YOUR_CONSUMER_KEY" />
			XmlElement TwitterKeyElement = FindElementWithAndroidName("meta-data", "name", ns, TwitterKeyMetaDataName, dict);
			if (TwitterKeyElement == null)
			{
				TwitterKeyElement = doc.CreateElement("meta-data");
				TwitterKeyElement.SetAttribute("name", ns, TwitterKeyMetaDataName);
				dict.AppendChild(TwitterKeyElement);
			}
			TwitterKeyElement.SetAttribute("value", ns, "" + GMOSetting.TwitterConsumerKey); 
			
			//add the TwitterConsumerSecret
			//<meta-data android:name="com.gmo.gamesdk.twitter.consumer.secret" android:value="YOUR_SECRET_KEY" />
			XmlElement TwitterSecretElement = FindElementWithAndroidName("meta-data", "name", ns, TwitterSecretMetaDataName, dict);
			if (TwitterSecretElement == null)
			{
				TwitterSecretElement = doc.CreateElement("meta-data");
				TwitterSecretElement.SetAttribute("name", ns, TwitterSecretMetaDataName);
				dict.AppendChild(TwitterSecretElement);
			}
			TwitterSecretElement.SetAttribute("value", ns, "" + GMOSetting.TwitterConsumerSecret); 
			#endregion
			
			#region GMO Meta Data Elements
			// Add Apikey 
			XmlElement GMOApikeyElement = FindElementWithAndroidName("meta-data", "name", ns, GMOApikeyMetaDataName, dict);
			if (GMOApikeyElement == null)
			{
				GMOApikeyElement = doc.CreateElement("meta-data");
				GMOApikeyElement.SetAttribute("name", ns, GMOApikeyMetaDataName);
				dict.AppendChild(GMOApikeyElement);
			}
			GMOApikeyElement.SetAttribute("value", ns, "" + GMOSetting.InAppApiKey); 
			
			#endregion

			#region GG Play Service
			XmlElement GGPlayServiceElement = FindElementWithAndroidName("meta-data", "name", ns, "com.google.android.gms.version", dict);
			if (GGPlayServiceElement == null)
			{
				GGPlayServiceElement = doc.CreateElement("meta-data");
				GGPlayServiceElement.SetAttribute("name", ns, "com.google.android.gms.version");
				dict.AppendChild(GGPlayServiceElement);
			}
			GGPlayServiceElement.SetAttribute("value", ns, "" + "7571000"); 

			#endregion
			
			// Creating Elements is depend on which SDKs was used 
			if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null && System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				CreateGMOElements(doc, dict, ns);
				CreateOnClanElements(doc, dict, ns);
			}
			else if (System.Type.GetType("GMOSDKHandler,Assembly-CSharp") != null) {
				CreateGMOElements(doc, dict, ns);
				RemoveOnClanElements(dict, ns);
			}
			else if (System.Type.GetType("OnClanSDKHandler,Assembly-CSharp") != null) {
				CreateOnClanElements(doc, dict, ns);
				RemoveGMOElements(dict, ns);
			}
			
			Debug.Log("Complete setting manifest!!!");

			if (GMOSetting.UsingAppFlyer)
				CreateAppsflyerElements (doc, dict, ns);
			else
				RemoveAppsflyerElements (dict, ns);

			doc.Save(fullPath);
		}
		
		#region Social Elements
		private static XmlElement CreateFBActivityElement(XmlDocument doc, string ns)
		{
			//<activity android:name="com.facebook.LoginActivity" android:theme="@android:style/Theme.Translucent.NoTitleBar" android:label="@string/app_name" />
			XmlElement activityElement = doc.CreateElement("activity");
			activityElement.SetAttribute("name", ns, FacebookActivityName);
			activityElement.SetAttribute("configChanges", ns, "keyboard|keyboardHidden|screenLayout|screenSize|orientation");
			activityElement.SetAttribute("theme", ns, "@android:style/Theme.Translucent.NoTitleBar");
			activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
			return activityElement;
		}
		#endregion
		
		#region GMOSDK Activity Elements 
		private static void CreateGMOElements(XmlDocument doc, XmlNode dict, string ns)
		{
			XmlElement GMOBaseActivityElement = FindElementWithAndroidName("activity", "name", ns, GMOBaseActivityName, dict);
			if (GMOBaseActivityElement == null)
			{
				GMOBaseActivityElement = CreateBaseActivityElement(doc, ns);
				dict.AppendChild(GMOBaseActivityElement);
			}
		}
		
		private static void RemoveGMOElements(XmlNode dict, string ns)
		{
			XmlElement GMOBaseActivityElement = FindElementWithAndroidName("activity", "name", ns, GMOBaseActivityName, dict);
			if (GMOBaseActivityElement != null)
			{
				dict.RemoveChild((XmlNode)GMOBaseActivityElement);
			}
		}
		
		private static XmlElement CreateBaseActivityElement(XmlDocument doc, string ns)
		{
			//<activity android:name="com.gmo.gamesdk.UserActivity" android:configChanges="orientation|keyboardHidden|screenSize" android:launchMode="singleTask" android:theme="@style/Theme.GMO.GameSDK" android:windowSoftInputMode="adjustPan">
			XmlElement activityElement = doc.CreateElement("activity");
			activityElement.SetAttribute("name", ns, GMOBaseActivityName);
			activityElement.SetAttribute("configChanges", ns, "orientation|keyboardHidden|screenSize");
			activityElement.SetAttribute("theme", ns, "@android:style/Theme.Dialog");
			activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
			return activityElement;
		}
		
		#endregion
		
		#region OnClan Activity Elements 
		private static void CreateOnClanElements(XmlDocument doc, XmlNode dict, string ns)
		{
			XmlElement OnClanActivityElement = FindElementWithAndroidName("activity", "name", ns, OnClanActivityName, dict);
			if (OnClanActivityElement == null)
			{
				OnClanActivityElement = CreateOnClanActivityElement(doc, ns);
				dict.AppendChild(OnClanActivityElement);
			}
			
			XmlNode OnClanLoginActivityElement = FindNodeWithAndroidName("activity", "name", ns, OnClanLoginActivityName, dict);
			if (OnClanLoginActivityElement == null)
			{
				OnClanLoginActivityElement = CreateOnClanLoginActivityElement(doc, ns);
				dict.AppendChild(OnClanLoginActivityElement);
			}
			
			XmlElement OnClanChatServiceElement = FindElementWithAndroidName("service", "name", ns, OnClanChatServiceName, dict);
			if (OnClanChatServiceElement == null)
			{
				OnClanChatServiceElement = CreateOnClanChatServiceElement(doc, ns);
				dict.AppendChild(OnClanChatServiceElement);
			}
		}
		
		private static void RemoveOnClanElements(XmlNode dict, string ns)
		{
			XmlElement OnClanActivityElement = FindElementWithAndroidName("activity", "name", ns, OnClanActivityName, dict);
			if (OnClanActivityElement != null)
			{
				dict.RemoveChild((XmlNode)OnClanActivityElement);
			}
			
			XmlNode OnClanLoginActivityElement = FindNodeWithAndroidName("activity", "name", ns, OnClanLoginActivityName, dict);
			if (OnClanLoginActivityElement != null)
			{
				dict.RemoveChild(OnClanLoginActivityElement);
			}
			
			XmlElement OnClanChatServiceElement = FindElementWithAndroidName("service", "name", ns, OnClanChatServiceName, dict);
			if (OnClanChatServiceElement != null)
			{
				dict.RemoveChild((XmlNode)OnClanChatServiceElement);
			}
		}
		
		private static XmlElement CreateOnClanActivityElement(XmlDocument doc, string ns)
		{
			//<activity android:name="com.onclan.android.core.OnClanActivity" android:configChanges="orientation|keyboardHidden|screenSize" android:theme="@android:style/Theme.Dialog">
			XmlElement activityElement = doc.CreateElement("activity");
			activityElement.SetAttribute("name", ns, OnClanActivityName);
			activityElement.SetAttribute("configChanges", ns, "orientation|keyboardHidden|screenSize");
			activityElement.SetAttribute("theme", ns, "@android:style/Theme.Dialog");
			activityElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
			return activityElement;
		}
		
		private static XmlNode CreateOnClanLoginActivityElement(XmlDocument doc, string ns)
		{
			XmlNode activityNode = doc.CreateNode("element", "activity", "");
			
			XmlAttribute attr = doc.CreateAttribute("name", ns);
			attr.Value = OnClanLoginActivityName;
			activityNode.Attributes.SetNamedItem(attr);
			
			XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");
			
			XmlElement actionElement = doc.CreateElement("action");
			actionElement.SetAttribute("name", ns, "com.onclan.android.sdk.login");
			
			XmlElement categoryElement = doc.CreateElement("category");
			categoryElement.SetAttribute("name", ns, "android.intent.category.DEFAULT");
			
			XmlElement dataElement = doc.CreateElement("data");
			dataElement.SetAttribute("host", ns, "sdk");
			dataElement.SetAttribute("scheme", ns, "onclan");
			
			intentFilterNode.AppendChild(actionElement);
			intentFilterNode.AppendChild(categoryElement);
			intentFilterNode.AppendChild(dataElement);
			
			activityNode.AppendChild(intentFilterNode);
			return activityNode;
		}
		
		private static XmlElement CreateOnClanChatServiceElement(XmlDocument doc, string ns)
		{
			//<service android:name="com.onclan.android.chat.mqtt.ChatService" />
			XmlElement serviceElement = doc.CreateElement("service");
			serviceElement.SetAttribute("name", ns, OnClanChatServiceName);
			serviceElement.InnerText = "\n    ";  //be extremely anal to make diff tools happy
			return serviceElement;
		}
		#endregion
		
		#region AppFlyers Elements 
		private static void CreateAppsflyerElements(XmlDocument doc, XmlNode dict, string ns)
		{
			XmlNode AppsflyerElement = FindNodeWithAndroidName("receiver", "name", ns, AppsFlyerReceiverName, dict);
			if (AppsflyerElement == null)
			{
				AppsflyerElement = CreateAppFlyerReceiverElement(doc, ns);
				dict.AppendChild(AppsflyerElement);
			}

			XmlNode AppsflyerRemoveReceiver = FindNodeWithAndroidName("receiver", "name", ns, "com.appsflyer.AppsFlyerLib", dict);
			if (AppsflyerRemoveReceiver == null)
			{
				AppsflyerRemoveReceiver = CreateAppFlyerRemoveReceiverElement(doc, ns);
				dict.AppendChild(AppsflyerRemoveReceiver);
			}

			XmlElement AppsFlyerKey = FindElementWithAndroidName("meta-data", "name", ns, "com.gmo.appsflyerKey", dict);
			if (AppsFlyerKey == null)
			{
				AppsFlyerKey = doc.CreateElement("meta-data");
				AppsFlyerKey.SetAttribute("name", ns, "com.gmo.appsflyerKey");
				dict.AppendChild(AppsFlyerKey);
			}
			AppsFlyerKey.SetAttribute("value", ns, "" + GMOSetting.AppFlyerKey); 
		}
		
		private static void RemoveAppsflyerElements(XmlNode dict, string ns)
		{
			XmlElement AppsflyerElement = FindElementWithAndroidName("receiver", "name", ns, AppsFlyerReceiverName, dict);
			if (AppsflyerElement != null)
			{
				dict.RemoveChild((XmlNode)AppsflyerElement);
			}
		}

		private static XmlNode CreateAppFlyerReceiverElement(XmlDocument doc, string ns)
		{
			XmlNode activityNode = doc.CreateNode("element", "receiver", "");
			
			XmlAttribute attr = doc.CreateAttribute("name", ns);
			attr.Value = "com.appsflyer.MultipleInstallBroadcastReceiver";
			activityNode.Attributes.SetNamedItem(attr);
			
			XmlAttribute attrExporter = doc.CreateAttribute("exported", ns);
			attrExporter.Value = "true";
			activityNode.Attributes.SetNamedItem(attrExporter);
			
			XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");
			
			XmlElement actionElement = doc.CreateElement("action");
			actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
			
			intentFilterNode.AppendChild(actionElement);
			
			activityNode.AppendChild(intentFilterNode);
			return activityNode;
		}

		private static XmlNode CreateAppFlyerRemoveReceiverElement(XmlDocument doc, string ns)
		{
			XmlNode activityNode = doc.CreateNode("element", "receiver", "");

			XmlAttribute attr = doc.CreateAttribute("name", ns);
			attr.Value = "com.appsflyer.AppsFlyerLib";
			activityNode.Attributes.SetNamedItem(attr);

			XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");

			XmlElement actionElement = doc.CreateElement("action");
			actionElement.SetAttribute("name", ns, "android.intent.action.PACKAGE_REMOVED");

			intentFilterNode.AppendChild(actionElement);

			XmlElement dataElement = doc.CreateElement("data");
			dataElement.SetAttribute("scheme", ns, "package");

			intentFilterNode.AppendChild(dataElement);

			activityNode.AppendChild(intentFilterNode);
			return activityNode;
		}
		#endregion
		
		#region AdsWorks Elements 
		private static XmlNode CreateAdsWorkReceiverElement(XmlDocument doc, string ns)
		{
			XmlNode activityNode = doc.CreateNode("element", "receiver", "");
			
			XmlAttribute attr = doc.CreateAttribute("name", ns);
			attr.Value = "com.google.ads.conversiontracking.InstallReceiver";
			activityNode.Attributes.SetNamedItem(attr);
			
			XmlAttribute attrExporter = doc.CreateAttribute("exported", ns);
			attrExporter.Value = "true";
			activityNode.Attributes.SetNamedItem(attrExporter);
			
			XmlNode intentFilterNode = doc.CreateNode("element", "intent-filter", "");
			
			XmlElement actionElement = doc.CreateElement("action");
			actionElement.SetAttribute("name", ns, "com.android.vending.INSTALL_REFERRER");
			
			intentFilterNode.AppendChild(actionElement);
			
			activityNode.AppendChild(intentFilterNode);
			return activityNode;
		}
		#endregion
	}

}