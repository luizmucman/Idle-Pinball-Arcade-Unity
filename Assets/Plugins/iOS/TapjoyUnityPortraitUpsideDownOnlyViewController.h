#import <UIKit/UIKit.h>
#import <Tapjoy/Tapjoy.h>
#import "UnityViewControllerBase.h"

// Currently our iOS SDK does not support Portrait Upside Down as the only orientation.
// This class should help with futureproofing if we decide to revisit that behavior.
@interface TapjoyUnityPortraitUpsideDownOnlyViewController : UnityPortraitUpsideDownOnlyViewController <TJCTopViewControllerProtocol>
@property (nonatomic, assign) BOOL canRotate;
@end
