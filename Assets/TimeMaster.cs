using UnityEngine;
using System.Collections;
using System;

public class TimeMaster : MonoBehaviour
{

    DateTime currentDate;
    DateTime oldDate;

    public string saveLocation;
    public static TimeMaster instance;

    // Use this for initialization
    void Awake()
    {

        instance = this;

        saveLocation = "LastSavedDate1";
    }

    public float CheckDate()
    {
        currentDate = System.DateTime.Now;
        string tempString = PlayerPrefs.GetString(saveLocation, "1");

        long tempLong = Convert.ToInt64(tempString);

        DateTime oldDate = DateTime.FromBinary(tempLong);
        print("oldDate : " + oldDate);

        TimeSpan difference = currentDate.Subtract(oldDate);
        print("difference :" + difference.Hours);
        int diffhour = PlayerPrefs.GetInt("hours",8) - (difference.Hours);
        int diffmint = PlayerPrefs.GetInt("minutes", 0) - difference.Minutes;
        int diffsec  =  PlayerPrefs.GetInt("seconds", 0) - difference.Hours;
        GameManager.instance.hours = diffhour;
        GameManager.instance.minutes = diffmint;
        GameManager.instance.seconds = diffsec;
        return (float)difference.TotalSeconds;

    }

    public void SaveDate()
    {
        PlayerPrefs.SetString(saveLocation, System.DateTime.Now.ToBinary().ToString());
        print("saving this date to player prefs" + System.DateTime.Now);
    }
    // Update is called once per frame
    void Update()
    {

    }
}