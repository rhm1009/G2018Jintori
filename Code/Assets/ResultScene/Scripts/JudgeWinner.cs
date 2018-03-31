using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeWinner : MonoBehaviour {

    public bool LightOn;
    public GameObject SpotLight;
    // Score Panel... read on drag&drop
    public GameObject ScorePnl;


    public GameObject Result, Win, Draw;

    // true = Win / false = Draw
    bool bWin;
    // 1 = red ...
    public int iWinner;
    // 0~3, drawer -> true
    public bool[] bDrawers;

    void Start()
    {
        LightOn = false;

        iWinner = 0;
        bDrawers = new bool[4];

        int[] score = ScorePnl.GetComponent<ReadScore>().Score;
        bWin = JudgeGame(score);
    }
    void Update()
    {
        if (SpotLight.activeSelf && !LightOn)
        {
            LightOn = true;
            Result.SetActive(false);

            if (bWin)
            {
                Win.SetActive(true);
                SetWinner(iWinner);
            }
            else
            {
                Draw.SetActive(true);
                SetDraw(bDrawers);
            }
        }
    }

    bool JudgeGame(int[] scr_)
    {
        for (int i = 0, max = 0; i < 4; i++)
        {
            if (scr_[i] > max)
            {
                iWinner = i;
                max = scr_[i];
            }
            else if (scr_[i] == max && max != 0)
            {
                bDrawers[iWinner] = true;
                bDrawers[i] = true;
            }
        }

        if (bDrawers[0] || bDrawers[1] ||
            bDrawers[2] || bDrawers[3])
            return false;
        else return true;
    }

    void SetWinner(int p_)
    {
        // PC : Player Color
        Color PC = Color.white;
        if (p_ == 0) PC = Color.red;
        else if (p_ == 1) PC = Color.blue;
        else if (p_ == 2) PC = Color.yellow;
        else if (p_ == 3) PC = Color.green;

        Win.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PC;
        Win.transform.GetChild(1).GetComponent<TextMesh>().color = PC + Color.white / 4.0f;
        Win.transform.GetChild(1).GetComponent<TextMesh>().text = (p_ + 1) + "P";
    }

    void SetDraw(bool[] ps_)
    {
        for (int i = 0; i < 4; i++)
        {
            Draw.transform.GetChild(i).gameObject.SetActive(ps_[i]);
        }
    }
}
