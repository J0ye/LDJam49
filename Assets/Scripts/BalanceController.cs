using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MouseController
{
    public Collider2D bound;
    public GameObject cursor;

    protected new void Update()
    {
        if (Input.GetButton("Fire1"))    // Left Mouse Button Click
        {
            if (MouseClickToRay(out hit))
            {
                if ((hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Goal")) && !IsPointOffLimits(MousePos()))
                {
                    SetTarget(hit.transform);
                }
            }
        }
        
        if(cursor != null)
        {
            cursor.transform.position = MousePos();
            RaycastHit2D temp;
            if (MouseClickToRay(out temp))
            {
                Thing t;
                if (temp.transform.TryGetComponent<Thing>(out t))
                {
                    cursor.SetActive(true);
                }
            }
            else
            {
                cursor.SetActive(false);
            }
        }

        base.Update();

        if (target != null)
        {
            if (!IsPointInBound(MousePos())
                && (target.CompareTag("Thing")
                || target.CompareTag("Goal")))
            {
                ClearTarget();
            }
        }
        if (IsPointOffLimits(MousePos()))
        {
            ClearTarget();
        }
    }

    protected override void MoveTarget()
    {
        if (target != null)
        {
            /*if(target.CompareTag("Thing") && !IsPointInBound(MousePos()))
            {
                TryResetTarget();
                return; 
            }*/

            Vector3 targetPos = MousePos() - target.position;
            target.GetComponent<Rigidbody2D>().velocity = targetPos * speed;
        }
    }

    protected override void ClearTarget()
    {
        if (target != null)
        {
            if (target.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
            {
                target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                target.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
            target = null;
        }
    }

    protected bool IsPointInBound(Vector3 point)
    {
        if (bound == null) return true;
        return bound.bounds.Contains(point);
    }
}
