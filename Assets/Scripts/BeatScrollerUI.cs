using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScrollerUI : MonoBehaviour
{
    public float beatTempo;
    public float beatTempoSeconds;
    public bool hasStarted;

    private RectTransform theTransform;

    // Start is called before the first frame update
    void Start()
    {
        theTransform = this.gameObject.GetComponent<RectTransform>();
        beatTempoSeconds = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            float theXpos = theTransform.anchoredPosition.x;
            float theYpos = theTransform.anchoredPosition.y;
            theTransform.anchoredPosition = new Vector3(theXpos - (beatTempoSeconds * Time.deltaTime * 200f), theYpos, 0);
        }
    }
}
