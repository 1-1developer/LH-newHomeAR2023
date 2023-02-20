using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class touhOBJtest : MonoBehaviour
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
                    touchedObj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);

                    try
                    {
                        pointer = touchedObj.GetComponent<Pointer>();
                    }
                    catch (System.Exception)
                    {
                    }
                    uIController.InPlanPannelAR(pointer.HouseID);

                    //uIController.PickHighlight(uIController.buttons[pointer.HouseID]);
                }
            }
            
        }
        else
        {
            if (touchedObj)
            {
                touchedObj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.white);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        PopUpObjectByTouch();
    }
}
