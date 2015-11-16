#import "GMOWrapper.h"
#import "GMOSDK/GMOSDK.h"
#import <FBSDKShareKit/FBSDKShareKit.h>
#import <FBSDKLoginKit/FBSDKLoginKit.h>
#import <FBSDKCoreKit/FBSDKCoreKit.h>

@interface GMOWrapper()
+ (NSMutableDictionary *) unpackToDictionary: (NSString *) parameters;
@end

@implementation GMOWrapper

static GMOWrapper* sharedMyInstance = nil;

+ (id) sharedInstance {
    @synchronized(self) {
        if( sharedMyInstance == nil )
            sharedMyInstance = [[super allocWithZone:NULL] init];
    }
    return sharedMyInstance;
} // end sharedInstance()


+ (NSMutableDictionary *) unpackToDictionary: (NSString *) parameters {
    NSMutableDictionary *dictionary = [[NSMutableDictionary alloc] init];
    NSArray *pairs = [parameters componentsSeparatedByString:@";"];
    
    for (NSString *str in pairs) {
        NSArray *contentPair = [str componentsSeparatedByString:@":"];
        [dictionary setValue:[contentPair objectAtIndex:1] forKey:[contentPair objectAtIndex:0]];
    }
    
    return dictionary;
}

extern "C" {
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        
        return res;
    }
    
    // SDK Functions
    const void init() {
        [GMOGameSDK configure];
        [GMOGameSDK sharedInstance].delegate = [GMOWrapper sharedInstance];
    }
    
    const void setSDKButtonVisibility(bool isVisible) {
        [[GMOGameSDK sharedInstance] setSDKButtonVisibility:isVisible];
    }
    
    const void setAutoShowLogin(bool autoShowLogin) {
        [[GMOGameSDK sharedInstance] setAutoShowLoginDialog: autoShowLogin];
    }
    
    const void setHideWelcomeView(bool property) {
        [[GMOGameSDK sharedInstance] setHideWelcomeView: property];
    }

    const void setHidePaymentView(bool property) {
        [[GMOGameSDK sharedInstance] setHidePaymentView: property];
    }
    
    const void setKeepLoginSession(bool isKeepLoginSession) {
        [[GMOGameSDK sharedInstance] setKeepLoginSession:isKeepLoginSession];
    }
    
    const void inviteFacebookFriends(){
        [GMOGameSDK inviteFacebookFriendsWithCompleteBlock:^(NSDictionary *dict) {
            
        } andErorrBlock:^(NSError *error) {
            
        }];
    }
    
    // User Functions
    const void showUserInfoView(){
        [GMOGameSDK showUserInfoView];
    }
    
    const void showLoginView(){
        [GMOGameSDK showLoginView];
    }
    
    const void showGoogleLogin(){
        [GMOGameSDK showGoogleLogin];
    }
    
    const void showFacebookLogin(){
        [GMOGameSDK showFacebookLogin];
    }
    
    const void showFacebookLoginWithPermissions(const char *permissions){
        NSString *_permissions = [NSString stringWithUTF8String:permissions];
        NSArray *permissionArray = [_permissions componentsSeparatedByString:@"|"];
        [GMOGameSDK showFacebookLoginWithPermissions:permissionArray andWithCompleteBlock:^(GMOUserLoginResult *object) {
            
        } andErrorBlock:^(NSError *error) {
            
        }];
    }
    
    const void showTwitterLogin(){
        [GMOGameSDK showTwitterLogin];
    }
    
    const void showRegisterView(){
        [GMOGameSDK showRegisterView];
    }
    
    const void switchAccount(){
        [GMOGameSDK switchAccount];
    }
    
    const void showTransactionHistory(){
        [GMOGameSDK showTransactionHistory];
    }
    
    const void logout(){
        [GMOGameSDK logOut];
    }
    
    const bool isUserLoggedIn(){
        return [GMOGameSDK isUserLoggedIn];
    }
    
    const char* getUserInfo(){
        GMOUserLoginResult *userLoginResult = [GMOGameSDK getUserInfo];
        NSString *emptyString = @"";
        if (!userLoginResult.accessToken) {
            return [emptyString UTF8String];
        }
        NSString *email = userLoginResult.email ? userLoginResult.email : @"";
        NSString *json = @"{";
        json = [json stringByAppendingString:@"\"accessToken\":\""];
        json = [json stringByAppendingString:userLoginResult.accessToken];
        json = [json stringByAppendingString:@"\","];
        json = [json stringByAppendingString:@"\"email\":\""];
        json = [json stringByAppendingString:email];
        json = [json stringByAppendingString:@"\","];
        json = [json stringByAppendingString:@"\"userId\":\""];
        json = [json stringByAppendingString:userLoginResult.userID];
        json = [json stringByAppendingString:@"\","];
        json = [json stringByAppendingString:@"\"username\":\""];
        json = [json stringByAppendingString:userLoginResult.userName];
        json = [json stringByAppendingString:@"\","];
        json = [json stringByAppendingString:@"}"];
        
        return [json UTF8String];
    }
    
    // Payment Functions
    const void showPaymentView(){
        [GMOGameSDK showPaymentView];
    }
    
    const void showPaymentViewWithPackageID(const char *packageID){
        [GMOGameSDK showPaymentViewWithPackageID:[NSString stringWithUTF8String:packageID]];
    }
    
    const void sendStateToWrapper(const char *state){
        sharedMyInstance.wrapperPaymentState = cStringCopy(state);
    }
    
    const void setCharacter(const char *name, const char *characterID, const char *serverName, const char *serverID){
        [GMOGameSDK setCharacterWithCharacterName: [NSString stringWithUTF8String:name] characterID:[NSString stringWithUTF8String:characterID] serverName:[NSString stringWithUTF8String:serverName] serverID:[NSString stringWithUTF8String:serverID] onCompleteBlock:^(NSDictionary *dict) {
            
        } onErrorBlock:^(NSError *error) {
            
        }];
    }
    
    const void closePaymentView(){
        [GMOGameSDK closePaymentView];
    }
    
    // Track Functions
    const void sendEventWithCategoryWithValue(const char *category, const char *action, const char *label, int value){
        [GMOGameSDK sendEventWithCategory:[NSString stringWithUTF8String:category] withEventAction:[NSString stringWithUTF8String:action] withLabel:[NSString stringWithUTF8String:label] withValue:value];
        
    }
    
    const void sendEventWithCategory(const char *category, const char *action, const char *label){
        [GMOGameSDK sendEventWithCategory:[NSString stringWithUTF8String:category] withEventAction:[NSString stringWithUTF8String:action] withLabel:[NSString stringWithUTF8String:label]];
        
    }
    
    const void sendViewWithName(const char *name){
        [GMOGameSDK sendViewWithName:[NSString stringWithUTF8String:name]];
    }
    
    // Notification Functions
    const void registerPushNotificationWithGroupName(const char *name){
        [GMOGameSDK registerPushNotificationWithGroupName:[NSString stringWithUTF8String:name]];
    }
    
    // Facebook AppEvent functions
    const void fbLogEvent(const char *name){
        [FBSDKAppEvents logEvent:[NSString stringWithUTF8String:name]];
    }
    
    const void fbLogEventWithParameter(const char *name, double value, const char *parameters){
        NSDictionary *paramDictionary = [GMOWrapper unpackToDictionary:[NSString stringWithUTF8String:parameters]];
        NSLog(@"FBLogEvent: %@", paramDictionary);
        [FBSDKAppEvents logEvent:[NSString stringWithUTF8String:name] valueToSum:value parameters:paramDictionary];
        
    }
}

#pragma mark - Gameconfig delegate

- (void) didCloseLoginView {
    NSLog(@"Close login view");
    UnitySendMessage("GMOSDKReceiver", "OnCloseLoginView", "");
}

- (NSString*) getPaymentStateWithPackageID:(NSString *) packageID {
    return [NSString stringWithFormat:@"%s", sharedMyInstance.wrapperPaymentState];
}

/*
 * Callback after login
 */
- (void) didLoginSuccess:(GMOUserLoginResult*) userLoginResult {
    NSLog(@"Login Success!!!");
    if (!userLoginResult.accessToken) {
        return;
    }
    NSString *email = userLoginResult.email ? userLoginResult.email : @"";
    NSString *json = @"{";
    json = [json stringByAppendingString:@"\"accessToken\":\""];
    json = [json stringByAppendingString:userLoginResult.accessToken];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"email\":\""];
    json = [json stringByAppendingString:email];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"userId\":\""];
    json = [json stringByAppendingString:userLoginResult.userID];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"username\":\""];
    json = [json stringByAppendingString:userLoginResult.userName];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"}"];
    
    UnitySendMessage("GMOSDKReceiver", "OnLoginSuccess", [json UTF8String]);
    
    [FBSDKAppEvents logEvent:@"GMO_mobile_complete_login"];
}

/*
 * Callback when login error
 */
- (void) didLoginErrorWithMessage:(NSString *)message withError:(NSError *)error {
    UnitySendMessage("GMOSDKReceiver", "OnLoginError", [message UTF8String]);
}

/*
 * Callback after logout
 */
- (void) didLogOut:(NSString*) userName {
    NSLog(@"Logout %@", userName);
    NSString *temp = @"";
    if (!userName.length){
        temp = userName;
    }
    
    UnitySendMessage("GMOSDKReceiver", "OnLogoutSuccess", [temp UTF8String]);
}

- (void) didPaymentSuccessWithResult:(GMOPaymentResult*) paymentResult withPackage:(NSString *) packageID {
    if (!packageID.length){
        packageID = @"";
    }

    NSString *json = @"{";
    json = [json stringByAppendingString:@"\"packageID\":\""];
    json = [json stringByAppendingString:packageID];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"amount\":\""];
    json = [json stringByAppendingString:[[NSNumber numberWithFloat:paymentResult.getAmountPaymentResult] stringValue]];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"currency\":\""];
    json = [json stringByAppendingString:paymentResult.currency];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"time\":\""];
    json = [json stringByAppendingString:paymentResult.time];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"transactionId\":\""];
    json = [json stringByAppendingString:paymentResult.transactionID];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"type\":\""];
    json = [json stringByAppendingString:paymentResult.type];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"\"productID\":\""];
    NSString *productID = paymentResult.appleProductID ? paymentResult.appleProductID : @"";
    json = [json stringByAppendingString:productID];
    json = [json stringByAppendingString:@"\","];
    json = [json stringByAppendingString:@"}"];
    
    UnitySendMessage("GMOSDKReceiver", "OnPaymentSuccess", [json UTF8String]);
    
    // Purchase log
    NSString *otherPackageID = @"";
    if (paymentResult.packageID){
        otherPackageID = paymentResult.packageID;
    }
    
    [FBSDKAppEvents logPurchase:[paymentResult getAmountPaymentResult] currency:[paymentResult currency]
                     parameters:@{FBSDKAppEventParameterNameContentType:[paymentResult type],
                                  FBSDKAppEventParameterNameContentID:otherPackageID}];
}

- (void) didPaymentErrorWithMessage:(NSString *)message withError:(NSError *)error {
    UnitySendMessage("GMOSDKReceiver", "OnPaymentFailed", [message UTF8String]);
}

@end
