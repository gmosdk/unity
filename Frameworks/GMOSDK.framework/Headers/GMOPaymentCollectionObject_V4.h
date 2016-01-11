//
//  GMOSDKPayment_V4.h
//  GMOSDK
//
//  Created by Tue Nguyen on 07.11.14.
//
//

#import "GMOBaseObject.h"

typedef enum {
    PAYMENT_APPLE = 0,
    PAYMENT_METHOD_INVALID,
} PAYMENT_METHOD;

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
@end
