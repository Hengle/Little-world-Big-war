using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public Button create;
    public Button F2;
    public Button Catch;
    public bool CatchState;


    void Start()
    {
        CatchState = false;
        _instance = this;

        create.onClick.AddListener(CreateManager._instance.createItem);
        F2.onClick.AddListener(CatchFunction._instance.F2);
        Catch.onClick.AddListener(CatchManager);
    }

    public void CatchManager()
    {
        if (CatchState == false)
        {
            CatchState = true;
        }
        else if (CatchState == true)
        {
            CatchState = false;
            CatchFunction._instance.isCtach = false;
        }
    }
}

