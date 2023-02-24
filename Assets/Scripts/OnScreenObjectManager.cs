using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectManager : MonoBehaviour
{
    public bool ARok;
    public GameObject maker;
    public GameObject[] Houses;
    public GameObject HouseRoot;

    Vector3 scaleOrigin;
    Quaternion rootRotorigin;

    int index;
    // Start is called before the first frame update
    void Start()
    {
        rootRotorigin = HouseRoot.transform.localRotation;
        scaleOrigin = HouseRoot.transform.localScale;
        NothingOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NothingOn()
    {
        HouseRoot.transform.localRotation = rootRotorigin;
        HouseRoot.transform.localScale = scaleOrigin;

        ARok = false;
        for (int i = 0; i < Houses.Length; i++)
        {
            Houses[i].SetActive(false);
        }
    }

    public void OnMaker()
    {
        HouseRoot.transform.localRotation = rootRotorigin;
        HouseRoot.transform.localScale = scaleOrigin;


        ARok = true;
        for (int i = 0; i < Houses.Length; i++)
        {
            Houses[i].SetActive(false);
        }
    }

    public void OnHouse(string housename)
    {
        ARok = false;
        foreach (GameObject h in Houses)
        { 
            if(h.gameObject.name == housename)
                h.SetActive(true);
            else
                h.SetActive(false);
        }
    }
}
