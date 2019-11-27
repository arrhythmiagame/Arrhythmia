using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Public Variables")]
    public static GameManager instance;

    [Header("Booleans")]
    [SerializeField] bool inputAllowed = false;
    [SerializeField] bool startPlaying;

    [Header("References")]
    [SerializeField] AudioSource theMusic;
    [SerializeField] BeatScrollerUI theBS;
    [SerializeField] Animator theAnimator;
    [SerializeField] Text scoreText;
    [SerializeField] Text multiplierText;
    [SerializeField] GameObject resultsScreen;
    [SerializeField] BeatMonitor beatMonitor;
    [SerializeField] Text percentHitText, normalsText, goodsText, perfectsText, missesText, finalScoreText;

    [Header("Score")]
    [SerializeField] int currentScore;
    [SerializeField] int scorePerNote = 100;
    [SerializeField] int scorePerGoodNote = 125;
    [SerializeField] int scorePerPerfectNote = 150;
    [SerializeField] int currentMultiplier;
    [SerializeField] int multiplierTracker;
    [SerializeField] int[] multiplierThresholds;

    [Header("Notes")]
    [SerializeField] float totalNotes;
    [SerializeField] float normalHits;
    [SerializeField] float goodHits;
    [SerializeField] float perfectHits;
    [SerializeField] float missedNotes;

    [Header("Colors")]
    [SerializeField] Color normalColor;
    [SerializeField] Color goodColor, perfectColor, missedColor;

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
    public void AllowInput()
    {
        inputAllowed = true;
    }
    // Disable input at end of frame
    public void DisableInput()
    {
        StartCoroutine(DisableInputCoro());
    }
    private IEnumerator DisableInputCoro()
    {
        yield return new WaitForEndOfFrame();
        inputAllowed = false;
    }
    // Checks if input is allowed
    public bool CheckInputAllowed()
    {
        return inputAllowed;
    }
    void LateUpdate()
    {
        string currentAnimationName = theAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if(currentAnimationName == "Character_Idle")
        {
            theAnimator.SetBool("Paused", true);
        }
    }
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
        DisableInput();
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