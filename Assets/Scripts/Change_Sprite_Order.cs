using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Sprite_Order : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] changingSprites;
    [SerializeField] SpriteRenderer targetSprite;
    [Header("Debug info")]
    [SerializeField] int endDelta = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            endDelta = 0;
            foreach (var changingSprite in changingSprites)
            {
                int thisDelta = targetSprite.sortingOrder - changingSprite.sortingOrder + 1;
                if (thisDelta > endDelta){
                    endDelta = thisDelta;
                }
            }
            foreach (var changingSprite in changingSprites)
            {
                changingSprite.sortingOrder = changingSprite.sortingOrder + endDelta;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            foreach (var changingSprite in changingSprites)
            {
                var newOrder = changingSprite.sortingOrder - endDelta;
                changingSprite.sortingOrder = newOrder;
            }
        }
    }
}
