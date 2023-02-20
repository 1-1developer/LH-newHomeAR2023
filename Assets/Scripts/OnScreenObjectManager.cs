using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectManager : MonoBehaviour
{
    public bool ARok;
    public GameObject maker;
    public GameObject[] Houses;
    // Start is called before the first frame update
    void Start()
    {
        NothingOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NothingOn()
    {
        ARok = false;
        for (int i = 0; i < Houses.Length; i++)
        {
            Houses[i].SetActive(false);
        }
    }

    public void OnMaker()
    {
        ARok = true;
        for (int i = 0; i < Houses.Length; i++)
        {
            Houses[i].SetActive(false);
        }
    }

    public void OnHouse(int HouseID)
    {
        ARok = false;
        Houses[HouseID].SetActive(true);
    }
}
