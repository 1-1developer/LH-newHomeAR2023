using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public TextMeshPro debugTxt;
    public touhOBJtest touchManger;
    public OnScreenObjectManager onScreenObjectManager;

    const string Onboarding = "Onboarding";
    const string UIContainer = "AR_screen";
    const string Perspective = "ImageScreen";

    const string OpenButton1 = "Button_on01"; //메인화면 버튼1
    const string OpenButton2 = "Button_on02"; //메인화면 버튼1
    const string OpenButton3 = "Button_on03"; //메인화면 버튼1
    const string OpenButton4= "Button_on04"; //메인화면 버튼1
    const string OpenButton5 = "Button_on_viewmap"; //메인화면 투시도 버4

    const string HomeButton = "ButtonHOME";
    const string SideSheet = "SideSheet";
    const string SideSheetTwo = "SideSheetTwo";
    const string CloseButton = "CloseButton"; // sideTwo닫기 버튼
    const string TopTextGroup = "Top_TextGroup";

    const string BackButton = "BackButton";
    const string BackButtonP = "Button_Back_p";
    const string HousePlan = "House_Plan1";

    public int buttonNum = 3; // sidebar버튼s

    //온보딩 엘리먼트
    private VisualElement _Onboarding;
    private GroupBox _TopTextGroup;

    //투시도
    private VisualElement _perspective;
    private Button _BackButtonP;

    //좌측 버튼 ui
    private VisualElement _UIContainer;
    private VisualElement _sideSheet;
    private VisualElement _sideSheetTwo;
    private VisualElement _HousePlan;
    //버튼
    private Button _openButton1;
    private Button _openButton2;
    private Button _openButton3;
    private Button _openButton4;
    private Button _openButton5;

    private Button _homeButton;
    private Button _BackButton;
    private Button _closeButton;

    //private Button[] _HouseSelectButton;
    public Button _HouseSelectedButton;


    public List<Button> buttons = new List<Button>();

    private VisualElement m_root; // 메인루트
    private VisualElement ar_root; // ar선택창 루트
    RaycastHit hit;
    void Start()
    {
        // root visualElement참조
        m_root = GetComponent<UIDocument>().rootVisualElement;
        ar_root = m_root.Q<VisualElement>("menu");
        _perspective = m_root.Q<VisualElement>(Perspective);


        _UIContainer = ar_root.Q<VisualElement>(UIContainer);
        _Onboarding = m_root.Q<VisualElement>(Onboarding);
        _HousePlan = m_root.Q<VisualElement>(HousePlan);

        //온보딩 화면 버튼
        _openButton1 = m_root.Q<Button>(OpenButton1);
        _openButton5 = m_root.Q<Button>(OpenButton5);
        _TopTextGroup = m_root.Q<GroupBox>(TopTextGroup);

        //홈버튼
        _homeButton = m_root.Q<Button>(HomeButton);


        //버튼선택슬라이드1,2
        _sideSheet = m_root.Q<VisualElement>(SideSheet);
        _sideSheetTwo = m_root.Q<VisualElement>(SideSheetTwo);
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
        // 동일구조 콜백함수 모듈화 ******
        _openButton5.RegisterCallback<ClickEvent>(OnPersviewButtonClicked);

        _homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        _BackButtonP.RegisterCallback<ClickEvent>(OnBackButtonPClicked);

        _sideSheet.RegisterCallback<TransitionEndEvent>(OnSideSheetOut);
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
        _openButton1.UnregisterCallback<ClickEvent>(OnBoardButtonClicked);
        _homeButton.UnregisterCallback<ClickEvent>(OnHomeButtonClicked);
        _closeButton.UnregisterCallback<ClickEvent>(OnCloseButtonClicked);
        _sideSheet.UnregisterCallback<TransitionEndEvent>(OnSideSheetOut);
    }



    private void OnHomeButtonClicked(ClickEvent evt) // 홈으로 돌아가기
    {
        //시트그룹
        ar_root.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;

        _homeButton.RemoveFromClassList("Button_Home--in");
        _sideSheet.RemoveFromClassList("SideSheet--in");
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");

        //object 제거
        onScreenObjectManager.NothingOn();
    }

    private void OnBoardButtonClicked(ClickEvent evt)
    {
        //시트 열기
        ar_root.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");
        _sideSheet.AddToClassList("SideSheet--in");

        //마커표시
        onScreenObjectManager.OnMaker();
    }



    private void OnPersviewButtonClicked(ClickEvent evt)
    {
        //시트 열기
        _perspective.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;
    }

    private void OnSideSheetOut(TransitionEndEvent evt)
    {
        if (!_sideSheet.ClassListContains("SideSheet--in"))
        {
            //AR시트그룹 감추기
            ar_root.style.display = DisplayStyle.None;
        }
    }
    void SetupSelectButton() //버튼가져오기
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(m_root.Q<Button>("HouseSelectButton" + $"{i + 1}"));
            buttons[i].RegisterCallback<ClickEvent>(OnHouseButtonClicked);
        }
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


    public void InPlanPannelAR()  //마커선택시
    {
        _sideSheetTwo.AddToClassList("SideSheetTwo--in");
    }

    private void OnCloseButtonClicked(ClickEvent evt) //선택닫기
    {
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");
    }

    private void OnHouseButtonClicked(ClickEvent evt)  //집내부 관람 화면
    {
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        ar_root.style.display = DisplayStyle.None;

        //집 오브젝트
        onScreenObjectManager.OnHouse();
    }

    private void OnBackButtonPClicked(ClickEvent evt)  //투시도 뒤로가기
    {
        _Onboarding.style.display = DisplayStyle.Flex;
        _perspective.style.display = DisplayStyle.None;
    }

    private void OnBackButtonClicked(ClickEvent evt)  //집내부 뒤로가기
    {
        _HousePlan.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        _perspective.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");
        //집 오브젝트
        onScreenObjectManager.OnMaker();
    }


    void Update()
    {
    }
}
