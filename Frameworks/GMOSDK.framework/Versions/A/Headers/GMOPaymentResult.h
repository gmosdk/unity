//
//  GMOPaymentResult.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/19/14.
//
//

#import <Foundation/Foundation.h>

#import "GMOBaseObject.h"

@interface GMOPaymentResult : GMOBaseObject
- (float)getAmountPaymentResult;
- (NSString *)transactionID;
- (NSString *)type;
- (NSString *)currency;
- (NSString *)time;
- (NSString *)packageID;
//Payment Apple
- (NSString *)appleProductID;
@end
