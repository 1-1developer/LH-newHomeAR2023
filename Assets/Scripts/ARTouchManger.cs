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
    RaycastHit hit;
    Pointer[] pointers;
    Pointer pointer;
    // Start is called before the first frame update
    void Start()
    {
        pointers = FindObjectsOfType<Pointer>();
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
                    touchedObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                    uIController.InPlanPannelAR();

                    try
                    {
                        pointer = touchedObj.GetComponent<Pointer>();
                    }
                    catch (System.Exception)
                    {
                    }
                    uIController.PickHighlight(uIController.buttons[pointer.HouseID]);
                }
            }

        }
        else
        {
            if (touchedObj)
            {
                touchedObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
            }
        }
    }


    private void UILookAt()
    {

        for (int i = 0; i < pointers.Length; i++)
        {
            //pointers[i].transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            pointers[i].transform.LookAt(Camera.main.transform);
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

    private void Update()
    {
        PopUpObjectByTouch();
        UILookAt();
    }
}
