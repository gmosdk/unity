# GMO SDK

## Upgrade Unity SDK
> **Important**: Remove all files, folders in `Unity Project/Library` before upgrade SDK. Some cache files contain string `Appota`.

- Please import new `GMOUnitySDK.unitypackage` file to upgrade
- Class `AppotaSDKHandler` has been changed to `GMOSDKHandler`
- Class `AppotaSDKReciever` has been changed to `GMOSDKReciever`
- Class `AppotaSetting` changed to `GMOSetting`
- Class `AppotaSession` changed to `GMOSession`
- ID settings menu: `Appota/Configurations` changed to `GMO/Configurations` 
- Framework `AppotaSDK.framework` changed to `GMOSDK.framework` (iOS)
- Bundle `AppotaBundle.bundle` changed to `GMOBundle.bundle` (iOS)

## Developer project
- Need to rename all variable with name `Appvn` and `Appota` to `GMO`
