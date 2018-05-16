using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleItemManager : MonoBehaviour
{

    public Image C_I;
    private Transform m_Transform;
    private Text C_P;
    private Text C_G;


    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        C_I = m_Transform.Find("Image").GetComponent<Image>();
        C_P = m_Transform.Find("Price").GetComponent<Text>();
        C_G = m_Transform.Find("Gas").GetComponent<Text>();
    }

    public void setItem(string C_Name,float Price,float Gas)
    {
        C_I.sprite = Resources.Load<Sprite>(C_Name);
        C_P.text ="Price:"+Price.ToString();
        C_G.text ="Gas:"+Gas.ToString();
    }
}
