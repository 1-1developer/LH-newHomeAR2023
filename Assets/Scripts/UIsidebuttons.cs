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
    const string BackButtonO = "Button_Back_o";


    private Button _bt_land;
    private Button _bt_play;

    private VisualElement _imageScreen;
    private VisualElement _imageContainer;
    private VisualElement _scrollImage;

    private Button _bt_quality1;
    private Button _bt_quality2; //풀스크롤화면

    private Button _BackButton_o;

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

        _BackButton_o = m_root.Q<Button>(BackButtonO);


        _bt_land.RegisterCallback<ClickEvent>(OnBt_land);
        _bt_play.RegisterCallback<ClickEvent>(OnBt_play);
        _bt_quality1.RegisterCallback<ClickEvent>(OnBt_quality1);
        _bt_quality2.RegisterCallback<ClickEvent>(OnBt_quality2);

        _BackButton_o.RegisterCallback<ClickEvent>(Onbt_Back);
    }

    private void Onbt_Back(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        _scrollImage.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.None;

        _imageScreen.style.display = DisplayStyle.None;
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

        _scrollImage.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.Flex;
        _imageContainer.style.backgroundImage = sprite.texture;
    }

    public void OnBt_landcall()
    {
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_land);
    }
    public void OnBt_playcall()
    {
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_play);
    }
    public void OnBt_quality1call()
    {
        AudioManager.PlayDefaultButtonSound();
        SetImage(im_qual01);
    }
    public void OnBt_quality2call()
    {
        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _imageContainer.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.Flex;
    }
    public void OnBt_quality3call() //// 필로티화면으로 채우기
    {
        AudioManager.PlayDefaultButtonSound();
        _imageScreen.style.display = DisplayStyle.Flex;

        _imageContainer.style.display = DisplayStyle.None;
        _scrollImage.style.display = DisplayStyle.Flex;
    }
}
