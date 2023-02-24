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
    public GameObject[] bulidings = new GameObject[4];


    RaycastHit hit;
    
    Pointer[] pointers = new Pointer[4];
    Material[] materials = new Material[4];

    Pointer pointer;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            pointers[i] = bulidings[i].GetComponent<Pointer>();
            materials[i] = bulidings[i].transform.GetChild(0).GetComponent<MeshRenderer>().material;
        }
       
        //pointers[1] = _ARImageTracker.GetSpwan("oq").transform.GetChild(1).GetComponent<Pointer>();
    }
    private void Update()
    {
        PointerSet(uIController.GetID());
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

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Pointer")))
            {
                if (hit.transform.gameObject)
                {
                    touchedObj = hit.transform.gameObject;
                    //touchedObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("Color", Color.red);       
                    Debugtxt.text = $"터치 :{hit.transform.gameObject.name}";
                    try
                    {
                        pointer = touchedObj.GetComponent<Pointer>();
                    }
                    catch (System.Exception)
                    {
                        Debugtxt.text = $"포인터 가져오기 실패";
                    }
                    //uIController.PickHighlight(uIController.buttons[pointer.HouseID]);
                    if (pointer)
                    {
                        if ((pointer.HouseID == uIController.GetID()) && pointer.isSelected)
                        {
                            pointer.ispicked = true;
                            uIController.InPlanPannelAR(pointer.HouseID);
                        }
                    }
                    else
                    {
                        Debugtxt.text = $"포인터없음";
                    }
                }
 
            }
            else
            {
                Debugtxt.text = $"ray x";
            }

        }
        else
        {
            if (touchedObj)
            {
                //touchedObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("Color", Color.white);
            }
        }
    }
    public void PointerSet(int id)
    {
        foreach (Pointer pt in pointers)
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

    private void UILookAt()
    {
        
        for (int i = 0; i < pointers.Length; i++)
        {
            //pointers[i].transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            pointers[i].transform.LookAt(Camera.main.transform);
            Debug.Log("look");
        }
    }

    private void HighlightPick()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitPoint;

        if(Physics.Raycast(ray, out hitPoint, 100.0f))
        {

        }

        
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, .5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        //arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            touchedObj.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            touchedObj.SetActive(false);
        }
    }


}
