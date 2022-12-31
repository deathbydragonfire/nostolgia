using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Image image;
    [SerializeField] float transitionSpeed = 3f;
    [HideInInspector] public bool active = false;

    public enum FadeDirection
    {
        In,
        Out
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = true;
    }

    public void Fade(FadeDirection direction)
    {
        StartCoroutine(_Fade(direction));
    }

    IEnumerator _Fade(FadeDirection direction)
    {
        image = GetComponent<Image>();
        active = true;
        float startAlpha = direction == FadeDirection.In ? 1f : 0f;
        float endAlpha = direction == FadeDirection.In ? 0f : 1f;
        float alpha = startAlpha;
        if (direction == FadeDirection.In)
        {
            image.enabled = true;
            while (alpha > endAlpha)
            {
                alpha -= transitionSpeed * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return new WaitForEndOfFrame();
            }
            image.enabled = false;
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            image.enabled = true;

            while (alpha < endAlpha)
            {
                alpha += transitionSpeed * Time.deltaTime;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return new WaitForEndOfFrame();
            }
        }
        active = false;
    }
}
