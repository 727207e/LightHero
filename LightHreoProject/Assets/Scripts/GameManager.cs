﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region 변수선언
    static public GameManager instance = null;
    [Header("CanversPanels")]
    private string gameVersion = "1";
    public GameObject ButtonPanel;
    public GameObject LoginPanel;
    public GameObject[] panels = new GameObject[5];
    public InputField nickNameInput;
    enum e_Panel
    {
        커넥션  = 0, // DisconnectPanel
        상점    = 1, // ShopPanel
        인벤창  = 2, // InvenPanel
        로비    = 3, // LobbyPanel
        상태창  = 4, // PlayerStatursPanel
    }
    e_Panel staturs;


    public GameObject ConnectBtn;

    public GameObject NickNameLabel;


    [Header("LobbyPanel")]
    public TMP_InputField roomInput;
    public Text WelcomeText;
    public Text LobbyInfoText;
    public Button[] CellBtn;
    public Button NextBtn;
    public Button PreviousBtn;
  
    [Header("PlayerStaturs")]
    public GameObject Tmp_money;
    public GameObject Tmp_Attack;
    public GameObject Tmp_Defenc;
    public GameObject Tmp_Speed;

    public int m_money;
    public int m_atack;
    public int m_defenc;
    public float m_speed;

    [Header("Member variable")]
    public GameObject Btn_WallController;
    public int Filed_MonsterCount;
    public bool MoveMod;


    public TouchScreenKeyboard keyboard { get; private set; }
    #endregion
    public int Getm_money()
    {
        return this.m_money;
    }
    public int Setm_money(int addm_money)
    {
        this.m_money = addm_money;
        return this.m_money;
    }
  
    public int Add_Money(int value)
    {
        this.m_money += value;
        Tmp_money.GetComponent<TextMeshProUGUI>().text = m_money.ToString();
        return this.m_money;
    }

    public void Add_Attack(int value)
    {
        this.m_atack += value;
        // comma(string num)
        Tmp_Attack.GetComponent<TextMeshProUGUI>().text = m_atack.ToString();
        
    }

    public int Add_Defenc(int value)
    {
        this.m_defenc += value;
        // comma(string num)
        Tmp_Defenc.GetComponent<TextMeshProUGUI>().text = m_defenc.ToString();
        return this.m_defenc;
    }

    public float Add_Speed(float value)
    {
        this.m_speed += value;
        // comma(string num)
        Tmp_Speed.GetComponent<TextMeshProUGUI>().text = m_speed.ToString();
        return this.m_speed;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    
        MainCanversInitalize();
        GameManager.instance.Add_Attack(30);
        GameManager.instance.Add_Defenc(10);
        GameManager.instance.Add_Speed(10);

        if (Btn_WallController == null)
            return;
      
    }

    private void Update()
    {
      
    }

    private string comma(string num)
    {
        int len, point;
        string str;

        num = num + "";
        point = num.Length % 3;
        len = num.Length;

        str = num.Substring(0, point);
        while (point < len)
        {
            if (str != "") str += ",";
            str += num.Substring(point, point + 3);
            point += 3;
        }

        return str;

    }

   

    public void LoadShop()
    {
        SwichpanelsView(e_Panel.상점);
    }

    public void LoadInventory()
    {
        SwichpanelsView(e_Panel.인벤창);
    }

    public void LoadDisconect()
    {
        SwichpanelsView(e_Panel.커넥션);
    }

    public void TestFuntion()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, true, true, true, true);

            keyboard.active = true;
        }
    }
    public bool GameMod;

    #region 메인커넥션

    private void MainCanversInitalize()
    {


        if (GameObject.Find("MainCanvers") == null)
        {
            return;
        }

        GameObject mainCvs = GameObject.Find("MainCanvers");

        for (int i = 0; i < mainCvs.transform.childCount; i++)
        {
            string compo_name = mainCvs.transform.GetChild(i).name;

            switch (compo_name)
            {
                case "DisconnectPanel":
                    panels[(int)e_Panel.커넥션] = mainCvs.transform.GetChild(i).gameObject;
                    break;
                case "ShopPanel":
                    panels[(int)e_Panel.상점] = mainCvs.transform.GetChild(i).gameObject;
                    break;
                case "InvenPanel":
                    panels[(int)e_Panel.인벤창] = mainCvs.transform.GetChild(i).gameObject;
                    break;
                case "LobbyPanel":
                    panels[(int)e_Panel.로비] = mainCvs.transform.GetChild(i).gameObject;
                    break;
                case "PlayerStatursPanel":
                    panels[(int)e_Panel.상태창] = mainCvs.transform.GetChild(i).gameObject;
                    break;
            }

        }

        DisconnectPanelInitalize();

        LobbyPanelInitalize();

        PlayerStatursInitalize();

        SwichpanelsView(e_Panel.커넥션);
    }

    private void SwichpanelsView(e_Panel target_panels)
    {
        this.staturs = target_panels;
        switch (staturs)
        {
            case e_Panel.커넥션:
                {
                    panels[(int)e_Panel.커넥션].SetActive(true);
                    panels[(int)e_Panel.상점].SetActive(false);
                    panels[(int)e_Panel.인벤창].SetActive(false);
                    panels[(int)e_Panel.로비].SetActive(false);
                    break;
                }
            case e_Panel.상점:
                {
                    panels[(int)e_Panel.커넥션].SetActive(false);
                    panels[(int)e_Panel.상점].SetActive(true);
                    panels[(int)e_Panel.인벤창].SetActive(false);
                    panels[(int)e_Panel.로비].SetActive(false);

                    break;
                }
            case e_Panel.인벤창:
                {
                    panels[(int)e_Panel.커넥션].SetActive(false);
                    panels[(int)e_Panel.상점].SetActive(false);
                    panels[(int)e_Panel.인벤창].SetActive(true);
                    panels[(int)e_Panel.로비].SetActive(false);
                    break;
                }
            case e_Panel.로비:
                {
                    panels[(int)e_Panel.커넥션].SetActive(false);
                    panels[(int)e_Panel.상점].SetActive(false);
                    panels[(int)e_Panel.인벤창].SetActive(false);
                    panels[(int)e_Panel.로비].SetActive(true);
                    break;
                }

            default:
                {
                    Debug.LogWarning("SwichpanelsView에 지정되어있지않은 상태가 출력되었습니다");
                    break;
                }


        }
    }


    public void Connect()
    {
        LoginPanel.SetActive(false);

        ButtonPanel.SetActive(true);
        //nickNameInput.gameObject.SetActive(false);
        //ConnectBtn.SetActive(false);
        //NickNameLabel.SetActive(false);
    }
    #endregion

    #region 디스커넥션

    public void DisconnectPanelInitalize()
    {
        for (int i = 0; i < panels[(int)e_Panel.커넥션].transform.childCount; i++)
        {
            string compo_name = panels[(int)e_Panel.커넥션].transform.GetChild(i).name;

            switch (compo_name)
            {
                case "LoginPanel":
                    LoginPanel = panels[(int)e_Panel.커넥션].transform.GetChild(i).gameObject;
                    LoginPanel.SetActive(true);
                    break;

                case "ButtonPanel":
                    ButtonPanel = panels[(int)e_Panel.커넥션].transform.GetChild(i).gameObject;
                    ButtonPanel.SetActive(false);
                    break;
            }
        }



        for (int i = 0; i < LoginPanel.transform.childCount; i++)
        {
            string compo_name = LoginPanel.transform.GetChild(i).name;

            switch (compo_name)
            {
                case "NickNameInput":
                    nickNameInput = LoginPanel.transform.GetChild(i).GetComponent<InputField>();
       
                    break;

                case "ConnecBtn":
                    ConnectBtn = LoginPanel.transform.GetChild(i).gameObject;
                   
                    break;

                case "NickNameLabel":
                    NickNameLabel = LoginPanel.transform.GetChild(i).gameObject;
                  
                    break;
            }
        }



    }


    #endregion

    #region 상점

    #endregion 인벤토리

    #region 로비

    public void ShowRooms()
    {
        SwichpanelsView(e_Panel.로비);
    }



    private void LobbyPanelInitalize() //// MainCanversInitalize()에서 호출 사용 다른곳에서 사용x
    {
        GameObject RoomControllPanel = null;
        for (int i = 0; i < panels[(int)e_Panel.로비].transform.childCount; i++)
        {
            string compo_name = panels[(int)e_Panel.로비].transform.GetChild(i).name;

            switch (compo_name)
            {
                case "RoomControllPanel":
                    RoomControllPanel = panels[(int)e_Panel.로비].transform.GetChild(i).gameObject;
                    break;
                case "WelcomeText":
                    WelcomeText = panels[(int)e_Panel.로비].transform.GetChild(i).gameObject.GetComponent<Text>();
                    break;
                case "LobbyInfoText":
                    LobbyInfoText = panels[(int)e_Panel.로비].transform.GetChild(i).gameObject.GetComponent<Text>(); ;
                    break;
            }

        }

        RoomControllPanelSetUp(RoomControllPanel);
    }

    private void RoomControllPanelSetUp(GameObject targetObject)
    {
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            string compo_name = targetObject.transform.GetChild(i).name;

            switch (compo_name)
            {
                case "RoomInput":
                    roomInput = targetObject.transform.GetChild(i).gameObject.GetComponent<TMP_InputField>();
                    break;

            }

        }
    }

    #endregion

    #region 플레이어상태

    private void PlayerStatursInitalize() // MainCanversInitalize()에서 호출 사용 다른곳에서 사용 x
    {
        if (panels[(int)e_Panel.상태창] == null)
        {
            Debug.LogWarning("플레이어 상태창이 초기화가 이루어지지 못했다");
            return;
        }

     
        for (int i = 0; i < panels[(int)e_Panel.상태창].transform.childCount; i++)
        {

            string compo_name = panels[(int)e_Panel.상태창].transform.GetChild(i).name;

            if (compo_name == "Text_Gold")
            {
                Tmp_money = panels[(int)e_Panel.상태창].transform.GetChild(i).gameObject;
            }
            else if (compo_name == "Text_Attack")
            {
                Tmp_Attack = panels[(int)e_Panel.상태창].transform.GetChild(i).gameObject;
            }
            else if (compo_name == "Text_Def")
            {
                Tmp_Defenc = panels[(int)e_Panel.상태창].transform.GetChild(i).gameObject;
            }
            else if (compo_name == "Text_Speed")
            {
                Tmp_Speed = panels[(int)e_Panel.상태창].transform.GetChild(i).gameObject;
            }

        }
    }

    #endregion

    #region 방

   


 



    string str;

    public void CreateRoom()
    {
        StartCoroutine(LoadScene(1));
    }

  

    #endregion


    public Text ListText;
    int currentPage = 1, maxPage, multiple;
 
    #region 방리스트 갱신

    #endregion




    public IEnumerator LoadScene(int SceneNumber)
    {
        yield return null;

        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(SceneNumber);
        while (!asyncOper.isDone)
        {


            if (asyncOper.progress >= 0.1f)
            {
                GameManager.instance.GetComponent<AudioSource>().Stop();
            }
            if (asyncOper.progress >= 0.3f)
            {

            }
            if (asyncOper.progress >= 0.5f)
            {

            }
            if (asyncOper.progress >= 0.9f)
            {
                asyncOper.allowSceneActivation = true;

            }
            yield return null;
        }
    }


   


    /******************
     * 문제 
     * 1. 로비는 무작이의 로비를 선택한다
     * 2. 마스터클라이언트는 문제가 없다 하지만
     * 게스트 클라이언트에서는 마스터 클라이언트의 플레이어가 나오지않는다
     * 
     * 
     ******************/
}
