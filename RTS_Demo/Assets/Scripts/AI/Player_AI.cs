using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FSM状态机
/// </summary>
public class Player_AI : AIBehaviour
{
    public bool alive;

    private AI_State state;

    void Start()
    {
        alive = true;
        state = AI_State.Stand;
        StartCoroutine(FSM());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            state = AI_State.Attack;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            state = AI_State.Move;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            state = AI_State.Stand;
        }
    }

    /// <summary>
    /// FSM有限状态机
    /// </summary>
    /// <returns></returns>
    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case AI_State.Attack:
                    State_Attack();
                    break;
                case AI_State.Move:
                    State_Move();
                    break;
                case AI_State.Stand:
                    State_Stand();
                    break;
            }          
            yield return null;
        }
    }

    public override void State_Attack()
    {
        print("我正在攻击");
    }

    public override void State_Move()
    {
        print("我正在移动");
    }

    public override void State_Stand()
    {
        //print("我正在站立");
    }
}
