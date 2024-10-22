using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Menager
{
    const string PlayerPropertyCoin = "Player_coin";
    const string PlayerPropertyKey = "Player_Key";
    const string PlayerPropertyIsland = "Player_curent_island";
    const string PlayerPropertyPlaneBought = "Player_bought_island";
    const string PlayerPropertyPlaneUpgradeBought = "Player_bought_upgrade_island";

    private static int _m_inCoin;
    public static int m_inCoin
    {
        get
        {
            Init();
            return _m_inCoin;
        }
        set
        {
            Init();
            var t = value - m_inCoin;
            _m_inCoin = value;

            if(t != 0)
                PlayerPrefs.SetInt(PlayerPropertyCoin, _m_inCoin);

            m_evOnCoinChg?.Invoke(_m_inCoin);
        }
    }


    private static int _m_inKey;
    public static int m_inKey
    {
        get
        {
            Init();
            return _m_inKey;
        }
        set
        {
            Init();
            var t = value - m_inKey;
            _m_inKey = value;

            if (t != 0)
                PlayerPrefs.SetInt(PlayerPropertyKey, _m_inKey);

            m_evOnKeyChg?.Invoke(_m_inKey);
        }
    }


    private static int _m_inAlreadyBought;
    public static int m_inAlreadyBought
    {
        get 
        {
            Init();
            return _m_inAlreadyBought;
        } 
        set
        {
            Init();
            _m_inAlreadyBought = value;
            m_evOnAlreadyBoughtChg?.Invoke(_m_inAlreadyBought);
            PlayerPrefs.SetInt(PlayerPropertyPlaneUpgradeBought + m_inIdOfIsland, _m_inAlreadyBought);


        }
    }


    private static int _m_inIdOfISland =-1;
    private static Island _m_airCurIsland;
    public static Island m_airCurIsland
    {
        get{
            Init();
            return _m_airCurIsland;
        }
        private set
        {
            Init();
            _m_airCurIsland = value;
            m_evOnIslandChg?.Invoke(_m_airCurIsland);
        }
    }
    public static int m_inIdOfIsland
    {
        get
        {
          Init();
          return  _m_inIdOfISland;
        }
        set
        {
            Init();
            if (value >= All_Island.m_lsAllIrIsland.Count) return;

            var t = value - _m_inIdOfISland;
            _m_inIdOfISland = value;
            if(t != 0)
            {
                m_inAlreadyBought = PlayerPrefs.GetInt(PlayerPropertyPlaneUpgradeBought + m_inIdOfIsland, 0);
                m_airCurIsland = All_Island.m_lsAllIrIsland[_m_inIdOfISland];
                PlayerPrefs.SetInt(PlayerPropertyIsland, _m_inIdOfISland);
            }


        }
    }
    private static int _m_inCurLvl = -1;
    public static int m_inCurLvl
    {
        get
        {
            Init();
            return _m_inCurLvl;
        }
        set
        {
            Init();
            var t = value - _m_inCurLvl;
            _m_inCurLvl = value;
            if (_m_inCurLvl > All_Island.m_lsAllIrIsland.Count - 1) _m_inCurLvl = All_Island.m_lsAllIrIsland.Count - 1;
            PlayerPrefs.SetInt(PlayerPropertyPlaneBought, _m_inCurLvl);

        }
    }


    public static event OnCoinCh m_evOnAlreadyBoughtChg;

    public static event OnCoinCh m_evOnKeyChg;

    public delegate void OnCoinCh(int coin);
    public static event OnCoinCh m_evOnCoinChg;

    public delegate void OnIslandCh(Island plane);
    public static event OnIslandCh m_evOnIslandChg;


    private static bool m_bInit = false;

    private static void Init()
    {
        if (m_bInit) return;
        m_bInit = true;
        m_inCoin = PlayerPrefs.GetInt(PlayerPropertyCoin, 0);
        m_inIdOfIsland = PlayerPrefs.GetInt(PlayerPropertyIsland, 0);
        m_inCurLvl = PlayerPrefs.GetInt(PlayerPropertyPlaneBought, 0);
        m_inAlreadyBought = PlayerPrefs.GetInt(PlayerPropertyPlaneUpgradeBought + m_inIdOfIsland, 0);
        m_inKey = PlayerPrefs.GetInt(PlayerPropertyKey, 0);


    }


}
