using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{

    public string nextScene = "GamePlay";
    public GameObject GdprPopUp;












    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("GdprAccepted"))
        {

            GdprPopUp.SetActive(true);
            //  Debug.Log("Gdpr Value  First Time" + PlayerPrefs.GetInt("GdprAccepted"));

        }
        else
        {
            GdprSelected(PlayerPrefs.GetInt("GdprAccepted"));
            //  Debug.Log("Gdpr Value " + PlayerPrefs.GetInt("GdprAccepted"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Load()
    {
        // Debug.Log("loading MM");
        yield return new WaitForSeconds(4);
        //AdsManagerWrapper.Instance.ShowBannerSmart();

        SceneManager.LoadScene(nextScene);
    }



    public void GdprSelected(int status)
    {

        PlayerPrefs.SetInt("GdprAccepted", status);


        //try
        {
            if (PlayerPrefs.GetInt("GdprAccepted") == 0)
            {
                // Debug.Log("ads manager Initialized with Gdpr Not Accepted");

              //  AdsManagerWrapper.Instance.initialize(false);
            }
            else
            {
                //  Debug.Log("ads manager Initialized with Gdpr Accepted");
             //   AdsManagerWrapper.Instance.initialize(true);
            }

        }
        //catch
        //{

        //    Debug.Log("Failed to Initialize ads Manager");
        //}

        GdprPopUp.SetActive(false);
        StartCoroutine(Load());

    }





}
