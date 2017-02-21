//
//  GMOGameSDK+Advance.h
//  GMOSDK
//
//  Created by Hieu on 12/25/14.
//
//

#import "GMOGameSDK.h"
#import "GMOSDKConst.h"
@class UIViewController;
typedef void (^GMOSDKUserLoginResultObjectBlock) (GMOUserLoginResult *object);
@class GMOPaymentCollectionObject_V4;
@interface GMOGameSDK (Advance)
/** *
 *  get list payment object

 *  @return NSArray
 */
+ (NSArray*) getListPaymentObject;
/**
 *  invite facebook friends
 * @param :resultBlock : success result
 * @param : errorBlock : error Result
 */
+ (void)inviteFacebookFriendsWithCompleteBlock:(GMOSDKDictionaryBlock) resultBlock
                                 andErorrBlock:(GMOSDKErrorBlock) errorBlock
__attribute__ ((deprecated("use inviteFacebookFriendsWithCompleteBlock:andErorrBlock:fromViewController: instead")));

/**
 *  invite facebook friends from a view controller
 * @param : resultBlock : success result
 * @param : errorBlock : error Result
 * @param: view Controller to present SFSafariViewController (only iOS 9 and later)
 */
+ (void)inviteFacebookFriendsWithCompleteBlock:(GMOSDKDictionaryBlock) resultBlock
                                 andErorrBlock:(GMOSDKErrorBlock) errorBlock
                            fromViewController:(UIViewController *)viewController;
/**
 *  Show facebook login without GMO login view use Delegate
 */
+ (void) showFacebookLogin
__attribute__ ((deprecated("use showFacebookLoginFromViewController: instead")));

/**
 *  Show facebook login without GMO login view use Delegate from A view controller
 * @param: view Controller to present SFSafariViewController (only iOS 9 and later)
 */
+(void) showFacebookLoginFromViewController:(UIViewController *)viewController;

/**
 *  Show facebook Login without GMO Login View use Block
 *`
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showFacebookLoginWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                              andErrorBlock:(GMOSDKErrorBlock) errorBlock
__attribute__ ((deprecated("useshowFacebookLoginWithCompleteBlock:andErrorBlock:fromViewController: instead")));

/**
 *  Show facebook Login without GMO Login View use Block from a view controller
 *`
 *  @param completeBlock
 *  @param errorBlock
* @param: view Controller to present SFSafariViewController (only iOS 9 and later)
 */
+ (void) showFacebookLoginWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                              andErrorBlock:(GMOSDKErrorBlock) errorBlock
                         fromViewController:(UIViewController *) viewController;
/**
 *  Show facebook Login without GMO Login View with permissions use Block
 *
 *  @param permissions
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showFacebookLoginWithPermissions:(NSArray *)permissions
                     andWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(GMOSDKErrorBlock) errorBlock
__attribute__ ((deprecated("showFacebookLoginWithPermissions:andWithCompleteBlock:andErrorBlock:fromViewController: instead")));

/**
 *  Show facebook Login without GMO Login View with permissions use Block
 *
 *  @param permissions
 *  @param completeBlock
 *  @param errorBlock
 * @param: view Controller to present SFSafariViewController (only iOS 9 and later)
 */
+ (void) showFacebookLoginWithPermissions:(NSArray *)permissions
                     andWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(GMOSDKErrorBlock) errorBlock
                       fromViewController:(UIViewController *) viewController;
/**
 *  show google login without GMO Login View use Delegate
 */
+ (void) showGoogleLogin;
/**
 *  show google login without GMO Login View use Block
 *
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showGoogleLoginWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                            andErrorBlock:(GMOSDKErrorBlock) errorBlock;
/**
 *  show Twitter login without GMO Login view use Delegate
 */
+ (void) showTwitterLogin;
/**
 *  Show Twitter login without GMO Login view use Block
 *
 *  @param completeBlock
 *  @param errorBlock
 */
+ (void) showTwitterLoginWithCompleteBlock:(GMOSDKUserLoginResultObjectBlock) completeBlock
                             andErrorBlock:(GMOSDKErrorBlock) errorBlock;
/**
 *  Login GMO without GMO Login View with username and password  use Delegate
 *
 *  @param userName
 *  @param passWord
 */
+ (void) loginGMOWithUsername:(NSString *) userName
                        passWord:(NSString *) passWord;
/**
 *  Login GMO  without GMO Login View with username and password use block
 *
 *  @param userName
 *  @param passWord
 *  @param completionBlock
 *  @param errorBlock
 */
+ (void) loginGMOWithUsername:(NSString *) userName
                        passWord:(NSString *) passWord
             withCompletionBlock:(GMOSDKUserLoginResultObjectBlock ) completionBlock
                   andErrorBlock:(GMOSDKErrorBlock ) errorBlock;
/**
 *  register GMO  without GMO Login View use Delegate for Callback success
 *
 *  @param userName
 *  @param passWord
 *  @param email
 */
+ (void) registerGMOWithUsername:(NSString *) userName
                           passWord:(NSString *) passWord
                           andEmail:(NSString *) email;
/**
 *  Register GMO   without GMO Login View use Block for Callback succees
 *
 *  @param userName
 *  @param passWord
 *  @param email
 *  @param coml
 */
+ (void) registerGMOWithUsername:(NSString *) userName
                           passWord:(NSString *) passWord
                           andEmail:(NSString *) email
                   withComleteBlock:(GMOSDKUserLoginResultObjectBlock) completionBlock
                      andErrorBlock:(GMOSDKErrorBlock ) errorBlock;
/**
 *  quick login without GMO Loginview use Delegate for callback success
 */
+ (void) quickLogin;
/**
 *  Quick login without GMO loginView use Blocking
 *
 *  @param comletionBlock
 *  @param errorBlock
 */
+ (void) quickLoginWithComleteBlock:(GMOSDKUserLoginResultObjectBlock) comletionBlock
                     andErrorBlock:(GMOSDKErrorBlock) errorBlock;

@end
