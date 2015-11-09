AppotaGameSDK iOS Update for iOS9
====

# Please wait till stable version for iOS9 SDK to update, use XCode 6 for now is better solution

## Preparing Your Apps for iOS9
If you are building your Apps with iOS SDK 9.0 (`XCode7`). iOS 9 introduces changes that are likely to impact your app and `AppotaGameSDK` integration . This guide will review actions you should take to ensure the best app experience when using the AppotaGameSDK for iOS.


## Whitelist SDK servers for Network Requests
You will be affected by [App Transport Security](https://developer.apple.com/library/prerelease/ios/technotes/App-Transport-Security-Technote/). Accept all API

```
<key>NSAppTransportSecurity</key>
<dict>
  <key>NSAllowsArbitraryLoads</key>
      <true/>
</dict>
```

## Bitcode and App Thinning
The iOS 9 SDK offers [App Thinning](https://developer.apple.com/library/prerelease/watchos/documentation/IDEs/Conceptual/AppDistributionGuide/AppThinning/AppThinning.html) which includes compiling bitcode.

In the the past we did not include bitcode and you had to disable it in your app in your build settings or with the `ENABLE_BITCODE=NO` compiler flag. As of v4.6.0 we now include bitcode so you can either explicitly enable it to use it or leave it disabled.

## Whitelist URL schemes
Accept `url schemes` to support `canOpenURL` function for `FacebookSDK` login

```
<key>LSApplicationQueriesSchemes</key>
<array>
  <string>fbapi</string>
  <string>fb-messenger-api</string>
  <string>fbauth2</string>
  <string>fbshareextension</string>    
</array>

```
