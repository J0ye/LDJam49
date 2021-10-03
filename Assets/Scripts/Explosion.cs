using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float force = 100f;
    public float range = 2f;
    public LayerMask mask = new LayerMask();

    protected RaycastHit2D[] hits;
    // Start is called before the first frame update
    void Awake()
    {
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.one, mask);

        foreach(RaycastHit2D hit in hits)
        {
            Rigidbody2D temp;

            if(hit.transform.TryGetComponent<Rigidbody2D>(out temp))
            {
                ApplyForce(temp, hit.point);
            }
        }
    }

    protected void ApplyForce(Rigidbody2D target, Vector3 pos)
    {
        Vector3 direction = target.transform.position - transform.position;
        target.AddForceAtPosition(direction * force, pos, ForceMode2D.Impulse);
    }

    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, range, 0.6f);
#endif
    }
}
