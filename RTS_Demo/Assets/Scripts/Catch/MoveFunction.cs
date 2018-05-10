using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFunction : MonoBehaviour
{
    public static MoveFunction _instance;

    void Awake()
    {
        _instance = this;
    }


    /// <summary>
    /// 移动分类与实现
    /// </summary>
    public void Move()
    {
        Debug.Log("AAA");
        CatchFunction._instance.Catch();
        if (CatchFunction._instance.isF2 == true)
        {
            CatchFunction._instance.F2Move();
        }
        if(CatchFunction._instance.isSingle==true)
        {
            CatchFunction._instance.SingleMove();
        }
        if (CatchFunction._instance.catchMove == true)
        {
            CatchFunction._instance.CatchMove();
        }
    }
}
