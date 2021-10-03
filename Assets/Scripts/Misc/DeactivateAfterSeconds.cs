using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterSeconds : MonoBehaviour
{
    public float seconds = 1f;

    private bool running = false;
    // Start is called before the first frame update
    void Update()
    {
        if(gameObject.activeSelf && !running)
        {
            running = true;
            StartCoroutine(Action());
        }
    }

    IEnumerator Action()
    {
        yield return new WaitForSeconds(seconds);
        running = false;
        gameObject.SetActive(false);
    }

}
