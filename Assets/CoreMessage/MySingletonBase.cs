using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingletonBase<T> : MonoBase where T:MySingletonBase<T>
{
    public static T Instance;
    // Start is called before the first frame update
    public virtual void Awake()
    {
        //Debug.Log("Awake:" + gameObject.name);
        //判断当前是否存在实例
        if (Instance != null)
        {
            //Debug.Log("Destroy--:" + gameObject.name);
            Destroy(gameObject);
            return;
        }
        //Debug.Log("Awake-Instance:" + gameObject.name);
        Instance = this as T;
        //不要销毁当前实例
        DontDestroyOnLoad(gameObject);
    }

}
