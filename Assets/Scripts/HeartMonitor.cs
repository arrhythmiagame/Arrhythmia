using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMonitor : MonoBehaviour
{
    private Image theImage;
    private bool filling = true;
    public float theTempo;

    private void Start()
    {
        theImage = this.gameObject.GetComponent<Image>();
        theTempo = theTempo / 60f;
    }
    public void Update()
    {
        float alpha = theImage.color.a;
        if (alpha <= 0f)
        {
            theImage.color = new Color(1f, 1f, 1f, 1f);
            alpha = 1f;
        }
        alpha -= theTempo * Time.deltaTime * .5f;
        if (alpha > 0.5f)
        {
            if (Input.anyKeyDown)
            {
                theImage.color = new Color(0f, 1f, 0f, alpha);
            }
        } else
        {
            if (Input.anyKeyDown)
            {
                theImage.color = new Color(1f, 0f, 0f, alpha);
            }
        }
        theImage.color = new Color(theImage.color.r, theImage.color.g, theImage.color.b, alpha);
    }
}
