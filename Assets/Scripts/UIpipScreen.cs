using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIpipScreen : UIController
{
    const string Closebutton = "close_area";

    const string TopButton_city = "topButton_city";
    const string TopButton_scale = "topButton_scale";
    const string TopButton_public = "topButton_public";

    private Button _closebutton_pip;
    private Button[] _xbutton_pip = new Button[3];



    //top bar 버튼
    private Button _topButton_city;
    private Button _topButton_scale;
    private Button _topButton_public;

    List<VisualElement> _windows = new List<VisualElement>();


    //미리보는 도시
    List<Button> _buttonPips = new List<Button>();
    List<VisualElement> _btextPips = new List<VisualElement>();
    List<VisualElement> _backimages = new List<VisualElement>();
    //List<ScrollView> _m_scrolls = new List<ScrollView>();

    int buttoncnt = 4;
    int _3cnt = 3;

    //주요입지와 규모

    //공공분양
    List<Button> _buttonPips_P = new List<Button>();
    List<VisualElement> _btextPips_P = new List<VisualElement>();
    List<ScrollView> _m_scrolls_P = new List<ScrollView>();
    protected override void SetVisualElements()
    {
        base.SetVisualElements();


        //상단바버튼
        _topButton_city = m_root.Q<Button>(TopButton_city);
        _topButton_scale = m_root.Q<Button>(TopButton_scale);
        _topButton_public = m_root.Q<Button>(TopButton_public);

        //상단바
        _topButton_city.RegisterCallback<ClickEvent>(OnTopButtonClicked_C);
        _topButton_scale.RegisterCallback<ClickEvent>(OnTopButtonClicked_S);
        _topButton_public.RegisterCallback<ClickEvent>(OnTopButtonClicked_P);

        _closebutton_pip = m_root.Q<Button>(Closebutton);
        for (int i = 0; i < 3; i++)
        {
            _xbutton_pip[i] = m_root.Q<Button>("Button_x"+$"{i+1}");
        }

        //미리보는도시
        for (int i = 0; i < buttoncnt; i++)
        {
            _buttonPips.Add(m_root.Q<Button>("button_pip" + $"{i + 1}"));
            _btextPips.Add(m_root.Q<VisualElement>("button_pip_text" + $"{i + 1}"));
            _backimages.Add(m_root.Q<VisualElement>("backimage" + $"{i + 1}"));
        }
        _buttonPips[0].RegisterCallback<ClickEvent>(OnpipbuttonClicked0);
        _buttonPips[1].RegisterCallback<ClickEvent>(OnpipbuttonClicked1);
        _buttonPips[2].RegisterCallback<ClickEvent>(OnpipbuttonClicked2);
        _buttonPips[3].RegisterCallback<ClickEvent>(OnpipbuttonClicked3);
        
        _closebutton_pip.RegisterCallback<ClickEvent>(closePip);

        //주요입지와 규모

        for (int i = 0; i < _3cnt; i++)
        {
            _buttonPips_P.Add(m_root.Q<Button>("button_pip" + $"{i + 1}" + $"{i + 1}"));
            _btextPips_P.Add(m_root.Q<VisualElement>("button_pip_text" + $"{i + 1}" + $"{i + 1}"));
            _m_scrolls_P.Add(m_root.Q<ScrollView>("m_scroll" + $"{i + 1}" + $"{i + 1}"));
            _windows.Add( m_root.Q<VisualElement>("pop_window" + $"{i + 1}"));
            _xbutton_pip[i].RegisterCallback<ClickEvent>(closePip);
        }
        _buttonPips_P[0].RegisterCallback<ClickEvent>(OnpipbuttonClicked_P0);
        _buttonPips_P[1].RegisterCallback<ClickEvent>(OnpipbuttonClicked_P1);
        _buttonPips_P[2].RegisterCallback<ClickEvent>(OnpipbuttonClicked_P2);

    }
    private void OnTopButtonClicked_C(ClickEvent evt) // 미리보는도시
    {
        AudioManager.PlayDefaultButtonSound();
        ShowPipWindow(0);
        _pop_pip(_buttonPips_P[0], _btextPips_P[0], _m_scrolls_P[0], _buttonPips_P, _btextPips_P, _m_scrolls_P);
    }
    private void OnTopButtonClicked_S(ClickEvent evt) // 규모
    {
        AudioManager.PlayDefaultButtonSound();
        ShowPipWindow(1);
    }
    private void OnTopButtonClicked_P(ClickEvent evt) // 공공
    {
        AudioManager.PlayDefaultButtonSound();
        ShowPipWindow(2);
    }

    private void OnpipbuttonClicked_P0(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips_P[0], _btextPips_P[0], _m_scrolls_P[0], _buttonPips_P, _btextPips_P, _m_scrolls_P);

    }
    private void OnpipbuttonClicked_P1(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips_P[1], _btextPips_P[1], _m_scrolls_P[1], _buttonPips_P, _btextPips_P, _m_scrolls_P);

    }
    private void OnpipbuttonClicked_P2(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips_P[2], _btextPips_P[2], _m_scrolls_P[2], _buttonPips_P, _btextPips_P, _m_scrolls_P);

    }

    private void OnpipbuttonClicked0(ClickEvent evt)
    {
        _pop_pip(_buttonPips[0], _btextPips[0], _backimages[0], _buttonPips, _btextPips, _backimages);
    }
    private void OnpipbuttonClicked1(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips[1], _btextPips[1], _backimages[1], _buttonPips, _btextPips, _backimages);
    }
    private void OnpipbuttonClicked2(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips[2], _btextPips[2], _backimages[2], _buttonPips, _btextPips, _backimages);
    }
    private void OnpipbuttonClicked3(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _pop_pip(_buttonPips[3], _btextPips[3], _backimages[3], _buttonPips, _btextPips, _backimages);
    }

    private void closePip(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        pip_root.style.display = DisplayStyle.None;
    }

    public void _pop_pip(Button s_button,VisualElement text,ScrollView m_scroll,
        List<Button> buttonPips, List<VisualElement> btextPips,List<ScrollView> m_scrolls) // 선택버튼 하이라이트, 선택창
    {
        foreach (Button bt in buttonPips)
        {
            if (bt == s_button)
            {
                bt?.AddToClassList("Button_pip2--high");
            }
            else
            {
                bt?.RemoveFromClassList("Button_pip2--high");
            }
        }
        foreach (VisualElement t in btextPips)
        {
            if (t == text)
            {
                t?.AddToClassList("btext_pip2--high");
            }
            else
            {
                t?.RemoveFromClassList("btext_pip2--high");
            }
        }
        foreach (ScrollView sc in m_scrolls)
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
    public void _pop_pip(Button s_button, VisualElement text, VisualElement backimage,
        List<Button> buttonPips, List<VisualElement> btextPips, List<VisualElement> backimages) //  오버라이딩
    {
        foreach (Button bt in buttonPips)
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
        foreach (VisualElement t in btextPips)
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
        foreach (VisualElement bi in backimages)
        {
            if (bi == backimage)
            {
                bi.style.display = DisplayStyle.Flex;
            }
            else
            {
                bi.style.display = DisplayStyle.None;

            }
        }
    }

    void ShowPipWindow(int index)
    {
        pip_root.style.display = DisplayStyle.None;
        foreach (VisualElement win in _windows)
        {
            if (win == _windows[index])
            {
                win.style.display = DisplayStyle.Flex;
            }
            else
            {
                win.style.display = DisplayStyle.None;
            }
        }
        _Onboarding.style.display = DisplayStyle.None;
        pip_root.style.display = DisplayStyle.Flex;

    }
}
