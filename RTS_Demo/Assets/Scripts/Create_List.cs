using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Create_List : MonoBehaviour
{
    public static Create_List _instance;
    public List<List<GameobjectProperties>> Corps;
    public List<GameobjectProperties> GP_List;

    void Awake()
    {
        _instance = this;

        Corps = new List<List<GameobjectProperties>>();
        GP_List = new List<GameobjectProperties>();
        JsonToObject();
    }



    private void JsonToObject()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Soldier_List");
        JsonData jsonData = JsonMapper.ToObject(textAsset.text);
        for (int i = 0; i < jsonData.Count; i++)
        {
            JsonData jd = jsonData[i]["Corps"];
            for (int j = 0; j < jd.Count; j++)
            {
                GameobjectProperties GP = JsonMapper.ToObject<GameobjectProperties>(jd[j].ToJson());
                GP_List.Add(GP);
            }
            Corps.Add(GP_List);
        }
    }
}

