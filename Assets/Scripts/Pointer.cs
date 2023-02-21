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

    GameObject rect;

    Vector3 v_scale = new Vector3(.03f, .03f, .03f);
    Vector3 v_UpScale = new Vector3(.065f, .06f, .05f);
    float speed = 3f;
    Color coloraa;
    Color colororigin;
    Color coloralpha = new Color (1,1,1,0);
    private void Awake()
    {
        this.transform.localScale = v_scale;
        material = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        rect = transform.GetChild(1).gameObject;
        colororigin = rect.GetComponent<MeshRenderer>().material.GetColor("_BaseColor");
        coloraa = coloralpha;
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (this.isSelected)
        {
            this.rect.SetActive(true);
            rect.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", coloraa);

            if (this.ispicked)
            {
                coloraa = Color.Lerp(coloraa, colororigin, Time.deltaTime * speed);

                this.material.SetTexture("_BaseMap", pick.texture);
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, v_UpScale, Time.deltaTime* speed);
            }
            else
            {
                coloraa = Color.Lerp(coloraa, coloralpha, Time.deltaTime * speed);

                this.transform.localScale = Vector3.Lerp(this.transform.localScale, v_scale, Time.deltaTime* speed);
                this.material.SetTexture("_BaseMap", red.texture);
            }
        }
        else
        {
            this.rect.SetActive(false);
            this.ispicked = false;
            this.material.SetTexture("_BaseMap", _default.texture);
            this.transform.localScale = v_scale;
        }
    }
}
