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
        Move();
    }

    public void createItem()
    {
            ObjPool._instance.GetObj("Cube", 2f);        
    }

    /// <summary>
    /// 移动方法Demo
    /// </summary>
    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            //CreateFunction.instance.F2(hit.point);
        }
    }

}
