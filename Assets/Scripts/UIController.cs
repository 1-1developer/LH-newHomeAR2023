using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] protected UIDocument m_Document;



    //public ARTouchManger touchManger;
    [SerializeField] protected OnScreenObjectManager onScreenObjectManager;
    [SerializeField] protected ARImageTracker imageTracker;

    const string Onboarding = "Onboarding_Main";
    const string UIContainer = "AR_screen";
    const string Perspective = "ImageScreen";
    const string PopScreen = "PopScreen";

    const string OpenButton1 = "Button_on01"; //메인화면 버튼-주택평면
    const string OpenButton2 = "Button_on02"; //메인화면 버튼-스마트홈
    const string OpenButton3 = "Button_on03"; //메인화면 버튼 -외부공간
    const string OpenButton4= "Button_on04"; //메인화면 버튼 - 품질혁신
    const string OpenButton5 = "Button_on_viewmap"; //메인화면 - 투시도

    const string AR_House = "menu_house"; //ar트랙화면 - 주택평면
    const string AR_Outside = "menu_outside"; 
    const string AR_Smarthome = "menu_smart"; 
    const string AR_Quality = "menu_quality";



    const string HomeButton = "ButtonHOME";
    const string SideSheetTwo = "SideSheetTwo"; // side2단 
    const string CloseButton = "CloseButton"; // sideTwo닫기 버튼
    const string TopTextGroup = "Top_TextGroup";

    const string BackButtonP = "Button_Back_p";
    const string HousePlan = "House";


    //메인버튼 아이디
    int ID;


    //온보딩 엘리먼트
    protected VisualElement _Onboarding;
    protected GroupBox _TopTextGroup;

    //투시도
    private VisualElement _perspective;
    private Button _BackButtonP;

    //좌측 버튼 ui
    private VisualElement _UIContainer;
    private VisualElement _sideSheetTwo;
    protected VisualElement _HousePlan;
    private VisualElement _text_Point01;
    private VisualElement _text_Point02;
    private VisualElement _text_Point_out; //이게뭐지 ?
    private VisualElement _text_Point_smart;
    private VisualElement _text_Point_qual;

    private VisualElement _sideSheet_o;
    private VisualElement _sideSheet_s;
    private VisualElement _sideSheet_q;


    //버튼
    private Button _openButton1;
    private Button _openButton2;
    private Button _openButton3;
    private Button _openButton4;
    private Button _openButton5;

    protected Button _homeButton;
    private Button _closeButton;


    //private Button[] _HouseSelectButton;
    public Button _HouseSelectedButton;

    List<Button> _closeButtons = new List<Button>(); 

    public List<VisualElement> sidesheets = new List<VisualElement>();
    public List<VisualElement> artexts = new List<VisualElement>();
    public List<Button> _smartbuttons = new List<Button>();


    protected VisualElement m_root; // 메인루트
    protected VisualElement ar_root; // ar선택창 루트
    protected VisualElement s_root; // 시작 루핑영상 루트
    protected VisualElement pip_root; // pip 루트
    protected VisualElement smart_root; // 스마트 루트

    protected VisualElement _AR_House; // ar트랙화면
    protected VisualElement _AR_Outside; // ar트랙화면
    protected VisualElement _AR_Smarthome; // ar트랙화면
    protected VisualElement _AR_Quality; // ar트랙화면


    bool videoOn = false; // video


    List<VisualElement> _ARScreens = new List<VisualElement>();

    RaycastHit hit;
    protected void Awake()
    {
        if (m_Document == null)
            m_Document = GetComponent<UIDocument>();
        SetVisualElements();
    }

    protected virtual void SetVisualElements()
    {
        // get a reference to the root VisualElement 
        if (m_Document != null)
            m_root = m_Document.rootVisualElement;
        s_root = m_root.Q<VisualElement>("StartScreen");
        pip_root = m_root.Q<VisualElement>(PopScreen);
        smart_root = m_root.Q<VisualElement>("SmartDetail");
        _Onboarding = m_root.Q<VisualElement>(Onboarding);
        _HousePlan = m_root.Q<VisualElement>(HousePlan);


    }
    protected void Start()
    {
        // root visualElement참조
        ar_root = m_root.Q<VisualElement>("menu");

        _AR_House = m_root.Q<VisualElement>(AR_House);
        _AR_Outside = m_root.Q<VisualElement>(AR_Outside);
        _AR_Smarthome = m_root.Q<VisualElement>(AR_Smarthome);
        _AR_Quality = m_root.Q<VisualElement>(AR_Quality);

        _ARScreens.Add(_AR_House);
        _ARScreens.Add(_AR_Outside);
        _ARScreens.Add(_AR_Smarthome);
        _ARScreens.Add(_AR_Quality);


        _perspective = m_root.Q<VisualElement>(Perspective);


        _UIContainer = ar_root.Q<VisualElement>(UIContainer);

        //온보딩 화면 버튼
        _openButton1 = m_root.Q<Button>(OpenButton1);
        _openButton2 = m_root.Q<Button>(OpenButton2);
        _openButton3 = m_root.Q<Button>(OpenButton3);
        _openButton4 = m_root.Q<Button>(OpenButton4);
        _openButton5 = m_root.Q<Button>(OpenButton5);
        _TopTextGroup = m_root.Q<GroupBox>(TopTextGroup);


        //-------------ar트랙화면


        //홈버튼
        _homeButton = m_root.Q<Button>(HomeButton);
        //사이트시트

        sidesheets.Add(m_root.Q<VisualElement>(SideSheetTwo));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_o"));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_s"));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_q"));


        //하단텍스트
        artexts.Add(m_root.Q<VisualElement>("text_active"));


       _text_Point02 = m_root.Q<VisualElement>("text_active2");


        //닫기
        //_closeButton = m_root.Q<Button>(CloseButton);

        for (int i = 0; i < 4; i++)
        {
            _closeButtons.Add(m_root.Q<Button>("CloseButton" + $"{i}"));
        }

        //뒤로가기
        _BackButtonP = _perspective.Q<Button>(BackButtonP);


        //시작할 때 감추기
        ar_root.style.display = DisplayStyle.None;
        _HousePlan.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;



        _openButton1.RegisterCallback<ClickEvent>(OnBoardButtonClicked);
        _openButton2.RegisterCallback<ClickEvent>(OnBoardButtonClicked_explanation);
        // 동일구조 콜백함수 모듈화 ******
        _openButton5.RegisterCallback<ClickEvent>(OnPersviewButtonClicked);

        _homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        //_closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _BackButtonP.RegisterCallback<ClickEvent>(OnBackButtonPClicked);

        _closeButtons[0].RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _closeButtons[1].RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _closeButtons[2].RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _closeButtons[3].RegisterCallback<ClickEvent>(OnCloseButtonClicked);


        //for (int i = 0; i < 5; i++)
        //{
        //    _smartbuttons.Add(m_root.Q<Button>("SmartSelectButton" + $"{i + 1}"));
        //}
    }

    private void OnBoardButtonClicked(ClickEvent evt) //AR 시 작
    {
        AudioManager.PlayDefaultButtonSound();

        CloseMain();
        _AR_House.style.display = DisplayStyle.None;
        _AR_Quality.style.display = DisplayStyle.None;
        _AR_Outside.style.display = DisplayStyle.None;
        _AR_Smarthome.style.display = DisplayStyle.None;
    }
    private void OnBoardButtonClicked_explanation(ClickEvent evt) //설명으로 돌아가기
    {
        AudioManager.PlayDefaultButtonSound();
        GoHome();
    }

    private void OnEnable()
    {
        //등록할 콜백함수들
        //_openButton.RegisterCallback<ClickEvent>(OnBoardButtonClicked);
        //_homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        //_closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        //_sideSheet.RegisterCallback<TransitionEndEvent>(OnSideSheetOut);
        //SetupSelectButton();
    }

    private void OnDisable()
    {
        //_openButton1.UnregisterCallback<ClickEvent>(OnBoardButtonClicked);
        //_homeButton.UnregisterCallback<ClickEvent>(OnHomeButtonClicked);
        //_closeButton.UnregisterCallback<ClickEvent>(OnCloseButtonClicked);
        //_sideSheet.UnregisterCallback<TransitionEndEvent>(OnSideSheetOut);
    }



    private void OnHomeButtonClicked(ClickEvent evt) // 홈버튼-홈으로 돌아가기
    {
        AudioManager.PlayDefaultButtonSound();
        //시트그룹
        ar_root.style.display = DisplayStyle.None;

        _AR_House.style.display = DisplayStyle.None;
        _AR_Quality.style.display = DisplayStyle.None;
        _AR_Outside.style.display = DisplayStyle.None;
        _AR_Smarthome.style.display = DisplayStyle.None;

        _Onboarding.style.display = DisplayStyle.Flex;

        _homeButton.RemoveFromClassList("Button_Home--in");
        closeSidesheet();
        imageTracker.GetComplex().PointerInitialize();
        //object 제거
        onScreenObjectManager.NothingOn();
    }

    /*
    private void OnBoardButtonClicked_out(ClickEvent evt) // 외부공간
    {
        //AudioManager.PlayDefaultButtonSound();

        CloseMain(_AR_Outside);
        ID = 1;
    }
    private void OnBoardButtonClicked_smart(ClickEvent evt) // 스마트홈
    {
        //AudioManager.PlayDefaultButtonSound();

        CloseMain(_AR_Smarthome);
        ID = 2;
    }
    private void OnBoardButtonClicked_quality(ClickEvent evt) // 품질혁신
    {
        //AudioManager.PlayDefaultButtonSound();

        CloseMain(_AR_Quality);
        ID = 3;
    }
    */
    void CloseMain()
    {
        //시트 열기
        ar_root.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");

        //마커표시 // 여기다 선택 ID마커만 활성화 코드도 추가
        onScreenObjectManager.OnMaker();
    }

    //public int GetID()
    //{
    //    return ID;
    //}
    private void OnPersviewButtonClicked(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        //시트 열기
        _perspective.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;
        videoOn = true;
    }

    public bool GetVideoOn()
    {
        return videoOn;
    }
    private void OnSideSheetOut(TransitionEndEvent evt)
    {
        if (!_sideSheetTwo.ClassListContains("SideSheet--in"))
        {
            //AR시트그룹 감추기
            ar_root.style.display = DisplayStyle.None;
        }
    }
    public void InPlanPannelAR(int ID)  //마커선택시 사이트시트 열기
    {
        foreach (VisualElement s in _ARScreens)
        {
            if(s == _ARScreens[ID])
            {
                s.style.display = DisplayStyle.Flex;
               
                artexts[0].AddToClassList("text_Point--pade");
            }
            else
            {
                //artexts[0].RemoveFromClassList("text_Point--pade");
                s.style.display = DisplayStyle.None;
            }
        }
        foreach (VisualElement sh in sidesheets)
        {
            if (sh == sidesheets[ID])
            {
                sh?.AddToClassList("SideSheetTwo--in");
            }
            else
            {
                sh?.RemoveFromClassList("SideSheetTwo--in");
            }
        }
        _text_Point02.RemoveFromClassList("text_Point--pade");

    }



    private void OnCloseButtonClicked(ClickEvent evt) //사이트시트 닫기
    {
        AudioManager.PlayDefaultButtonSound();
        closeSidesheet();
    }

    public void closeSidesheet()
    {
        for (int i = 0; i < sidesheets.Count; i++)
        {
            sidesheets[i].RemoveFromClassList("SideSheetTwo--in");
            artexts[0].RemoveFromClassList("text_Point--pade");
        }
        _text_Point02.AddToClassList("text_Point--pade");
    }




    private void OnBackButtonPClicked(ClickEvent evt)  //투시도 뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        _Onboarding.style.display = DisplayStyle.Flex;
        _perspective.style.display = DisplayStyle.None;
        videoOn = false;
    }


    public void GoHome() // 일정시간 후 홈으로 돌아가기 //수정필
    {
        //시트그룹
        if(ar_root.style.display == DisplayStyle.Flex)
            ar_root.style.display = DisplayStyle.None;
        if (_AR_House.style.display == DisplayStyle.Flex)
            _AR_House.style.display = DisplayStyle.None;
        if (_AR_Quality.style.display == DisplayStyle.Flex)
            _AR_Quality.style.display = DisplayStyle.None;
        if (_AR_Outside.style.display == DisplayStyle.Flex)
            _AR_Outside.style.display = DisplayStyle.None;
        if (_AR_Smarthome.style.display == DisplayStyle.Flex)
            _AR_Smarthome.style.display = DisplayStyle.None;

        _perspective.style.display = DisplayStyle.None;

        _HousePlan.style.display = DisplayStyle.None;

        pip_root.style.display = DisplayStyle.None;
        smart_root.style.display = DisplayStyle.None;

        _Onboarding.style.display = DisplayStyle.Flex;
        s_root.style.display = DisplayStyle.Flex;


        if (_homeButton.ClassListContains("Button_Home--in"))
            _homeButton.RemoveFromClassList("Button_Home--in");
        for (int i = 0; i < sidesheets.Count; i++)
        {
            sidesheets[i].RemoveFromClassList("SideSheetTwo--in");
        }


        //object 제거
        onScreenObjectManager.NothingOn();
    }

    void Update()
    {
    }
}
