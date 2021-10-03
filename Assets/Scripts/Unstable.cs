using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Unstable : MonoBehaviour
{
    public float speedLimit = 5f;
    public float activationSpeed = 1f;
    public bool stable = true;

    public UnityEvent OnUnstable = new UnityEvent();

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > activationSpeed)
        {
            TryParticleSystem(true);
        } else
        {
            TryParticleSystem(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > speedLimit && stable)
        {
            OnUnstable.Invoke();
            stable = false;
        }
    }

    private void TryParticleSystem(bool target)
    {
        if(transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(target);
        }
    }
}
