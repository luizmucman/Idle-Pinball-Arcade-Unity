#import <UIKit/UIKit.h>
#import <Tapjoy/Tapjoy.h>
#import "UnityViewControllerBase.h"

@interface TapjoyUnityLandscapeLeftOnlyViewController : UnityLandscapeLeftOnlyViewController <TJCTopViewControllerProtocol>
@property (nonatomic, assign) BOOL canRotate;
@end
