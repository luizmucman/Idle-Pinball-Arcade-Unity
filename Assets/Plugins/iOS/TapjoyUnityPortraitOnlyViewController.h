#import <UIKit/UIKit.h>
#import <Tapjoy/Tapjoy.h>
#import "UnityViewControllerBase.h"

@interface TapjoyUnityPortraitOnlyViewController : UnityPortraitOnlyViewController <TJCTopViewControllerProtocol>
@property (nonatomic, assign) BOOL canRotate;
@end
