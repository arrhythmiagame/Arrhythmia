using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    private Image theImage;
    public float theTempo;

    private void Start()
    {
        theImage = this.gameObject.GetComponent<Image>();
        theTempo = theTempo / 60f;
    }
    public void Update()
    {
        float alpha = theImage.color.a;
        if (alpha >= 0.1f)
        {
            alpha -= theTempo * Time.deltaTime * .5f;
            theImage.color = new Color(theImage.color.r, theImage.color.g, theImage.color.b, alpha);
        }
    }
    public void PulseImage()
    {
        theImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
