using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 创建管理器
/// </summary>
public class CreateManager : MonoBehaviour
{
    public static CreateManager _instance;

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {

    }

    public void createItem(string item_Name, Vector3 create_Postion)
    {
        ObjPool._instance.GetObj(item_Name, 2f, create_Postion);
    }
}
