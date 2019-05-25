using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
        beatDistance = Mathf.Abs(thisRect.position.x - BeatMonitorRect.position.x);
        if (beatDistance < 30f)
        {
            if (!canBePressed)
            {
                canBePressed = true;
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
        if (AnyButtonPressed())
        {

            if (canBePressed)
            {
                if (beatDistance < 10)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                }else if (beatDistance < 20)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                }
                gameObject.SetActive(false);
            }
        }
    }
    private bool AnyButtonPressed()
    {
        if (Input.anyKeyDown) return true;
        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0) return true;
        if (CrossPlatformInputManager.GetAxis("Vertical") != 0) return true;
        return false;
    }
}
