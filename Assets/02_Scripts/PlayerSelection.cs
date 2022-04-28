using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


public class PlayerSelection : MonoBehaviour
{

    [System.Serializable]
    public class PlayerAttributes
    {
       
        public string Name;

        public Color DefaultColor;
        public GameObject PlayerObject;
        public Transform ActualPos;
        [Range(0, 100)]
        public int Speed;
        [Range(0, 100)]
        public int Handling;
        [Range(0, 100)]
        public int Acceleration;
        public bool Locked;


        public int amount;


    }


    [System.Serializable]
    public class Selection_Elements
    {
        public GameObject LoadingScreen;
        public GameObject CongratulationsTitle, EarnMoreTitle,MainPanel,CustomizerPanel;

        public Image FillBar;
        [Header("Player Attributes")]
       

        public Image Speed_Bar_Img;
        public Image Handling_Bar_Img;
        public Image Acceleration_Bar_Img;

        public Text InfoText;
        public Text TotalCash;


        [Header("UI Buttons")]
        public Button PlayBtn;
        public GameObject NextBtn;
        public GameObject PrevBtn;
        public GameObject BuyStatusPanel;
        public GameObject PricePanel;


        public Text BuyStatusTitleTxt, BuyStatusPhraseTxt;
        public Button BuyButton;
        public Button PlayButtonGenericMenu;
    }
    

    [Header("Scene Selection")]
    public string PreviousScene;
    public string NextScene;

    [Header("UI Elements")]
    public Selection_Elements Selection_UI;

    [Header("Player Attributes")]
    public PlayerAttributes[] Players;



    public Transform DisplayPos;


    AsyncOperation async = null;
    public static int current;

    public GameObject OnPurcheseConfetti;
    public GameObject CustomizerPromptPanel;
    
    public GameObject[] ObjToHideOnPrompt;
    public GameObject[] Custimzerobjects;
    public GameObject[] MainPanelObj;
    public GameObject[] UnderLineimg;
    public GameObject[] DecalStickers;
    
    public Transform [] PaintableBodies;


    public GameObject[] VehicleNameImages;
    //public TinyCarCustomizer Customizer;

    public static PlayerSelection Instance;



    public void PromptCustomizer()
    {
        ////if (Customizer.IsCustomizedBefore(current))
        //{
        //    CustomizerPromptPanel.SetActive(true);
        //    HideOnPrompt(false);
        //}
        //else
        //{
        //    OpenCustomizer();

        //}

    }


    public void HidePrompt()
    {

       // CustomizerPromptPanel.SetActive(false);
        HideOnPrompt(true);


    }

    public void OpenCustomizer()
    {


        CustomizerPromptPanel.SetActive(false);

        HideOnPrompt(true);

        foreach (var item in Custimzerobjects)
        {
            item.SetActive(true);
        }
        foreach (var item in MainPanelObj)
        {
            item.SetActive(false);
        }
     //   DragMouseOrbit.CantMoveAll = true;

      

        Selection_UI.MainPanel.SetActive(false);
     //   Customizer.StartCustomizer();
        

    }

    
    public void CloseCustomizer()
    {
        Debug.Log("sdfadfgfsdgsfbv");
        Selection_UI.MainPanel.SetActive(true);
        GetPlayerInfo();

        foreach (var item in Custimzerobjects)
        {
            item.SetActive(false);
        }
        foreach (var item in MainPanelObj)
        {
            item.SetActive(true);
        }
       

        // PaintableBodies[current].parent = Players[current].PlayerObject.transform;
       // DragMouseOrbit.CantMoveAll = false;

    }



    void Start()
    {
        current = PlayerPrefs.GetInt(PlayerPrefKeys.SelectedVehicleIndex);



       
        foreach (var item in Players)
        {

            item.Name = item.PlayerObject.name;

        }


     

        //  Customizer.LoadAllTexturesOnStart();
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1;



       // Selection_UI.FillBar.fillAmount = 0.7f;
        Selection_UI.LoadingScreen.SetActive(false);



        Selection_UI.TotalCash.text = CoinsManager.instance.Totalcoins.ToString();

        PlayerPrefs.SetString(Players[0].Name, "Unlocked");

        GetPlayerInfo();

        SoundManager.Instance.PlayBG(SoundManager.Instance.MMSound, 0.5f);

       

    }

    
    void Update()
    {
        //if (async != null)
        //{
        //    Selection_UI.FillBar.fillAmount = async.progress;
        //    if (async.progress >= 0.9f)
        //    {
        //        Selection_UI.FillBar.fillAmount = 1.0f;
        //    }
        //}

       
    }


    public void UnlockAllVehiclePurchased()
    {


        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPrefs.SetString(Players[i].Name, "Unlocked");
        }
        /// unlocl all vehicle 
    }


    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Megus+Games&hl=en");
    }



    void GetPlayerInfo()
    {


        foreach (var item in Players)
        {
            item.PlayerObject.SetActive(false);
        }
      foreach (var item in VehicleNameImages)
        {
            item.SetActive(false);
        }
       

        Players[current].PlayerObject.SetActive(true);
        VehicleNameImages[current].SetActive(true);
        


        if (Players[current].Locked)
        {

            Selection_UI.PlayBtn.gameObject.SetActive(false);
            Selection_UI.BuyButton.gameObject.SetActive(true);
            Selection_UI.PlayButtonGenericMenu.interactable = false;
            Selection_UI.InfoText.text = Players[current].amount.ToString();

        }
        else
        {

            Selection_UI.PlayBtn.gameObject.SetActive(true);
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.PlayButtonGenericMenu.interactable = true;

        }


        if (current == 0)
        {
            Selection_UI.PrevBtn.SetActive(false);
            Selection_UI.NextBtn.SetActive(true);
            Selection_UI.BuyStatusPanel.SetActive(false);
        }
        else if (current == Players.Length - 1)
        {
            Selection_UI.PrevBtn.SetActive(true);
            Selection_UI.NextBtn.SetActive(false);
        }
        else
        {
            Selection_UI.PrevBtn.SetActive(true);
            Selection_UI.NextBtn.SetActive(true);
        }


       
        Selection_UI.Speed_Bar_Img.fillAmount = Players[current].Speed * 0.01f;
        Selection_UI.Handling_Bar_Img.fillAmount = Players[current].Handling * 0.01f;
        Selection_UI.Acceleration_Bar_Img.fillAmount = Players[current].Acceleration * 0.01f;



       
       

        if (Players[current].amount <= CoinsManager.instance.Totalcoins)
        {
            Selection_UI.InfoText.color = Color.green;

        }
        else
        {
            Selection_UI.InfoText.color = Color.red;
        }


        if (PlayerPrefs.GetString(Players[current].Name) == "Unlocked")
        {
          
            Selection_UI.PlayBtn.gameObject.SetActive(true);
            Selection_UI.PlayButtonGenericMenu.interactable = true;
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.InfoText.color = Color.green;

             //Selection_UI.PricePanel.SetActive(false);
           

        }
        //else
        //{
        //    Selection_UI.PricePanel.SetActive(true);




        //}

    }

    public void Buy()
    {
        OnPurcheseConfetti.SetActive(false);
        if (Players[current].amount <= CoinsManager.instance.Totalcoins)
        {
            CoinsManager.instance.DedCoins(Players[current].amount);
            Selection_UI.TotalCash.text = "COINS :" + CoinsManager.instance.Totalcoins.ToString();

            Players[current].Locked = false;
            PlayerPrefs.SetString(Players[current].Name, "Unlocked");
            Selection_UI.InfoText.color = Color.green;
           

            Selection_UI.PlayBtn.gameObject.SetActive(true);
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.PlayButtonGenericMenu.interactable = true;

            OnPurcheseConfetti.SetActive(true);
           
        }
        else

        {

            HideOnPrompt(false);

            Selection_UI.BuyStatusPanel.SetActive(true);
            Selection_UI.CongratulationsTitle.SetActive(false);
            Selection_UI.EarnMoreTitle.SetActive(true);
            Selection_UI.BuyStatusPhraseTxt.text = "Insufficient coins";

        }
        GetPlayerInfo();
        SoundManager.Instance.PlayBtnClick();
    }

    public void Previous()
    {
       

        current--;
        GetPlayerInfo();

       
        SoundManager.Instance.PlayBtnClick();
    }

    public void Next()
    {
        current++;
        GetPlayerInfo();
        SoundManager.Instance.PlayBtnClick();
    }



    public void PlayLevel()
    {

        Players[current].PlayerObject.SetActive(false);

        PlayerPrefs.SetInt(PlayerPrefKeys.SelectedVehicleIndex, current);
        HideOnPrompt(false);
        Selection_UI.LoadingScreen.SetActive(true);
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
        Selection_UI.BuyStatusPanel.SetActive(false);
        HideOnPrompt(true);

    }





    public void BackBtn()
    {

        HidePrompt();
        Selection_UI.LoadingScreen.SetActive(true);
        SceneManager.LoadScene(PreviousScene.ToString());
    }

      public void HideOnPrompt(bool check)
      {
        foreach (var item in ObjToHideOnPrompt)
        {
            item.SetActive(check);
        }

      } 


}
