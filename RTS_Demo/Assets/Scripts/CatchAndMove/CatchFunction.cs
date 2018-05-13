using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 选取与移动
/// </summary>
public class CatchFunction : MonoBehaviour
{
    public static CatchFunction _instance;

    public bool isCtach = false;//是否正在框选
    public bool catchMove = false; //是否处在框选状态
    public bool isSingle = false;//是否处在单个选取状态
    public bool isF2 = false;//是否处在F2选取状态

    public NavMeshAgent Nav;//单个Nav存储
    public List<NavMeshAgent> C_Nav;//抓取并临时存储的Nav
    public List<NavMeshAgent> F2_Nav;//F2存储Nav
    public List<NavMeshAgent> L_Nav;//总Nav
    public List<GameObjectController> C_GOC;

    private GameObjectController m_gameobject;

    void Awake()
    {
        _instance = this;
        Nav = null;
        F2_Nav = new List<NavMeshAgent>();
        C_Nav = new List<NavMeshAgent>();
        L_Nav = new List<NavMeshAgent>();
        C_GOC = new List<GameObjectController>();
    }

    /// <summary>
    /// 单个选取
    /// </summary>
    public void SingleCatch(RaycastHit hit)
    {
        if (C_GOC != null)
        {
            Debug.Log("Close");
            for (int i = 0; i < C_GOC.Count; i++)
            {
                C_GOC[i].isBeCatch = false;
            }
        }
        if (m_gameobject != null)
        {
            m_gameobject.isBeCatch = false;
        }
        //改变所有选取状态
        isSingle = true;
        isCtach = false;
        catchMove = false;
        isF2 = false;
        //获取特效UI组件设置开关
        m_gameobject = hit.collider.GetComponent<GameObjectController>();
        m_gameobject.isBeCatch = true;
        Nav = hit.collider.GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// 全体抓取
    /// </summary>
    public void AllCatch()
    {
        C_GOC.Clear();
        //遍历非战斗单位的Nav并添加进来
        for (int i = 0; i < CatchFunction._instance.L_Nav.Count; i++)
        {
            F2_Nav.Add(CatchFunction._instance.L_Nav[i]);
            m_gameobject = CatchFunction._instance.L_Nav[i].GetComponent<GameObjectController>();
            C_GOC.Add(m_gameobject);
            m_gameobject.isBeCatch = true;
            isCtach = false;
            isSingle = false;
            catchMove = false;
            isF2 = true;
        }
    }

    /// <summary>
    /// 全体移动
    /// </summary>
    public void F2Move(RaycastHit hit)
    {
        for (int i = 0; i < F2_Nav.Count; i++)
        {
            F2_Nav[i].SetDestination(hit.point);
        }
    }

    /// <summary>
    /// 框选移动
    /// </summary>
    public void CatchMove(RaycastHit hit)
    {
        for (int i = 0; i < C_Nav.Count; i++)
        {
            C_Nav[i].SetDestination(hit.point);
        }
    }

    /// <summary>
    /// 单个移动
    /// </summary>
    /// <param name="hit"></param>
    public void SingleMove(RaycastHit hit)
    {
            Nav.SetDestination(hit.point);
    }

    /// <summary>
    /// 神圣的F2
    /// </summary>
    public void F2()
    {
        if (F2_Nav != null)
        {
            F2_Nav.Clear();
        }
        AllCatch();
    }
}
