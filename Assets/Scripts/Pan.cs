using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pan : MonoBehaviour
{
    public Vector2 target;
    public float duration = 1f;
    public bool flip = false;
    public bool flipOnStart = false;

    private Vector2 start;

    void Start()
    {
        start = transform.localPosition;
        if(flipOnStart)
        {
            Vector3 eul = transform.rotation.eulerAngles;
            transform.Rotate(eul.x, 180, eul.z);
        }
        transform.DOLocalMove(target, duration, false);
    }

    void Update()
    {
        if((Vector2)transform.localPosition == target)
        {
            transform.DOLocalMove(start, duration, false);
            if (flip)
            {
                Vector3 eul = transform.rotation.eulerAngles;
                transform.Rotate(eul.x, 180, eul.z);
            }
        } else if((Vector2)transform.localPosition == start)
        {
            transform.DOLocalMove(target, duration, false);
            if (flip)
            {
                Vector3 eul = transform.rotation.eulerAngles;
                transform.Rotate(eul.x, 0, eul.z);
            }
        }
    }
}
