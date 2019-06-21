using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Text startBut;
    public GameObject screens;
    Animator animText;
    Animator animScreens;
    public Image[] loadingScreens;

    public Image loadingImagePr;
    public Image loadingImage;
    public Text progressText;

    public AudioSource background;

    SceneLoading sl;

    AudioSource audioS;
    
    // Start is called before the first frame update
    void Start()
    {
        animText = startBut.GetComponent<Animator>();
        animScreens = screens.GetComponent<Animator>();
        sl = GetComponent<SceneLoading>();

        loadingImage.gameObject.SetActive(false);
        progressText.gameObject.SetActive(false);
        loadingImagePr.gameObject.SetActive(false);
    }

    bool loading = false;
    int id;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            id = 1;
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            id = 2;
        } else
        {
            id = RandomMap();
        }

        if (Input.anyKeyDown && !loading)
        {
            audioS = startBut.GetComponent<AudioSource>();
            animText.SetTrigger("Start");
            audioS.Play();

            animScreens.SetBool("Map" + id, true);
            loading = true;
            
            StartCoroutine(AsyncLoading(id+1));
        }

        if (loading)
        {
            StartCoroutine(FadeOutSound());
        }
    }

    public float fadeTime = 1;

    IEnumerator FadeOutSound()
    {
        float t = background.volume;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime;
            background.volume = t / fadeTime;
        }

        yield break;
    }

    IEnumerator AsyncLoading(int id)
    {
        loadingImage.gameObject.SetActive(true);
        progressText.gameObject.SetActive(true);
        loadingImagePr.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);

        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);

            yield return null;
        }
    }

    int RandomMap()
    {
        var rand = Random.Range(1, 2);
        Debug.Log(rand);

        return rand;
    }
}
