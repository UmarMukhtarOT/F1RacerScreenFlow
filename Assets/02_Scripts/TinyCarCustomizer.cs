//using UnityEngine;
//using UnityEngine.UI;
//using HSVPickerExamples;
//using HSVPicker;


//public class TinyCarCustomizer : MonoBehaviour
//{
//    public enum CurrentState
//    {
//        Main = 0,
//        Decal = 1,
//        Paint = 2,
//        Rim = 3
//    };



//    [System.Serializable]
//    public class UI_Elements
//    {
//        [Header("UI Buttons")]

//        public GameObject SaveButton;//will not be used. The paste button will do this task instead.
//        public GameObject PasteButton;
//        public GameObject UndoButton;
//        public GameObject RestartButton;//will not be used
//        public GameObject PerspectiveButton;//will not be used
        


//        [Header("Panels")]
//        public GameObject MainPanel;
//        public GameObject DecalPanel;
//        public GameObject RimPanel;
//        public GameObject PaintPanel;
//        public GameObject DecalTouchPanel;
//        public GameObject DecalBtn;
//        public GameObject PaintBtn;


//    }



//    public CurrentState State;
//    [Header("------UI Elements------")]
//    public UI_Elements UI;
//    public int PlayerCount;
//    public LiveryCreator PaintScript;
   
   

//    [Header("------Body Paint Elements------")]

  
//    public ColorPicker CarBadyColorPicker;
//    public ColorPicker DecalColorPicker;



//    // Start is called before the first frame update
//    void Start()
//    {
//        SelectState(0);
//        UI.UndoButton.SetActive(false);
       
//    }




//    // Update is called once per frame
//    void Update()
//    {

//    }


//    public void LoadCarTexture(int index, MeshRenderer rend)
//    {

//        rend.material = PaintScript.canvasMaterial;
       

//        if (!IsCustomizedBefore(index))
//        {
//          //  Debug.Log("Not Customized");
//            PaintScript.canvasMaterial.color = PlayerSelection.Instance.Players[index].DefaultColor ;
//            PaintScript.canvasMaterial.SetTexture("_MainTex", null);
//        }
//        else
//        {
//            PaintScript.Load(("PaintableBody0" + index));
//            Color color;
//            if (ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(("BodyPaintCode" + index)), out color))
//                PaintScript.canvasMaterial.color = color;
//            Debug.Log("Customized");

//        }

//        rend.material = PaintScript.canvasMaterial;

//        //if (!PlayerPrefs.HasKey("BodyPaintCode"+index))
//        //{

//        //   PlayerPrefs.SetString("BodyPaintCode"+index,DefaultPaintCode[index]);

//        //}


//    }





//    public void StartCustomizer()
//    {
//        PaintScript.RunOnStart();
//        PaintScript.Decal=null;
//        SelectState(0);

//        if (!IsCustomizedBefore(PlayerSelection.current))
//        {
           
//            PaintScript.canvasMaterial.color = PlayerSelection.Instance.Players[PlayerSelection.current].DefaultColor;
           
//        }
//        else
//        {
           
//            Color color;
//            if (ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(("BodyPaintCode" + PlayerSelection.current)), out color))
//                PaintScript.canvasMaterial.color = color;
           

//        }

//    }

//    public bool IsCustomizedBefore(int index) {



//        if (PlayerPrefs.HasKey("PaintableBody0" + index)|| PlayerPrefs.HasKey("BodyPaintCode" + index))
//        {

//            return true;

//        }
//        else
//        {

//            return false;


//        }


//    }







//    public void UpdateStateUI()
//    {
//        switch (State)
//        {
//            case CurrentState.Main:

//                UI.MainPanel.SetActive(true);
//                UI.DecalPanel.SetActive(false);
//                UI.RimPanel.SetActive(false);
//                UI.PaintPanel.SetActive(false);
//                PaintScript.Decal = null;
//                //UI.DecalBtn.SetActive(true);
//                //UI.PaintBtn.SetActive(true);


//                break;
//            case CurrentState.Decal:
//                UI.MainPanel.SetActive(false);
//                UI.DecalPanel.SetActive(true);
//                UI.RimPanel.SetActive(false);
//                UI.PaintPanel.SetActive(false);
//              //  UI.DecalTouchPanel.SetActive(true);
//                //UI.DecalBtn.SetActive(false);
//                //UI.PaintBtn.SetActive(false);





//                break;
//            case CurrentState.Rim:
//                UI.MainPanel.SetActive(false);
//                UI.DecalPanel.SetActive(false);
//                UI.RimPanel.SetActive(true);
//                UI.PaintPanel.SetActive(false);
//               // UI.DecalTouchPanel.SetActive(false);
//                PaintScript.Decal = null;
//                //UI.DecalBtn.SetActive(false);
//                //UI.PaintBtn.SetActive(false);


//                break;
//            case CurrentState.Paint:
//                UI.MainPanel.SetActive(false);
//                UI.DecalPanel.SetActive(false);
//                UI.RimPanel.SetActive(false);
//                UI.PaintPanel.SetActive(true);
//              //  UI.DecalTouchPanel.SetActive(false);
//                PaintScript.Decal = null;
//                //UI.DecalBtn.SetActive(false);
//                //UI.PaintBtn.SetActive(false);

//                break;


//            default:
//                break;
//        }

//    }




//    public void SelectState(int num)
//    {

//        State = (CurrentState)num;
//        UpdateStateUI();
//      //  Debug.Log("State is " + State);


//    }



//    //this will called as Done Button, it will paste and SAVE  against 
//    public void PasteDecal()
//    {
//        UI.SaveButton.SetActive(true);
//        UI.UndoButton.SetActive(true);
//        UI.PasteButton.SetActive(false);
//        PaintScript.PasteDecal();
//      //  PaintScript.brushCursor.SetActive(false);



//    }


//    public void Save()
//    {
//        UI.SaveButton.SetActive(false);
//        UI.UndoButton.SetActive(false);
//        PaintScript.SaveLivery(("PaintableBody0" + PlayerSelection.current));
//        //PlayerPrefs.SetString("BodyPaintCode"+ PlayerSelection.current, ColorToHex(CarBadyColorPicker.CurrentColor));
//        PlayerPrefs.SetString("BodyPaintCode"+ PlayerSelection.current, ColorToHex(PaintScript.canvasMaterial.color));

//    }





//    public void PaintTheBody()
//    {

//        PaintScript.canvasMaterial.color = CarBadyColorPicker.CurrentColor;

//    }

//    public void SetDecalColor()
//    {

//       // PaintScript.deca
//            // = CarBadyColorPicker.CurrentColor;


//    }





//    public bool displayAlpha;

//    private string ColorToHex(Color32 color)
//    {
//        return displayAlpha
//            ? string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.r, color.g, color.b, color.a)
//            : string.Format("#{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
//    }



//}
