using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Island", menuName = "Game/New Object/Island")]
public class Island : ScriptableObject, IslandInfo
{
    [SerializeField]
    private Sprite m_spMainIco;
    public int m_inKeyNeed;

    [SerializeField]
    private string m_strDescript;
    public List<IslandUpgrade> m_scUpgrades = new List<IslandUpgrade>();




    public Sprite m_spIslandIco { get => m_spMainIco; set => m_spMainIco = value; }
    public string m_strDescr { get=> m_strDescript; set => m_strDescript = value; }
}
public interface IslandInfo
{
    public Sprite m_spIslandIco { get; set; }
    public string m_strDescr { get; set; }
}

[System.Serializable]
public class IslandUpgrade
{
    public string m_strName, m_strDescript;
    public int m_inCost;
}