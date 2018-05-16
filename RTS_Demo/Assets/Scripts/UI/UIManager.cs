using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public Button F2;
    public Button enemy;
    public bool CatchState;
    public GameObject C_B;
    public Transform P_G;



    void Start()
    {
        CatchState = false;
        _instance = this;

        enemy.onClick.AddListener(() => { CreateManager._instance.createItem("Enemy", new Vector3(17, 0.5f, 17)); });

        F2.onClick.AddListener(CatchFunction._instance.F2);

    }

    void Update()
    {

    }

    public void Light_Factory()
    {
        for (int i = 0; i < Create_List._instance.Corps.Count; i++)
        {
            GameObject go = Instantiate(C_B, P_G);
            Debug.Log(Create_List._instance.GP_List[i].GO_Name);
            go.GetComponent<SingleItemManager>().setItem(Create_List._instance.GP_List[i].GO_Name, Create_List._instance.GP_List[i].Price, Create_List._instance.GP_List[i].Gas);
        }
    }

}

