using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HocMananger : MonoBehaviour
{
    public List<Rigidbody2D> Cards = new List<Rigidbody2D>();

    protected List<LastFrameInfo> StartPositions = new List<LastFrameInfo>();
    protected bool simulating = false;
    // Start is called before the first frame update
    void Start()
    {
        SetList();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Thing temp;
        if(collision.transform.TryGetComponent<Thing>(out temp))
        {
            temp.ResetObject();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Thing temp;
        if (collision.transform.TryGetComponent<Thing>(out temp))
        {
            temp.ResetObject();
        }
    }

    public void PlaySim()
    {
        foreach(Rigidbody2D card in Cards)
        {
            card.bodyType = RigidbodyType2D.Dynamic;
        }

        simulating = true;
    }

    public void PauseSim()
    {
        int i = 0;
        foreach(Rigidbody2D card in Cards)
        {
            card.bodyType = RigidbodyType2D.Kinematic;
            card.velocity = Vector2.zero;
            card.angularVelocity = 0f;
            card.transform.SetToLastFrame(StartPositions[i]);
            i++;
        }

        simulating = false;
    }

    public void SetList()
    {
        foreach(Transform child in transform)
        {
            Rigidbody2D rb;
            if(child.TryGetComponent<Rigidbody2D>(out rb))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                Cards.Add(rb);
                StartPositions.Add(new LastFrameInfo(child));
            }
            else
            {
                Debug.Log(child.gameObject.name + " has no Rigidbody2D");
            }
        }
    }
}

public struct LastFrameInfo
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

    public LastFrameInfo(Transform values)
    {
        position = values.position;
        scale = values.localScale;
        rotation = values.rotation;
    }

    public void UpdateValues(Transform target)
    {
        position = target.position;
        scale = target.localScale;
        rotation = target.rotation;
    }

    public void UpdateValues(LastFrameInfo target)
    {
        position = target.position;
        scale = target.scale;
        rotation = target.rotation;
    }
}

static class ExtensionMethods
{
    public static void SetToLastFrame(this Transform trans, LastFrameInfo target)
    {
        trans.position = target.position;
        trans.rotation = target.rotation;
        trans.localScale = target.scale;
    }
}
