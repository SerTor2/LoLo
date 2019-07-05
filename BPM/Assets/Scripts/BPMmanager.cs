using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMmanager : MonoBehaviour
{
    public Player m_Player;
    public MobileControlManager mobileManager;

    [HideInInspector]
    public List<pincho> m_pinchos;
    [HideInInspector]
    public List<Plataform> m_Plataforms;
    [HideInInspector]
    public List<Desaparecible> m_Desaparecibles;

    static BPMmanager m_BPeerMInstance;
    public float m_Bpm;
    public float [] m_TimePatron;
    public static int m_BeatCountFull;
    float m_BeatInterval;
    float m_BeatTimer;

    public static int m_ContraBeatCountFull;
    float m_ContraBeatInterval;
    float m_ContraBeatTimer;

    bool m_PauseMove = false;

    void Awake ()
    {
	    if(m_BPeerMInstance != null && m_BPeerMInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_BPeerMInstance = this;
        }
	}

    private void Update()
    {
        BeatDetection();

        if (Input.GetKeyDown(KeyCode.P))
            m_PauseMove = !m_PauseMove;
    }

    void BeatDetection()
    {
        m_BeatInterval = m_TimePatron[m_BeatCountFull % m_TimePatron.Length] / m_Bpm;
        m_BeatTimer += Time.deltaTime;

        if(m_BeatTimer >= m_BeatInterval)
        {
            m_BeatTimer -= m_BeatInterval;
            m_BeatCountFull++;
            if(!m_PauseMove)
                MoveObjects();
        }
    }

    void MoveObjects()
    {
        foreach(Plataform l_Plataform in m_Plataforms)
        {
            l_Plataform.Move();
        }
        foreach(pincho l_pincho in m_pinchos)
        {
            l_pincho.Move();
        }
        foreach (Desaparecible l_desaparecible in m_Desaparecibles)
        {
            l_desaparecible.Move();
        }
        m_Player.Move();
    }

    public void AddPlataformGameElement(Plataform PlataformElement)
    {
        m_Plataforms.Add(PlataformElement);
    }

    public void AddPinchoGameElement(pincho PinchoElement)
    {
        m_pinchos.Add(PinchoElement);
    }

    public void AddDesaparecibleElement(Desaparecible DesaparecibleElement)
    {
        m_Desaparecibles.Add(DesaparecibleElement);
    }
}
