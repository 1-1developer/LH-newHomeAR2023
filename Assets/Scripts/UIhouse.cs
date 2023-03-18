using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIhouse : UIController
{
    //public Sprite house46;
    //public Sprite house59;
    //public Sprite house84;


    const string BackButton = "BackButton";
    const string DefaltButton = "house_default";
    const string OptionButton = "house_option";


    const string LH59 = "LH_59";
    const string LH59_ver2 = "59_ver2";
    const string LH59_ver3 = "59_ver3";
    const string LH84 = "LH_84";
    const string LH84_ver2 = "84_ver2";
    const string LH84_ver3 = "84_ver3";
    const string LH46 = "LH_46";
    const string LH46_ver2 = "46_ver2";
    const string LH46_ver3 = "46_ver3";



    const string m46 = "46m²";
    const string m59 = "59m²";
    const string m84 = "84m²";



    private Button _BackButton;
    private Button _defaltButton;
    private Button _optionButton;

    private Label _plantext;
    int buttonNum = 3; // ar sidebar버튼 개수

    public List<Button> buttons = new List<Button>();


    private VisualElement _houseplanpic;

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        _BackButton = m_root.Q<Button>(BackButton);
        _defaltButton = m_root.Q<Button>(DefaltButton);
        _optionButton = m_root.Q<Button>(OptionButton);
        _houseplanpic = m_root.Q<VisualElement>("HousePlanImage");
        _plantext = m_root.Q<Label>("planTxt");

        SetupSelectButton();
        _BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        _defaltButton.RegisterCallback<ClickEvent>(OnDefaltButtonClicked);
        _optionButton.RegisterCallback<ClickEvent>(OnOptionButtonClicked);
        buttons[0].RegisterCallback<ClickEvent>(OnHouseButtonClicked0);
        buttons[1].RegisterCallback<ClickEvent>(OnHouseButtonClicked1);
        buttons[2].RegisterCallback<ClickEvent>(OnHouseButtonClicked2);

        //집화면 단면도
    }

    private void OnDefaltButtonClicked(ClickEvent evt) //기본형
    {
        AudioManager.PlayDefaultButtonSound();
        if (_plantext.text ==m46 )
            onScreenObjectManager.OnHouse(LH46, LH46_ver2);//집모델띄우기
        if (_plantext.text == m59)                      
            onScreenObjectManager.OnHouse(LH59, LH59_ver2);
        if (_plantext.text == m84)                      
            onScreenObjectManager.OnHouse(LH84, LH84_ver2);
        _defaltButton.AddToClassList("Button_house--active");
        _optionButton.RemoveFromClassList("Button_house--active");
    }

    private void OnOptionButtonClicked(ClickEvent evt) //옵션형
    {
        AudioManager.PlayDefaultButtonSound();
        if (_plantext.text == m46)
            onScreenObjectManager.OnHouse(LH46,LH46_ver3);
        if (_plantext.text == m59)
            onScreenObjectManager.OnHouse(LH59, LH59_ver3);
        if (_plantext.text == m84)
            onScreenObjectManager.OnHouse(LH84, LH84_ver3);
        _optionButton.AddToClassList("Button_house--active");
        _defaltButton.RemoveFromClassList("Button_house--active");
    }

    void SetupSelectButton() //사이드바 버튼가져오기
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(m_root.Q<Button>("s_bt_h" + $"{i + 1}"));
        }
    }

    private void OnHouseButtonClicked0(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();

        //_houseplanpic.style.backgroundImage = house46.texture;
        //집 오브젝트
        onScreenObjectManager.OnHouse(LH46, LH46_ver2);//집모델띄우기
        _plantext.text = m46;
        updateHousebt(buttons[0]);
        _defaltButton.AddToClassList("Button_house--active");
        _optionButton.RemoveFromClassList("Button_house--active");
    }
    private void OnHouseButtonClicked1(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();

        //_houseplanpic.style.backgroundImage = house59.texture;


        //집 오브젝트
        onScreenObjectManager.OnHouse(LH59, LH59_ver2);//집모델띄우기
        _plantext.text = m59;
        updateHousebt(buttons[1]);
        _defaltButton.AddToClassList("Button_house--active");
        _optionButton.RemoveFromClassList("Button_house--active");

    }
    private void OnHouseButtonClicked2(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();

        //_houseplanpic.style.backgroundImage = house84.texture;

        //집 오브젝트
        onScreenObjectManager.OnHouse(LH84, LH84_ver2);//집모델띄우기
        _plantext.text = m84;
        updateHousebt(buttons[2]);
        _defaltButton.AddToClassList("Button_house--active");
        _optionButton.RemoveFromClassList("Button_house--active");

    }

    private void OnBackButtonClicked(ClickEvent evt)  //집내부 뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        _HousePlan.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;

        _homeButton.AddToClassList("Button_Home--in");
        //집 오브젝트
        onScreenObjectManager.OnMaker();
    }

    public void onHouseCall(string hh, string housename,string htext)
    {
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;

        onScreenObjectManager.OnHouse(hh, housename);//집모델띄우기
        _plantext.text = htext;
        _defaltButton.AddToClassList("Button_house--active");
        _optionButton.RemoveFromClassList("Button_house--active");
    }

    void updateHousebt(Button bb)
    {
        foreach (Button bt in buttons)
        {
            if (bt == bb)
            {
                bt?.AddToClassList("Button_hs--high");
            }
            else
            {
                bt?.RemoveFromClassList("Button_hs--high");
            }
        }
    }
    /*
     * public void PickHighlight(Button s_button) // 선택버튼 하이라이트 효과
    {
        foreach (Button bt in buttons)
        {
            if (bt == s_button)
            {
                bt?.AddToClassList("Button_Side01--sel");
            }
            else
            {
                bt?.RemoveFromClassList("Button_Side01--sel");
            }
        }
    }
     */
}
