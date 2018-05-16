using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 移动方法
/// </summary>
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
    public void Move(RaycastHit hit)
    {
            if (CatchFunction._instance.isF2 == true)
            {
                CatchFunction._instance.F2Move(hit);
            }
            if (CatchFunction._instance.catchMove == true)
            {
                CatchFunction._instance.CatchMove(hit);
            }
            if (CatchFunction._instance.isSingle == true)
            {
                CatchFunction._instance.SingleMove(hit);
            }
    }

}
