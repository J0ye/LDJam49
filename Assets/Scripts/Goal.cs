using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public string targetTag = "Goal";
    public float finishDelay = 1f;
    public UnityEvent OnTargetEnter = new UnityEvent();
    public UnityEvent OnGoal = new UnityEvent();

    private GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            OnTargetEnter.Invoke();
            target = collision.gameObject;
            StartCoroutine(Finish());
        }
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(finishDelay);
        if(target != null)
        {
            if(GetComponent<Collider2D>().bounds.Contains(target.transform.position))
            {
                SwitchPlayerController(false);
                OnGoal.Invoke();
            }
            target = null;
        }
    }

    private void SwitchPlayerController(bool target)
    {
        MouseController temp;
        if (Camera.main.TryGetComponent<MouseController>(out temp))
        {
            temp.enabled = target;
        }
    }
}
