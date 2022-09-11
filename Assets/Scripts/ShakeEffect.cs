using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public static ShakeEffect instance;
    public AnimationCurve curve;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(instance);
    }
    
    public void shakeScreen()
    {
        StartCoroutine("shakeAndStop");
    }

    IEnumerator shakeAndStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 1;

        Vector3 startPos = Camera.main.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 0.75f)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime);
            Camera.main.transform.position = startPos + Random.insideUnitSphere;
            yield return null;
        }

        Camera.main.transform.position = startPos;
    }
}
