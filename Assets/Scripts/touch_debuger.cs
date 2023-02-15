using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touch_debuger : MonoBehaviour
{
    public GameObject touchedObj;
    public Text Debugtxt;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void PopUpObjectByTouch()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                
            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Pointer")))
            {
                Debugtxt.text = "터치";
                if (hit.transform.gameObject)
                {
                    touchedObj = hit.transform.gameObject;
                    touchedObj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
                    Debugtxt.text = $"터치 :{hit.transform.gameObject.name}";
                }
            }
            else
                Debugtxt.text = "터치 ray 안됨";
            
        }
        else
        {
             Debugtxt.text = "터치x";
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
