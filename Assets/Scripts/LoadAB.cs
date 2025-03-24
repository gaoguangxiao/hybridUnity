using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoadAB : MonoBase
{
    GameObject demoOBj;

    public Text proText;
    // Start is called before the first frame update
    void Start()
    {

        NetManager.Instance.Register(this);

        //异步加载AB包的资源
        StartCoroutine(LoadABRes("centerobj", "obj"));

        //LoadSy("centerobj", "obj");
        //从远端资源下载
        //StartCoroutine(LoadWebABRes("https://iosfile.risekid.cn/static/centerobj", "obj"));
    }

    private void OnDestroy()
    {
        NetManager.Instance.UnRegister(this);
    }

    public override void ReceiveMessage(Message message)
    {
        Debug.Log("CheckUpdate ReceiveMessage：" + message.Command);
        if (message.Command == MessageType.loadPkg)
        {
            //Debug.Log("loadpkg complete：" + message.Data);
            proText.text = "更新完毕";
            //资源重新加载
            RereshRES();
        }
        else if (message.Command == "checkUpdate")
        {
            //计算mes的进度
            ResponseLoadPkg bridge = JsonConvert.DeserializeObject<ResponseLoadPkg>(message.ResponseObject.data);
            float pro = (float)bridge.loadedSize / bridge.totalSize * 100;
            proText.text = "更新进度:" + pro.ToString("F2") + "%";
        }
        //Debug.Log(message.Command);

    }

    //重新读取ab资源
    public void RereshRES()
    {
        //刷新时，卸载当前资源
        UNloadAS();
        //销毁当前展示
        GameObject obj = GameObject.FindGameObjectWithTag("demoObj");
        if(obj != null)
        {
            Destroy(obj);
        }
        //刷新当前资源
        //StartCoroutine(LoadWebABRes("https://iosfile.risekid.cn/static/centerobj", "obj"));

        StartCoroutine(LoadABRes("centerobj", "obj"));
    }

    //刷新loadpkg资源
    public void RereshWebRES()
    {
        //调用下载接口
        Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
        paramsDicts.Add("type", "1");
        paramsDicts.Add("item", "config.json");
        paramsDicts.Add("progressAction", "checkUpdate");
        Message message = new Message(MessageType.Type_plug, MessageType.loadPkg, paramsDicts);
        BridgeScript.Instance.CallApp(message);
    }

    //通过访问本地文件异步加载AB包的资源
    IEnumerator LoadABRes(string ABName, string rename)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string temporaryCachePath = Application.temporaryCachePath;
        string webResourcePath = Path.Combine(temporaryCachePath, "WebResource");
        // 确保目录存在
        if (!Directory.Exists(webResourcePath))
        {
            Directory.CreateDirectory(webResourcePath);
        }
        string hotUpdatePath = webResourcePath + "/pkg/static/1";
        Debug.Log("hotUpdatePath：" + hotUpdatePath);
        AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(hotUpdatePath + "/" + ABName);
#else
        AssetBundleCreateRequest ab = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
#endif
        Debug.Log("ab is" + ab);
        yield return ab;
        //加载资源-指定资源类型
        //AssetBundleRequest abq = ab.assetBundle.LoadAssetAsync(rename, typeof(GameObject));
        //yield return abq;
        //Instantiate(abq.asset);

        GameObject cu = ab.assetBundle.LoadAsset<GameObject>(rename);
        cu.tag = "demoObj";
        Instantiate(cu);

    }

    void UNloadAS()
    {
        //卸载所有加载的ab包，false，ab卸载但资源还在
        AssetBundle.UnloadAllAssetBundles(false);
    }

    //从服务器加载ab包资源
    IEnumerator LoadWebABRes(string url, string rename)
    {

        //UnityWebRequestAssetBundle.
        //string uri = @"http://localhost/AssetBundles\model.ab";
        //`UnityWebRequestAssetBundle`是一个用来加载`AssetBundle`的类，`AssetBundle`允许将游戏资源打包成单独的文件，可以减少初始化加载时间
        //UnityWebRequestAssetBundle request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
            if (ab != null)
            {
                GameObject cu = ab.LoadAsset<GameObject>(rename);
                cu.tag = "demoObj";
                Debug.Log("cu is" + cu);
                //
                Instantiate(cu);
            }
        }


    }
    //从内存中加载
    void LoadMemory()
    {
        AssetBundle ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(Application.streamingAssetsPath + "/spheret"));
        Debug.Log("ab is" + ab);
        GameObject cu = ab.LoadAsset<GameObject>("Sphere");//
        Debug.Log("cu is" + cu);
        //
        Instantiate(cu);
    }


    //通过访问本地文件同步加载AB包的资源
    void LoadSy(string ABName, string rename)
    {
        //第一步加载AB包，通过文件路径加载
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + ABName);
        Debug.Log("LoadFromFile ab is" + ab);

        //第二步，加载AB包中的资源
        GameObject cu = ab.LoadAsset<GameObject>(rename);//
        cu.tag = "demoObj";
        Debug.Log("cu is" + cu);
        //
        Instantiate(cu);

    }


    // Update is called once per frame
    void Update()
    {

    }
}
