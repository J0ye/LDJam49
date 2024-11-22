using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ShootingThing : Thing
{
    public LayerMask layerMask;
    private bool grounded;
    private bool ready = true;
    public GameObject prefab;
    private float force = 50f;

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down, 0.3f, layerMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (grounded && ready)
        {        
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        ready = false;
        GameObject projectile = Instantiate(prefab, transform.position, prefab.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);

        yield return new WaitForSeconds(3f);
        ready = true;
    }

}
