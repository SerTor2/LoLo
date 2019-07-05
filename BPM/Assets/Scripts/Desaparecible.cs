using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desaparecible : MonoBehaviour, IRestartGameElement
{

    public List<AnimationClip> animaciones;
    private Animation m_animation;
    public BoxCollider m_colldier;
    private float m_returnPos = 0;
    public float maxTimetoReturn = 5;
    private GameController m_controller;
    private BPMmanager m_BPMManager;

    // Use this for initialization
    void Start () {
        m_animation = GetComponent<Animation>();
        m_controller = Camera.main.GetComponent<GameController>();
        m_BPMManager = Camera.main.GetComponent<BPMmanager>();
        m_BPMManager.AddDesaparecibleElement(this);
        m_controller.AddRestartGameElement(this);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Pisado()
    {
        m_returnPos = maxTimetoReturn;
        m_animation.clip = animaciones[0];
        m_colldier.enabled = false;
        m_animation.Play();
    }

    public void Move()
    {
        if (m_returnPos > 0)
        {
            m_returnPos -= 1;
            if (m_returnPos <= 0)
            {
                m_animation.clip = animaciones[1];
                m_colldier.enabled = true;
                m_animation.Play();
            }
        }
    }

    public void RestartGame()
    {
        if (!m_colldier.enabled)
        {
            m_animation.clip = animaciones[1];
            m_colldier.enabled = true;
            m_animation.Play();
            m_returnPos = -1;
        }
    }

       
}
