using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStartScreen : MonoBehaviour
{
    float timer;
    const float _sTIME = 3.5f;

    private VisualElement _s1;
    private VisualElement _s2;
    private VisualElement _s3;

    private Button _startButton;
    // Start is called before the first frame update
    void Start()
    {
       var m_root = GetComponent<UIDocument>().rootVisualElement;

        _s1 = m_root.Q<VisualElement>("s1");
        _s2 = m_root.Q<VisualElement>("s2");
        _s3 = m_root.Q<VisualElement>("s3");
        _startButton = m_root.Q<Button>("startButton");

        _startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
        //_s1.RegisterCallback<TransitionEndEvent>(OnSideSheetOut);

    }

    private void OnStartButtonClicked(ClickEvent evt)
    {
        // _Onboarding.style.display = DisplayStyle.Flex;
        _s1.style.display = DisplayStyle.None;
        _s2.style.display = DisplayStyle.None;
        _s3.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer> _sTIME)
        {
            SwitchScreen();
            timer = 0;
        }
    }

    private void SwitchScreen()
    {
        //if(_s1.)
    }
}
