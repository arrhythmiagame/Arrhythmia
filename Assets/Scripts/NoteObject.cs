using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    private bool canBeMissed;

    public KeyCode keyToPress;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)){
            if (canBePressed)
            {
                canBeMissed = false;
                GameManager.instance.NoteHit();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
            canBeMissed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && canBeMissed)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}
