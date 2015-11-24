//
//  GMOGameSDK+Internal.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/20/14.
//
//

#import "GMOGameSDK.h"
#import "GMOSDKConst.h"
/**
 State of GMOGameSDK to provide correct GUI
 */
//#ifdef GMOGameSDK_BUILD_ANE
//#ifdef __cplusplus
//extern "C" {
//#endif
//    typedef void *      FREContext;
//#ifdef __cplusplus
//}
//#endif
//#endif

typedef enum {
    GMOGameSDKNormalState,
    GMOGameSDKUserLoginState,
    GMOGameSDKLoadingState
} GMOGameSDKState;
@class GMOTrackObject;
@interface GMOGameSDK (Internal)

#pragma mark - Getter function
+ (GMOGameSDKState) getGMOGameSDKState;

+ (NSString*) getGMOAPIKey;

+ (NSString*) getGoogleClientID;

+ (NSString*) getGoogleClientSecret;

+ (NSString*) getTwitterConsumerKey;

+ (NSString*) getTwitterConsumerSecret;

+ (NSString*) getPaymentTarget;

+ (NSString*) getValidDNS;
/**
 *  Get list payment - return list of GMOSDKPaymentCollection object
 *  Parsed with JSON value and payment config option
 *  @return list of GMOSDKPaymentCollection
 */
+ (NSArray*) getListPayment;

//#ifdef GMOGameSDK_BUILD_ANE
//+ (FREContext )getANEContext;
//+ (void) setANEContext:(FREContext )context;
//#endif

/**
 *  Get list payment - return list of GMOSDKPaymentCollection object
    with packageID
 *  Parsed with JSON value and payment config option
 *  @return list of GMOSDKPaymentCollection
 */
+ (NSArray *) getListPaymentWithPackageID:(NSString *)packageID;
#pragma mark - Init function
/**
 *  Load remote config (from track and json config all at once and then update them to database)
 */
+ (void) loadRemoteConfig;

+ (void) getAndSetTrackObjectWithCompletionBlock:(GMOSDKObjectBlock) completionBlock
                                  withErrorBlock:(GMOSDKErrorBlock) errorBlock;

// Init with trackobjet function
- (void) initAfterRetrieveFirstTrackObject:(GMOTrackObject*) trackObject;
//show check update
- (void) checkUpdateWithDict:(NSDictionary *)dictionary;
/**
 *
 *  Handle facebook url in AppDelegate
 *  @param url open url callback
 */
+ (void) handleFacebookOpenURL:(NSURL*) url;

+ (void) handleGMOOpenURL:(NSURL*) url;

/**
 *  Error stacktrace function
 */
- (void) addErrorStackTrace;
@end
