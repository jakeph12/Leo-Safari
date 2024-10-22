using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Player_Menager;

public class Main_Menu_Checker : WindowUi
{
    [SerializeField]
    private WindowUi m_wiTutor,m_wiDaly,m_wiSettings, m_wiSecondGame,m_wiFirst_Game;
    [SerializeField]
    private Text m_txKey, m_txCoin;
    [SerializeField]
    private GameObject m_gmDot;
    [SerializeField]
    private Slider m_slMain;
    [SerializeField]
    private Image m_imMain;

    void Start()
    {
        var P = PlayerPrefs.GetInt("TutorP", 0);
        if (P == 0)
        {

            var g = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiTutor,TypeWindow.NoClosed);
            PlayerPrefs.SetInt("TutorP", 1);
            Bottom_Menu_controller.m_sinThis.Hide(false);
            g.GetComponent<WindowUi>().m_acOnDestroy += () => Bottom_Menu_controller.m_sinThis.Hide(true);

        }

        Bottom_Menu_controller.m_evChangePage += OnPageCh;
        m_acOnDestroy += () =>
        {
            Bottom_Menu_controller.m_evChangePage -= OnPageCh;
        };
        if (m_txKey)
        {

            OnkeyCh(m_inKey);
            m_evOnKeyChg += OnkeyCh;
            m_acOnDestroy += () => m_evOnKeyChg -= OnkeyCh;


        }
        if (m_txCoin)
        {
            OnCoinCh(m_inCoin);
            m_evOnCoinChg += OnCoinCh;
            m_acOnDestroy += () => m_evOnCoinChg -= OnCoinCh;
        }
        if (m_slMain)
        {
            onUpgradeCh(m_inAlreadyBought);
            m_evOnAlreadyBoughtChg += onUpgradeCh;
            m_acOnDestroy += () => m_evOnAlreadyBoughtChg -= onUpgradeCh;
        }
        if (m_imMain)
        {
            OnIslandCh(m_airCurIsland);
            m_evOnIslandChg += OnIslandCh;
            m_acOnDestroy += () => m_evOnIslandChg -= OnIslandCh;
        }
    }

    public void OnIslandCh(Island si)
    {
        m_imMain.sprite = si.m_spIslandIco;
    }


    public void onUpgradeCh(int index)
    {
        m_slMain.value = index;
    }

    public void OnCoinCh(int coin)=> m_txCoin.text = coin.ToString();
    public void OnkeyCh(int key)
    {
        m_txKey.text = key.ToString();
        /*
        if(key > 0)
            m_gmDot.SetActive(true);
        else 
            m_gmDot.SetActive(false);*/
    }

    public void OpenUp(WindowUi ws)
    {
        var n = Main_Menu_Controller.m_sinThis.OpenWindow(ws, TypeWindow.NoClosed);
        m_gmBlock.SetActive(true);

        Bottom_Menu_controller.m_sinThis.Hide(false);
        n.GetComponent<WindowUi>().m_acOnSDestroy += () =>
        {
            Bottom_Menu_controller.m_sinThis.Hide(true);
            m_gmBlock.SetActive(false);

        };
    }
    public void OnPageCh(int ch)
    {

        Main_Menu_Controller.m_sinThis.CloseAll(TypeWindow.Additional);
        Main_Menu_Controller.m_sinThis.CloseAll(TypeWindow.NoClosed);
        switch (ch)
        {
            case 0:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiFirst_Game, TypeWindow.Additional);
                break;
            case 1:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiSecondGame, TypeWindow.Additional);

                break;
            case 2:

                break;

            case 3:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiDaly, TypeWindow.Additional);
                break;
            case 4:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiSettings, TypeWindow.Additional);
                break;
        }
    }
}
