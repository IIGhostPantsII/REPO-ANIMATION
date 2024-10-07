using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f;

    private Image imageComp;
    private Color startColor;
    private Color endColor;

    [SerializeField] private bool _fadeIn;
    [SerializeField] private bool _fadeOut;
    [SerializeField] private bool _fromFull; // Bool - Use if you want image to fade from full opacity or 0

    private void Awake()
    {
        imageComp = GetComponent<Image>();
        if (imageComp == null)
        {
            Debug.LogError("No Image component found on the GameObject. Please add one for fading to work.");
        }
        else
        {
            startColor = imageComp.color;
        }

        if(_fadeIn)
        {
            if(_fromFull)
            {
                imageComp.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
            }
            FadeIn();
        }
        else if(_fadeOut)
        {
            if(_fromFull)
            {
                imageComp.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
            }
            FadeOut();
        }
    }

    // Start fading out the GameObject
    public void FadeOut()
    {
        if (imageComp == null) return;
        
        startColor = imageComp.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fully transparent
        StartCoroutine(FadeCoroutine(startColor, endColor));
    }

    // Start fading in the GameObject
    public void FadeIn()
    {
        if (imageComp == null) return;
        
        startColor = imageComp.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque
        StartCoroutine(FadeCoroutine(startColor, endColor));
    }

    // Coroutine to handle the fading effect
    private IEnumerator FadeCoroutine(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);
            imageComp.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        // Ensure the final color is set
        imageComp.color = endColor;

        if(_fadeOut)
        {
            Destroy(gameObject);
        }
    }
}
