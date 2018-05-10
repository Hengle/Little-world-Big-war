using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class ObjPool : MonoBehaviour
{
    public static ObjPool _instance;

    private Dictionary<string, GameObject> objPool;//物体池
    private Dictionary<int, GameObject> l_obj;//子对象池
    private int obj_Index;//池子计数器
    private int l_obj_Index;//取对象计数器

    void Awake()
    {
        obj_Index = 1;
        l_obj_Index = 1;
        _instance = this;
        objPool = new Dictionary<string, GameObject>();
        l_obj = new Dictionary<int, GameObject>();
    }

    /// <summary>
    /// 获取物体
    /// </summary>
    /// <param name="obj_name"></param>
    /// <param name="time"></param>
    public void GetObj(string obj_name, float time)
    {
        GameObject obj = Resources.Load<GameObject>(obj_name);
        GameObject obj_active;
        //如果没有物体池创建物体池以及物体
        if (objPool.ContainsKey(obj_name) == false)
        {
            //Debug.Log(objPool.ContainsKey(obj_name));
            //StartCoroutine(CreateFunction._instance.IE_CreateItem(obj, time));
            obj_active = CreateFunction._instance.CreateItem(obj);
            l_obj.Add(obj_Index, obj_active);
            obj_Index++;
            objPool.Add(obj_name, obj_active);
        }
        //有物体池则创建物体
        else
        {
            l_obj.TryGetValue(l_obj_Index, out obj_active);
            //看子对象池是否有可用的且处于为激活状态，如果有则使用
            if (l_obj.ContainsKey(l_obj_Index) && obj_active.activeSelf == false)
            {
                CreateFunction._instance.objAddList(obj_active);
                obj_active.SetActive(true);
                l_obj_Index++;
                //Debug.Log(l_obj_Index);
            }
            //如果没有则创建新的对象并加入对象池
            else
            {
                //StartCoroutine(CreateFunction._instance.IE_CreateItem(obj, time));
                obj_active = CreateFunction._instance.CreateItem(obj);
                l_obj.Add(obj_Index, obj_active);
                obj_Index++;
                l_obj_Index = 1;
                //Debug.Log(l_obj_Index);
            }
        }
        /*
        foreach (KeyValuePair<int, GameObject> pair in l_obj)
        {
            Debug.Log(pair.Key + " " + pair.Value);
        }
        Debug.Log(objPool.ContainsKey(obj_name));
        */
    }
}

