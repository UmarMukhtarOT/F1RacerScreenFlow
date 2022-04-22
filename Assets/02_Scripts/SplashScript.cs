using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{

    public float delay;
    public string nextScene;
    public Slider FillBar;
    AsyncOperation async = null;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SplashCor());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (async != null)
        {
           FillBar.value = async.progress;
            if (async.progress >= 0.9f)
            {
                FillBar.value = 1.0f;
            }
        }


    }


    IEnumerator SplashCor()
    {

        yield return new WaitForSeconds(delay);
        async = SceneManager.LoadSceneAsync(nextScene.ToString());
        yield return async;


    }

    


}
