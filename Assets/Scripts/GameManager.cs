using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool debugMode = false;

    public bool inputAllowed = false;
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScrollerUI theBS;
    public Animator theAnimator;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public Text scoreText;
    public Text multiplierText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedNotes;

    public GameObject resultsScreen;
    public BeatMonitor beatMonitor;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, finalScoreText;
    public Color normalColor, goodColor, perfectColor, missedColor;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Application.targetFrameRate = 30;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObjectUI>().Length;
    }
    public void IdlePulse()
    {
        theAnimator.SetBool("Paused", false);
    }
    // Allows input for a certain amount of time based on the beat scroller
    public bool CheckInputAllowed()
    {
        return inputAllowed;
    }
    public void AllowInput()
    {
        inputAllowed = true;
    }
    void LateUpdate()
    {
        string currentAnimationName = theAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if(currentAnimationName == "Character_Idle")
        {
            theAnimator.SetBool("Paused", true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (ActionButtonPressed())
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
        }
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                theAnimator.SetBool("Paused", true);
                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedNotes;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                finalScoreText.text = currentScore.ToString();

                resultsScreen.SetActive(true);

            }
        }
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
        beatMonitor.PulseImage(normalColor);
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
        beatMonitor.PulseImage(goodColor);
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
        beatMonitor.PulseImage(perfectColor);
    }

    public void NoteHit()
    {
        inputAllowed = false;
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        inputAllowed = false;
        currentMultiplier = 1;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        missedNotes++;
        beatMonitor.PulseImage(missedColor);
    }

    public bool ActionButtonPressed()
    {
        if (Input.GetButtonDown("Dash")) return true;
        if (Input.GetButtonDown("Attack")) return true;
        if (Input.GetButtonDown("Block")) return true;
        if (Input.GetButtonDown("Ultimate")) return true;
        return false;
    }
}