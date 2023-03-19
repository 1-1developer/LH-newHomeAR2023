using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPointerTouch : MonoBehaviour
{

    public UIController uIController;
    public GameObject touchedObj;
    public Text Debugtxt;
    public ARImageTracker _ARImageTracker;
    public UIhouse uIhouse;
    public UIsidebuttons uIsidebuttons;
    public UIsmartdetail uIsmartdetail;

    RaycastHit hit;

    Material[] materials = new Material[5];

    OnScreenObjectManager OjManager;
    Pointer pointer;
    string ptName;
    // Start is called before the first frame update
    void Start()
    {
        OjManager = GetComponent<OnScreenObjectManager>();
    }
    private void Update()
    {
        PopUpObjectByTouch();
    }
    private void PopUpObjectByTouch()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && OjManager.ARok)
        {
            Touch touch = Input.GetTouch(0); 
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit))
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
                        }
                    }
                    ptName = hit.transform.name;
                    switch (ptName)
                    {
                        case "houseBt1":
                            uIhouse.onHouseCall("LH_46", "46_ver2", "46m²");
                            break;
                        case "houseBt2":
                            uIhouse.onHouseCall("LH_59", "59_ver2", "59m²");
                            break;
                        case "houseBt3":
                            uIhouse.onHouseCall("LH_84", "84_ver2", "84m²");
                            break;
                        case "outBt1":
                            uIsidebuttons.OnBt_landcall();
                            break;
                        case "outBt2":
                            uIsidebuttons.OnBt_playcall();
                            break;
                        case "smartBt":
                            uIsmartdetail.onClickSmart();
                            break;
                        case "qualBt1":
                            uIsidebuttons.OnBt_quality1call();
                            break;
                        case "qualBt2":
                            uIsidebuttons.OnBt_quality2call();
                            break;
                        case "qualBt3":
                            uIsidebuttons.OnBt_quality3call();
                            break;
                        case "communityBt":
                            uIsidebuttons.OnBt_community();
                            break;
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
            if (pt.HouseID == id)
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
