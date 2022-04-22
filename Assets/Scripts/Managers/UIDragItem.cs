using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler
{
    public UIDropSlot currentSlot;
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private Core core;

    public delegate void PointerDownOnCoreDelegate(string type, float amount);
    public static event PointerDownOnCoreDelegate PointerDownOnCore;

    public delegate void OnCorePartAddDelegate(Core core);
    public static event OnCorePartAddDelegate OnCorePartAdd;

    public delegate void OnCorePartRemoveDelegate(Core core);
    public static event OnCorePartRemoveDelegate OnCorePartRemove;

    public delegate void SaveCoresAfterChangeDelegate();
    public static event SaveCoresAfterChangeDelegate SaveCoreAfterChange;

    public Vector2 initialPos;

    private void Start()
    {
        core = GetComponent<Core>();
       // core.CoreName = core.name;
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(core.coreType == CoreType.SPEED)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue);
        }
        if(core.coreType == CoreType.ACC)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue);
        }
        if(core.coreType == CoreType.BOOST)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue); 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
      
        foreach (var hit in results)
        {
            var slot = hit.gameObject.GetComponent<UIDropSlot>();//Umar:: From Where you picked the core

           


            if (slot)
            {
                Debug.Log("slot.Name " + slot.name);

                if (currentSlot.tag == Tags.CoreAvailableContainer && slot.tag == Tags.CoreAvailableContainer)
                {
                    transform.parent = currentSlot.transform;
                    transform.localPosition = GetComponent<UIDragItem>().initialPos;
                  //  Debug.Log("----------Slot interChange Not Allowed---------- ");

                    return;


                }

                if (slot.gameObject.tag == Tags.CoreInUseContainer && currentSlot.gameObject.tag == Tags.CoreAvailableContainer)
                {
                    OnCorePartAdd?.Invoke(core);
                }



                if(slot.gameObject.tag == Tags.CoreAvailableContainer && currentSlot.gameObject.tag == Tags.CoreInUseContainer)
                {

                   
                    if (slot.name == (core.CoreName + "Container"))
                    {
                        OnCorePartRemove?.Invoke(core);
                      //  Debug.Log("On Core Part Remove");
                      //  Debug.Log("***********Slot name************ " + slot.name);
                      

                    }
                    else
                    {

                        transform.parent = currentSlot.transform;
                        transform.localPosition = Vector3.zero;
                      //  Debug.Log("***********Core Doesn't Match Container************ ");

                        return;

                      
                    }
                    
                }


                if (slot.currentItem)
                {
                    slot.currentItem.SwapObject(currentSlot);
                    currentSlot = slot;
                    currentSlot.currentItem = this;
                }
                else
                {
                    currentSlot.currentItem = null;
                    currentSlot = slot;
                    currentSlot.currentItem = this;
                }
                break;
            }
        }
      
        transform.SetParent(currentSlot.transform,true);
        if(currentSlot.gameObject.tag == Tags.CoreAvailableContainer)
        {
            transform.GetComponent<RectTransform>().localPosition = initialPos;
            Debug.Log(initialPos);
        }
        else
        {
            Debug.Log(Vector3.zero);
            transform.localPosition = Vector3.zero;
        }

        SaveCoreAfterChange?.Invoke();
    }

    public void SwapObject(UIDropSlot slot)
    {
        currentSlot = slot;
        currentSlot.currentItem = this;
        transform.SetParent(currentSlot.transform);
        transform.localPosition = Vector3.zero;
    }
}