using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestartGameElement
{
    void RestartGame();
}

public class GameController : MonoBehaviour
{
    List<IRestartGameElement> m_RestartGameElements = new List<IRestartGameElement>();
    List<GameObject> m_RestartGameObjects = new List<GameObject>();
    public Player player;
    private Vector3 target;
    private Vector3 DistanceReference;
    private Vector3 respawnPos;
    public float speed = 3;
    public BPMmanager bpmManager;
    public float bpm;
    public int level;
    public float timeMin = 30;
    public float VarianteSeg = 4;

    private DataManager dataManager;

    // Use this for initialization
    void Start()
    {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        DistanceReference = player.transform.position - transform.position;
        target = transform.position;
        respawnPos = transform.position;
        bpm = bpmManager.m_Bpm;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position - DistanceReference;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), speed * Time.deltaTime);

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void Restart()
    {
        transform.position = respawnPos;
        target = transform.position;

        foreach (GameObject l_restart in m_RestartGameObjects)
        {
            l_restart.SetActive(true);
        }

        foreach (IRestartGameElement l_restart in m_RestartGameElements)
        {
            l_restart.RestartGame();
        }
    }

    public void ShowStars(float _time)
    {
        int stars = 0;
        if (_time <= timeMin + VarianteSeg)
            stars = 3;
        else if (_time <= timeMin + VarianteSeg * 1.5f)
            stars = 2;
        else stars = 1;

        dataManager.ChangeStars(level, stars);

    }

    public void AddRestartPos()
    {
        respawnPos = player.transform.position - DistanceReference;
    }

    public void AddRestartGameElement(IRestartGameElement RestartGameElement)
    {
        m_RestartGameElements.Add(RestartGameElement);
    }

    public void AddRestartGameObjects(GameObject RestartGameObject)
    {
        m_RestartGameObjects.Add(RestartGameObject);
    }
}
