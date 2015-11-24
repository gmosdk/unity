using System.Collections;
using SimpleJSON;

public class GMOSession {
	
	private string _gmoAccessToken;
	private string _gmoEmail;	
	private string _gmoExpireDate;	
	private string _gmoRefreshToken;	
	private string _gmoUserId;	
	private string _gmoUsername;

	private static GMOSession _instance;

	public GMOSession(string gmoSession) {
		if (string.IsNullOrEmpty (gmoSession)) {
			InitializeDefault();
		} else {
			var json = JSON.Parse(gmoSession);
		
			if (json["accessToken"] != null && json["userId"] != null) {
				_gmoAccessToken = json["accessToken"].Value;
				_gmoEmail = json["email"].Value;
				_gmoExpireDate = json["expireDate"].Value;
				_gmoRefreshToken = json["refreshToken"].Value;
				_gmoUserId = json["userId"].Value;
				_gmoUsername = json["username"].Value;
			} 
			else {
					InitializeDefault ();
			}
		}
	}

	public GMOSession(){
		InitializeDefault ();
	}

	private void InitializeDefault(){
		_gmoAccessToken = "";
		_gmoEmail = "";
		_gmoExpireDate = "";
		_gmoRefreshToken = "";
		_gmoUserId = "";
		_gmoUsername = "";
	}
	
	// Singleton GMOSession
	public static GMOSession Instance
	{
		get
		{
			if(_instance == null) _instance = new GMOSession();
			return _instance;
		}
	}

	public void UpdateInstance(GMOSession instance) {
		this._gmoAccessToken = instance.AccessToken;
		this._gmoEmail = instance.Email;
		this._gmoExpireDate = instance.ExpireDate;
		this._gmoRefreshToken = instance.RefreshToken;
		this._gmoUserId = instance.UserID;
		this._gmoUsername = instance.UserName;
	}


	public string AccessToken
	{
		get { return _gmoAccessToken; }
		set
		{
			if (_gmoAccessToken != value)
			{
				_gmoAccessToken = value;
			}
		}
	}
	
	public string Email
	{
		get { return _gmoEmail; }
		set
		{
			if (_gmoEmail != value)
			{
				_gmoEmail = value;
			}
		}
	}
	
	public string ExpireDate
	{
		get { return _gmoExpireDate; }
		set
		{
			if (_gmoExpireDate != value)
			{
				_gmoExpireDate = value;
			}
		}
	}
	
	public string RefreshToken
	{
		get { return _gmoRefreshToken; }
		set
		{
			if (_gmoRefreshToken != value)
			{
				_gmoRefreshToken = value;
			}
		}
	}
	
	public string UserID
	{
		get { return _gmoUserId; }
		set
		{
			if (_gmoUserId != value)
			{
				_gmoUserId = value;
			}
		}
	}
	
	public string UserName
	{
		get { return _gmoUsername; }
		set
		{
			if (_gmoUsername != value)
			{
				_gmoUsername = value;
			}
		}
	}
}
