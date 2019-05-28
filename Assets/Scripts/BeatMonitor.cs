using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatMonitor : MonoBehaviour
{
    private Image theImage;
    public BeatScrollerUI theBS;

    private void Start()
    {
        theImage = this.gameObject.GetComponent<Image>();
    }
    public void Update()
    {
        float alpha = theImage.color.a;
        if (alpha > 0.25f)
        {
            alpha -= theBS.beatTempoSeconds * Time.deltaTime;
            theImage.color = new Color(theImage.color.r, theImage.color.g, theImage.color.b, alpha);
        }
        else
        {
            theImage.color = new Color(1f, 1f, 1f, 0.25f);
        }
    }
    public void PulseImage(Color theColor)
    {
        theImage.color = theColor;
    }
}
