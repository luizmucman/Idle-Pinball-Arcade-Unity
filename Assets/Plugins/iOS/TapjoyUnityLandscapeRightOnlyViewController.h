#import <UIKit/UIKit.h>
#import <Tapjoy/Tapjoy.h>
#import "UnityViewControllerBase.h"

@interface TapjoyUnityLandscapeRightOnlyViewController : UnityLandscapeRightOnlyViewController <TJCTopViewControllerProtocol>
@property (nonatomic, assign) BOOL canRotate;
@end
