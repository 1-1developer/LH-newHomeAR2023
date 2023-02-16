using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTimer : MonoBehaviour
{
    public float Timer;
    const float MAXTIME = 30f;
    public Text timerdebugtext;
    public UIController uIController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        timerdebugtext.text = "debug.time:" + Timer.ToString();

        if (Input.touchCount > 0)
        {
            Timer = 0;
        }

        if(Timer> MAXTIME)
        {
            uIController.GoHome();
            Timer = 0;
        }
    }
}
