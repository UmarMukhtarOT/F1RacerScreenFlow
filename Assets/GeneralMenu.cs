using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralMenu : MonoBehaviour
{
    public int screenNum;
    public GameObject shopPanel,eventsPanel,loadingPanel;
    public Button[] lowerButtons;
    public string garageSceneName, mainMenuSeneName,gamePlaySceneName;



    // Start is called before the first frame update
    void Start()
    {

        if (screenNum==0)
        {
            foreach (var item in lowerButtons)
            {
                item.interactable = true;

            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    void SetUPScreen(int Num)
    {

        lowerButtons[Num].interactable = false;
        switch (Num)
        {

            
            case 0:
                shopPanel.SetActive(true);

                break;

            case 1:
            loadingPanel.SetActive(true);
            SceneManager.LoadScene(garageSceneName);
           
                break;

            case 2:
            loadingPanel.SetActive(true);
            SceneManager.LoadScene(gamePlaySceneName);
           
                break;
            case 3:
            eventsPanel.SetActive(true);
          
           
                break;



            default:
                break;
        }



    }














}
