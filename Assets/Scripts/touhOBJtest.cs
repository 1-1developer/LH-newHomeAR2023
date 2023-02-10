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
            try
            {
                Touch touch = Input.GetTouch(0); //네 개의 손가락 중 몇 번째 인덱스 
                                                 //List<ARRaycastHit> hits = new List<ARRaycastHit>();
                                                 //List<RaycastHit> hits = new List<RaycastHit>();
                Ray ray = Camera.current.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Pointer")))
                {
                    if (hit.transform.gameObject)
                    {
                        touchedObj = hit.transform.gameObject;
                        touchedObj.GetComponent<MeshRenderer>().material.color = Color.green;
                        uIController.InPlanPannelAR();

                        try
                        {
                            pointer = touchedObj.GetComponent<Pointer>();
                        }
                        catch (System.Exception)
                        {
                        }
                        uIController.buttons[pointer.HouseID].style.backgroundColor = Color.red;
                        //uIController.buttons[pointer.HouseID].style.backgroundColor = Color.black;
                    }
                }
            }
            catch (System.Exception)
            {
            }
            
        }
        else
        {
            if (touchedObj)
            {
                touchedObj.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        PopUpObjectByTouch();
    }
}
