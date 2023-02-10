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
    const string SideSheet = "SideSheet";
    const string OpenButton = "Button_on01";
    const string HomeButton = "ButtonHOME";
    const string CloseButton = "CloseButton";
    const string SideSheetTwo = "SideSheetTwo";
    const string BackButton = "BackButton";
    const string TopTextGroup = "Top_TextGroup";

    const string HousePlan = "House_Plan1";

    public int buttonNum = 3;

    //온보딩 엘리먼트
    private VisualElement _Onboarding;
    private GroupBox _TopTextGroup;
    //좌측 버튼 ui
    private VisualElement _UIContainer;
    private VisualElement _sideSheet;
    private VisualElement _sideSheetTwo;
    private VisualElement _HousePlan;
    //버튼
    private Button _openButton;
    private Button _homeButton;
    private Button _BackButton;
    private Button _closeButton;

    //private Button[] _HouseSelectButton;
    public Button _HouseSelectedButton;


    public List<Button> buttons = new List<Button>();


    RaycastHit hit;
    void Start()
    {
        // root visualElement참조
        var root = GetComponent<UIDocument>().rootVisualElement;

        _UIContainer = root.Q<VisualElement>(UIContainer);
        _Onboarding = root.Q<VisualElement>(Onboarding);
        _HousePlan = root.Q<VisualElement>(HousePlan);
        //온보딩 화면 버튼
        _openButton = root.Q<Button>(OpenButton);
        _TopTextGroup = root.Q<GroupBox>(TopTextGroup);

        //홈버튼
        _homeButton = root.Q<Button>(HomeButton);


        //버튼선택슬라이드1,2
        _sideSheet = root.Q<VisualElement>(SideSheet);
        _sideSheetTwo = root.Q<VisualElement>(SideSheetTwo);
        //닫기
        _closeButton = root.Q<Button>(CloseButton);
        //뒤로가기
        _BackButton = root.Q<Button>(BackButton);

        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(root.Q<Button>("HouseSelectButton"+$"{i+1}"));
            buttons[i].RegisterCallback<ClickEvent>(OnHouseButtonClicked);
        }
        //시작할 때 감추기
        _UIContainer.style.display = DisplayStyle.None;
        _HousePlan.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;


        _openButton.RegisterCallback<ClickEvent>(OnBoardButtonClicked);
        _homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        _sideSheet.RegisterCallback<TransitionEndEvent>(OnSideSheetOut);
        


    }
    private void OnEnable()
    {
        //등록할 콜백함수들
        //_openButton.RegisterCallback<ClickEvent>(OnBoardButtonClicked);
        //_homeButton.RegisterCallback<ClickEvent>(OnHomeButtonClicked);
        //_closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        //_sideSheet.RegisterCallback<TransitionEndEvent>(OnSideSheetOut);
    }

    private void OnDisable()
    {
        _openButton.UnregisterCallback<ClickEvent>(OnBoardButtonClicked);
        _homeButton.UnregisterCallback<ClickEvent>(OnHomeButtonClicked);
        _closeButton.UnregisterCallback<ClickEvent>(OnCloseButtonClicked);
        _sideSheet.UnregisterCallback<TransitionEndEvent>(OnSideSheetOut);
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
        _UIContainer.style.display = DisplayStyle.None;

        //집 오브젝트
        onScreenObjectManager.OnHouse();
    }

    private void OnBackButtonClicked(ClickEvent evt)  //집내부 뒤로가기
    {
        _HousePlan.style.display = DisplayStyle.None;
        _UIContainer.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        //집 오브젝트
        onScreenObjectManager.OnMaker();
    }
    private void OnHomeButtonClicked(ClickEvent evt) // 홈으로 돌아가기
    {
        //시트그룹
        _Onboarding.style.display = DisplayStyle.Flex;

        _sideSheet.RemoveFromClassList("SideSheet--in");
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");

        //object 제거
        onScreenObjectManager.NothingOn();
    }

    private void OnBoardButtonClicked(ClickEvent evt)
    {
        //시트 열기
        _UIContainer.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _sideSheet.AddToClassList("SideSheet--in");

        //마커표시
        onScreenObjectManager.OnMaker();
    }

    private void OnSideSheetOut(TransitionEndEvent evt)
    {
        if (!_sideSheet.ClassListContains("SideSheet--in"))
        {
            //AR시트그룹 감추기
            _UIContainer.style.display = DisplayStyle.None;
        }
    }
    void Update()
    {
        ////Debug.Log(_sideSheetTwo.ClassListContains("SideSheetTwo--in"));
        //if (Input.touchCount > 0)
        //{
        //    try
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        Ray ray = Camera.current.ScreenPointToRay(touch.position);

        //        if (Physics.Raycast(ray, out hit, 10000))
        //        {
        //            debugTxt.text = hit.transform.gameObject.name;
        //        }
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //}
    }
}
