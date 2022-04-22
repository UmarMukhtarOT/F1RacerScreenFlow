using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAnimationController : MonoBehaviour
{

    public bool _parentDeactive;

    public Image _currentPopup;

    public bool GDRP;
    public bool GDRP1;
    public bool statPopup;

    // Use this for initialization
    void Start()
    {

    }
    public void ReverseAnimationForPopu()
    {


        GetComponent<Animation>().Play("PopupClose");
        Invoke("DisablePopup", 0.5f);

    }
    void DisablePopup()
    {

        if (!_parentDeactive)
        {

            transform.gameObject.SetActive(false);

            if (statPopup)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            }
            else
            {
                GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            }
        }
        else
        {
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            if (statPopup)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(677, 1129);
            }
            if (GDRP)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(520, 418);

            }
            if (GDRP1)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(520, 788);
            }

            transform.parent.gameObject.SetActive(false);
        }
    }
}

