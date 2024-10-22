using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUi : MonoBehaviour
{
    public Action m_acOnDestroy;
    public Action m_acOnSDestroy;

    [SerializeField] 
    protected GameObject m_gmBlock;
    [HideInInspector]
    public Vector2 m_vcStartPos;


    public virtual void DellObj(bool skipAnim = false)
    {
        float times = skipAnim ? 0 : 1;
        m_acOnSDestroy?.Invoke();
        transform.DOLocalMove(m_vcStartPos, times).OnComplete(() =>
        {
            m_acOnDestroy?.Invoke();
            Destroy(gameObject);

        });

    }
    public virtual void Init()
    {
        m_gmBlock.SetActive(false);
    }
}
