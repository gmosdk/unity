//
//  GMOSDKTrackObject.h
//  GMOSDK
//
//  Created by Hieu on 8/4/14.
//
//

#import "GMOBaseObject.h"

#define kGMO_CARRIER_ALL @"GMO_CARRIER_ALL"

typedef NS_ENUM(NSInteger, GMOCountryCode) {
    GMOCountryCodeVN,
    GMOCountryCodeIndonesia,
    GMOCountryCodeUnknown
};

@class GMOLanguageObject;
@interface GMOTrackObject : GMOBaseObject
//get country code
- (NSString*) getCountryCode;
//get country code type
- (GMOCountryCode) getCountryCodeType;
// validate track object
- (BOOL) isValidTrackObject;
//hide head profile
- (BOOL) isHideHeadProfile;
//check isDebugmode
- (BOOL) isDebugMode;
//check enable active code
- (BOOL) isEnableActiveCode;
// get GA tracking id
- (NSString*) getGATrackingID;
//get pid
- (NSString*) getPID;
// get list paymentMethod name
- (NSArray*) getListPaymentMethodName;

/**
 *
 *
 *  @return list of payment config
 */
- (NSArray*) getPaymentConfigList;
/**
 *  @return Payment config URL config on developer portal
 */
- (NSString*) getPaymentConfigURL;

/**
 *
 *
 *  @return List GMOLanguageObject
 */
- (NSArray*) getListLanguageObject;

/**
 *
 *
 *  @param languageID
 *
 *  @return GMOLanguageObject
 */
- (GMOLanguageObject*) getLanguageObjectWithID:(NSString*) languageID;

/**
 *  Gameserver dict
 *
 *  @return game server dictionary
 */
- (NSDictionary*) getGameServerDict;
/**
 *  get Carrier name from CountryCode Type
 *
 *  @return carrier Name
 */
- (NSString *) getCarrierNameFromCountryCodeType;

/**
 *  List app to verify called in feedback function
 *
 *  @return list verify app
 */
- (NSArray*) getVerifyApps;
@end
