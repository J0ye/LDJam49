using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimplePhysicsRotate : MonoBehaviour
{
    public Vector3 rot = Vector3.zero;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(rot * speed * Time.deltaTime, Space.World);
        GetComponent<Rigidbody2D>().MoveRotation(Time.realtimeSinceStartup * speed);
    }
}
