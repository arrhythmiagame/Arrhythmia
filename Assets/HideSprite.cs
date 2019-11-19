using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSprite : MonoBehaviour
{
    [SerializeField] GameObject objectToHide;
    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.name == "Character")
        {
            objectToHide.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            objectToHide.SetActive(true);
        }
    }
}
