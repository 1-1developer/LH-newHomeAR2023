using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIhouse : UIController
{
    public Sprite house46;
    public Sprite house59;
    public Sprite house84;

    private Button _BackButton;

    private Label _plantext;
    int buttonNum = 3; // ar sidebar버튼 개수

    public List<Button> buttons = new List<Button>();

    const string BackButton = "BackButton";

    private VisualElement _houseplanpic;

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        _BackButton = m_root.Q<Button>(BackButton);
        _houseplanpic = m_root.Q<VisualElement>("HousePlanImage");
        _plantext = m_root.Q<Label>("planTxt");

        SetupSelectButton();
        _BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        buttons[0].RegisterCallback<ClickEvent>(OnHouseButtonClicked0);
        buttons[1].RegisterCallback<ClickEvent>(OnHouseButtonClicked1);
        buttons[2].RegisterCallback<ClickEvent>(OnHouseButtonClicked2);

        //집화면 단면도
    }

    void SetupSelectButton() //사이드바 버튼가져오기
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(m_root.Q<Button>("HouseSelectButton" + $"{i + 1}"));
        }
    }

    private void OnHouseButtonClicked0(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;
        _houseplanpic.style.backgroundImage = house46.texture;
        //집 오브젝트
        onScreenObjectManager.OnHouse(0);//트래커 id가져오기
        _plantext.text = "46m²";

    }
    private void OnHouseButtonClicked1(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;
        _houseplanpic.style.backgroundImage = house59.texture;


        //집 오브젝트
        onScreenObjectManager.OnHouse(1);//트래커 id가져오기
        _plantext.text = "59m²";

    }
    private void OnHouseButtonClicked2(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;
        _houseplanpic.style.backgroundImage = house84.texture;

        //집 오브젝트
        onScreenObjectManager.OnHouse(2);//트래커 id가져오기
        _plantext.text = "84m²";

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
