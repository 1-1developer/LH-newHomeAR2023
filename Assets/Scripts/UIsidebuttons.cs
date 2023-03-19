using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIsidebuttons : UIController
{
    public Sprite im_land;
    public Sprite im_play;
    public Sprite im_qual01;


    const string Bt_land = "OutSelectButton1";
    const string Bt_play = "OutSelectButton2";

    const string Bt_quality1 = "QualitySelectButton1";
    const string Bt_quality2 = "QualitySelectButton2";

    const string ImageScreen = "ImageScreen_o";
    const string ImageContainer = "imageContainer";
    const string ImageScroll = "Scrollimage";
    const string ImageScroll2 = "Scrollimage2";
    const string ImageScroll3 = "scroll_commu";
    const string BackButtonO = "Button_Back_o";


    private Button _bt_land;
    private Button _bt_play;

    private VisualElement _imageScreen;
    private VisualElement _imageContainer;
    private VisualElement _scrollImage;
    private VisualElement _scrollImage2;
    private VisualElement _Commu_scrollImage;

    private Button _bt_quality1;
    private Button _bt_quality2; //풀스크롤화면

    private Button _BackButton_o;

    List<Button> _cBts = new List<Button>();
    List<ScrollView> _m_scrolls_c = new List<ScrollView>();


    //스마트
    //public List<Button> _smartbuttons = new List<Button>();

    protected override void SetVisualElements()
    {
        base.SetVisualElements();

        _bt_land = m_root.Q<Button>(Bt_land);
        _bt_play = m_root.Q<Button>(Bt_play);

        _bt_quality1 = m_root.Q<Button>(Bt_quality1);
        _bt_quality2 = m_root.Q<Button>(Bt_quality2);

        _imageScreen = m_root.Q<VisualElement>(ImageScreen);
        _imageContainer = m_root.Q<VisualElement>(ImageContainer);
        _scrollImage = m_root.Q<VisualElement>(ImageScroll);
        _scrollImage2 = m_root.Q<VisualElement>(ImageScroll2);
        _Commu_scrollImage = m_root.Q<VisualElement>(ImageScroll3);

        _BackButton_o = m_root.Q<Button>(BackButtonO);


        _bt_land.RegisterCallback<ClickEvent>(OnBt_land);
        _bt_play.RegisterCallback<ClickEvent>(OnBt_play);
        _bt_quality1.RegisterCallback<ClickEvent>(OnBt_quality1);
        _bt_quality2.RegisterCallback<ClickEvent>(OnBt_quality2);

        _BackButton_o.RegisterCallback<ClickEvent>(Onbt_Back);

        for (int i = 0; i < 6; i++)
        {
            _cBts.Add(m_root.Q<Button>("cBt_" + $"{i + 1}"));
            _m_scrolls_c.Add(m_root.Q<ScrollView>("sC0" + $"{i + 1}"));
        }

        _cBts[0].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P0);
        _cBts[1].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P1);
        _cBts[2].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P2);
        _cBts[3].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P3);
        _cBts[4].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P4);
        _cBts[5].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P5);
    }
    private void OnCMbuttonClicked_P0(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[0], _m_scrolls_c[0]);
    }
    private void OnCMbuttonClicked_P1(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[1],_m_scrolls_c[1]);
    }
    private void OnCMbuttonClicked_P2(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[2], _m_scrolls_c[2]);
    }
    private void OnCMbuttonClicked_P3(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[3], _m_scrolls_c[3]);
    }
    private void OnCMbuttonClicked_P4(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[4], _m_scrolls_c[4]);
    }
    private void OnCMbuttonClicked_P5(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[5], _m_scrolls_c[5]);
    }

    private void Onbt_Back(ClickEvent evt) //뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        _scrollImage.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.None;

        _imageScreen.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.Flex;
        onScreenObjectManager.ARok = true;
    }

    private void OnBt_land(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        SetImage(im_land);
    }
    private void OnBt_play(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_play);
    }
    private void OnBt_quality1(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_qual01);
    }
    private void OnBt_quality2(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _imageContainer.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.Flex;
    }

    void SetImage(Sprite sprite)
    {
        _imageScreen.style.display = DisplayStyle.Flex;

        _scrollImage2.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.None;
        _Commu_scrollImage.style.display = DisplayStyle.None;

        _imageContainer.style.display = DisplayStyle.Flex;
        _imageContainer.style.backgroundImage = sprite.texture;
    }

    public void OnBt_landcall()
    {
        onScreenObjectManager.ARok = false;
        _TopTextGroup.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.None;
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_land);
    }
    public void OnBt_playcall()
    {
        onScreenObjectManager.ARok = false;
        _TopTextGroup.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.None;
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_play);
    }
    public void OnBt_quality1call()
    {
        onScreenObjectManager.ARok = false;
        _TopTextGroup.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.None;
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_qual01);
    }
    public void OnBt_quality2call()
    {
        onScreenObjectManager.ARok = false;
        _TopTextGroup.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.None;
        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _scrollImage2.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.None;
        _Commu_scrollImage.style.display = DisplayStyle.None;

        _scrollImage.style.display = DisplayStyle.Flex;
    }
    public void OnBt_quality3call() //// 필로티화면으로 채우기
    {
        onScreenObjectManager.ARok = false;
        _TopTextGroup.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.None;
        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _imageContainer.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.None;
        _Commu_scrollImage.style.display = DisplayStyle.None;

        _scrollImage2.style.display = DisplayStyle.Flex;
    }
    public void OnBt_community()
    {
        onScreenObjectManager.ARok = false;
        ar_root.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.None;

        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _imageContainer.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.None;
        _scrollImage2.style.display = DisplayStyle.None;
        _Commu_scrollImage.style.display = DisplayStyle.Flex;
    }
    void selectCommu(Button cbt,ScrollView sv) {
        foreach (Button bt in _cBts)
        {
            if (bt == cbt)
            {
                bt?.AddToClassList("Button_c--high");
            }
            else
            {
                bt?.RemoveFromClassList("Button_c--high");
            }
        }
        foreach (VisualElement ss in _m_scrolls_c)
        {
            if (ss == sv)
            {
                ss.style.display = DisplayStyle.Flex;
            }
            else
            {
                ss.style.display = DisplayStyle.None;

            }
        }
    }

}
