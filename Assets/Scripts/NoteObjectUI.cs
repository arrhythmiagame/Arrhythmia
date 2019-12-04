using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjectUI : MonoBehaviour
{

    [SerializeField] RectTransform BeatMonitorRect;
    [SerializeField] AudioClip noteToPlay;
    [SerializeField] AudioSource player;
    [Header("Debug Only")]
    [SerializeField] bool canBePressed = false;
    [SerializeField] float beatDistance;
    private RectTransform thisRect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        thisRect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        beatDistance = thisRect.position.x - BeatMonitorRect.position.x;
        if (beatDistance < 30f && beatDistance > -30f)
        {
            if (!canBePressed)
            {
                canBePressed = true;
                GameManager.instance.AllowInput();
                GameManager.instance.IdlePulse();
                PlayNote();
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
                if (beatDistance > 20 || beatDistance < -20)
                {
                    GameManager.instance.NormalHit();
                }
                else if (beatDistance > 10 || beatDistance < -10)
                {
                    GameManager.instance.GoodHit();
                }
                else if (beatDistance > -10 && beatDistance < 10)
                {
                    GameManager.instance.PerfectHit();
                }
                gameObject.SetActive(false);
            }
        }
    }

    private void PlayNote()
    {
        player.PlayOneShot(noteToPlay);
    }
}
