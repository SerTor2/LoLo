using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pincho : MonoBehaviour, IRestartGameElement
{
    public List<GameObject> pinchos;
    private int numPincho = 0;
    private int numTime = 0;
    public int MaxNumTime = 1;
    public int pinchosAtTime = 1;
    private GameController m_controller;
    private BPMmanager m_BPMManager;
    public List<AnimationClip> animationsClip;
	// Use this for initialization
	void Start () {
        m_controller = Camera.main.GetComponent<GameController>();
        m_BPMManager = Camera.main.GetComponent<BPMmanager>();
        m_BPMManager.AddPinchoGameElement(this);
        m_controller.AddRestartGameElement(this);
	}
	
	// Update is called once per frame
	void Update () {      

        if (numPincho >= pinchos.Count)
            numPincho = 0;
	}

    public void Move()
    {
        numTime++;
        if (numTime >= MaxNumTime)
        {
            numTime = 0;

            for (int i = 0; i < pinchosAtTime; i++)
            {
                if (numPincho + i >= pinchos.Count)
                {
                    if (m_controller.bpm <= 90)
                    {
                        pinchos[numPincho + i - pinchos.Count].GetComponent<Animation>().clip = animationsClip[0];
                        pinchos[numPincho + i - pinchos.Count].GetComponent<Animation>().Play();
                    }
                    else
                    {
                        pinchos[numPincho + i - pinchos.Count].GetComponent<Animation>().clip = animationsClip[1];
                        pinchos[numPincho + i - pinchos.Count].GetComponent<Animation>().Play();
                    }
                }
                else
                {
                    if (m_controller.bpm <= 90)
                    {
                        pinchos[numPincho + i].GetComponent<Animation>().clip = animationsClip[0];
                        pinchos[numPincho + i].GetComponent<Animation>().Play();
                    }
                    else
                    {
                        pinchos[numPincho + i].GetComponent<Animation>().clip = animationsClip[1];
                        pinchos[numPincho + i].GetComponent<Animation>().Play();
                    }
                }
            }


            numPincho++;
        }
    }

    public void RestartGame()
    {
        numPincho = 0;
        numTime = 0;
    }
}
