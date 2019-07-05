using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public int m_StopBPM;
    int m_IniStopBPM;

    private void Start()
    {
        m_IniStopBPM = m_StopBPM;
    }

    public bool CanMove()
    {
        if (m_StopBPM > 0)
        {
            m_StopBPM--;
            return false;
        }
        else
        {
            m_StopBPM = m_IniStopBPM;
            return true;
        }
    }
}
