using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    WaitForSeconds oneSec;

    LevelUI levelUI;
    StartingPresent sp;

    public bool countdown;
    public int maxTurnTimer = 50;
    int currentTimer;
    float internalTimer;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        levelUI = LevelUI.GetInstance();

        sp = GetComponent<StartingPresent>();

        oneSec = new WaitForSeconds(1);

        levelUI.AnnouncerTextLine.gameObject.SetActive(false);

        StartCoroutine("StartGame");
    }

    private void Update()
    {
        if (countdown)
        {
            HandleTurnTimer();
        }
    }

    bool end = false;

    void HandleTurnTimer()
    {
        levelUI.LevelTimer.text = currentTimer.ToString();

        internalTimer += Time.deltaTime;

        if (internalTimer > 1)
        {
            currentTimer--;
            internalTimer = 0;
        }

        if (currentTimer <= 0)
        {
            EndTurnFunction(true);
            countdown = false;
        }

        Debug.Log(levelUI.healthSliders[1].value);

        if ((levelUI.healthSliders[0].value <= 0) || (levelUI.healthSliders[1].value <= 0))
        {
            EndTurnFunction(false);
        }
    }

    IEnumerator StartGame()
    {
        yield return InitTurn();
    }

    IEnumerator InitTurn()
    {

        //levelUI.AnnouncerTextLine.gameObject.SetActive(false);

        currentTimer = maxTurnTimer;
        countdown = false;
        yield return EnableControl();
    }

    IEnumerator EnableControl()
    {
        levelUI.AnnouncerTextLine.gameObject.SetActive(true);
        levelUI.AnnouncerTextLine.text = "Ready?";
        levelUI.AnnouncerTextLine.color = Color.white;

        yield return oneSec;
        yield return oneSec;

        levelUI.AnnouncerTextLine.text = "3";
        levelUI.AnnouncerTextLine.color = Color.green;
        yield return oneSec;
        levelUI.AnnouncerTextLine.text = "2";
        levelUI.AnnouncerTextLine.color = Color.yellow;
        yield return oneSec;
        levelUI.AnnouncerTextLine.text = "1";
        levelUI.AnnouncerTextLine.color = Color.red;
        yield return oneSec;
        levelUI.AnnouncerTextLine.text = "Fight!";
        levelUI.AnnouncerTextLine.color = Color.red;

        sp.EnableFighters();

        yield return oneSec;
        levelUI.AnnouncerTextLine.gameObject.SetActive(false);
        countdown = true;
    }

    public void EndTurnFunction(bool timeOut = false)
    {
        countdown = false;

        levelUI.LevelTimer.text = maxTurnTimer.ToString();

        if (timeOut)
        {
            levelUI.AnnouncerTextLine.gameObject.SetActive(true);
            levelUI.AnnouncerTextLine.text = "Time Out!";
            levelUI.AnnouncerTextLine.color = Color.cyan;
        } else
        {
            levelUI.AnnouncerTextLine.gameObject.SetActive(true);
            levelUI.AnnouncerTextLine.text = "K.O.";
            levelUI.AnnouncerTextLine.color = Color.red;
        }

        sp.DisableFighters();

        StartCoroutine(EndTurn());
    }

    IEnumerator EndTurn()
    {
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;

        if(levelUI.healthSliders[0].value*2 > levelUI.healthSliders[1].value)
        {
            levelUI.AnnouncerTextLine.text = levelUI.playerName.text + " Wins!";
            levelUI.AnnouncerTextLine.color = Color.green;
        } else if(levelUI.healthSliders[0].value * 2 < levelUI.healthSliders[1].value)
        {
            levelUI.AnnouncerTextLine.text = levelUI.enemyName.text + " Wins!";
            levelUI.AnnouncerTextLine.color = Color.red;
        } else if(levelUI.healthSliders[0].value * 2 == levelUI.healthSliders[1].value)
        {
            levelUI.AnnouncerTextLine.text = "Draw!";
            levelUI.AnnouncerTextLine.color = Color.blue;
        }

        yield return oneSec;
        yield return oneSec;
        yield return oneSec;

        levelUI.AnnouncerTextLine.gameObject.SetActive(false);
    }


}
