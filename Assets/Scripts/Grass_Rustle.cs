using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Rustle : MonoBehaviour
{
    [SerializeField] AudioClip grassSound;
    [SerializeField] Sprite grassDown;
    private Sprite grassUp;
    private SpriteRenderer theSprite;
    private void Start()
    {
        theSprite = gameObject.GetComponent<SpriteRenderer>();
        grassUp = theSprite.sprite;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = true;
            theSprite.sprite = grassDown;
            AudioSource.PlayClipAtPoint(grassSound, transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = false;
            theSprite.sprite = grassUp;
        }
    }
}
