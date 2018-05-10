using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 选取与移动
/// </summary>
public class CatchFunction : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    public static CatchFunction _instance;
    public bool isCtach = false;
    public bool catchMove = false;
    public bool isSingle = false;
    public bool isF2 = false;
    public NavMeshAgent Nav;
    public List<NavMeshAgent> C_Nav;//抓取并临时存储的Nav
    public List<NavMeshAgent> F2_Nav;//F2存储Nav

    void Awake()
    {
        _instance = this;
        Nav = null;
        F2_Nav = new List<NavMeshAgent>();
        C_Nav = new List<NavMeshAgent>();
    }

    void Update()
    {
        //F2();
    }

    /// <summary>
    /// 单个选取
    /// </summary>
    public void Catch()
    {
        hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (hit.collider.tag == "Player")
        {
            isSingle = true;
            isCtach = false;
            catchMove = false;
            isF2 = false;
            Nav = hit.collider.GetComponent<NavMeshAgent>();
        }

    }

    /// <summary>
    /// 全体抓取
    /// </summary>
    public void AllCatch()
    {
        //Debug.Log(CreateFunction._instance.L_Nav.Count);
        for (int i = 0; i < CreateFunction._instance.L_Nav.Count; i++)
        {
            F2_Nav.Add(CreateFunction._instance.L_Nav[i]);
            isCtach = false;
            isSingle = false;
            catchMove = false;
            isF2 = true;

            //高亮显示
        }
    }

    /// <summary>
    /// 单个移动
    /// </summary>
    public void SingleMove()
    {
        if (hit.collider.tag == "Land" && Nav != null)
        {
            Nav.SetDestination(hit.point);
        }
    }

    /// <summary>
    /// 全体移动
    /// </summary>
    public void F2Move()
    {
        for (int i = 0; i < F2_Nav.Count; i++)
        {
            F2_Nav[i].SetDestination(hit.point);
        }
    }

    public void CatchMove()
    {
        for (int i = 0; i < C_Nav.Count; i++)
        {
            C_Nav[i].SetDestination(hit.point);
        }
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
