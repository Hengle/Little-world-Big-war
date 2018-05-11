using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物体基本属性
/// </summary>
public enum AI_State
{
    Move,
    Attack,
    Stand
}

public class AllGameobject:MonoBehaviour
{

    private float hp;
    private float mp;
    private float attack;
    private float defense;
    private float speed;

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public float MP
    {
        get { return mp; }
        set { mp = value; }
    }

    public float Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    
}
