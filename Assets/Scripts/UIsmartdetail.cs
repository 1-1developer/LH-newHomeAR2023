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

    int smartpannelcnt = 3;

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        for (int i = 0; i < smartpannelcnt; i++)
        {
            smartpannels.Add(m_root.Q<VisualElement>("smartpannel" + $"{i + 1}"));
        }

        _lastButton = m_root.Q<Button>("Button_last");
        _nextButton = m_root.Q<Button>("Button_next");
        _backButton_s = m_root.Q<Button>("Button_Back_s");

        _lastButton.RegisterCallback<ClickEvent>(GoLast);
        _nextButton.RegisterCallback<ClickEvent>(GoNext);
        _backButton_s.RegisterCallback<ClickEvent>(GoBack);

    }

    private void GoBack(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        onScreenObjectManager.ARok = true;

        smart_root.style.display = DisplayStyle.None;
    }

    private void GoNext(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        count++;
        if (count >2)
        {
            count = 2;
            return;
        }
        openSmartpannel(smartpannels[count]);
        updateButton(); 
    }
    private void GoLast(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

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
        if (count == 2)
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
    public void onClickSmart()
    {
        AudioManager.PlayDefaultButtonSound();
        onScreenObjectManager.ARok = false;
        count = 0;
        openSmartpannel(smartpannels[0]);
        updateButton();
    }
}
