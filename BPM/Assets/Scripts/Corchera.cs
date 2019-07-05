using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corchera : MonoBehaviour, IRestartGameElement
{

    public float speedRotation = 360;
    private GameController controller;
    private Quaternion rotation;
    public MeshRenderer hijo;
    public bool active = true;
    public Vector3 direction;
    // Use this for initialization
    void Start()
    {
        controller = Camera.main.GetComponent<GameController>();
        controller.AddRestartGameElement(this);
        controller.AddRestartGameObjects(gameObject);
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles += new Vector3(0, speedRotation * Time.deltaTime, 0); //1 vuelta en 1sec

    }

    public void RestartGame()
    {
        active = true;
        hijo.enabled = true;
        transform.rotation = rotation;
    }
}
