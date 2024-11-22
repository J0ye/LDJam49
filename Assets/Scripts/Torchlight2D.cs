using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.Rendering.Universal.Light2D))]
public class Torchlight2D : MonoBehaviour
{
    public Vector2 timeBetweenFlicker = Vector2.one;
    public Vector2 intensityLevel = Vector2.one;

    private new UnityEngine.Rendering.Universal.Light2D light;
    private bool isFlickering = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering) StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        isFlickering = true;
        float rand = UnityEngine.Random.Range(timeBetweenFlicker.x, timeBetweenFlicker.y);
        yield return new WaitForSeconds(rand);
        float intens = UnityEngine.Random.Range(intensityLevel.x, intensityLevel.y);
        light.intensity = intens;
        ChangeStaticSound(intens);
        isFlickering = false;
    }

    private void ChangeStaticSound(float val)
    {
        AudioSource source;
        if(TryGetComponent<AudioSource>(out source))
        {
            source.volume = val;
        }

    }
}
