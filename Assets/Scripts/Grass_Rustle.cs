using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_Rustle : MonoBehaviour
{
    [SerializeField] AudioClip grassSound;
    private SpriteRenderer theSprite;
    private void Start()
    {
        theSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = true;
            AudioSource.PlayClipAtPoint(grassSound, transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Character")
        {
            theSprite.flipX = false;
        }
    }
}
