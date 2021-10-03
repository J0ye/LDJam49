using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HocMananger cards;

    private Rigidbody2D playBall;
    private LastFrameInfo startPosition;
    private bool simulating = false;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playBall == null) SetPlayer();
    }
    
    public void SwitchState()
    {
        Debug.Log("Ball: " + playBall.gameObject + " and simulating: " + simulating);
        if(simulating)
        {
            PauseSim();
        } else
        {
            PlaySim();
        }
    }

    public void PlaySim()
    {
        playBall.bodyType = RigidbodyType2D.Dynamic;

        SwitchPlayerController(false);
        cards.PlaySim();
        simulating = true;
    }

    public void PauseSim()
    {
        playBall.bodyType = RigidbodyType2D.Kinematic;
        playBall.velocity = Vector2.zero;
        playBall.angularVelocity = 0f;
        playBall.transform.SetToLastFrame(startPosition);

        SwitchPlayerController(true);
        cards.PauseSim();
        simulating = false;
    }

    public void SetPlayer()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        playBall = target.GetComponent<Rigidbody2D>();
        playBall.bodyType = RigidbodyType2D.Kinematic;
        if(!simulating) startPosition = new LastFrameInfo(playBall.transform);
    }

    private void SwitchPlayerController(bool target)
    {
        MouseController temp;
        if(Camera.main.TryGetComponent<MouseController>(out temp))
        {
            temp.enabled = target;
        }
    }
}
