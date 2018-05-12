using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraFollow : MonoBehaviour {

    public Transform m_Transform;

	void Start ()
    {
		
	}

	void Update ()
    {
        Vector3 targetPos = new Vector3(0, 70.9f, -70.9f) + m_Transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5);
    }
}
