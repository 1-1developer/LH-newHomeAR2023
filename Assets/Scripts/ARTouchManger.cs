using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARTouchManger : MonoBehaviour
{
    public UIController uIController;
    public GameObject touchedObj;
    public Text Debugtxt;
    public ARImageTracker _ARImageTracker;

    RaycastHit hit;
    
    Material[] materials = new Material[5];

    Pointer pointer;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        PopUpObjectByTouch();
    }
    private void PopUpObjectByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //
                                             //List<ARRaycastHit> hits = new List<ARRaycastHit>();
                                             //List<RaycastHit> hits = new List<RaycastHit>();
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.transform.gameObject)
                {
                    touchedObj = hit.transform.gameObject;
                    Debugtxt.text = $"터치 :{hit.transform.gameObject.name}";

                    if (touchedObj.GetComponent<Pointer>())
                    {
                        AudioManager.PlayDefaultButtonSound();
                        pointer = touchedObj.GetComponent<Pointer>();
                        PointerSet(pointer.HouseID);
                        if (pointer.isSelected)
                        {
                            //uIController.InPlanPannelAR(pointer.HouseID);
                            Debug.Log("inplanpannel");
                        }
                    }
                    else
                    {
                        //uIController.closeSidesheet();
                        //_ARImageTracker.GetComplex().PointerInitialize();
                    }
                }
                else
                {
                    //_ARImageTracker.GetComplex().PointerInitialize();

                }
            }
            else
            {
                Debugtxt.text = $"ray x";
            }

        }
        else
        {
        }
    }
    public void PointerSet(int id)
    {
        foreach (Pointer pt in _ARImageTracker.GetComplex().pointers)
        {
            if(pt.HouseID == id)
            {
                pt.isSelected = true;
            }
            else
            {
                pt.isSelected = false;
            }
        }
    }



}
