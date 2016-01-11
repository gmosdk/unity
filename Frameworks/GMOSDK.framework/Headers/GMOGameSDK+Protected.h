//
//  GMOGameSDK+Protected.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/20/14.
//
//

#import "GMOGameSDK.h"
#import "GMOTrackObject.h"
#import "GMOUserLoginResult.h"
#import "GMODevConfigObject.h"
@interface GMOGameSDK (Protected)
@property(readonly, strong)  GMOTrackObject *trackObject;
@property(readonly, strong)  GMOUserLoginResult *userLoginResultObject;
@property(readonly, strong)  GMODevConfigObject *paymentConfigObject;
@property(nonatomic, assign) BOOL isHideWelcomeView;
@property(assign, nonatomic) BOOL isKeepLoginSession;
@property (readwrite) BOOL autoShowPaymentButton;
@property (assign, nonatomic) BOOL autoShowLoginDialog;
@property (assign, nonatomic) BOOL isHidePaymentView;
@property (assign, nonatomic) BOOL isHideLogo;
@property (nonatomic, readonly) int fbInviteState;

@end
