using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Rustle : MonoBehaviour
{
    [SerializeField] AudioClip grassSound;
    [SerializeField] Sprite grassDown;
    [SerializeField] bool reversed;
    private Sprite grassUp;
    private SpriteRenderer theSprite;
    private void Start()
    {
        reversed = (Random.value > 0.5f);
        theSprite = gameObject.GetComponent<SpriteRenderer>();
        grassUp = theSprite.sprite;
        theSprite.flipX = reversed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = reversed;
            reversed = !reversed;
            theSprite.sprite = grassDown;
            AudioSource.PlayClipAtPoint(grassSound, transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = reversed;
            reversed = !reversed;
            theSprite.sprite = grassUp;
        }
    }
}
