using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{

    public bool debugAxes;
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScrollerUI theBS;

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
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    public Color normalColor, goodColor, perfectColor, missedColor;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObjectUI>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugAxes) {
            if (Input.GetAxis("Horizontal") != 0)
            {
                Debug.Log("1st axis");
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                Debug.Log("2nd axis");
            }
            if (Input.GetAxis("3RDaxis") != 0)
            {
                Debug.Log("3rd axis");
            }
            if (Input.GetAxis("4THaxis") != 0)
            {
                Debug.Log("4th axis");
            }
            if (Input.GetAxis("5THaxis") != 0)
            {
                Debug.Log("5th axis");
            }
            if (Input.GetAxis("6THaxis") != 0)
            {
                Debug.Log("6th axis");
            }
            if (Input.GetAxis("7THaxis") != 0)
            {
                Debug.Log("7th axis");
            }
            if (Input.GetAxis("8THaxis") != 0)
            {
                Debug.Log("8th axis");
            }
            if (Input.GetAxis("9THaxis") != 0)
            {
                Debug.Log("9th axis");
            }
            if (Input.GetAxis("10THaxis") != 0)
            {
                Debug.Log("10th axis");
            }
            if (Input.GetAxis("11THaxis") != 0)
            {
                Debug.Log("11th axis");
            }
            if (Input.GetAxis("12THaxis") != 0)
            {
                Debug.Log("12th axis");
            }
            if (Input.GetAxis("13THaxis") != 0)
            {
                Debug.Log("13th axis");
            }
            if (Input.GetAxis("14THaxis") != 0)
            {
                Debug.Log("14th axis");
            }
            if (Input.GetAxis("15THaxis") != 0)
            {
                Debug.Log("15th axis");
            }
            if (Input.GetAxis("16THaxis") != 0)
            {
                Debug.Log("16th axis");
            }
            if (Input.GetAxis("17THaxis") != 0)
            {
                Debug.Log("17th axis");
            }
            if (Input.GetAxis("18THaxis") != 0)
            {
                Debug.Log("18th axis");
            }
            if (Input.GetAxis("19THaxis") != 0)
            {
                Debug.Log("19th axis");
            }
            if (Input.GetAxis("20THaxis") != 0)
            {
                Debug.Log("20th axis");
            }
            if (Input.GetAxis("21STaxis") != 0)
            {
                Debug.Log("21st axis");
            }
            if (Input.GetAxis("22NDaxis") != 0)
            {
                Debug.Log("22nd axis");
            }
            if (Input.GetAxis("23RDaxis") != 0)
            {
                Debug.Log("23rd axis");
            }
            if (Input.GetAxis("24THaxis") != 0)
            {
                Debug.Log("24th axis");
            }
            if (Input.GetAxis("25THaxis") != 0)
            {
                Debug.Log("25th axis");
            }
            if (Input.GetAxis("26THaxis") != 0)
            {
                Debug.Log("26th axis");
            }
            if (Input.GetAxis("27THaxis") != 0)
            {
                Debug.Log("27th axis");
            }
            if (Input.GetAxis("28THaxis") != 0)
            {
                Debug.Log("28th axis");
            }
        }
        else { 
        if (!startPlaying)
        {
            if (AnyButtonPressed())
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
        } else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedNotes;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes)*100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";
                if(percentHit > 60f)
                {
                    rankVal = "D";
                    if (percentHit > 70f)
                    {
                        rankVal = "C";
                        if (percentHit > 80f)
                        {
                            rankVal = "B";
                            if (percentHit > 90f)
                            {
                                rankVal = "A";
                                if (percentHit > 95f)
                                {
                                    rankVal = "S";
                                    if (percentHit == 100f)
                                    {
                                        rankVal = "SS";
                                    }

                                }

                            }

                        }

                    }

                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();

                resultsScreen.SetActive(true);

            }
        }
        }
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
    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        missedNotes++;
        beatMonitor.PulseImage(missedColor);
    }
    private bool AnyButtonPressed()
    {
        if (Input.anyKeyDown) return true;
        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0) return true;
        if (CrossPlatformInputManager.GetAxis("Vertical") != 0) return true;
        return false;
    }

}