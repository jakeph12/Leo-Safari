using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class All_Island
{
    private static List<Island> _m_lsAllIrISland = new List<Island>();
    public static List<Island> m_lsAllIrIsland
    {
        get{
            Init();
            return _m_lsAllIrISland;
        }
        private set
        {
            Init();
            m_lsAllIrIsland = value;
        }
    }
    private static bool m_bInit = false;

    private static void Init()
    {
        if (m_bInit) return;
        m_bInit = true;
        m_lsAllIrIsland.AddRange(
            Resources.LoadAll("Island/", typeof(Island))
            .Cast<Island>()
            .OrderBy(Island => Island.m_inKeyNeed)

            );
    }


}
