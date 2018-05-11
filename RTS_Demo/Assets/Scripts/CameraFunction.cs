using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunction : MonoBehaviour
{
    public static CameraFunction _instance;

    private Transform cube_Transform;
    private ETCJoystick m_Joystick;

    public float speed;
    public bool isMoveMap;

    void Awake()
    {
        m_Joystick = gameObject.GetComponent<ETCJoystick>();
        cube_Transform = GameObject.Find("CameraController").GetComponent<Transform>();
        isMoveMap = false;
        _instance = this;

        m_Joystick.onMoveSpeed.AddListener(OnMoveSpeed);
        m_Joystick.onMoveStart.AddListener(OnMoveStart);
    }

    void Update()
    {

    }

    private void OnMoveSpeed(Vector2 v2)
    {
        if (Input.touchCount == 2)
        {
            Vector3 dir = new Vector3(v2.x, 0, v2.y) * speed;
            cube_Transform.Translate(-dir);
        }
    }

    private void OnMoveStart()
    {
        if (Input.touchCount == 2)
        {
            isMoveMap = true;
        }
    }
}
