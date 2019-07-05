using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLifesManager : MonoBehaviour
{
    private int lifes;
    public Text textLifes;
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        if (dataManager.GetStarted())
            lifes = dataManager.GetLifes();
        else
            lifes = 3;
        textLifes.text = lifes.ToString(); //cambiarlo y hacer que cambie solo cuando se modifican las vidas    }

    }
}
