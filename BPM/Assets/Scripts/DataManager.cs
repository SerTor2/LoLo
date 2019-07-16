using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /*  #region Singleton Pattern
      private static DataManager instance;
      private DataManager() { }
      public static DataManager GetInstance()
      {
          if (instance == null)
          {
              instance = new DataManager();
          }
          return instance;
      }
      #endregion*/


    private int lifes = 0;
    private bool started = false;

    private List<int> starsLevels = new List<int>();
    public int maxLevels = 2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameObject other = GameObject.FindGameObjectWithTag("DataManager");
        started = true;
        if (other != gameObject)
        {
            lifes = other.GetComponent<DataManager>().GetLifes();
            starsLevels = other.GetComponent<DataManager>().GetListStarsLevels();
            started = true;
            DestroyImmediate(other);
        }
        else
        {
            lifes = 10;
            for (int i = 0; i < maxLevels; i++)
                starsLevels.Add(0);
        }
    }


    public void ModifyLifes(int _lifes)
    {
        lifes = Mathf.Clamp(lifes + _lifes, 0, 3);
    }

    public bool GetStarted()
    {
        return started;
    }

    public int GetLifes()
    {
        return lifes;
    }

    public List<int> GetListStarsLevels()
    {
        return starsLevels;
    }

    public void ChangeStars(int _level, int _stars)
    {
        if (starsLevels.Count >= _level)
        {
            int stars = Mathf.Max(starsLevels[_level - 1], _stars);
            starsLevels[_level - 1] = Mathf.Clamp(stars, 0, 3);
        }
    }
}
