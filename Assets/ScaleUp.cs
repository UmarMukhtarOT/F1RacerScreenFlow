using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{

    public Transform fill;
    private  Vector3 BtnDownScale=new Vector3(2f,2f,2f);
    private  Vector3 BtnUpScale = new Vector3(1.2f, 1.2f, 1.2f);
    
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BtnPressed()
    {

        transform.localScale = BtnDownScale;
        fill.transform.localScale = BtnDownScale;



    }


    public void BtnReleased()
    {
        transform.localScale = BtnUpScale;
        fill.transform.localScale = BtnUpScale;


    }
}



