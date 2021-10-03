using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Collider2D offLimit;
    public LayerMask layerMask;
    [Range(0.1f, 100f)]
    public float speed = 70f;
    public float rotationSpeed = 2f;

    protected RaycastHit2D hit;
    public Transform target;

    // Update is called once per frame
    protected void Update()
    {
        if (IsPointOffLimits(MousePos()))
        {
            ClearTarget();
        }

        
        if (Input.GetButtonUp("Fire1"))
        {
            ClearTarget();
        } else if (Input.GetButton("Fire1"))    // Left Mouse Button Click
        {
            if (MouseClickToRay(out hit))
            {
                if (hit.transform.gameObject.CompareTag("Thing") && !IsPointOffLimits(MousePos()))
                {
                    SetTarget(hit.transform);
                }
            }

            TurnTarget();
        }
        else if (Input.GetButton("Fire2")) // Right Mouse Button Click
        {
            TryResetTarget();
        }
    }

    public void FixedUpdate()
    {
        MoveTarget();
    }

    protected virtual void MoveTarget()
    {
        if (target != null && Input.GetKey("Fire1"))
        {
            Vector3 targetPos = MousePos() - target.position;
            target.GetComponent<Rigidbody2D>().velocity = targetPos * speed;
        }
    }

    protected void TurnTarget()
    {
        if (Input.mouseScrollDelta.y != 0 && target != null)
        {
            target.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            target.RotateAround(target.transform.position, Vector3.forward, Input.mouseScrollDelta.y * rotationSpeed);
        }
    }

    protected void TryResetTarget()
    {
        if (MouseClickToRay(out hit))
        {
            Thing temp;
            if (hit.transform.TryGetComponent<Thing>(out temp))
            {
                temp.ResetObject();
            }
        }
    }

    protected void SetTarget(Transform t)
    {
        //t.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        target = t;
    }

    protected virtual void ClearTarget()
    {
        if(target != null)
            target = null;
    }

    protected Vector3 MousePos()
    {
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        return mousePos;
    }

    protected bool MouseClickToRay(out RaycastHit2D value)
    {
        value = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, layerMask);

        if (value)
        {
            return true;
        }
        return false;
    }

    protected bool IsPointOffLimits(Vector3 point)
    {
        if (offLimit == null) return false;
        return offLimit.bounds.Contains(point);
    }
}
