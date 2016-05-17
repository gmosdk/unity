//
//  GMOSDKConst.h
//  GMOSDK
//
//  Created by Tue Nguyen on 7/1/14.
//  Copyright (c) 2014 DB-Interactive. All rights reserved.
//

#ifndef GMOSDK_GMOSDKConst_h
#define GMOSDK_GMOSDKConst_h

#ifdef __cplusplus
#define GMO_PAYMENT_EXTERN extern "C" __attribute__((visibility ("default")))
#else
#define GMO_PAYMENT_EXTERN extern __attribute__((visibility ("default")))
#endif

#define GMOGameSDK_DEBUG NO

#define GMO_BUILD_UNITY
#define GMO_BUILD_SPECIAL NO
//#define GMOGameSDK_BUILD_ANE

typedef void (^GMOSDKStringBlock)(NSString *returnString);
typedef void (^GMOSDKArrayBlock)(NSArray *list);
typedef void (^GMOSDKDictionaryBlock)(NSDictionary *dict);
typedef void (^GMOSDKBOOLBlock)(BOOL b);
typedef void (^GMOSDKIntBlock)(int t);
typedef void (^GMOSDKVoidBlock) (void);
typedef void (^GMOSDKObjectBlock) (id object);
typedef void (^GMOSDKObjectMessageBlock) (id object, NSString* message);
typedef void (^GMOSDKErrorBlock) (NSError *error);
typedef void (^GMOSDKObjectMessageHandler) (id object, NSError *error , NSString *message);
typedef void (^GMOSDKObjectHandler) (id object, NSError *error);


#define GMOSDK_API_VERSION @"1.0"
#define GMO_DIALOG_PAYMENT_TAG 1430

#define GMOGameSDK_SCHEMA @"gmo"
#define GMOGameSDK_DRAGVIEW_TAG 1428

//
#define GMO_LANGUAGE_KEY [@"a0FwcG90YUxhbmdLZXk=" base64DecodedString]
#define GMOGame_SDK_VERSION @"4.0"
#define GMOGame_SDK_LABEL_VERSION @"4.1.pb15"
#define GMOGame_SDK_BUILD 55
#define GMO_LOGIN_DICT_KEY_SAVED [@"a2V5X2FwcG90YV9sb2dpbl9kaWN0X3NhdmVk" base64DecodedString]

//Debug mode
//#define DEBUG_MODE YES

#define DEBUG_GMO_RESOURCE NO

#define GMOGameSDK_LOG NO


#define GMOGameSDK_IS_IOS6_AND_UP ([[UIDevice currentDevice].systemVersion floatValue] >= 6.0)

#define GMOGameSDK_SYSTEM_VERSION_EQUAL_TO(v)                  ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedSame)
#define GMOGameSDK_SYSTEM_VERSION_GREATER_THAN(v)              ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedDescending)
#define GMOGameSDK_SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(v)  ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] != NSOrderedAscending)
#define GMOGameSDK_SYSTEM_VERSION_LESS_THAN(v)                 ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] == NSOrderedAscending)
#define GMOGameSDK_SYSTEM_VERSION_LESS_THAN_OR_EQUAL_TO(v)     ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] != NSOrderedDescending)


//Device
#define GMOGameSDK_IS_IPAD (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
#define GMOGameSDK_IS_IPHONE (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone)

#define GMO_SCREEN_WIDTH ([[UIScreen mainScreen] bounds].size.width)
#define GMO_SCREEN_HEIGHT ([[UIScreen mainScreen] bounds].size.height)
#define GMO_SCREEN_MAX_LENGTH (MAX(GMO_SCREEN_WIDTH, GMO_SCREEN_HEIGHT))
#define GMO_SCREEN_MIN_LENGTH (MIN(GMO_SCREEN_WIDTH, GMO_SCREEN_HEIGHT))

#define GMOGameSDK_IS_IPHONE_4 (GMOGameSDK_IS_IPHONE && GMO_SCREEN_MAX_LENGTH == 480.0)
#define GMOGameSDK_IS_IPHONE_5 (GMOGameSDK_IS_IPHONE && GMO_SCREEN_MAX_LENGTH == 568.0)
#define GMOGameSDK_IS_IPHONE_6 (GMOGameSDK_IS_IPHONE && GMO_SCREEN_MAX_LENGTH == 667.0)
#define GMOGameSDK_IS_IPHONE_6PLUS (GMOGameSDK_IS_IPHONE && GMO_SCREEN_MAX_LENGTH == 736.0)


//Orientation
#define GMOGameSDK_IS_PORTRAIT ([UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationPortrait || [UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationPortraitUpsideDown)
#define GMOGameSDK_IS_LANDSCAPE ([UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationLandscapeRight || [UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationLandscapeLeft)
#define GMOGameSDK_LANDSCAPE_RIGHT ([UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationLandscapeRight)
#define GMOGameSDK_LANDSCAPE_LEFT ([UIApplication sharedApplication].statusBarOrientation == UIInterfaceOrientationLandscapeLeft)

#endif
