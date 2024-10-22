using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class Main_Tutor : WindowUi
{
    [SerializeField]
    private Text m_txName, m_txDescr, m_txBt;
    [SerializeField]
    private Image m_mgMain;
    [SerializeField]
    private Sprite[] m_spMain;
    [SerializeField]
    private GameObject m_gmStartPos;


    private string[] m_strDescr =
    {
        "Winning mini-games gives you a chance to earn keys. Collect keys to unlock new, exciting reserves and explore exotic wildlife.",
        "Use your coins to buy amazing upgrades—new animals, decorations, and natural wonders to make your reserve thrive.",
        "With enough keys, you can unlock new, beautiful reserves! From lush jungles to icy mountains, each one has its own unique charm and creatures.",
        "Enhance and fully upgrade each reserve to reach mastery! The more you upgrade, the closer you get to discovering rare and mythical creatures.",
    };

    private string[] m_strName =
    {
        "Play Mini-Games, Earn Keys",
        "Upgrade Your Sanctuary",
        "Unlock New Reserves",
        "Master Your Reserves",
    };

    void Start()
    {
        
    }
    private bool m_bPlay;
    private int m_inIndex = 0;
    public void Next()
    {
        if (m_bPlay) return;
        if (m_inIndex == m_strDescr.Length - 1)
        {
            DellObj();
            return;
        }

        if(m_inIndex == m_strDescr.Length - 2)
        {
            m_txBt.text = "Start Flying";
        }
        else
        {
            m_txBt.text = "Continue";
        }

        var New = Instantiate(m_mgMain, m_mgMain.transform.parent.transform);

        New.GetComponent<Image>().sprite = m_spMain[m_inIndex];

        New.transform.localPosition = m_gmStartPos.transform.localPosition;

        New.transform.DOLocalMove(m_mgMain.transform.localPosition,1).OnComplete(() => 
        { 
            Destroy(m_mgMain);
            m_mgMain = New;
        });
        m_mgMain.transform.DOLocalMove(New.transform.localPosition,1).OnComplete(() => m_bPlay = false);
        Generate(m_strDescr[m_inIndex], m_txDescr, 0.5f).Forget();
        Generate(m_strName[m_inIndex], m_txName, 0.5f).Forget();

        m_inIndex++;
        m_bPlay = true;
    }


    public static async UniTask Generate(string st,Text tx,float second)
    {
        var c = st.ToCharArray();
        int t = (int)((second * 1000) / c.Length);
        tx.text = "";
        foreach (char cr in c)
        {
            tx.text += cr;
            await UniTask.Delay(t);
        }

    }
}
