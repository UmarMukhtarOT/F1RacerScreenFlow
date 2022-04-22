using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public int Totalcoins;
    public int TestCoins;

    public static CoinsManager instance;
    public bool GiveTestCoins;
    public Text TotalCoinsText;
   
    



    // Start is called before the first frame update
    void Start()
    {

        if (instance==null)
        {
            instance = this;
        }


        if (GiveTestCoins)
        {
            AddCoins(TestCoins);

        }


      //  AddCoins(100000);
        Totalcoins = PlayerPrefs.GetInt("TotalCoins");
        UpdateStat();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddCoins(int amount)
    {
        Totalcoins += amount;
        PlayerPrefs.SetInt("TotalCoins", Totalcoins);
        UpdateStat();
    }

    public void DedCoins(int amount)
    {
        Totalcoins -= amount;
        PlayerPrefs.SetInt("TotalCoins", Totalcoins);
        UpdateStat();
    }


    void UpdateStat()
    {

        if (TotalCoinsText)
        {
            TotalCoinsText.text = Totalcoins + "";

        }

    }


}
