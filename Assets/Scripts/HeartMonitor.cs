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
        if (theImage.fillAmount == 1)
        {
            theImage.fillOrigin = (int)Image.OriginHorizontal.Left;
            filling = false;
        } else if (theImage.fillAmount == 0)
        {
            theImage.fillOrigin = (int)Image.OriginHorizontal.Right;
            filling = true;
        }
        {
            if (filling)
            {
                theImage.fillAmount += theTempo * Time.deltaTime;
            } else
            {
                theImage.fillAmount -= theTempo * Time.deltaTime;
            }
        }
    }
}
