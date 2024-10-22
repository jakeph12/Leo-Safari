using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_GameFirst : MonoBehaviour,IDragHandler,IDropHandler,IBeginDragHandler,IEndDragHandler
{
    public bool _m_bEmpty = true;
    public int m_inId;
    public bool m_bEmpty
    {
        get => _m_bEmpty;
        set
        {
            _m_bEmpty = value;
            if (!_m_bEmpty) return;

            m_inId = 0;
            m_imCur.sprite = null;
            m_imCur.enabled = false;
            PlayerPrefs.SetInt("Slot" + m_inIndex, 0);
        }
    }
    public bool m_bSellSlot;


    private Image m_imCur;
    private Vector2 m_inPos;
    private CanvasGroup m_grCanvas;
    private int m_inIndex;
    [SerializeField]private Game_First m_scrController;


    public void Awake()
    {
        if (m_bSellSlot) return;
        m_imCur = transform.GetChild(0).GetComponent<Image>();
        m_inIndex = transform.GetSiblingIndex();
        m_grCanvas = GetComponent<CanvasGroup>();
        m_scrController = GetComponentInParent<Game_First>();

        var cur = PlayerPrefs.GetInt("Slot" + m_inIndex, 0);

        if(cur != 0)
            SetTh(cur);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_bEmpty) return;
            transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (m_bSellSlot)
        {
            var cdd = eventData.pointerDrag.gameObject.GetComponent<Slot_GameFirst>();
            Player_Menager.m_inCoin += cdd.m_inId*10;
            var r = Random.Range(0, 101);
            if (r > 90) Player_Menager.m_inKey++;
            cdd.m_bEmpty = true;
            return;
        }
        if (m_bEmpty) return;
        var c = eventData.pointerDrag.gameObject.GetComponent<Slot_GameFirst>();
        if (c.m_inId >= m_scrController.m_splAllSp.Count || c.m_inId != m_inId) return;
        m_inId++;
        m_imCur.sprite = m_scrController.m_splAllSp[m_inId -1];
        c.m_bEmpty = true;
        PlayerPrefs.SetInt("Slot" + m_inIndex, m_inId);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        transform.SetAsLastSibling();
        m_inPos = transform.position;
        m_grCanvas.interactable = false;
        m_grCanvas.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        transform.position = m_inPos;
        m_grCanvas.interactable = true;
        m_grCanvas.blocksRaycasts = true;
        transform.SetSiblingIndex(m_inIndex);

    }
    public void SetTh(int i = 1)
    {
        m_imCur.enabled = true;
        m_inId = i;
        m_bEmpty = false;
        m_imCur.sprite = m_scrController.m_splAllSp[m_inId-1];
        PlayerPrefs.SetInt("Slot" + m_inIndex, m_inId);
    }
}
