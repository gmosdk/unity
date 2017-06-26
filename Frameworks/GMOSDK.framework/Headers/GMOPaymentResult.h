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
- (float) amount;
- (NSString *)currency;
- (NSString *)purchaseTime;
- (NSString *)productID;
- (NSString *)packageName;
@end
