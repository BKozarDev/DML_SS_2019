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
    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
        bars = GetComponentInChildren<CinematicBars>();
        bars.Show(300, 1);

        anim = GetComponent<Animator>();

        //player_1.GetComponent<StateManager>().dontMove = true;
        //player_2.GetComponent<StateManager>().dontMove = true;

        player_1.GetComponent<InputHandle>().enabled = false;
        player_2.GetComponent<AI_Controller>().enabled = false;

        cam = GetComponentInChildren<Camera>();

        anim.SetInteger("Location", 1);
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Camera_Start_Done_Boss1") || skip)
        {
            wall.SetActive(false);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            fade.SetActive(false);
            cam.enabled = false;
            bars.Hide(1);

            player_1.GetComponent<InputHandle>().enabled = true;
            player_2.GetComponent<AI_Controller>().enabled = true;
            //player_2.GetComponent<StateManager>().dontMove = false;
            //player_1.GetComponent<StateManager>().dontMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            anim.SetTrigger("Skip");
            skip = true;
        }
    }
}
