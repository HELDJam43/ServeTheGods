using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float duration = 10000;
    Image ui;
    Coroutine co;
    public delegate void OnTimerComplete();
    OnTimerComplete completeEvent;
    Transform targetPos;
    float yOffset = 2;
    void Awake()
    {
        ui = transform.FindDeepChild<Image>("timer meter");
    }
    public void StartTimer(float dur, Transform target, float offset, OnTimerComplete e)
    {
        targetPos = target;
        yOffset = offset;
        duration = dur;
        co = StartCoroutine(UpdateTimer());
        completeEvent = e;
    }
    public void StartTimer(float dur, Transform target, float offset, OnTimerComplete e,Color c)
    {
        ui.color = c;
        targetPos = target;
        yOffset = offset;
        duration = dur;
        co = StartCoroutine(UpdateTimer());
        completeEvent = e;
    }
    void Update()
    {
        if (targetPos)
            transform.position = Vector3.Slerp(transform.position, targetPos.position + (Vector3.up * yOffset), Time.deltaTime * 10);
    }
    public void StopTimer()
    {
        if (co != null)
            StopCoroutine(co);
        Destroy(this.gameObject);
    }
    IEnumerator UpdateTimer()
    {
        if (ui)
        {
            float t = 0;
            while (t < duration)
            {
                ui.fillAmount = t / duration;
                t += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }
        if (completeEvent != null)
            completeEvent();
        co = null;
        StopTimer();
    }
}
