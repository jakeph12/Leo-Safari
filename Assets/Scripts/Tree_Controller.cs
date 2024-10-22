using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Controller : MonoBehaviour
{
    public Action m_acBorder;
    public Action m_acPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BorderTree")
            m_acBorder?.Invoke();
        else if (collision.transform.tag == "Player")
            m_acPlayer?.Invoke();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
