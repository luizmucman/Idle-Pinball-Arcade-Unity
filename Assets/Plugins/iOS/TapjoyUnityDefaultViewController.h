#import <UIKit/UIKit.h>
#import <Tapjoy/Tapjoy.h>
#import "UnityViewControllerBase.h"

@interface TapjoyUnityDefaultViewController : UnityDefaultViewController <TJCTopViewControllerProtocol>
@property (nonatomic, assign) BOOL canRotate;
@end
