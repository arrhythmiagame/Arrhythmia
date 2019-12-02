using UnityEngine;
using TMPro;
using System.Collections;

public class TextFade : MonoBehaviour
{
    [SerializeField] TMP_Text theText;
    [SerializeField] float minAlpha = 0.5f;
    [SerializeField] float maxAlpha = 1f;
    [SerializeField] float fadeSpeed = 3f;
    [Header("Debug Only")]
    [SerializeField] float currentAlpha;
    [SerializeField] float targetAlpha;
    [SerializeField] float currentAlphaCalc;
    // Start is called before the first frame update
    void Start()
    {
        targetAlpha = minAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha = theText.alpha;
        currentAlphaCalc = Mathf.Round(currentAlpha * 10) / 10;
        if (targetAlpha == currentAlphaCalc)
        {
            if (targetAlpha == minAlpha)
            {
                targetAlpha = maxAlpha;
            }
            else
            {
                targetAlpha = minAlpha;
            }
        }
        theText.alpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);
    }
}
