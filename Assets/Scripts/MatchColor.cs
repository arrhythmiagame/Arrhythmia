using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchColor : MonoBehaviour
{
    public GameObject sourceColorObj;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().color = sourceColorObj.GetComponent<Image>().color;
    }
}
