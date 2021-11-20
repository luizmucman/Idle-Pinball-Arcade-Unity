#import "TapjoyUnityLandscapeLeftOnlyViewController.h"

@interface TapjoyUnityLandscapeLeftOnlyViewController ()
@end

@implementation TapjoyUnityLandscapeLeftOnlyViewController

- (id)init{
    self = [super init];
    if (self) {
        _canRotate = YES;
    }
    return self;
}

- (BOOL)shouldAutorotate {
    return _canRotate;
}

@end
