using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 创建方法
/// </summary>
public class CreateFunction : MonoBehaviour
{
    public static CreateFunction _instance;
    public List<GameObject> allGameobject;
    private Transform GameManager;
    public float stop;

    void Awake()
    {
        _instance = this;
        allGameobject = new List<GameObject>();
        GameManager = GameObject.Find("GameobjectPool").GetComponent<Transform>();
    }

    /// <summary>
    /// 建造方法
    /// </summary>
    /// <param name="m_GameObject"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator IE_CreateItem(GameObject obj, float time,Vector3 CreatePostion)
    {
        Debug.Log("Create");
        yield return new WaitForSeconds(time);
        CreateItem(obj, CreatePostion);
    }

    /// <summary>
    /// 创建小兵
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="CreatePostion"></param>
    /// <returns></returns>
    public GameObject CreateItem(GameObject obj,Vector3 CreatePostion)
    {
        GameObject m_gameobject = GameObject.Instantiate(obj, CreatePostion, Quaternion.identity, GameManager);
        m_gameobject.AddComponent<NavMeshAgent>();
        m_gameobject.AddComponent<Player_AI>();
        objAddList(m_gameobject);
        m_gameobject.GetComponent<NavMeshAgent>().stoppingDistance =stop;
        m_gameobject.name = obj.name + allGameobject.Count.ToString();

        return m_gameobject;
    }

    /// <summary>
    /// 将对象添加进集合中
    /// </summary>
    /// <param name="obj"></param>
    public void objAddList(GameObject obj)
    {
        GameObject m_gameobject = obj;
        allGameobject.Add(obj);
        CatchFunction._instance.L_Nav.Add(m_gameobject.GetComponent<NavMeshAgent>());
    }
}
