using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

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
    private List<int> starsLevels = new List<int>();
    public int maxLevels = 2;
    public int maxLifes = 3;
    public float timeToRecoverLife = 10;
    private float currentTimeRecoverLife = 0;
    private string nameRute;
    private DateTime timeToEnter;

    private void Awake()
    {
        if (dataManager == null)
        {
            nameRute = Application.persistentDataPath + "/datos.dat";
            dataManager = this;
            timeToEnter = DateTime.Now;
            DontDestroyOnLoad(gameObject);
            if (File.Exists(nameRute))
                dataManager.LoadDates();
            else
            {
                lifes = 3;
                for (int i = 0; i < maxLevels; i++)
                    starsLevels.Add(0);
            }
        }
        else if (dataManager != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        RecoverLifes();
    }

    private void RecoverLifes()
    {
        if (lifes < maxLifes)
        {
            currentTimeRecoverLife += Time.deltaTime;
            if(currentTimeRecoverLife >= timeToRecoverLife)
            {
                currentTimeRecoverLife = 0;
                dataManager.ModifyLifes(1);
            }
        }

    }


    public void ModifyLifes(int _lifes)
    {
        lifes += _lifes;
        if (lifes < 0)
            lifes = 0;
        if (lifes > maxLifes)
            lifes = maxLifes;
        if (lifes == maxLifes)
            currentTimeRecoverLife = 0;
        dataManager.SaveDates();
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
        dataManager.SaveDates();

    }

    public void SaveDates()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(nameRute);

        DatesToSave datos = new DatesToSave();
        datos.ChangeDates(lifes, starsLevels);

        bf.Serialize(file, datos);

        file.Close();
    }

    public void LoadDates()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(nameRute, FileMode.Open);

        DatesToSave datos = (DatesToSave)bf.Deserialize(file);
        lifes = datos.lifes;
        starsLevels = datos.starsLevels;

        float days = datos.timeToSave.Day;
        float hours = datos.timeToSave.Hour;
        float minutes = datos.timeToSave.Minute;
        float seconds = datos.timeToSave.Second;

        file.Close();

        days = timeToEnter.Day - days;
        hours = timeToEnter.Hour - hours;
        minutes = timeToEnter.Minute - minutes;
        seconds = timeToEnter.Second - seconds;

        if (days > 1)
        {
            dataManager.ModifyLifes(maxLifes);
        }
        else
        {
            float timeTotal = (days * 3600 *24) + (hours * 3600) + (minutes* 60) + seconds + currentTimeRecoverLife;
            int lifesToUP = Mathf.RoundToInt(timeTotal / timeToRecoverLife);
            currentTimeRecoverLife = timeTotal % timeToRecoverLife;
            dataManager.ModifyLifes(lifesToUP);
        }

    }
}

[Serializable] class DatesToSave
{
    public int lifes;
    public List<int> starsLevels;
    public DateTime timeToSave;

    public void ChangeDates(int _lifes, List<int> _stars)
    {
        lifes = _lifes;
        starsLevels = _stars;
        timeToSave = DateTime.Now;
    }
}
