using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Game_First : WindowUi
{
    public List<Sprite> m_splAllSp = new List<Sprite>();
    [SerializeField]
    private List<Slot_GameFirst> m_sllMain = new List<Slot_GameFirst>();
    [SerializeField]
    private Button m_btMain;
    [SerializeField]
    private Text m_txCoin,m_txKey;
    private CancellationTokenSource m_srToken;
    public void Start()
    {
        m_srToken = new CancellationTokenSource();
        CullDown(m_srToken.Token).Forget();
        if (m_txCoin)
        {
            OnCoin(Player_Menager.m_inCoin);
            Player_Menager.m_evOnCoinChg += OnCoin;
            m_acOnDestroy += () => Player_Menager.m_evOnCoinChg -= OnCoin;
        }
        if (m_txKey)
        {
            OnKey(Player_Menager.m_inKey);
            Player_Menager.m_evOnKeyChg += OnKey;
            m_acOnDestroy += () => Player_Menager.m_evOnKeyChg -= OnKey;
        }
        m_acOnDestroy += () => {
            m_srToken.Cancel();
            };
    }

    public void OnKey(int i)=> m_txKey.text = i.ToString();

    public void OnCoin(int i) => m_txCoin.text = i.ToString();

    public void OnCreated()
    {
        var Played = PlayerPrefs.GetInt("PlayredInFirstGame",0);
    }

    public void SetTh()
    {
        var r = Random.Range(0,m_sllMain.Count);
        m_sllMain[r].SetTh();
        CullDown(m_srToken.Token).Forget();
    }
    public async UniTask CullDown(CancellationToken tk)
    {
        m_btMain.interactable = false;
        var im = m_btMain.GetComponent<Image>();
        im.fillAmount = 0;
        for (int i = 0; i < 10; i++)
        {
            await UniTask.Delay(1000, tk.IsCancellationRequested);
            if (tk.IsCancellationRequested)
            {
                m_srToken.Dispose();
                return;
            }
            im.fillAmount += 0.1f;

        }

        m_btMain.interactable = true;
    }

}
