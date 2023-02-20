using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIsmartdetail : UIController
{
    List<VisualElement> smartpannels = new List<VisualElement>();

    Button _lastButton;
    Button _nextButton;
    Button _backButton_s;

    int count;

    int smartpannelcnt = 5;

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        for (int i = 0; i < smartpannelcnt; i++)
        {
            smartpannels.Add(m_root.Q<VisualElement>("smartpannel" + $"{i + 1}"));
            _smartbuttons.Add(m_root.Q<Button>("SmartSelectButton" + $"{i + 1}"));
        }

        _lastButton = m_root.Q<Button>("Button_last");
        _nextButton = m_root.Q<Button>("Button_next");
        _backButton_s = m_root.Q<Button>("Button_Back_s");

        _lastButton.RegisterCallback<ClickEvent>(GoLast);
        _nextButton.RegisterCallback<ClickEvent>(GoNext);
        _backButton_s.RegisterCallback<ClickEvent>(GoBack);

        _smartbuttons[0].RegisterCallback<ClickEvent>(OnsmartbuttonClicked0);
        _smartbuttons[1].RegisterCallback<ClickEvent>(OnsmartbuttonClicked1);
        _smartbuttons[2].RegisterCallback<ClickEvent>(OnsmartbuttonClicked2);
        _smartbuttons[3].RegisterCallback<ClickEvent>(OnsmartbuttonClicked3);
        _smartbuttons[4].RegisterCallback<ClickEvent>(OnsmartbuttonClicked4);
    }

    private void GoBack(ClickEvent evt)
    {
        smart_root.style.display = DisplayStyle.None;
    }

    private void GoNext(ClickEvent evt)
    {
        count++;
        if (count >4)
        {
            count = 4;
            return;
        }
        openSmartpannel(smartpannels[count]);
        updateButton(); 
    }

    

    private void GoLast(ClickEvent evt)
    {
        count--;
        if (count < 0)
        {
            count = 0;
            return;
        }
        openSmartpannel(smartpannels[count]);
        updateButton();
    }

    void updateButton()
    {
        if (count == 4)
        {
            _nextButton.AddToClassList("Button_next_last--un");
        }
        else if (count == 0)
        {
            _lastButton.AddToClassList("Button_next_last--un");
        }
        else
        {
            if (_nextButton.ClassListContains("Button_next_last--un"))
                _nextButton.RemoveFromClassList("Button_next_last--un"); // 활성화
            if (_lastButton.ClassListContains("Button_next_last--un"))
                _lastButton.RemoveFromClassList("Button_next_last--un"); // 활성화
        }
    }

    private void OnsmartbuttonClicked0(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        count = 0;
        openSmartpannel(smartpannels[0]);
        updateButton();
    }
    private void OnsmartbuttonClicked1(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        count = 1;
        openSmartpannel(smartpannels[1]);
    }
    private void OnsmartbuttonClicked2(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        count = 2;
        openSmartpannel(smartpannels[2]);
    }
    private void OnsmartbuttonClicked3(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        count = 3;
        openSmartpannel(smartpannels[3]);
    }
    private void OnsmartbuttonClicked4(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        count = 4;
        openSmartpannel(smartpannels[4]);
        updateButton();
    }
    void openSmartpannel( VisualElement pannel)
    {
        smart_root.style.display = DisplayStyle.Flex;
        foreach (VisualElement sp in smartpannels)
        {
            if (sp == pannel)
            {
                sp.style.display = DisplayStyle.Flex;
            }
            else
            {
                sp.style.display = DisplayStyle.None;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cnt: "+count);
    }
}
