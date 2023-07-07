using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements;

public class UIsidebuttons : UIController
{
    public Sprite im_land;
    public Sprite im_play;
    public Sprite im_qual01;

    public Sprite[] co_0;
    public Sprite[] co_1;
    public Sprite[] co_2;
    public Sprite[] co_3;
    public Sprite[] co_4;

    public RenderTexture rt;
    public VideoClip[] q_clips;


    public Sprite[] p_0;
    public Sprite[] p_1;
    public Sprite[] p_2;

    const string Bt_quality1 = "QualitySelectButton1";
    const string Bt_quality2 = "QualitySelectButton2";

    const string ImageScreen = "ImageScreen_o";
    const string ImageContainer = "imageContainer";
    const string ImageScroll = "Scrollimage";
    const string ImageScroll2 = "Scrollimage2";
    const string ImageScroll3 = "scroll_commu";

    const string BackButtonO = "Button_Back_o";
    const string BackButtonp1 = "Button_Back_p1";
    const string BackButtonp2 = "Button_Back_p2";

    const string ButtonClast = "Button_last_c";
    const string ButtonCnext = "Button_next_c";
    const string sc = "sC01";

    const string ButtonQlast = "Button_last_q";
    const string ButtonQnext = "Button_next_q";

    const string ButtonPnext = "Button_next_p";
    const string ButtonPlast = "Button_last_p";

    const string PlayImage = "playgroundImage";

    private VisualElement _imageScreen;
    private VisualElement _imageContainer;
    private VisualElement _scrollImage;
    private VisualElement _scrollImage2;
    private VisualElement _Commu_scrollImage;

    private VisualElement _Commu_Image;
    private VisualElement _Playground_Image;

    private Button _bt_quality1;
    private Button _bt_quality2; //풀스크롤화면

    private Button _bt_c_next;
    private Button _bt_c_last;
    private Button _bt_q_next;
    private Button _bt_q_last;

    private Button _bt_p_next;
    private Button _bt_p_last;

    private Button[] _bt_ps = new Button[3];

    public int c_cnt;
    public int c_ID;
    int[] c_sNum = new int[5];

    public int q_cnt;

    public int p_cnt;
    public int p_ID;

    private Button _BackButton_o;
    private Button _BackButton_p1;
    private Button _BackButton_p2;

    List<Button> _cBts = new List<Button>();
    List<VisualElement> _qPages = new List<VisualElement>();

    VideoPlayer videoPlayer;

    //스마트
    //public List<Button> _smartbuttons = new List<Button>();
    private void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.targetTexture = rt;
    }

    protected override void SetVisualElements()
    {
        base.SetVisualElements();
        _bt_c_next= m_root.Q<Button>(ButtonCnext);
        _bt_c_last= m_root.Q<Button>(ButtonClast);
        _bt_q_next = m_root.Q<Button>(ButtonQnext);
        _bt_q_last = m_root.Q<Button>(ButtonQlast);
        _bt_p_next = m_root.Q<Button>(ButtonPnext);
        _bt_p_last = m_root.Q<Button>(ButtonPlast);

        _Commu_Image = m_root.Q<VisualElement>(sc);
        _Playground_Image = m_root.Q<VisualElement>(PlayImage);


        _bt_quality1 = m_root.Q<Button>(Bt_quality1);
        _bt_quality2 = m_root.Q<Button>(Bt_quality2);

        _imageScreen = m_root.Q<VisualElement>(ImageScreen);
        _imageContainer = m_root.Q<VisualElement>(ImageContainer);
        _scrollImage = m_root.Q<VisualElement>(ImageScroll);
        _scrollImage2 = m_root.Q<VisualElement>(ImageScroll2);
        _Commu_scrollImage = m_root.Q<VisualElement>(ImageScroll3);

        _BackButton_o = m_root.Q<Button>(BackButtonO);
        _BackButton_p1 = m_root.Q<Button>(BackButtonp1);
        _BackButton_p2 = m_root.Q<Button>(BackButtonp2);

        _bt_quality1.RegisterCallback<ClickEvent>(OnBt_quality1);
        _bt_quality2.RegisterCallback<ClickEvent>(OnBt_quality2);

        _BackButton_o.RegisterCallback<ClickEvent>(Onbt_Back);
        _BackButton_p1.RegisterCallback<ClickEvent>(Onbt_Backp1);
        _BackButton_p2.RegisterCallback<ClickEvent>(Onbt_Backp2);

        for (int i = 0; i < 5; i++)
        {
            _cBts.Add(m_root.Q<Button>("cBt_" + $"{i + 1}"));
        }
        for (int i = 0; i < 7; i++)
        {
            _qPages.Add(m_root.Q<VisualElement>("qp" + $"{i}"));
        }
        for (int i = 0; i < 3; i++)
        {
            _bt_ps[i] = m_root.Q<Button>("playbt" + $"{i}");
        }

        c_sNum[0] = 2;
        c_sNum[1] = 7;
        c_sNum[2] = 2;
        c_sNum[3] = 5;
        c_sNum[4] = 9;

        _cBts[0].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P0);
        _cBts[1].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P1);
        _cBts[2].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P2);
        _cBts[3].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P3);
        _cBts[4].RegisterCallback<ClickEvent>(OnCMbuttonClicked_P4);

        _bt_ps[0].RegisterCallback<ClickEvent>(OnpsBtClicked_0);
        _bt_ps[1].RegisterCallback<ClickEvent>(OnpsBtClicked_1);
        _bt_ps[2].RegisterCallback<ClickEvent>(OnpsBtClicked_2);

        _bt_c_next.RegisterCallback<ClickEvent>(GoNext);
        _bt_c_last.RegisterCallback<ClickEvent>(GoLast);

        _bt_q_next.RegisterCallback<ClickEvent>(GoNextq);
        _bt_q_last.RegisterCallback<ClickEvent>(GoLastq);

        _bt_p_next.RegisterCallback<ClickEvent>(GoNextp);
        _bt_p_last.RegisterCallback<ClickEvent>(GoLastp);
    }



    private void OnpsBtClicked_0(ClickEvent evt)
    {
        p_ID = 0;
        AudioManager.PlayDefaultButtonSound();
        showPlayScreen();
    }
    private void OnpsBtClicked_1(ClickEvent evt)
    {
        p_ID = 1;
        AudioManager.PlayDefaultButtonSound();
        showPlayScreen();
    }
    private void OnpsBtClicked_2(ClickEvent evt)
    {
        p_ID = 2;
        AudioManager.PlayDefaultButtonSound();
        showPlayScreen();
    }


    private void OnCMbuttonClicked_P0(ClickEvent evt)
    {
        c_ID = 0;
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[0]);
    }
    private void OnCMbuttonClicked_P1(ClickEvent evt)
    {
        c_ID = 1;
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[1]);
    }
    private void OnCMbuttonClicked_P2(ClickEvent evt)
    {
        c_ID = 2;
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[2]);
    }
    private void OnCMbuttonClicked_P3(ClickEvent evt)
    {
        c_ID = 3;
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[3]);
    }
    private void OnCMbuttonClicked_P4(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        selectCommu(_cBts[4]);
        c_ID = 4;
    }

    private void Onbt_Back(ClickEvent evt) //뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        videoPlayer.Stop();
        _scrollImage.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.None;

        _imageScreen.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.Flex;
        onScreenObjectManager.ARok = true;
        c_cnt = 0;
        q_cnt = 0;
        p_cnt = 0;
    }

    private void Onbt_Backp1(ClickEvent evt) //뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        videoPlayer.Stop();
        _scrollImage.style.display = DisplayStyle.None;
        _imageContainer.style.display = DisplayStyle.None;

        _imageScreen.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.Flex;
        onScreenObjectManager.ARok = true;
        c_cnt = 0;
        q_cnt = 0;
        p_cnt = 0;
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
        //_imageContainer.style.backgroundImage = sprite.texture;
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
        showQpages(_qPages[0]);
        setVideo(q_cnt);
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

    void selectCommu(Button cbt) {
        refreshbt();
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
    }
    void showPlayScreen()
    {
        _Playground_Image.style.display = DisplayStyle.Flex;
        showPlaySprite(p_ID, p_cnt);
    }

    private void GoNext(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        c_cnt++;
        if (c_cnt > c_sNum[c_ID]-1)
        {
            c_cnt = c_sNum[c_ID];
            return;
        }
        updateButton();
        showComuSprite(c_ID, c_cnt);
    }
    private void GoLast(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        c_cnt--;
        if (c_cnt < 0)
        {
            c_cnt = 0;
            return;
        }
        updateButton();
        showComuSprite(c_ID, c_cnt);
    }
    void refreshbt()
    {
        c_cnt = 0;
        _bt_c_last.AddToClassList("Button_next_last--un");
        _bt_c_next.RemoveFromClassList("Button_next_last--un");
        showComuSprite(c_ID, c_cnt);
    }
    void updateButton()
    {
        if (c_cnt == c_sNum[c_ID]-1)
        {
            _bt_c_next.AddToClassList("Button_next_last--un");
        }
        else if (c_cnt == 0)
        {
            _bt_c_last.AddToClassList("Button_next_last--un");
        }
        else
        {
            if (_bt_c_next.ClassListContains("Button_next_last--un"))
                _bt_c_next.RemoveFromClassList("Button_next_last--un"); // 활성화
            if (_bt_c_last.ClassListContains("Button_next_last--un"))
                _bt_c_last.RemoveFromClassList("Button_next_last--un"); // 활성화
        }
    }

    void showComuSprite(int idnum,int count)
    {
        switch (idnum)
        {
            case 0:
                _Commu_Image.style.backgroundImage = co_0[count].texture;
                break;
            case 1:
                _Commu_Image.style.backgroundImage = co_1[count].texture;
                break;
            case 2:
                _Commu_Image.style.backgroundImage = co_2[count].texture;
                break;
            case 3:
                _Commu_Image.style.backgroundImage = co_3[count].texture;
                break;
            case 4:
                _Commu_Image.style.backgroundImage = co_4[count].texture;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 놀이터 페이지
    /// </summary>
    private void GoNextp(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        p_cnt++;
        if (p_cnt > 4)
        {
            p_cnt = 4;
            return;
        }
        updateButtonp();
        showPlaySprite(p_ID, p_cnt);
    }
    private void GoLastp(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        p_cnt--;
        if (p_cnt < 0)
        {
            p_cnt = 0;
            return;
        }
        updateButtonp();
        showPlaySprite(p_ID, p_cnt);
    }
    void refreshbtp()
    {
        p_cnt = 0;
        _bt_p_last.AddToClassList("Button_next_last--un");
        _bt_p_next.RemoveFromClassList("Button_next_last--un");
        showPlaySprite(p_ID, p_cnt);
    }
    void updateButtonp()
    {
        if (p_cnt == 4)
        {
            _bt_p_next.AddToClassList("Button_next_last--un");
        }
        else if (p_cnt == 0)
        {
            _bt_p_last.AddToClassList("Button_next_last--un");
        }
        else
        {
            if (_bt_p_next.ClassListContains("Button_next_last--un"))
                _bt_p_next.RemoveFromClassList("Button_next_last--un"); // 활성화
            if (_bt_p_last.ClassListContains("Button_next_last--un"))
                _bt_p_last.RemoveFromClassList("Button_next_last--un"); // 활성화
        }
    }

    void showPlaySprite(int idnum, int count)
    {
        switch (idnum)
        {
            case 0:
                _Playground_Image.style.backgroundImage = p_0[count].texture;
                break;
            case 1:
                _Playground_Image.style.backgroundImage = p_1[count].texture;
                break;
            case 2:
                _Playground_Image.style.backgroundImage = p_2[count].texture;
                break;
            default:
                break;
        }
    }

    private void Onbt_Backp2(ClickEvent evt) //뒤로가기 놀이
    {
        AudioManager.PlayDefaultButtonSound();
        _Playground_Image.style.display = DisplayStyle.None;
        refreshbtp();
    }


    /// <summary>
    /// 품질혁신 페이지    /// </summary>
    /// <param name="qp"></param>
    void showQpages(VisualElement qp)
    {
        foreach (VisualElement a in _qPages)
        {
            if (qp == a)
                a.style.display = DisplayStyle.Flex;
            else
                a.style.display = DisplayStyle.None;
        }
    }
    private void GoNextq(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        q_cnt++;
        if (q_cnt > 6)
        {
            q_cnt = 6;
            return;
        }
        showQpages(_qPages[q_cnt]);
        setVideo(q_cnt);
        updateButtonq();
    }
    private void GoLastq(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();

        q_cnt--;
        if (q_cnt < 0)
        {
            q_cnt = 0;
            return;
        }
        showQpages(_qPages[q_cnt]);
        setVideo(q_cnt);
        updateButtonq();
    }
    void updateButtonq()
    {
        if (q_cnt == 6)
        {
            _bt_q_next.AddToClassList("Button_next_last--un");
        }
        else if (q_cnt == 0)
        {
            _bt_q_last.AddToClassList("Button_next_last--un");
        }
        else
        {
            if (_bt_q_next.ClassListContains("Button_next_last--un"))
                _bt_q_next.RemoveFromClassList("Button_next_last--un"); // 활성화
            if (_bt_q_last.ClassListContains("Button_next_last--un"))
                _bt_q_last.RemoveFromClassList("Button_next_last--un"); // 활성화
        }
    }



    void setVideo(int index)
    {
        switch (index)
        {
            case 0:
                videoPlayer.clip = q_clips[0];
                videoPlayer.Play();
                break;
            case 1:
                videoPlayer.clip = q_clips[1];
                videoPlayer.Play();
                break;
            case 2:
                videoPlayer.Stop();
                return;
            case 3:
                videoPlayer.clip = q_clips[2];
                videoPlayer.Play();
                break;
            case 4:
                videoPlayer.clip = q_clips[3];
                videoPlayer.Play();
                break;
            case 5:
                videoPlayer.Stop();
                return;
            case 6:
                videoPlayer.clip = q_clips[4];
                videoPlayer.Play();
                break;
            default:
                break;
        }

    }
}
