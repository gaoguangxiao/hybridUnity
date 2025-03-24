// [!] important set UnityFramework in Target Membership for this file
// [!]           and set Public header visibility

#import <Foundation/Foundation.h>

typedef void (*CallbackDelegate)(const char*);

// NativeCallsProtocol defines protocol with methods you want to be called from managed
@protocol NativeCallsProtocol
@required

- (void) showHostMainWindow:(NSString*)color;
// other methods
//unity send native
- (void)sendMessageToNaticve:(NSString *)body;

///注册回调
- (void)reiegerBridge:(CallbackDelegate)callback;

@end

__attribute__ ((visibility("default")))
@interface FrameworkLibAPI : NSObject
// call it any time after UnityFrameworkLoad to set object implementing NativeCallsProtocol methods
+(void) registerAPIforNativeCalls:(id<NativeCallsProtocol>) aApi;

@end


