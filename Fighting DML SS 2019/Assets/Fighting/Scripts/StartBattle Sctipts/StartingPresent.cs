using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingPresent : MonoBehaviour
{
    private CinematicBars bars;
    public GameObject fade;
    public GameObject wall;
    public GameObject player_1, player_2;

    private Animator anim;
    Camera cam;
    private bool skip;

    LevelManager lm;
    LevelUI levelUI;

    // Start is called before the first frame update
    void Start()
    {
        levelUI = LevelUI.GetInstance();

        levelUI.AnnouncerTextLine.gameObject.SetActive(false);
        levelUI.healthSliders[0].gameObject.SetActive(false);
        levelUI.healthSliders[1].gameObject.SetActive(false);
        levelUI.LevelTimer.gameObject.SetActive(false);

        fade.SetActive(true);
        bars = GetComponentInChildren<CinematicBars>();
        bars.Show(200, 1);

        anim = GetComponent<Animator>();

        //player_1.GetComponent<StateManager>().dontMove = true;
        //player_2.GetComponent<StateManager>().dontMove = true;

        DisableFighters();


        cam = GetComponentInChildren<Camera>();
        lm = GetComponent<LevelManager>();
        lm.enabled = false;


        anim.SetInteger("Location", 1);
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Camera_Start_Done_Boss1") || skip || anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            wall.SetActive(false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            fade.SetActive(false);
            cam.enabled = false;
            bars.Hide(1);

            lm.enabled = true;
            levelUI.healthSliders[0].gameObject.SetActive(true);
            levelUI.healthSliders[1].gameObject.SetActive(true);
            levelUI.LevelTimer.gameObject.SetActive(true);

            //player_2.GetComponent<StateManager>().dontMove = false;
            //player_1.GetComponent<StateManager>().dontMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            anim.SetTrigger("Skip");
            skip = true;
        }
    }

    public void DisableFighters()
    {
        player_1.GetComponent<InputHandle>().enabled = false;
        player_2.GetComponent<AI_Controller>().enabled = false;
    }

    public void EnableFighters()
    {
        player_1.GetComponent<InputHandle>().enabled = true;
        player_2.GetComponent<AI_Controller>().enabled = true;
    }
}
