using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// FSM状态机
/// </summary>
public class Player_AI : AIBehaviour
{
    public int precision;
    public bool alive;
    public AI_State state;

    private NavMeshAgent m_Nav;
    private Ray ray;
    private RaycastHit hit;
    private Transform m_transform;
    private float m_distance;
    public bool isMove;

    void Start()
    {
        precision = 12;
        alive = true;
        state = AI_State.Stand;
        StartCoroutine(FSM());
        m_Nav = gameObject.GetComponent<NavMeshAgent>();
        m_transform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        m_distance = Vector3.Distance(transform.position, m_Nav.destination);
        if (m_distance < m_Nav.stoppingDistance)
        {
            state = AI_State.Stand;
            isMove = false;
        }
        Attack_Check();
    }

    /// <summary>
    /// FSM有限状态机
    /// </summary>
    /// <returns></returns>
    IEnumerator FSM() 
    {
        while (alive && gameObject.tag == "Player_1")
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
        isMove = true;
        print("我正在移动");
    }

    public override void State_Stand()
    {
        print("我正在站立");
    }

    private void Attack_Check()
    {
        float subAngle = (360 / 2) / precision;
        for (int i = 0; i < 360 / precision; i++)
        {
            ray = new Ray(m_transform.position, (Quaternion.Euler(0, -1 * subAngle * (i + 1), 0)) * m_transform.forward);
            //Debug.DrawRay(m_transform.position, (Quaternion.Euler(0, -1 * subAngle * (i + 1), 0)) * m_transform.forward.normalized*360,Color.red);
            if (Physics.Raycast(ray, out hit, 5f))
            {
                if (gameObject.tag == "Player_1")
                {
                    if (hit.collider.tag == "Player_2"&&isMove==false)
                    {
                        state = AI_State.Attack;
                    }
                }
            }
        }
    }
}
