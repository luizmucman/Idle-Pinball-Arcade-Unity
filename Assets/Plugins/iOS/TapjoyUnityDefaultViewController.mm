#import "TapjoyUnityDefaultViewController.h"

@interface TapjoyUnityDefaultViewController ()
@end

@implementation TapjoyUnityDefaultViewController

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
