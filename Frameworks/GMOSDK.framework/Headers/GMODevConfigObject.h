//
//  GMOSDKDevConfigObject.h
//  GMOSDK
//
//  Created by Tue Nguyen on 10.11.14.
//
//

#import "GMOBaseObject.h"

typedef enum {
    GMOLoginNormalMethod,
    GMOQuickLoginMethod,
    GMOLoginSocialMethod,
    GMOLoginFBMethod,
    GMOGoogleMethod,
    GMOTwitterMethod,
    GMOLoginOnclanMethod,
} GMOLoginMethod;

@class GMOPaymentCollectionObject_V4;
@class GMOPaymentPackage_V4;
@class GMOTrackObject;

@interface GMODevConfigObject : GMOBaseObject
// get list login method
- (NSArray*) getListLoginMethod;
// check valid devconfig object :
- (BOOL) isValidDevConfigObject;
// check if has Login method
- (BOOL) isHasLoginMethod:(GMOLoginMethod ) loginMethod;
//get listPayment package
- (NSArray*) getListPaymentPackage;
//get list packageId
- (NSMutableArray *) getListPackageID;
//check valid packageId
- (BOOL) isValidPackageID:(NSString *)packageID;
//get package with packageId
- (GMOPaymentPackage_V4 *) getPackageWithItPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithListPaymentMethods:(NSArray*) listPaymentMethods
                                        withCountryCode:(NSString*) countryCode;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod;

- (BOOL) isValidPaymentMethod:(NSString*) paymentMethod withPackageID:(NSString *)packageID;

- (NSArray*) filterPaymentPackageWithMethod:(NSString*) paymentMethod
                            withCountryCode:(NSString*) countryCode;

- (NSArray*) filterPackageWithPaymentCollection:(GMOPaymentCollectionObject_V4*) paymentCollection andGMOTrackObject:(GMOTrackObject *) trackObject;

- (NSArray*) filterPackageWithPaymentCollection:(GMOPaymentCollectionObject_V4*) paymentCollection  andGMOTrackObject:(GMOTrackObject *)trackObject withPackageID:(NSString *)packageID;

@end
