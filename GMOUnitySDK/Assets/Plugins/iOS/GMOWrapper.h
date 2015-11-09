//
//  GMOWrapper.h
//  GMOGameUnitySDK
//
//  Created by tutv on 03/09/14
//
//

#import <UIKit/UIKit.h>
#import "GMOSDK/GMOSDK.h"

@interface GMOWrapper : NSObject<GMOGameSDKCallback>{
    
}

+ (id) sharedInstance;

@property (readwrite) char *wrapperPaymentState;

@end
