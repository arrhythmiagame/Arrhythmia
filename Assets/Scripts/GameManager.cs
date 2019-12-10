using System;
using System.IO;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("Public Variables")]
    public static GameManager instance;
    [Header("Bools")]
    [SerializeField] bool inputAllowed = false;
    [SerializeField] bool startPlaying = false;
    [Header("Audio")]
    [SerializeField] AudioSource theAmbientAudio;
    [SerializeField] AudioSource theBeatAudio;
    [SerializeField] BeatScrollerUI theBeatScroller;
    [Header("Character")]
    [SerializeField] GameObject theCharacterObject;
    [SerializeField] CharacterObject thisCharacterClass;
    [SerializeField] Animator theCharacterAnimator;
    [Header("Score")]
    [SerializeField] Text scoreText;
    [SerializeField] Text multiplierText;
    [SerializeField] GameObject resultsScreen;
    [SerializeField] BeatMonitor beatMonitor;
    [SerializeField] Text percentHitText, normalsText, goodsText, perfectsText, missesText, finalScoreText;
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
        LoadSaveData();
    }
    private void LoadSaveData()
    {
        string saveFileData = File.ReadAllText(PlayerPrefs.GetString("SavePath"));
        if (saveFileData == "")
        {
            Debug.LogError("No save data to load!"); // this shouldn't happen, but I'm putting it here in case.
        }
        else {
            string[] saveLines = saveFileData.Split('\n');
            int theSaveIndex = PlayerPrefs.GetInt("CurrentSaveIndex");
            if (theSaveIndex >= saveLines.Length - 1)
            {
                theSaveIndex = saveLines.Length - 2;
            }
            string thisSave = saveLines[theSaveIndex];
            byte[] bytesToDecode = Convert.FromBase64String(thisSave);
            string thisJson = Encoding.UTF8.GetString(bytesToDecode);
            thisCharacterClass = JsonUtility.FromJson<CharacterObject>(thisJson);
        }
    }

    void LateUpdate()
    {
        CheckIdle();
    }
    void Update()
    {
        if (!startPlaying)
        {
            if (ActionButtonPressed())
            {
                startPlaying = true;
                theBeatScroller.hasStarted = true;
                theAmbientAudio.Play();
            }
        }
        else
        {
            if (!theAmbientAudio.isPlaying && !resultsScreen.activeInHierarchy)
            {
                ActivateScoreDisplay();
            }
        }
    }
    // TODO Replace this with better score tracking.
    private void ActivateScoreDisplay()
    {
        theCharacterAnimator.SetBool("Paused", true);
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
    // Allows the idle animation to start
    public void IdlePulse()
    {
        theCharacterAnimator.SetBool("Paused", false);
    }
    // Checks if the idle animation is running and pauses it if it is
    private void CheckIdle()
    {
        string currentAnimationName = theCharacterAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (currentAnimationName == "Character_Idle")
        {
            theCharacterAnimator.SetBool("Paused", true);
        }
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