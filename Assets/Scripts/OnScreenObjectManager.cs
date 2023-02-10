using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenObjectManager : MonoBehaviour
{
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
        maker.SetActive(false);
        House.SetActive(false);
    }

    public void OnMaker()
    {
        maker.SetActive(true);
        House.SetActive(false);
    }

    public void OnHouse()
    {
        House.SetActive(true);
        maker.SetActive(false);
    }
}
