using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f;
    private Image imageComp;
    private TextMeshProUGUI textMeshProComp;
    private Color startColor;
    private Color endColor;

    [SerializeField] private bool _fadeIn;
    [SerializeField] private bool _fadeOut;
    [SerializeField] private bool _fromFull;
    [SerializeField] private bool _fromFunction;

    private void Awake()
    {
        imageComp = GetComponent<Image>();
        textMeshProComp = GetComponent<TextMeshProUGUI>();
        if(imageComp == null && textMeshProComp == null)
        {
            Debug.LogError("No Image or TextMeshPro component found");
            return;
        }
        startColor = imageComp ? imageComp.color : textMeshProComp.color;
        if(_fadeIn && !_fromFunction)
        {
            if(_fromFull)
            {
                SetStartColor(0f);
            }
            FadeIn();
        }
        else if(_fadeOut && !_fromFunction)
        {
            if(_fromFull)
            {
                SetStartColor(1f);
            }
            FadeOut();
        }
    }

    private void SetStartColor(float alpha)
    {
        if(imageComp)
        {
            imageComp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
        else if(textMeshProComp)
        {
            textMeshProComp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
    }

    public void FadeOut()
    {
        if(imageComp == null && textMeshProComp == null) return;
        startColor = imageComp ? imageComp.color : textMeshProComp.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        StartCoroutine(FadeCoroutine(startColor, endColor));
    }

    public void FadeIn()
    {
        if(imageComp == null && textMeshProComp == null) return;
        startColor = imageComp ? imageComp.color : textMeshProComp.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        StartCoroutine(FadeCoroutine(startColor, endColor));
    }

    private IEnumerator FadeCoroutine(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;
        while(elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);
            if(imageComp)
            {
                imageComp.color = Color.Lerp(startColor, endColor, t);
            }
            else if(textMeshProComp)
            {
                textMeshProComp.color = Color.Lerp(startColor, endColor, t);
            }
            yield return null;
        }
        if(imageComp)
        {
            imageComp.color = endColor;
        }
        else if(textMeshProComp)
        {
            textMeshProComp.color = endColor;
        }
        if(_fadeOut)
        {
            Destroy(gameObject);
        }
    }
}
