//
//  GMOUserLoginResult.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/22/14.
//
//

#import "GMOBaseObject.h"

@interface GMOUserLoginResult : GMOBaseObject
- (NSString*) userName;
- (NSString*) userID;
- (NSString*) accessToken;
- (NSString*) email;
- (BOOL) isRequireUpdateInfor;
- (BOOL) isHasEmail;
@end
