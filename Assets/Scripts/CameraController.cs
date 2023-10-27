using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = PlayerController.instance.transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
