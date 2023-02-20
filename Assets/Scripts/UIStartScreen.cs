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
    //private VisualElement _s1;
    //private VisualElement _s2;
    //private VisualElement _s3;
    //private VisualElement _s4;

    private Button _startButton;

    List<VisualElement> _loops = new List<VisualElement>();
    int count = 1;
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

    }

    private void OnStartButtonClicked(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        // _Onboarding.style.display = DisplayStyle.Flex;
        s_root.style.display = DisplayStyle.None;
        count = 0;
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
        if(count == 0)
        {
            _loops[3].AddToClassList("loopScreen-0");
            count++;
        }
        else if(count == 1)
        {
            _loops[1].RemoveFromClassList("loopScreen-0");
            count++;
        }
        else if (count == 2)
        {
            _loops[1].AddToClassList("loopScreen-0");
            _loops[2].RemoveFromClassList("loopScreen-0");
            count++;
        }
        else if (count == 3)
        {
            _loops[2].AddToClassList("loopScreen-0");
            _loops[3].RemoveFromClassList("loopScreen-0");
            count++;
        }

        if (count >= 4)
        {
            count = 0;
        }
    }
}
