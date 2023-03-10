using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complex : MonoBehaviour
{
    public Pointer[] pointers = new Pointer[5];
    public Material _m_default;
    public Material _m_default_glass;
    public Material _m_ghost;

    [SerializeField]
    GameObject[] buildings = new GameObject[5];
    void Start()
    {
        buildingSetDefalt();
    }

    // Update is called once per frame
    void Update()
    {
        buildingHighlight();
    }

    public void buildingHighlight()
    {
        for (int i = 0; i < pointers.Length; i++)
        {
            if (pointers[i].isSelected)
            {
                foreach (GameObject b in buildings)
                {
                    if (b == buildings[i])
                    {
                        b.transform.GetChild(0).GetComponent<MeshRenderer>().material = _m_default;
                        b.transform.GetChild(1).GetComponent<MeshRenderer>().material = _m_default_glass;
                    }
                    else
                    {
                        b.transform.GetChild(0).GetComponent<MeshRenderer>().material = _m_ghost;
                        b.transform.GetChild(1).GetComponent<MeshRenderer>().material = _m_ghost;
                    }
                }
            }
        }
    }
    public void buildingSetDefalt()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = _m_default;
            buildings[i].transform.GetChild(1).GetComponent<MeshRenderer>().material = _m_default_glass;
        }
    }

    public void PointerInitialize()
    {
        buildingSetDefalt();
        for (int i = 0; i < pointers.Length; i++)
        {
            pointers[i].isSelected = false;
        }
    }
}
