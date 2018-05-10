using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI行为抽象类
/// </summary>
public abstract class AIBehaviour : AllGameobject
{
    public abstract void State_Move(); 
    public abstract void State_Attack();
    public abstract void State_Stand();
}
