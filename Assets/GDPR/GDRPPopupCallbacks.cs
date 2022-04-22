using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GDRPPopupCallbacks : MonoBehaviour
{
   
   


    public void Start()
    {
       


    }
    
   
    public void OnPrivacyButtonClicked()
    {
        Application.OpenURL("https://megusgaming.com/privacy.html");
    }
    
   

 
}
