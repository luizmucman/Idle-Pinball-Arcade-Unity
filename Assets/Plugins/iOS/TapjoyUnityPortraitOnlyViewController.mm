#import "TapjoyUnityPortraitOnlyViewController.h"

@interface TapjoyUnityPortraitOnlyViewController ()
@end

@implementation TapjoyUnityPortraitOnlyViewController

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
