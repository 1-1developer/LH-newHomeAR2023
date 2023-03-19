using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStartScreen : UIController
{
    float timer;
    const float _sTIME = 3.5f;
    int loopcount = 4;

    private Button _startButton;
    private Button _openButton2;

    List<VisualElement> _loops = new List<VisualElement>();
    // Start is called before the first frame update

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        _startButton = m_root.Q<Button>("Button_Start");

        _startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);

        for (int i = 0; i < loopcount; i++)
        {
            _loops.Add(m_root.Q<VisualElement>("s" + $"{i + 1}"));
        }
    }
    void Start()
    {
        //SetVisualElements();
        //_s1 = m_root.Q<VisualElement>("s1");
        //_s2 = m_root.Q<VisualElement>("s2");
        //_s3 = m_root.Q<VisualElement>("s3");
        //_s4 = m_root.Q<VisualElement>("s4");
        _Scount = 0;
    }

    private void OnStartButtonClicked(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        // _Onboarding.style.display = DisplayStyle.Flex;
        s_root.style.display = DisplayStyle.None;
        _Scount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(_Scount == 0)
        {
            _loops[1].AddToClassList("loopScreen-0");
            _loops[2].AddToClassList("loopScreen-0");
            _loops[3].AddToClassList("loopScreen-0");
            _Scount++;
            timer = 0;
        }
        if(timer> _sTIME)
        {
            SwitchScreen();
            timer = 0;
        }
    }

    private void SwitchScreen()
    {
        if(_Scount == 0)
        {
            _loops[1].AddToClassList("loopScreen-0");
            _loops[2].AddToClassList("loopScreen-0");
            _loops[3].AddToClassList("loopScreen-0");
            _Scount++;
        }
        else if(_Scount == 1)
        {
            _loops[1].RemoveFromClassList("loopScreen-0");
            _Scount++;
        }
        else if (_Scount == 2)
        {
            _loops[1].AddToClassList("loopScreen-0");
            _loops[2].RemoveFromClassList("loopScreen-0");
            _Scount++;
        }
        else if (_Scount == 3)
        {
            _loops[2].AddToClassList("loopScreen-0");
            _loops[3].RemoveFromClassList("loopScreen-0");
            _Scount++;
        }

        if (_Scount >= 4)
        {
            _Scount = 0;
        }
    }
}
