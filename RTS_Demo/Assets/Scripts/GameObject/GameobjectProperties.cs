using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameobjectProperties 
{
    private string GO_name;
    private float hp;
    private float mp;
    private float attack;
    private float defense;
    private float speed;
    private float price;
    private float gas;
    private float attackDistatnce;


    public string GO_Name
    {
        get { return GO_name; }
        set { GO_name = value; }
    }

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

    public float Price
    {
        get { return price; }
        set { price = value; }
    }

    public float Gas
    {
        get { return gas; }
        set { gas = value; }
    }

    public float AttackDistance
    {
        get { return attackDistatnce; }
        set { attackDistatnce = value; }
    }

    public GameobjectProperties() { }

    public GameobjectProperties(string GO_name, float hp, float mp, float attack, float defense, float speed, float price, float gas, float attackDistance)
    {
        this.GO_name = GO_name;
        this.hp = hp;
        this.mp = mp;
        this.attack = attack;
        this.defense = defense;
        this.speed= speed;
        this.price= price;
        this.gas= gas;
        this.AttackDistance= AttackDistance;
    }

}
