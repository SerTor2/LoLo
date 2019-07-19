using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLifesManager : MonoBehaviour
{
    private int lifes;
    public Text textLifes;

    private void Update()
    {
        lifes = DataManager.dataManager.GetLifes();
        textLifes.text = lifes.ToString();
    }
}
