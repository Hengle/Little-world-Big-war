using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

/// <summary>
/// 框选功能与触摸功能
/// </summary>
public class DrawRectangle : MonoBehaviour
{
    public static DrawRectangle _instance;

    public Color quads_Clor = Color.green;
    private Vector3 start;//记下鼠标按下位置
    private bool mouseIsDown; //是否开始画线标志
    private Material lineColor;//画线的颜色
    public Touch touch;//触摸变量
    private int CheckCount;//计数器防止重复添加过多的东西到集合
    private GameObjectController m_gameobject;
    private RaycastHit hit;

    void Awake()
    {
        _instance = this;
        CheckCount = 0;
        start = Vector3.zero;
        mouseIsDown = false;
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineColor = new Material(shader);
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
        if (mouseIsDown)
        {
            lineColor.SetPass(0);
            Vector3 end = touch.position;//鼠标当前位置
            GL.PushMatrix();//保存摄像机变换矩阵
            //Debug.Log("E:" + end.x);
            GL.LoadPixelMatrix();//设置用屏幕坐标绘图
            GL.Begin(GL.QUADS);
            CatchFunction._instance.isCtach = true;
            GL.Color(new Color(quads_Clor.r, quads_Clor.g, quads_Clor.b, 0.1f));//设置颜色和透明度，方框内部透明
            GL.Vertex3(start.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(quads_Clor);//设置方框的边框颜色 边框不透明
            GL.Vertex3(start.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, start.y, 0);
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
            //把可选择的对象保存在characters数组里  
            Vector3 location = Camera.main.WorldToScreenPoint(CreateFunction._instance.allGameobject[i].transform.position);//把对象的position转换成屏幕坐标  
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)//z方向就用摄像机的设定值，看不见的也不需要选择了  
            {
                //Null
            }
            else
            {
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
        //当触摸手指为1时
        if (Input.touchCount == 1)
        {
            CameraFunction._instance.isMoveMap = false;
            RaycastHit hit = CatchFunction._instance.Hit();
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) //手指触摸时
            {
                start = touch.position;//获取手指坐标
                CatchFunction._instance.isCtach = false;//更改标志位
            }
            if (touch.phase == TouchPhase.Moved)//手指移动时
            {
                CheckCount = 0;//计数器初始化
                CatchFunction._instance.isCtach = true;//标志位开始框选
                mouseIsDown = true;
                //Debug.Log("S:" + start.x);
                //更改移动标志位
                CatchFunction._instance.isF2 = false;
                CatchFunction._instance.isSingle = false;
                CatchFunction._instance.catchMove = true;
            }
        }
        else if (touch.phase == TouchPhase.Ended)//手指离开屏幕时
        {
            mouseIsDown = false;
            if (CheckCount < 1)//开始清除上一轮的Nav集合，重新添加新的
            {
                CatchFunction._instance.C_Nav.Clear();
                CheckSelection(start, touch.position);
                CheckCount++;
            }
            CatchFunction._instance.isCtach = true;
        }
        if (touch.phase == TouchPhase.Ended && !CatchFunction._instance.isCtach && CameraFunction._instance.isMoveMap == false)//如果手指离开且标志位正在抓取为False时开始移动方法
        {
            Debug.Log("Move");
            MoveFunction._instance.Move();
        }
    }
}