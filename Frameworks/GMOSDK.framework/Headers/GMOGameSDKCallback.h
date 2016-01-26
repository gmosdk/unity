//
//  GMOSDKCallback.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/18/14.
//
//

#import <Foundation/Foundation.h>
#import "GMOPaymentResult.h"
#import "GMOUserLoginResult.h"

@protocol GMOGameSDKCallback <NSObject>

@optional
/*
 * Callback after close loginview
 */
- (void) didCloseLoginView;
/*
 * Callback when login error
 */
- (void) didLoginErrorWithMessage:(NSString *)message withError:(NSError *)error;
/*
 * Callback when payment error
 */
- (void) didPaymentErrorWithMessage:(NSString *)message withError:(NSError *)error;
/*
 * Callback when close Payment View
 */
- (void) didClosePaymentView;
/*
 * Callback update userInfo
 */
- (void) didUpdateUserInfo:(GMOUserLoginResult *)userLoginResult;

/*
 * Callback when Payment success
 */
- (void) didPaymentSuccessWithResult:(GMOPaymentResult*) paymentResult withPackage:(NSString *) packageID;


@required
/**
 *  Get payment state base on GMOPaymentPackage
 *
 *  @return PAYMENT_STATE
 */
- (NSString*) getPaymentStateWithPackageID:(NSString *) packageID;

/*
 * Callback after login
 */
- (void) didLoginSuccess:(GMOUserLoginResult*) userLoginResult;

/*
 * Callback after logout
 */
- (void) didLogOut:(NSString*) userName;
@end