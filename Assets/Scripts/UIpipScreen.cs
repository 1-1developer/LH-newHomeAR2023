using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIpipScreen : UIController
{
    //const string Closebutton = "close_area";

    private Button _closebutton_pip;
    private Button _xbutton_pip;

    //미리보는 도시
    List<Button> _buttonPips = new List<Button>();
    List<VisualElement> _btextPips = new List<VisualElement>();
    List<ScrollView> _m_scrolls = new List<ScrollView>();

    int buttoncnt = 5;

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        _closebutton_pip = m_root.Q<Button>("close_area");
        _xbutton_pip = m_root.Q<Button>("Button_x");
        //미리보는도시
        for (int i = 0; i < buttoncnt; i++)
        {
            _buttonPips.Add(m_root.Q<Button>("button_pip" + $"{i + 1}"));
            _btextPips.Add(m_root.Q<VisualElement>("button_pip_text" + $"{i + 1}"));
            _m_scrolls.Add(m_root.Q<ScrollView>("m_scroll" + $"{i + 1}"));
        }
        _buttonPips[0].RegisterCallback<ClickEvent>(OnpipbuttonClicked0);
        _buttonPips[1].RegisterCallback<ClickEvent>(OnpipbuttonClicked1);
        _buttonPips[2].RegisterCallback<ClickEvent>(OnpipbuttonClicked2);
        _buttonPips[3].RegisterCallback<ClickEvent>(OnpipbuttonClicked3);
        _buttonPips[4].RegisterCallback<ClickEvent>(OnpipbuttonClicked4);
        
        _closebutton_pip.RegisterCallback<ClickEvent>(closePip);
        _xbutton_pip.RegisterCallback<ClickEvent>(closePip);

    }

    private void OnpipbuttonClicked0(ClickEvent evt)
    {
        _pop_pip(_buttonPips[0], _btextPips[0],_m_scrolls[0]);
    }
    private void OnpipbuttonClicked1(ClickEvent evt)
    {
        _pop_pip(_buttonPips[1], _btextPips[1], _m_scrolls[1]);
    }
    private void OnpipbuttonClicked2(ClickEvent evt)
    {
        _pop_pip(_buttonPips[2], _btextPips[2], _m_scrolls[2]);
    }
    private void OnpipbuttonClicked3(ClickEvent evt)
    {
        _pop_pip(_buttonPips[3], _btextPips[3], _m_scrolls[3]);
    }
    private void OnpipbuttonClicked4(ClickEvent evt)
    {
        _pop_pip(_buttonPips[4], _btextPips[4], _m_scrolls[4]);
    }

    private void closePip(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        pip_root.style.display = DisplayStyle.None;
        Debug.Log("pipclose");
    }
    public void _pop_pip(Button s_button,VisualElement text,ScrollView m_scroll) // 선택버튼 하이라이트, 선택창
    {
        foreach (Button bt in _buttonPips)
        {
            if (bt == s_button)
            {
                bt?.AddToClassList("Button_pip--high");
            }
            else
            {
                bt?.RemoveFromClassList("Button_pip--high");
            }
        }
        foreach (VisualElement t in _btextPips)
        {
            if (t == text)
            {
                t?.AddToClassList("btext_pip--high");
            }
            else
            {
                t?.RemoveFromClassList("btext_pip--high");
            }
        }
        foreach (ScrollView sc in _m_scrolls)
        {
            if(sc == m_scroll)
            {
                sc.style.display = DisplayStyle.Flex;
            }
            else
            {
                sc.style.display = DisplayStyle.None;

            }
        }
    }

}
