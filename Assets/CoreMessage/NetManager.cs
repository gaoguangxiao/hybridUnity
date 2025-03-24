using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//接口地址

//存储主App域名
public class NetManager: ManagerBase
{
    private static NetManager instance;

    public static NetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetManager();
                MC.Instance.Register(instance);
            }
            return instance;
        }
    }

    //用户信息
    public static string PathDress = "/wap/api/buddy/dress";

    //https://gw.risekid.cn/wap/api/verification-code
    //https://gateway-test.risekid.cn
    //private string HostURL = "https://gw.risekid.cn";
    private string HostURL = "https://gateway-test.risekid.cn";

    public string GetHost()
    {
        return HostURL;
    }

    public void SetHost(string host)
    {
        HostURL = host;
    }

    private string WebHostURL = "https://qa.risekid.cn";

    public string GetWebHost()
    {
        return WebHostURL;
    }

    public void SetWebHost(string host)
    {
        WebHostURL = host;
    }

    //token信息
    private string Token;
    public string GetToken()
    {
        return Token;
    }

    public void SetToken(string token)
    {
        Token = token;
    }

    public override int GetMessageType()
    {
        return MessageType.Type_UI;
    }
}
