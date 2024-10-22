using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Seconds : WindowUi
{
    [SerializeField]
    private GameObject m_gmPlayerprefab,m_gmCoinPrefab,m_gmKeyPrefab,m_gmScenePrefab;
    [SerializeField]
    private float m_flSpeed,m_flTreeSpeed;
    [SerializeField]
    private List<GameObject> m_gmlAllBorder = new List<GameObject>();
    private Transform m_trStartPos, m_trEndPos, m_trMainPanel;
    private GameObject m_gmPlayer;
    [SerializeField]
    private WindowUi m_wiAnswer;
    [SerializeField]
    private Text m_txCoin,m_txKey;

    [SerializeField]
    private GameObject m_gmPanelOfStart;
    

    private int _m_inKey,_m_inCoin;
    private int m_inKey
    {
        get => _m_inKey;
        set
        {
            _m_inKey = value;
            if (m_txKey) m_txKey.text = _m_inKey.ToString();
        }
    }
    private int m_inCoin
    {
        get => _m_inCoin;
        set
        {
            _m_inCoin = value;
            if(m_txCoin) m_txCoin.text = _m_inCoin.ToString();
        }
    }

    public void Startss()
    {
        m_trMainPanel = Instantiate(m_gmScenePrefab).transform;
        m_trStartPos = m_trMainPanel.GetChild(0).transform;
        StartGame();
        Bottom_Menu_controller.m_sinThis.Hide(false);
        Main_Menu_Controller.m_sinThis.CloseAll(TypeWindow.Main);

    }

    void StartGame()
    {
        m_inCoin = 0;
        m_inKey = 0;
        for (int i = 0; i < 4; i++)
            SpawnRandomTree(i);

        var p = Instantiate(m_gmPlayerprefab, m_trMainPanel);
        p.transform.localPosition = Vector2.zero;
        p.GetComponent<Player_Script>().m_acOnDeath += OnPlayerDeadth;
        m_gmPlayer = p;
    }
    void SpawnRandomTree(int i)
    {
        var t = Random.Range(0, m_gmlAllBorder.Count);
        var N = Instantiate(m_gmlAllBorder[t], m_trStartPos);

        N.transform.localPosition = new Vector2(3 * i, 0);

        var ke = Random.Range(0, 101);

        GameObject Cur;
        System.Action cra = null;

        if(ke >= 90)
        {
            Cur = m_gmKeyPrefab;
            cra = () =>
            {
                m_inKey++;
            };

        }
        else if(ke >= 30)
        {
            Cur = m_gmCoinPrefab;
            cra = () =>
            {
                m_inCoin += 10;
            };
        }
        else
        {
            Cur = null;
        }

        if (Cur)
        {
            var ck =  Instantiate(Cur, N.transform.GetChild(1).transform);
            ck.transform.localPosition = Vector2.zero;

            cra += () => Destroy(ck);
            ck.GetComponent<Tree_Controller>().m_acPlayer += cra;
        }

                N.GetComponent<Rigidbody2D>().velocity = new Vector2(-m_flTreeSpeed, 0);
        N.GetComponent<Tree_Controller>().m_acBorder += () => {

            Destroy(N);
            SpawnRandomTree(0);

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReStart()
    {

    }

    public void OnPlayerDeadth()
    {
        m_gmBlock.SetActive(true);
        Destroy(m_gmPlayer);
        for(int i = 0;i < m_trStartPos.childCount;i++)
        {
            var t = i;
            Destroy(m_trStartPos.GetChild(t).gameObject);
        }
        var cur = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiAnswer, TypeWindow.NoClosed).GetComponent<Game_answer>();
        cur.Inits(m_inCoin,m_inKey,
        () =>
        {
            Destroy(m_trMainPanel.gameObject);
            DellObj();
            Player_Menager.m_inCoin += m_inCoin;
            Player_Menager.m_inKey += m_inKey;
            Bottom_Menu_controller.m_sinThis.Hide(true);
            Bottom_Menu_controller.m_sinThis.SetTo(2);
            Main_Menu_Controller.OpenMainMenu();
            cur.DellObj();
        },
        () =>
        {
            StartGame();
            cur.DellObj();
            m_gmBlock.SetActive(false);
        });
    }


    public void Flip()
    {
        if( m_gmPlayer)
            m_gmPlayer.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (m_flSpeed * 100),ForceMode2D.Force);
    }
    public override void Init()
    {
        base.Init();
    }
}
