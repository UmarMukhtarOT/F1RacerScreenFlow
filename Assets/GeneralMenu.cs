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

        if (screenNum==1)
        {

            DisablCurrentScreenBtn();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





  public  void SetUPScreen(int Num)
    {

        lowerButtons[Num].interactable = false;
        switch (Num)
        {

            
            case 0:
                shopPanel.SetActive(true);

                break;

            case 1:
           
           StartCoroutine(LoadLevel(garageSceneName));
           
                break;

            case 2:
            
           StartCoroutine(LoadLevel(gamePlaySceneName));
           
                break;
            case 3:
            eventsPanel.SetActive(true);
          
           
                break;



            default:
                break;
        }



  }




    IEnumerator LoadLevel(string Name)
    {
        foreach (var item in lowerButtons)
        {
            item.gameObject.SetActive(false);
        }
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(Name);

    }



    public void LoadMainMenu()
    {

        StartCoroutine(LoadLevel(mainMenuSeneName));

    }




    public void DisablCurrentScreenBtn()
    {
        lowerButtons[screenNum].interactable = false;



    }





}
