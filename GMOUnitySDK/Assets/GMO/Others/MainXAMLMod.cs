using UnityEngine;
using System.IO;
using System.Xml;

namespace GMO
{
	public class MainXAMLMod : MonoBehaviour {
		
		const string stringHeader = "using System;\nusing APTCallback; \nusing APTPaymentResult;\nusing APTPaymentService;";
		const string stringImplClass = @"MainPage : PhoneApplicationPage, GMOSDKCallback
    {";
		const string stringFunctionInit = @"
			SetupGeolocator();
            SDKInit();
        }";
		const string stringParagraph = 
			@"
		
		GMOGameSDK gameSDK;

        private void SDKInit()
        {
  			GMOSDKHandler.Instance._Init += _GMOSDKHandler__InitSDK;
            GMOSDKHandler.Instance._Logout += _GMOSDKHandler__Logout;
            GMOSDKHandler.Instance._MakePayment += _GMOSDKHandler__MakePayment;
            GMOSDKHandler.Instance._ShowLoginView += _GMOSDKHandler__ShowLoginView;
            GMOSDKHandler.Instance._ShowUserInfo += _GMOSDKHandler__ShowUserInfo;
            GMOSDKHandler.Instance._SwitchAccount += _GMOSDKHandler__SwitchAccount;
            GMOSDKHandler.Instance._SetAutoShowLogin += _GMOSDKHandler__SetAutoShowLogin;
        }

  		private void _GMOSDKHandler__InitSDK()
        {

   			Dispatcher.BeginInvoke(() =>
            {
				if (gameSDK == null) gameSDK = new GMOGameSDK
             	(
	                 GMOSetting.InAppApiKey,
	                 GMOSetting.SandboxApiKey,
	                 GMOSetting.IsUsingSandbox,
	                 GMOSetting.NoticeURL,
	                 GMOSetting.ConfigURL,
	                 GMOSetting.FacebookAppID,
	                 GMOSetting.FacebookAppSecretID,
	                 GMOSetting.GoogleClientId,
	                 GMOSetting.GoogleClientSecretId,
	                 GMOSetting.TwitterConsumerKey,
	                 GMOSetting.TwitterConsumerSecret,
	                 this
             	);
            });
           
        }


        private void _GMOSDKHandler__SwitchAccount()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.SwitchAccount();
            });
        }

        void _GMOSDKHandler__SetAutoShowLogin(bool obj)
        {
 			Dispatcher.BeginInvoke(() =>
            {
 				gameSDK.SetAutoShowLogin(obj);
			});
        }

        void _GMOSDKHandler__ShowUserInfo()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.ShowUserInfo();
            });
        }

        void _GMOSDKHandler__ShowLoginView()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.ShowLogin();

            });
        }

        void _GMOSDKHandler__MakePayment()
        {
            Dispatcher.BeginInvoke(() =>
            {
                gameSDK.MakePayment();
            });
        }

        void _GMOSDKHandler__Logout()
        {
            gameSDK.LogoutAccount();
        }

        //callback
        public void onPaymentError(string message)
        {
        }

        public void onPaymentSuccess(TransactionResult result)
        {
            GMOSDKReceiver.Instance.OnPaymentSuccess(result.getAmount());
        }

        public void onUserLoginError(string message)
        {
        }

        public void onUserLoginSuccess(UserLoginResult result)
        {
            GMOSDKReceiver.Instance.OnLoginSuccess(result.toJson());
        }

        public void onUserRegisterError(string message)
        {
        }

        public void onUserRegisterSuccess(UserLoginResult result)
        {
        }

        public void OnSwitchAccountSuccess(UserLoginResult result)
        {
        }
	}
}";
		
		public static void UpdateMainPageXAML(string path){
			
			var reader = new StreamReader(path);
			
			if (reader == null){
				Debug.Log("Can not find MainPage.xaml.xml in path: " + path);
				return;
			}
			
			string text = reader.ReadToEnd();
			reader.Close();
			
			if(text.IndexOf("SDKInit();", System.StringComparison.Ordinal) > 0){
				return;
			}
			
			int indexHeaderStart = text.IndexOf("using", System.StringComparison.Ordinal);
			int indexHeaderEnd = text.IndexOf(";", indexHeaderStart);
			
			string fixedText = text.Substring(0, indexHeaderStart);
			fixedText += stringHeader;
			fixedText += text.Substring(indexHeaderEnd + 1);
			
			int indexImplClassStart = fixedText.IndexOf("MainPage", System.StringComparison.Ordinal);
			int indexImplClassEnd = fixedText.IndexOf("{", indexImplClassStart);
			
			string fixedText1 = fixedText.Substring(0, indexImplClassStart);
			fixedText1 += stringImplClass;
			fixedText1 += fixedText.Substring(indexImplClassEnd + 1);
			
			int indexSDKInitStart = fixedText1.IndexOf("SetupGeolocator();", System.StringComparison.Ordinal);
			int indexSDKInitSEnd = fixedText1.IndexOf("}", indexSDKInitStart);
			
			string fixedText2 = fixedText1.Substring(0, indexSDKInitStart);
			fixedText2 += stringFunctionInit;
			fixedText2 += fixedText1.Substring(indexSDKInitSEnd + 1);
			
			string fixedText3 = fixedText2.Substring(0, fixedText2.Length - 4);
			fixedText3 += stringParagraph;
			
			var writer = new StreamWriter(path, false);
			writer.Write(fixedText3);
			Debug.Log ("text 3: " + fixedText3);
			
			writer.Close();
		}
	}
}