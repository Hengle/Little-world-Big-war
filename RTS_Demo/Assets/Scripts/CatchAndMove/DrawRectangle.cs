using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 框选功能与触摸功能
/// </summary>
public class DrawRectangle : MonoBehaviour
{
    public static DrawRectangle _instance;

    private int CheckCount;//计数器防止重复添加过多的东西到集合
    private int MoveCount;

    private Material lineColor;//画线的颜色
    private Touch touch_Catch_S;//框选起始位
    private Touch touch_Catch_E;//框选结束位
    private Touch touch_Catch_M_S;//相机起始位
    private Touch touch_Catch_M_E;//相机结束位
    private Vector3 touch_Postion_S;
    //private Vector3 touch_Postion_E;
    private Vector3 offest;//差距
    private GameObjectController m_gameobject;

    public Color quads_Clor = Color.green;
    public Touch touch;//触摸变量
    public Transform m_Transform;
    public Camera C;//移动相机
    public Camera M_C;//主相机
    public RaycastHit hitinfo;
    public bool isOnUI;

    public bool isMoveMap;
    public float speed;

    void Awake()
    {
        _instance = this;

        CheckCount = 0;
        MoveCount = 0;
        isMoveMap = false;
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineColor = new Material(shader);
        isOnUI = false;
    }

    void Update()
    {
        TouchFunction();
    }

    /// <summary>
    /// 绘制矩阵
    /// </summary>
    private void OnPostRender()
    {
        if (Input.touchCount == 2 && UIManager._instance.CatchState == true)
        {
            //Debug.Log("Draw");
            touch_Catch_E = Input.GetTouch(1);
            lineColor.SetPass(0);
            Vector3 end = touch_Catch_E.position;//鼠标当前位置
            GL.PushMatrix();//保存摄像机变换矩阵
            //Debug.Log("E:" + end.x);
            GL.LoadPixelMatrix();//设置用屏幕坐标绘图
            GL.Begin(GL.QUADS);
            CatchFunction._instance.isCtach = true;
            CatchFunction._instance.catchMove = true;
            GL.Color(new Color(quads_Clor.r, quads_Clor.g, quads_Clor.b, 0.1f));//设置颜色和透明度，方框内部透明
            GL.Vertex3(touch_Postion_S.x, touch_Postion_S.y, 0);
            GL.Vertex3(end.x, touch_Postion_S.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(touch_Postion_S.x, end.y, 0);
            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(quads_Clor);//设置方框的边框颜色 边框不透明
            GL.Vertex3(touch_Postion_S.x, touch_Postion_S.y, 0);
            GL.Vertex3(end.x, touch_Postion_S.y, 0);
            GL.Vertex3(end.x, touch_Postion_S.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(touch_Postion_S.x, end.y, 0);
            GL.Vertex3(touch_Postion_S.x, end.y, 0);
            GL.Vertex3(touch_Postion_S.x, touch_Postion_S.y, 0);
            GL.End();

            GL.PopMatrix();//恢复摄像机投影矩阵
        }
    }

    /// <summary>
    /// 矩阵检测
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void CheckSelection(Vector3 start, Vector3 end)
    {
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        if (start.x > end.x)
        {
            //这些判断是用来确保p1的xy坐标小于p2的xy坐标，因为画的框不见得就是左下到右上这个方向的  
            p1.x = end.x;
            p2.x = start.x;
        }
        else
        {
            p1.x = start.x;
            p2.x = end.x;
        }
        if (start.y > end.y)
        {
            p1.y = end.y;
            p2.y = start.y;
        }
        else
        {
            p1.y = start.y;
            p2.y = end.y;
        }

        for (int i = 0; i < CreateFunction._instance.allGameobject.Count; i++)
        {
            m_gameobject = CreateFunction._instance.allGameobject[i].GetComponent<GameObjectController>();
            m_gameobject.isBeCatch = false;
            Vector3 location = M_C.WorldToScreenPoint(CreateFunction._instance.allGameobject[i].transform.position);//把对象的position转换成屏幕坐标  
            Debug.Log(m_gameobject.name + ":" + location);
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)//z方向就用摄像机的设定值，看不见的也不需要选择了  
            {
                //Null
            }
            else
            {
                CatchFunction._instance.C_GOC.Add(m_gameobject);
                CatchFunction._instance.C_Nav.Add(CreateFunction._instance.allGameobject[i].GetComponent<NavMeshAgent>());
                m_gameobject.isBeCatch = true;
            }
        }
    }

    /// <summary>
    /// 触摸方法
    /// </summary>
    private void TouchFunction()
    {
        InputCount2();
        InputCout1();
    }

    /// <summary>
    /// 当触摸手指为1时
    /// </summary>
    private void InputCout1()
    {
        if (Input.touchCount == 1)//手指为1时
        {
            Ray ray = M_C.ScreenPointToRay(Input.GetTouch(0).position);
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //手指触摸时
            {
                touch_Catch_M_S = Input.GetTouch(0);
                MoveCount = 0;
                touch_Catch_M_E = Input.GetTouch(0);
                isMoveMap = false;
                CatchFunction._instance.isCtach = false;
                isOnUI = false;
            }
            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //手指触摸时
            {
                isOnUI = true;
            }
            if (touch.phase == TouchPhase.Moved)//手指移动时
            {
                touch_Catch_M_E = Input.GetTouch(0);
                isMoveMap = true;
                offest = C.ScreenToWorldPoint(touch_Catch_M_S.position) - C.ScreenToWorldPoint(touch_Catch_M_E.position);
                if (isMoveMap == true && UIManager._instance.CatchState == false && isOnUI == false)
                {
                    m_Transform.position = new Vector3(m_Transform.position.x + offest.x * speed, 0, m_Transform.position.z + offest.z * speed);
                }
                //Debug.Log(offest);

            }
            if (touch.phase == TouchPhase.Ended)//手指离开时
            {
                isOnUI = false;
                Physics.Raycast(ray, out hitinfo);
                //Debug.Log(hitinfo.collider.name);
                if (isMoveMap == false && CatchFunction._instance.isCtach == false)
                {
                    TouchKind();
                }
            }
        }
    }

    /// <summary>
    /// 触摸手指为2时
    /// </summary>
    private void InputCount2()
    {
        if (Input.touchCount == 2 && UIManager._instance.CatchState == true)
        {
            touch_Catch_S = Input.GetTouch(1);
            UIManager._instance.CatchState = true;
            if (touch_Catch_S.phase == TouchPhase.Began) //手指触摸时
            {
                MoveCount = 0;
                touch_Postion_S = touch_Catch_S.position;//获取手指坐标
                CatchFunction._instance.isCtach = true;
            }
            if (touch_Catch_S.phase == TouchPhase.Moved)//手指移动时
            {
                //计数器初始化
                //更改移动标志位
                CheckCount = 0;
                CatchFunction._instance.isCtach = true;//标志位开始框选
                CatchFunction._instance.isF2 = false;
                CatchFunction._instance.isSingle = false;
                CatchFunction._instance.catchMove = true;
                //touch_Postion_E = touch_Catch_S.position;
            }
            if (touch_Catch_S.phase == TouchPhase.Ended)//手指离开屏幕时
            {
                if (CheckCount < 1)//开始清除上一轮的Nav集合，重新添加新的
                {
                    CatchFunction._instance.C_GOC.Clear();
                    CatchFunction._instance.C_Nav.Clear();
                    CheckSelection(touch_Postion_S, Input.GetTouch(1).position);
                    //Debug.Log(touch_Postion_S);
                    //Debug.Log(touch_Postion_E);
                    CheckCount++;
                }
            }
        }
    }


    /// <summary>
    /// 触摸种类
    /// </summary>
    private void TouchKind()
    {
        if (hitinfo.collider.tag == "Land")
        {
            if (MoveCount < 1)
            {
                MoveFunction._instance.Move(hitinfo);
                //Debug.Log("Move");
                MoveCount++;
            }
        }
        if (hitinfo.collider.tag == "Player_1")
        {
            if (MoveCount < 1)
            {
                CatchFunction._instance.SingleCatch(hitinfo);
            }
        }
        if (hitinfo.collider.name == "Light_Factory")
        {
            UIManager._instance.Light_Factory();
        }
    }
}
