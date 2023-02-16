using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectManager : MonoBehaviour
{
    public bool ARok;
    public GameObject maker;
    public GameObject House;
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
        //maker.SetActive(false);
        House.SetActive(false);
    }

    public void OnMaker()
    {
        ARok = true;
        //maker.SetActive(true);
        House.SetActive(false);
    }

    public void OnHouse()
    {
        ARok = false;
        House.SetActive(true);
        //maker.SetActive(false);
    }
}
