using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_answer : WindowUi
{

    [SerializeField]
    private Text m_txCoin,m_txKey;
    [SerializeField]
    private Button m_btHome,m_btRestart;


    public void Inits(int coin,int key,Action OnHome,Action OnRestart)
    {
        m_txCoin.text = coin.ToString();
        m_txKey.text = key.ToString();
        m_btHome.onClick.AddListener(() => 
        {
            OnHome?.Invoke();

        });
        m_btRestart.onClick.AddListener(() => 
        {
            OnRestart?.Invoke();
        });
    }

}
