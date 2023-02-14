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

    const string OpenButton = "Button_on01"; //����ȭ�� ��ư1

    const string HomeButton = "ButtonHOME";
    const string SideSheet = "SideSheet";
    const string SideSheetTwo = "SideSheetTwo";
    const string CloseButton = "CloseButton"; // sideTwo�ݱ� ��ư
    const string TopTextGroup = "Top_TextGroup";

    const string BackButton = "BackButton";
    const string HousePlan = "House_Plan1";

    public int buttonNum = 3; // sidebar��ưs

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

    private VisualElement m_root; // ���η�Ʈ
    private VisualElement ar_root; // ar����â ��Ʈ
    RaycastHit hit;
    void Start()
    {
        // root visualElement����
        m_root = GetComponent<UIDocument>().rootVisualElement;
        ar_root = m_root.Q<VisualElement>("menu");

        _UIContainer = ar_root.Q<VisualElement>(UIContainer);
        _Onboarding = m_root.Q<VisualElement>(Onboarding);
        _HousePlan = m_root.Q<VisualElement>(HousePlan);

        //�º��� ȭ�� ��ư
        _openButton = m_root.Q<Button>(OpenButton);
        _TopTextGroup = m_root.Q<GroupBox>(TopTextGroup);

        //Ȩ��ư
        _homeButton = m_root.Q<Button>(HomeButton);


        //��ư���ý����̵�1,2
        _sideSheet = m_root.Q<VisualElement>(SideSheet);
        _sideSheetTwo = m_root.Q<VisualElement>(SideSheetTwo);
        //�ݱ�
        _closeButton = m_root.Q<Button>(CloseButton);
        //�ڷΰ���
        _BackButton = m_root.Q<Button>(BackButton);


        //������ �� ���߱�
        ar_root.style.display = DisplayStyle.None;
        _HousePlan.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;

        SetupSelectButton();

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
        //SetupSelectButton();
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
        ar_root.style.display = DisplayStyle.None;

        //�� ������Ʈ
        onScreenObjectManager.OnHouse();
    }

    private void OnBackButtonClicked(ClickEvent evt)  //������ �ڷΰ���
    {
        _HousePlan.style.display = DisplayStyle.None;
        ar_root.style.display = DisplayStyle.Flex;
        _BackButton.style.display = DisplayStyle.None;
        _TopTextGroup.style.display = DisplayStyle.Flex;

        _homeButton.AddToClassList("Button_Home--in");
        //�� ������Ʈ
        onScreenObjectManager.OnMaker();
    }
    private void OnHomeButtonClicked(ClickEvent evt) // Ȩ���� ���ư���
    {
        //��Ʈ�׷�
        ar_root.style.display = DisplayStyle.None;
        _Onboarding.style.display = DisplayStyle.Flex;

        _homeButton.RemoveFromClassList("Button_Home--in");
        _sideSheet.RemoveFromClassList("SideSheet--in");
        _sideSheetTwo.RemoveFromClassList("SideSheetTwo--in");

        //object ����
        onScreenObjectManager.NothingOn();
    }

    private void OnBoardButtonClicked(ClickEvent evt)
    {
        //��Ʈ ����
        ar_root.style.display = DisplayStyle.Flex;
        _Onboarding.style.display = DisplayStyle.None;

        _homeButton.AddToClassList("Button_Home--in");
        _sideSheet.AddToClassList("SideSheet--in");

        //��Ŀǥ��
        onScreenObjectManager.OnMaker();
    }

    private void OnSideSheetOut(TransitionEndEvent evt)
    {
        if (!_sideSheet.ClassListContains("SideSheet--in"))
        {
            //AR��Ʈ�׷� ���߱�
            ar_root.style.display = DisplayStyle.None;
        }
    }
    void SetupSelectButton() //��ư��������
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons.Add(m_root.Q<Button>("HouseSelectButton" + $"{i + 1}"));
            buttons[i].RegisterCallback<ClickEvent>(OnHouseButtonClicked);
        }
    }
    public void PickHighlight(Button s_button) // ���ù�ư ���̶���Ʈ ȿ��
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickHighlight(buttons[0]);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PickHighlight(buttons[1]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PickHighlight(buttons[2]);
        }
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
