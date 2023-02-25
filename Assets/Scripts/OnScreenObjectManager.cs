using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectManager : MonoBehaviour
{
    public bool ARok;
    public GameObject maker;
    public GameObject[] Houses;
    public GameObject[] houseDefault = new GameObject[3];
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
        for (int i = 0; i < houseDefault.Length; i++)
        {
            houseDefault[i].SetActive(false);
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
        for (int i = 0; i < houseDefault.Length; i++)
        {
            houseDefault[i].SetActive(false);
        }
    }

    public void OnHouse(string hh ,string housename)
    {
        ARok = false;
        HouseRoot.transform.localRotation = rootRotorigin;
        HouseRoot.transform.localScale = scaleOrigin;
        foreach (GameObject h in Houses)
        { 
            if(h.gameObject.name == housename)
                h.SetActive(true);
            else
                h.SetActive(false);
        }
        foreach (GameObject hd in houseDefault)
        {
            if (hd.gameObject.name == hh)
                hd.SetActive(true);
            else
                hd.SetActive(false);
        }
    }
}
