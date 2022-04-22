using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string nextScene;

    public Image FillImage;
    AsyncOperation async = null;
    public GameObject loadingScreen, SettingPanel, PrivacyPanel, StorePanel, RewardedPanel, ExitPanel, mainPanel,UserProfilePanel;
    public Text CoinTxt,UsernameDisplayText;
    public static bool ShowingPrivacy = true;
    public int TotalCars = 11, TotalLevels = 15;
    public InputField inputField;
    private string _userName;



     public string userName
     {
        get
        {
            if (_userName == string.Empty||_userName == null)
                _userName = PlayerPrefs.GetString("PlayerName", "Player");
            return _userName;
        }
        set
        {
            _userName = value;
            PlayerPrefs.SetString("PlayerName", _userName);
        }
     }







    public void Awake()
    {
        inputField.text = userName;
        UsernameDisplayText.text = userName;
    }







    void Start()
    {

        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            PlayerPrefs.SetInt("FirstPlay", 0);
            nextScene = "03_GamePlay";
            PromptUserNamePanel();


        }
        else
        {
            UsernameDisplayText.text = GetUserName();
            UserProfilePanel.SetActive(false);
        }

        FillImage.fillAmount = 0;
        CoinTxt.text = CoinsManager.instance.Totalcoins + "";
      

    

        Time.timeScale = 1;
     

    }



   

   
    void Update()
    {

        if (async != null)
        {
            FillImage.fillAmount = async.progress;
            if (async.progress >= 0.9f)
            {
                FillImage.fillAmount = 1.0f;
            }
        }

    }


    public void Play()
    {
        StartCoroutine(LevelStart());
        loadingScreen.SetActive(true);
      
    }


    public void SetUserName(string text)
    {
        userName = text;
    }

    public string GetUserName()
    {
        return _userName;

    }


    public void PromptUserNamePanel()
     {
        UserProfilePanel.SetActive(true);
        

     }


    public void OnUserNameNotedOK()
    {

        SetUserName(inputField.text);
        UsernameDisplayText.text = GetUserName();

        UserProfilePanel.SetActive(false);

    }





    IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(1.5f);
        async = SceneManager.LoadSceneAsync(nextScene);
        yield return async;
    }









    public void PromptExit(bool check)
    {
        ExitPanel.SetActive(check);
        mainPanel.SetActive(!check);

    }


    public void ExitYes()
    {
        Debug.Log("quit app");
        Application.Quit();
    }

    public void store()
    {
        StorePanel.SetActive(true);
        mainPanel.SetActive(false);

    }

    public void BackFromstore()
    {
        StorePanel.SetActive(false);
        mainPanel.SetActive(true);

    }

    public void Settings()
    {
        SettingPanel.SetActive(true);
    }


    public void GetCoins()
    {
        CloseRewardPanel();
        //***AD Call***//
       // AdsManagerWrapper.Instance.ShowRewardedVideo(RewardedAd);

    }


    public void RewardedAd()
    {
        CoinsManager.instance.AddCoins(1000);

    }



    public void AskForReward()
    {
        RewardedPanel.SetActive(true);
    }

    public void CloseRewardPanel()
    {
        RewardedPanel.SetActive(false);


    }

}
