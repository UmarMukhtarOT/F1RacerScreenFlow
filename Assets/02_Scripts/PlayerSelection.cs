using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSelection : MonoBehaviour
{

   
    public string PreviousScene;
    public string NextScene;

    public Transform DisplayPos;
    public static int current;
   
    public GameObject OnPurcheseConfetti;
    public static PlayerSelection Instance;
    


    [Header("UI Elements Refrence")]
    public UIRefManager UI_RefScript;

    [Space(10)]
    [Header("Vehical's Attributes")]
    public VehicleStatsScriptableObject[] Players;

    private GameObject currentVehicle;


    void Start()
    {
        current = PlayerPrefs.GetInt("SelectedPlayer");



     

      
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1;



        UI_RefScript.LoadingFillBar.fillAmount = 0.7f;
        UI_RefScript.LoadingScreen.SetActive(false);



        UI_RefScript.TotalCash.text = CoinsManager.instance.Totalcoins.ToString();

        PlayerPrefs.SetString(Players[0].vehicleName, "Unlocked");

        GetPlayerInfo();

        SoundManager.Instance.PlayBG(SoundManager.Instance.MMSound, 0.5f);

       
    }

    private void ShowVehicle()
    {
        if (currentVehicle)
            Destroy(currentVehicle);

        currentVehicle = Instantiate(Players[current].PlayerObject);
    }

    void Update()
    {
       
       
    }


    public void UnlockAllVehiclePurchased()
    {


        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPrefs.SetString(Players[i].vehicleName, "Unlocked");
        }
        /// unlocl all vehicle 
    }




    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Megus+Games&hl=en");
    }



    void UpdateUIstatus()
    {

        if (Players[current].IsLocked)
        {

            UI_RefScript.PlayBtn.gameObject.SetActive(false);
            UI_RefScript.BuyButton.gameObject.SetActive(true);

            UI_RefScript.InfoText.text = Players[current].Price.ToString();

        }
        else
        {

            UI_RefScript.PlayBtn.gameObject.SetActive(true);
            UI_RefScript.BuyButton.gameObject.SetActive(false);

        }



        if (current == 0)
        {
            UI_RefScript.PrevBtn.SetActive(false);
            UI_RefScript.NextBtn.SetActive(true);
            UI_RefScript.BuyStatusPanel.SetActive(false);
        }
        else if (current == Players.Length - 1)
        {
            UI_RefScript.PrevBtn.SetActive(true);
            UI_RefScript.NextBtn.SetActive(false);
        }
        else
        {
            UI_RefScript.PrevBtn.SetActive(true);
            UI_RefScript.NextBtn.SetActive(true);
        }

    }






    void GetPlayerInfo()
    {


        UpdateUIstatus();






       
     //   UI_RefScript.Speed_Bar_Img.fillAmount = Players[current].speed * 0.01f;
      //  UI_RefScript.Boost_Bar_Img.fillAmount = Players[current].boost * 0.01f;
     //   UI_RefScript.Acceleration_Bar_Img.fillAmount = Players[current].acceleration * 0.01f;



       
       

        if (Players[current].Price <= CoinsManager.instance.Totalcoins)
        {
            UI_RefScript.InfoText.color = Color.green;

        }
        else
        {
            UI_RefScript.InfoText.color = Color.red;
        }


        if (PlayerPrefs.GetString(Players[current].vehicleName) == "Unlocked")
        {
          
            UI_RefScript.PlayBtn.gameObject.SetActive(true);
           
            UI_RefScript.BuyButton.gameObject.SetActive(false);
            UI_RefScript.InfoText.color = Color.green;

            UI_RefScript.PricePanel.SetActive(false);
           

        }
        else
        {
            UI_RefScript.PricePanel.SetActive(true);




        }

    }










    public void Buy()
    {
        OnPurcheseConfetti.SetActive(false);
        if (Players[current].Price <= CoinsManager.instance.Totalcoins)
        {
            CoinsManager.instance.DedCoins(Players[current].Price);
            UI_RefScript.TotalCash.text = "COINS :" + CoinsManager.instance.Totalcoins.ToString();

            Players[current].IsLocked = false;
            PlayerPrefs.SetString(Players[current].vehicleName, "Unlocked");
            UI_RefScript.InfoText.color = Color.green;
           

            UI_RefScript.PlayBtn.gameObject.SetActive(true);
            UI_RefScript.BuyButton.gameObject.SetActive(false);
           

            OnPurcheseConfetti.SetActive(true);
           
        }
        else

        {



            UI_RefScript.BuyStatusPanel.SetActive(true);
            UI_RefScript.CongratulationsTitle.SetActive(false);
            UI_RefScript.EarnMoreTitle.SetActive(true);
            UI_RefScript.BuyStatusPhraseTxt.text = "Insufficient coins";

        }
        GetPlayerInfo();
        SoundManager.Instance.PlayBtnClick();
    }

    public void Previous()
    {
       

        current--;
        GetPlayerInfo();

        PlayerPrefs.SetInt("SelectedPlayer",current);
        SoundManager.Instance.PlayBtnClick();
    }

    public void Next()
    {
        current++;
        GetPlayerInfo();
        PlayerPrefs.SetInt("SelectedPlayer", current);

        SoundManager.Instance.PlayBtnClick();
    }



    public void PlayLevel()
    {

        Players[current].PlayerObject.SetActive(false);

        PlayerPrefs.SetInt("SelectedPlayer", current);
       // Debug.Log("SelectedPlayer Pref val: " + PlayerPrefs.GetInt("SelectedPlayer"));
       
        UI_RefScript.LoadingFillBar.fillAmount = 0.9f;

        UI_RefScript.LoadingScreen.SetActive(true);
        SoundManager.Instance.PlayBtnClick();
        StartCoroutine(LevelStart());
    }


    IEnumerator LevelStart()
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextScene.ToString());
       
    }


    public void closeStatusPanel()
    {
        Debug.Log(" clicked");
        UI_RefScript.BuyStatusPanel.SetActive(false);
     

    }

    public void BackBtn()
    {

       
        UI_RefScript.LoadingScreen.SetActive(true);
        SceneManager.LoadScene(PreviousScene.ToString());
    }

   
}
