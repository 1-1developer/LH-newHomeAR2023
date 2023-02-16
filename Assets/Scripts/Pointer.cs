using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public int HouseID;
    public bool isSelected;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
