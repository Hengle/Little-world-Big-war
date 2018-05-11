using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位控制器
/// </summary>
public class GameObjectController : MonoBehaviour
{
    public bool isBeCatch;//是否为被选状态
    public Transform aureole;

    void Start()
    {
        isBeCatch = false;
        aureole = GameObject.Find(gameObject.name + "/aureole").GetComponent<Transform>();
    }


    void Update()
    {
        if (isBeCatch == false)
        {
            aureole.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            aureole.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
