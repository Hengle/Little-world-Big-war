using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    private string building_Name;

    public string Building_Name
    {
        get { return building_Name; }
        set { building_Name = value; }
    }

    public Building() { }
    public Building(string building_Name)
    {
        this.building_Name = building_Name;
    }
}

