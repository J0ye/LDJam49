using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingHazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Thing temp;
        if(collision.gameObject.TryGetComponent<Thing>(out temp))
        {
            temp.ResetObject();
        }
    }
}
