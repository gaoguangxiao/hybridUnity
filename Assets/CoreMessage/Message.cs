using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Message
{

    //类型：UI、角色、音频、网络
    public int Type;

    //命令
    public string Command;

    //参数
    public string Content;

    //参数
    public Dictionary<string, object> Data;

    //构造
    public Message() {}

    //仅回调的全部数据
    public BridgeResponseObject ResponseObject;

    public Message(int type, string command, string content)
    {
        Type = type;
        Command = command;
        Content = content;
        //Data = JsonConvert.DeserializeObject(content);
    }

    /// <summary>
    /// 将类型，命令，请求参数转化为消息体，请求参数内部JSON
    /// </summary>
    /// <param name="type"></param>
    /// <param name="command"></param>
    /// <param name="dict"></param>
    public Message(int type, string command, Dictionary<string, object> dict)
    {
        Type = type;
        Command = command;
        Content = JsonConvert.SerializeObject(dict);
        Data = dict;
    }

    /// <summary>
    /// 用于单词的响应数据
    /// </summary>
    /// <param name="content"></param>
    public Message(string content)
    {
        Content = content;
    }
}

public class MessageType {
    //类型
    public static int Type_Net = 1;
    public static int Type_UI = 2;
    public static int Type_plug = 3;//插件功能

    //权限
    //public static string Command_StartPermissions = "startPermissions";

    ////配置聊天
    //public static string Command_SwitchChatConfigInfo = "switchChatConfigInfo";

    ////语音识别命令
    //public static string Command_StartRecognition = "startRecognition";
    //public static string Command_StopRecognition  = "stopRecognition";
    //public static string Command_CancelRecognition = "cancelRecognition";
    //public static string Command_RecognitionEvent = "recognitionEvent";//识别结果

    ////音频播放
    public static string Command_PlayAudio = "playAudio";
    //public static string Command_StopAudio = "stopAudio";
    //public static string Command_PlayPlayback = "playBack";
    //public static string Command_StopPlayback = "stopPlayBack";
    //public static string Command_PlayAssetsAudio = "playAssetsAudio";
    //public static string Command_AudioEvent = "audioEvent";
    //public static string Command_PlayBackEvent = "playBackEvent";//响应

    ////GPT回复
    //public static string Command_ConfigChat = "configChat";
    //public static string Command_SendChatMessage = "sendMessage";
    //public static string Command_EndChat = "endChat";
    //public static string Command_ChatReplyEvent = "chatReplyEvent";//响应Coze

    ////语言合成
    //public static string Command_StartSynthesis = "starSpeechSynthesis";
    //public static string Command_StopSynthesis = "stopSynthesis";
    //public static string Command_SynthesisEvent = "synthesisEvent";//响应

    
    //内部脚本通信
    public static string UI_RefreshData = "UI_RefreshData"; //刷新用户信息
    //public static string Net_Token = "broadcast"; //登录信息

    //UI命令
    public static string UI_ChatList = "chatList"; //聊天列表
    public static string UI_AiAnimator = "UI_AiAnimator"; //动画

    //Unity通信
    public static string UI_UnityClose = "UnityClose";//Unity关闭

    //网络
    public static string getStorage = "getStorage";//Unity

    //资源更新
    public static string loadPkg = "loadPkg";//Unity
}