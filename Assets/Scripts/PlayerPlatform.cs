using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    public float speed = 1f;
    public void Update()
    {
        Vector3 target = new Vector3(GetInputAxis().x, 0, GetInputAxis().z); // ignore the y axis
        if(target != Vector3.zero) Move(target);
    }

    protected void Move(Vector3 target)
    {
        var currentPos = transform.position;
        /*transform.position = Vector3.MoveTowards(currentPos,
            currentPos + target, speed * Time.deltaTime);*/
        GetComponent<Rigidbody>().velocity = target *speed;
    }

    protected virtual Vector3 GetInputAxis()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate direction vectors based on orientation
        Vector3 forward = transform.forward * v;
        Vector3 sideways = transform.right * h;

        Vector3 re = forward + sideways;
        return re;
    }
}
