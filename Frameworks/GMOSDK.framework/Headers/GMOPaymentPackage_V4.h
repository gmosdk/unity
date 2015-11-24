//
//  GMOPaymentPackage_V4.h
//  GMOSDK
//
//  Created by Tue Nguyen on 1/27/15.
//
//

#import "GMOBaseObject.h"
#import "GMOPaymentCollectionObject_V4.h"

typedef enum : NSUInteger {
    GMOGameCurrencyStringType,
    GMOGameCurrencyImageType,
} GMOGameCurrencyType;

@interface GMOGameCurrency : NSObject
@property (readonly) GMOGameCurrencyType type;
@property (nonatomic, strong) NSString *data;
@end

@interface GMOPaymentPackage_V4 : GMOBaseObject
@property (readwrite) PAYMENT_METHOD selectedPaymentMethod;
- (GMOGameCurrency*) getGameCurrency;
- (int) getPackageAmount;
- (NSString*) getMoneyCurrencyWithPaymentMethod:(NSString*) paymentMethod andGroup:(NSString *) group;
- (NSString*) getMoneyAmountWithPaymentMethod:(NSString*) paymentMethod andGroup:(NSString *)group;
- (NSString *)getContryCodeWithPaymentMethod:(NSString *)paymentMethod andGroup:(NSString *)group;
- (NSString *)getGroupNameWithPaymentMethod:(NSString *)paymentMethod;
- (NSArray *) getListPaymentMethod;
-(NSString *)getProductAppleID;

@end
