#import <Foundation/Foundation.h>
#import "NativeCallProxy.h"


@implementation FrameworkLibAPI

id<NativeCallsProtocol> api = NULL;

+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi
{
    api = aApi;
}

@end


extern "C" {
void showHostMainWindow(const char* color) {
    return [api showHostMainWindow:[NSString stringWithUTF8String:color]];
}

void didReceiveMessage(char *body) {
    [api sendMessageToNaticve:[NSString stringWithUTF8String:body]];
}
void registerCallBackDelegate(CallbackDelegate callback) {
    [api reiegerBridge:callback];
}
}

