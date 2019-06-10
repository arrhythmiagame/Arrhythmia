using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjectUI : MonoBehaviour
{

    public bool canBePressed = false;
    public float beatDistance;
    public RectTransform BeatMonitorRect;
    private RectTransform thisRect;
    
    // Start is called before the first frame update
    void Start()
    {
        thisRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        beatDistance = thisRect.position.x - BeatMonitorRect.position.x;
        if (beatDistance < 25f && beatDistance > -25f)
        {
            if (!canBePressed)
            {
                canBePressed = true;
                GameManager.instance.AllowInput();
            }
        }
        else
        {
            if (canBePressed)
            {
                canBePressed = false;
                GameManager.instance.NoteMissed();
                gameObject.SetActive(false);
            }
        }
        if (GameManager.instance.ActionButtonPressed())
        {

            if (canBePressed)
            {
                if (beatDistance > 10)
                {
                    GameManager.instance.PerfectHit();
                }else if (beatDistance > -10)
                {
                    GameManager.instance.GoodHit();
                }
                else
                {
                    GameManager.instance.NormalHit();
                }
                gameObject.SetActive(false);
            }
        }
    }
}
