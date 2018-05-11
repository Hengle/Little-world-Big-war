using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Camera camera = GetComponent<Camera>();
        Vector3 p = camera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(p);
    }
}
