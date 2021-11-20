#import "TapjoyUnityLandscapeRightOnlyViewController.h"

@interface TapjoyUnityLandscapeRightOnlyViewController ()
@end

@implementation TapjoyUnityLandscapeRightOnlyViewController

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
