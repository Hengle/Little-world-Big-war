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
/// <summary>
/// AI行为抽象类
/// </summary>
public abstract class AIBehaviour:MonoBehaviour
{ 
    public abstract void State_Move(); 
    public abstract void State_Attack();
    public abstract void State_Stand();
}
