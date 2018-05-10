using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class DrawRectangle : MonoBehaviour
{
    public Color quads_Clor = Color.green;

    private Vector3 start;//记下鼠标按下位置
    private bool mouseIsDown; //是否开始画线标志
    private Material lineColor;
    private Touch touch;
    private int CheckCount;

    // Use this for initialization
    void Start()
    {
        CheckCount = 0;
        start = Vector3.zero;
        mouseIsDown = false;
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineColor = new Material(shader);

    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                start = touch.position;
                CatchFunction._instance.isCtach = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                CheckCount = 0;
                CatchFunction._instance.isCtach = true;
                mouseIsDown = true;
                Debug.Log("S:" + start.x);
                CatchFunction._instance.isF2 = false;
                CatchFunction._instance.isSingle = false;
                CatchFunction._instance.catchMove = true;
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            mouseIsDown = false;
            if (CheckCount < 1)
            {
                CatchFunction._instance.C_Nav.Clear();
                CheckSelection(start, touch.position);
                CheckCount++;
            }
            CatchFunction._instance.isCtach = true;
        }
        if (touch.phase == TouchPhase.Ended && !CatchFunction._instance.isCtach)
        {
            Debug.Log("Move");
            MoveFunction._instance.Move();
        }
    }

    private void OnPostRender()
    {
        if (mouseIsDown)
        {
            lineColor.SetPass(0);
            Vector3 end = touch.position;//鼠标当前位置
            GL.PushMatrix();//保存摄像机变换矩阵
            Debug.Log("E:" + end.x);
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
            //把可选择的对象保存在characters数组里  
            Vector3 location = Camera.main.WorldToScreenPoint(CreateFunction._instance.allGameobject[i].transform.position);//把对象的position转换成屏幕坐标  
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)//z方向就用摄像机的设定值，看不见的也不需要选择了  
            {

            }
            else
            {
                CatchFunction._instance.C_Nav.Add(CreateFunction._instance.allGameobject[i].GetComponent<NavMeshAgent>());
            }
        }
    }
}