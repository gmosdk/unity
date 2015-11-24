//
//  GMOLanguageObject.h
//  GMOSDK
//
//  Created by Tue Nguyen on 11/26/14.
//
//

#import "GMOBaseObject.h"

@interface GMOLanguageObject : GMOBaseObject
- (NSString*) getLanguageName;

- (NSString*) getLanguageFile;

- (NSString*) getFlagURL;

- (NSString*) getLanguageID;

@end
