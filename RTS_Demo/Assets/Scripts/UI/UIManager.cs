using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button create;
    public Button F2;

    void Start()
    {
        create.onClick.AddListener(CreateManager._instance.createItem);
        F2.onClick.AddListener(CatchFunction._instance.F2);
    }
}

