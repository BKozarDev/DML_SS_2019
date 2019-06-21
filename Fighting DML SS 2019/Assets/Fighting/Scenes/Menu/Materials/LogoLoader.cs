using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoLoader : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        load = false;
        source.clip = clips[0];
        source.Play();

        StartCoroutine(Clips());
    }

    IEnumerator Clips()
    {
        yield return new WaitForSeconds(2);
        source.clip = clips[1];
        source.Play();
    }

    public bool load;

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            SceneManager.LoadScene(1);
        }
    }
}
