using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

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
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
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
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }
    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        missedNotes++;
    }
}