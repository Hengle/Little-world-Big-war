using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RemoveDemo : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.SetActive(false);
            CreateFunction._instance.allGameobject.Remove(gameObject);
            CreateFunction._instance.L_Nav.Remove(gameObject.GetComponent<NavMeshAgent>());
            CatchFunction._instance.C_Nav.Remove(gameObject.GetComponent<NavMeshAgent>());
            CatchFunction._instance.Nav = null;
        }
    }
}
