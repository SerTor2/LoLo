﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public int maxLevels = 2;
    public List<Text> starsListText = new List<Text>();
    private void Start()
    {        
        List<int> list = DataManager.dataManager.GetListStarsLevels();
        for (int i = 0; i < list.Count; i++)
        {
            if (starsListText.Count > i)
            {
                starsListText[i].text = list[i].ToString() + " / 3"; 
            }
            else break;
        }
    }

    public void GoToLevel(string _level)
    {
        if(DataManager.dataManager.GetLifes() > 0)
            SceneManager.LoadScene(_level);
    }
}
