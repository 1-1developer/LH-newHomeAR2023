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

    //�º��� ������Ʈ
    private VisualElement _Onboarding;
    private GroupBox _TopTextGroup;
    //���� ��ư ui
    private VisualElement _UIContainer;
    private VisualElement _sideSheet;
    private VisualElement _sideSheetTwo;
    private VisualElement _HousePlan;
    //��ư
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
        // root visualElement����
        var root = GetComponent<UIDocument>().rootVisualElement;

        _UIContainer = root.Q<VisualElement>(UIContainer);
        _Onboarding = root.Q<VisualElement>(Onboarding);
        _HousePlan = root.Q<VisualElement>(HousePlan);
        //�º��� ȭ�� ��ư
        _openButton = root.Q<Button>(OpenButton);
        _TopTextGroup = root.Q<GroupBox>(TopTextGroup);

        //Ȩ��ư
        _homeButton = root.Q<Button>(HomeButton);


        //��ư���ý����̵�1,2
        _sideSheet = root.Q<VisualElement>(SideSheet);
        _sideSheetTwo = root.Q<VisualElement>(SideSheetTwo);
        //�ݱ�
        _closeButton = root.Q<Button>(CloseButton);
        //�ڷΰ���
        _BackButton = root.Q<Button>(BackButton);

        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(root.Q<Button>("HouseSelectButton"+$"{i+1}"));
            buttons[i].RegisterCallback<ClickEvent>(OnHouseButtonClicked);
        }
        //������ �� ���߱�
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
        //����� �ݹ��Լ���
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

    public void InPlanPannelAR()  //��Ŀ���ý�
    {
        _sideSheetTwo.AddToClassList("SideSheetTwo--in");
    }
    private void OnCloseButtonClicked(ClickEvent evt) //���ôݱ�
    {
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");
    }
    private void OnHouseButtonClicked(ClickEvent evt)  //������ ���� ȭ��
    {
        _TopTextGroup.style.display = DisplayStyle.None;
        _BackButton.style.display = DisplayStyle.Flex;
        _HousePlan.style.display = DisplayStyle.Flex;
        _UIContainer.style.display = DisplayStyle.None;

        //�� ������Ʈ
        onScreenObjectManager.OnHouse();
    }

    private void OnBackButtonClicked(ClickEvent evt)  //������ �ڷΰ���
    {
        _HousePlan.style.display = DisplayStyle.None;
        _UIContainer.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;
        //�� ������Ʈ
        onScreenObjectManager.OnMaker();
    }
    private void OnHomeButtonClicked(ClickEvent evt) // Ȩ���� ���ư���
    {
        //��Ʈ�׷�
        _Onboarding.style.display = DisplayStyle.Flex;

        _sideSheet.RemoveFromClassList("SideSheet--in");
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");

        //object ����
        onScreenObjectManager.NothingOn();
    }

    private void OnBoardButtonClicked(ClickEvent evt)
    {
        //��Ʈ ����
        _UIContainer.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _sideSheet.AddToClassList("SideSheet--in");

        //��Ŀǥ��
        onScreenObjectManager.OnMaker();
    }

    private void OnSideSheetOut(TransitionEndEvent evt)
    {
        if (!_sideSheet.ClassListContains("SideSheet--in"))
        {
            //AR��Ʈ�׷� ���߱�
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
