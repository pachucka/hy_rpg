using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public Tilemap map;
    private Vector3 bottomLeft, topRight;

    private float halfHeight, halfWidth;

    private void Start()
    {
        target = PlayerController.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeft = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRight = map.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x), Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y), transform.position.z);
    }
}
