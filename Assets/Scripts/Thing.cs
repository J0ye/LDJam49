using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thing : MonoBehaviour
{
    protected LastFrameInfo startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new LastFrameInfo(transform);
    }

    #region Reset Object out of view
    public void LateUpdate()
    {
        if (!IsVisible())
        {
            StartCoroutine(ResetObjectAfterSec(0.1f));
        }
    }
    #endregion

    public void ResetObject()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        transform.SetToLastFrame(startPosition);
    }

    public IEnumerator ResetObjectAfterSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        if(!IsVisible()) ResetObject();
    }

    public void ResetObjectAfter(float sec)
    {
        StartCoroutine(ResetObjectAfterSec(sec));
    }

    public void KillObject()
    {
        Instantiate(gameObject, startPosition.position, startPosition.rotation);
        Destroy(gameObject);
    }

    public IEnumerator KillObjectAfterSec(float sec)
    {
        Instantiate(gameObject, startPosition.position, startPosition.rotation);
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }

    public void KillObjectAfter(float sec)
    {
        StartCoroutine(KillObjectAfterSec(sec));
    }

    public bool IsVisible()
    {
        SpriteRenderer temp;
        if (TryGetComponent<SpriteRenderer>(out temp))
        {
            if (temp.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
