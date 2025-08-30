using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public GameObject model;
    public float CameraZ = -10;

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(model.transform.position.x, model.transform.position.y, CameraZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
    }
}
