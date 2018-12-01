using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    Image godRageImage;
    Image[] stars;
    Coroutine godRageCoroutine = null;
    void Awake()
    {
        godRageImage = transform.FindDeepChild<Image>("God Rage");
        stars= transform.FindDeepChild("Review UI").GetComponentsInChildren<Image>();
    }
    void Start()
    {
        SetReviewValue(Random.Range(1, 11));
    }

    public void SetRageMeter(float val)
    {
        if (godRageCoroutine != null)
            StopCoroutine(godRageCoroutine);
        godRageCoroutine = StartCoroutine(UpdateGodRage(val));
    }

    public void SetReviewValue(int val) //1 -10
    {
        bool halfLast = val % 2 == 1;
        int target = val / 2;
        for (int i = 0; i < 5; i++)
        {
            stars[i].gameObject.SetActive(i <= target);
            stars[i].fillAmount = 1;
        }
        if (halfLast)
            stars[0].fillAmount = .5f;
    }

    IEnumerator UpdateGodRage(float targetValue)
    {
        float t = 0;
        float duration = .5f;
        float startVal = godRageImage.fillAmount;
        while (t < duration)
        {
            godRageImage.fillAmount = Mathf.Lerp(startVal, targetValue, t/duration);
            t += Time.deltaTime;
            yield return null;
        }
        godRageImage.fillAmount = targetValue;
        godRageCoroutine = null;
    }
}
