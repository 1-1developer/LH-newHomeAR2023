using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARTouchManger : MonoBehaviour
{
    public TextMesh debugTxt;
    public GameObject placeObject;

    public GameObject image;

    Animator animator;
    GameObject spawnObject;
    // Start is called before the first frame update
    RaycastHit hit;
    void Start()
    {
        animator = placeObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PopUpObjectByTouch();
        UILookAt();
    }

    GameObject hitObj;
    private void PopUpObjectByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //네 개의 손가락 중 몇 번째 인덱스 
            //List<ARRaycastHit> hits = new List<ARRaycastHit>();
            //List<RaycastHit> hits = new List<RaycastHit>();
            Ray ray = Camera.current.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Buliding")))
            {
                hitObj = hit.transform.gameObject;
                //Pose hitPose = hits[0].pose;
                if (!spawnObject)
                {
                    spawnObject = Instantiate(placeObject, hit.transform.position, Quaternion.identity);
                    spawnObject.GetComponent<Animator>().SetTrigger("pop");
                }
                else
                {
                    spawnObject.transform.position = hit.transform.position + Vector3.up * .015f;
                }
                image.SetActive(true);
                hitObj.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
        else
        {
            if (hitObj)
            {
                hitObj.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }


    private void UILookAt()
    {
        spawnObject.transform.LookAt(transform.position + Camera.current.transform.rotation * Vector3.forward, Camera.current.transform.rotation * Vector3.up);
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, .5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        //arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placeObject.SetActive(false);
        }
    }
}
