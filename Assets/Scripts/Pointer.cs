using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public int HouseID;
    public bool isSelected;
    public bool ispicked;
    public Sprite _default;
    public Sprite red;
    public Sprite pick;
    Material material;

    Vector3 v_scale = new Vector3(.03f, .03f, .03f);
    Vector3 v_UpScale = new Vector3(.065f, .05f, .05f);
    float speed = 3f;
    private void Awake()
    {
        this.transform.localScale = v_scale;
        material = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (this.isSelected)
        {
            if (this.ispicked)
            {
                this.material.SetTexture("_BaseMap", pick.texture);
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, v_UpScale, Time.deltaTime* speed);
            }
            else
            {
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, v_scale, Time.deltaTime* speed);
                this.material.SetTexture("_BaseMap", red.texture);
            }
        }
        else
        {
            this.ispicked = false;
            this.material.SetTexture("_BaseMap", _default.texture);
            this.transform.localScale = v_scale;
        }
    }
}
