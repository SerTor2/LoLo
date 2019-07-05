using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour, IRestartGameElement
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public Direction m_IniDirection;
    Direction m_restartDirection;
    public int m_NumOfMove;
    public int m_CurrentPosition;
    private Vector3 target;
    private GameController m_controller;
    private BPMmanager m_BPMManager;
    private Vector3 restartPos;
    private int restartNum;
    private int restartcurrent;
    int m_Count = 0;
    public int m_NumTime = 1;
    Vector3[] m_EulerRotation = new Vector3[] { new Vector3(0, -90, 0),
                                                new Vector3(0, 90, 0),
                                                new Vector3(0, 0, 0),
                                                new Vector3(0, 180, 0)};

    private void Start()
    {
        m_controller = Camera.main.GetComponent<GameController>();
        m_BPMManager = Camera.main.GetComponent<BPMmanager>();
        m_BPMManager.AddPlataformGameElement(this);
        m_controller.AddRestartGameElement(this);
        target = transform.position;
        restartNum = m_NumOfMove;
        restartcurrent = m_CurrentPosition;
        restartPos = transform.position;
        m_restartDirection = m_IniDirection;
        ChangeRotation();
    }

    private void Update()
    {
        if (transform.position != target)
            transform.position = Vector3.MoveTowards(transform.position, target, 10 * Time.deltaTime);
    }

    public void RestartGame()
    {
        m_IniDirection = m_restartDirection;
        transform.position = restartPos;
        target = transform.position;
        m_CurrentPosition = restartcurrent;
        m_NumOfMove = restartNum;
    }

    void ChangeRotation()
    {
        if (m_IniDirection == Direction.LEFT)
            transform.eulerAngles = m_EulerRotation[0];
        else if (m_IniDirection == Direction.RIGHT)
            transform.eulerAngles = m_EulerRotation[1];
        else if (m_IniDirection == Direction.UP)
            transform.eulerAngles = m_EulerRotation[2];
        else
            transform.eulerAngles = m_EulerRotation[3];
    }

    public void Move()
    {
        m_Count++;

        if (m_Count % m_NumTime == 0)
        {
            m_Count = 0;
            if (m_IniDirection == Direction.LEFT)
            {
                m_CurrentPosition++;
                target = transform.position + new Vector3(-1, 0, 0);
                if (m_CurrentPosition >= m_NumOfMove)
                {
                    m_IniDirection = Direction.RIGHT;
                    m_CurrentPosition = 1;
                    ChangeRotation();
                }
            }
            else if (m_IniDirection == Direction.RIGHT)
            {
                m_CurrentPosition++;
                target = transform.position + new Vector3(1, 0, 0);
                if (m_CurrentPosition >= m_NumOfMove)
                {
                    m_IniDirection = Direction.LEFT;
                    m_CurrentPosition = 1;
                    ChangeRotation();
                }
            }

            if (m_IniDirection == Direction.UP)
            {
                m_CurrentPosition++;
                target = transform.position + new Vector3(0, 0, 1);
                if (m_CurrentPosition >= m_NumOfMove)
                {
                    m_IniDirection = Direction.DOWN;
                    m_CurrentPosition = 1;
                    ChangeRotation();
                }
            }
            else if (m_IniDirection == Direction.DOWN)
            {
                m_CurrentPosition++;
                target = transform.position + new Vector3(0, 0, -1);
                if (m_CurrentPosition >= m_NumOfMove)
                {
                    m_IniDirection = Direction.UP;
                    m_CurrentPosition = 1;
                    ChangeRotation();
                }
            }
        }       
    }
}
