using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Upgrade : WindowUi
{
    private Island m_plPlane;
    [SerializeField]
    private GameObject m_gmPannelOfAllSlots;
    public bool m_bLoad = false;
    [SerializeField]
    private Text m_txName,m_txCoin;

    void Start()
    {
        if(!m_bLoad)
            Rel(Player_Menager.m_airCurIsland);
        Player_Menager.m_evOnIslandChg += Rel;

        if (m_txCoin)
        {
            OnCoinCh(Player_Menager.m_inCoin);
            Player_Menager.m_evOnCoinChg += OnCoinCh;
            m_acOnDestroy += () => Player_Menager.m_evOnCoinChg -= OnCoinCh;
        }
        
    }
    void Rel(Island ple)=> SetP(ple);

    public void OnCoinCh(int coin)
    {
        m_txCoin.text = coin.ToString();
    }

    public void SetP(Island ple,bool tes = false)
    {
        m_plPlane = ple;
        var pl = m_plPlane.name.Split('.')[1];
        for (int i = 0; i < m_gmPannelOfAllSlots.transform.childCount; i++)
        {
            var t = i;
            var c = m_gmPannelOfAllSlots.transform.GetChild(t).GetComponent<UpgradeSlot>();
            if (m_plPlane.m_scUpgrades.Count > t)
            {
                var up = m_plPlane.m_scUpgrades[t];
                var answer = PlayerPrefs.GetInt($"{pl}.{up.m_strName}", 0);



                if (up != null)
                    c.SetLabel(up.m_strName, up.m_strDescript, up.m_inCost.ToString());

                if (answer == 1 || tes)
                {
                    c.m_btBuy.GetComponent<Button>().interactable = false;
                    continue;
                }
                else
                {
                    c.m_btBuy.GetComponent<Button>().interactable = true;
                }

                c.m_btBuy.GetComponent<Button>().onClick.RemoveAllListeners();

                c.m_btBuy.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (up.m_inCost > Player_Menager.m_inCoin) return;
                    c.m_btBuy.SetActive(false);
                    PlayerPrefs.SetInt($"{pl}.{up.m_strName}", 1);
                    Player_Menager.m_inCoin -= up.m_inCost;
                    Player_Menager.m_inAlreadyBought++;
                });
            }
        }

    }
    public void Setlabel(string n)
    {
        m_txName.text = n;
    }
    private void OnDestroy()
    {
        Player_Menager.m_evOnIslandChg -= Rel;
    }
}
