using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text AnnouncerTextLine;
    public Text LevelTimer;

    public Text playerName;
    public Text enemyName;

    public string plName;
    public string enName;

    public Slider[] healthSliders;

    private void Start()
    {
        playerName.text = plName;
        enemyName.text = enName;
    }

    public static LevelUI instance;
    public static LevelUI GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

}
