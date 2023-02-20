using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] protected UIDocument m_Document;



    //public ARTouchManger touchManger;
    public OnScreenObjectManager onScreenObjectManager;

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

    const string BackButton = "BackButton";
    const string BackButtonP = "Button_Back_p";
    const string HousePlan = "House_Plan1";

    const string TopButton_city = "topButton_city";
    const string TopButton_scale = "topButton_scale";
    const string TopButton_public = "topButton_public";

    //메인버튼 아이디
    int ID;

    int buttonNum = 3; // ar sidebar버튼 개수

    //온보딩 엘리먼트
    private VisualElement _Onboarding;
    private GroupBox _TopTextGroup;

    //투시도
    private VisualElement _perspective;
    private Button _BackButtonP;

    //좌측 버튼 ui
    private VisualElement _UIContainer;
    private VisualElement _sideSheetTwo;
    private VisualElement _HousePlan;
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

    private Button _homeButton;
    private Button _BackButton;
    private Button _closeButton;
    //top bar 버튼
    private Button _topButton_city;
    private Button _topButton_scale;
    private Button _topButton_public;

    //private Button[] _HouseSelectButton;
    public Button _HouseSelectedButton;


    public List<Button> buttons = new List<Button>();
    public List<VisualElement> sidesheets = new List<VisualElement>();
    public List<VisualElement> artexts = new List<VisualElement>();

    protected VisualElement m_root; // 메인루트
    protected VisualElement ar_root; // ar선택창 루트
    protected VisualElement s_root; // 시작 루핑영상 루트
    protected VisualElement pip_root; // pip 루트
    protected VisualElement smart_root; // 스마트 루트

    protected VisualElement _AR_House; // ar트랙화면
    protected VisualElement _AR_Outside; // ar트랙화면
    protected VisualElement _AR_Smarthome; // ar트랙화면
    protected VisualElement _AR_Quality; // ar트랙화면


    RaycastHit hit;
    public List<Button> _smartbuttons = new List<Button>();
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
    }
    protected void Start()
    {
        // root visualElement참조
        ar_root = m_root.Q<VisualElement>("menu");

        _AR_House = m_root.Q<VisualElement>(AR_House);
        _AR_Outside = m_root.Q<VisualElement>(AR_Outside);
        _AR_Smarthome = m_root.Q<VisualElement>(AR_Smarthome);
        _AR_Quality = m_root.Q<VisualElement>(AR_Quality);

        _perspective = m_root.Q<VisualElement>(Perspective);


        _UIContainer = ar_root.Q<VisualElement>(UIContainer);
        _Onboarding = m_root.Q<VisualElement>(Onboarding);
        _HousePlan = m_root.Q<VisualElement>(HousePlan);

        //온보딩 화면 버튼
        _openButton1 = m_root.Q<Button>(OpenButton1);
        _openButton2 = m_root.Q<Button>(OpenButton2);
        _openButton3 = m_root.Q<Button>(OpenButton3);
        _openButton4 = m_root.Q<Button>(OpenButton4);
        _openButton5 = m_root.Q<Button>(OpenButton5);
        _TopTextGroup = m_root.Q<GroupBox>(TopTextGroup);


        //-------------ar트랙화면
        //상단바버튼
        _topButton_city = m_root.Q<Button>(TopButton_city);
        _topButton_scale = m_root.Q<Button>(TopButton_scale);
        _topButton_public = m_root.Q<Button>(TopButton_public);

        //홈버튼
        _homeButton = m_root.Q<Button>(HomeButton);
        //사이트시트

        sidesheets.Add(m_root.Q<VisualElement>(SideSheetTwo));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_o"));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_s"));
        sidesheets.Add(m_root.Q<VisualElement>("SideSheet_q"));


        //하단텍스트
        artexts.Add(m_root.Q<VisualElement>("text_active_h"));
        artexts.Add(m_root.Q<VisualElement>("text_active_out"));
        artexts.Add(m_root.Q<VisualElement>("text_active_smart"));
        artexts.Add(m_root.Q<VisualElement>("text_active_quality"));

       _text_Point02 = m_root.Q<VisualElement>("text_active2");



        //닫기
        _closeButton = m_root.Q<Button>(CloseButton);

        //뒤로가기
        _BackButton = m_root.Q<Button>(BackButton);
        _BackButtonP = _perspective.Q<Button>(BackButtonP);


        //시작할 때 감추기
        ar_root.style.display = DisplayStyle.None;
        _HousePlan.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;

        SetupSelectButton();

        _openButton1.RegisterCallback<ClickEvent>(OnBoardButtonClicked);
        _openButton2.RegisterCallback<ClickEvent>(OnBoardButtonClicked_smart);
        _openButton3.RegisterCallback<ClickEvent>(OnBoardButtonClicked_out);
        _openButton4.RegisterCallback<ClickEvent>(OnBoardButtonClicked_quality);
        // 동일구조 콜백함수 모듈화 ******
        _openButton5.RegisterCallback<ClickEvent>(OnPersviewButtonClicked);

        _homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        _BackButtonP.RegisterCallback<ClickEvent>(OnBackButtonPClicked);
        //상단바
        _topButton_city.RegisterCallback<ClickEvent>(OnTopButtonClicked_C);
        _topButton_scale.RegisterCallback<ClickEvent>(OnTopButtonClicked_C);
        _topButton_public.RegisterCallback<ClickEvent>(OnTopButtonClicked_C);


        //for (int i = 0; i < 5; i++)
        //{
        //    _smartbuttons.Add(m_root.Q<Button>("SmartSelectButton" + $"{i + 1}"));
        //}
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

    void SetupSelectButton() //사이드바 버튼가져오기
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(m_root.Q<Button>("HouseSelectButton" + $"{i + 1}"));
            buttons[i].RegisterCallback<ClickEvent>(OnHouseButtonClicked);
        }
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

        //object 제거
        onScreenObjectManager.NothingOn();
    }

    private void OnBoardButtonClicked(ClickEvent evt) // 주택평면
    {
        CloseMain(_AR_House);
        ID = 0;
    }
    private void OnBoardButtonClicked_out(ClickEvent evt) // 외부공간
    {
        CloseMain(_AR_Outside);
        ID = 1;
    }
    private void OnBoardButtonClicked_smart(ClickEvent evt) // 스마트홈
    {
        CloseMain(_AR_Smarthome);
        ID = 2;
    }
    private void OnBoardButtonClicked_quality(ClickEvent evt) // 품질혁신
    {
        CloseMain(_AR_Quality);
        ID = 3;
    }
    void CloseMain(VisualElement visual)
    {
        AudioManager.PlayDefaultButtonSound();
        //시트 열기
        ar_root.style.display = DisplayStyle.Flex;
        visual.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");

        //마커표시 // 여기다 선택 ID마커만 활성화 코드도 추가
        onScreenObjectManager.OnMaker();
    }

    public int GetID()
    {
        return ID;
    }
    private void OnPersviewButtonClicked(ClickEvent evt)
    {
        AudioManager.PlayDefaultButtonSound();
        //시트 열기
        _perspective.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;
    }
    private void OnTopButtonClicked_C(ClickEvent evt) // 미리보는도시
    {
        AudioManager.PlayDefaultButtonSound();

        pip_root.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;
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
        sidesheets[ID].AddToClassList("SideSheetTwo--in");
        artexts[ID].AddToClassList("text_Point--pade");
        _text_Point02.RemoveFromClassList("text_Point--pade");
        //텍스트 나오기
    }
    private void OnCloseButtonClicked(ClickEvent evt) //사이트시트 닫기
    {
        AudioManager.PlayDefaultButtonSound();
        closeSidesheet();
    }

    void closeSidesheet()
    {
        for (int i = 0; i < sidesheets.Count; i++)
        {
            sidesheets[i].RemoveFromClassList("SideSheetTwo--in");
            artexts[i].RemoveFromClassList("text_Point--pade");
        }
        _text_Point02.AddToClassList("text_Point--pade");
    }
    public void PickHighlight(Button s_button) // 선택버튼 하이라이트 효과
    {
        foreach (Button bt in buttons)
        {
            if(bt == s_button)
            {
                bt?.AddToClassList("Button_Side01--sel");
            }
            else
            {
                bt?.RemoveFromClassList("Button_Side01--sel");
            }
        }
    }

    private void OnHouseButtonClicked(ClickEvent evt)  //집내부 관람 화면
    {
        AudioManager.PlayDefaultButtonSound();
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;

        //집 오브젝트
        onScreenObjectManager.OnHouse(0);//트래커 id가져오기
    }

    private void OnBackButtonPClicked(ClickEvent evt)  //투시도 뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        _Onboarding.style.display = DisplayStyle.Flex;
        _perspective.style.display = DisplayStyle.None;
    }

    private void OnBackButtonClicked(ClickEvent evt)  //집내부 뒤로가기
    {
        AudioManager.PlayDefaultButtonSound();
        _HousePlan.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        _perspective.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");
        //집 오브젝트
        onScreenObjectManager.OnMaker();
    }
    public void GoHome() // 일정시간 후 홈으로 돌아가기 //수정필
    {
        //시트그룹
        ar_root.style.display = DisplayStyle.None;
        _AR_House.style.display = DisplayStyle.None;
        _AR_Quality.style.display = DisplayStyle.None;
        _AR_Outside.style.display = DisplayStyle.None;
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
