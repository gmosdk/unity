//
//  GMOSDKPayment_V4.h
//  GMOSDK
//
//  Created by Tue Nguyen on 07.11.14.
//
//

#import "GMOBaseObject.h"

typedef enum {
    PAYMENT_SMS = 0,
    PAYMENT_PREMIUM_SMS = 1,
    PAYMENT_CARD = 2,
    PAYMENT_INTERNET_BANKING = 3,
    PAYMENT_PAYPAL = 4,
    PAYMENT_UNIPIN = 7,
    PAYMENT_APPLE = 8,
    PAYMENT_EWALLET = 9,
    PAYMENT_METHOD_INVALID = 10,
} PAYMENT_METHOD;

typedef NS_ENUM(NSInteger, SMSTYPE) {
    SMSTYPE_UNKNOW = -1,
    SMSTYPE_MOMT,
    SMSTYPE_OTP,
    SMSTYPE_DIRECT
};

typedef NS_ENUM(NSInteger, EWALLETTYPE){
    EWALLETTYPE_OTP,
    EWALLETTYPE_OTHER
    
};
// Collection of payment object with same payment machenism
@interface GMOPaymentCollectionObject_V4 : GMOBaseObject {
    
}

- (id) initWithPaymentMethod:(PAYMENT_METHOD) paymentMethod;

- (id) initWithPaymentMethod:(PAYMENT_METHOD)paymentMethod andObjectDictionary:(NSDictionary *)dictionary;

@property (nonatomic, strong) NSArray *listAmount;
@property (readonly) PAYMENT_METHOD paymentMethod;

- (NSString*) getPaymentCollectionImageName;
- (NSString*) getPaymentCollectionName;
- (NSString *)getPaymentCollectionGroupName;
- (id) getVendorName;
- (NSString *) getCurrency;
+(PAYMENT_METHOD) getPaymentMethodFromString:(NSString*) pMethod;
+ (NSString*) getStringFromPaymentMethod:(PAYMENT_METHOD) pMethod;
- (SMSTYPE)getSMSType;
- (EWALLETTYPE) getEwalletType;
- (NSString *) getCardCode;
- (NSString *) getCardSerial;
@end
