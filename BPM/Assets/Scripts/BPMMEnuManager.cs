using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BPMMEnuManager : MonoBehaviour
{

    static BPMMEnuManager m_BPeerMInstance;
    public float m_Bpm;
    public float[] m_TimePatron;
    public static int m_BeatCountFull;
    float m_BeatInterval;
    float m_BeatTimer;

    public static int m_ContraBeatCountFull;
    float m_ContraBeatInterval;
    float m_ContraBeatTimer;


    void Awake()
    {
        if (m_BPeerMInstance != null && m_BPeerMInstance != this)
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
    }

    void BeatDetection()
    {
        m_BeatInterval = m_TimePatron[m_BeatCountFull % m_TimePatron.Length] / m_Bpm;
        m_BeatTimer += Time.deltaTime;

        if (m_BeatTimer >= m_BeatInterval)
        {
            m_BeatTimer -= m_BeatInterval;
            m_BeatCountFull++;
        }
    }

    public void PressStart(string scene)
    {
        Destroy(gameObject);
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}