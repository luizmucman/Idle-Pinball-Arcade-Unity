#import "TapjoyUnityPortraitUpsideDownOnlyViewController.h"

@interface TapjoyUnityPortraitUpsideDownOnlyViewController ()
@end

@implementation TapjoyUnityPortraitUpsideDownOnlyViewController

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
