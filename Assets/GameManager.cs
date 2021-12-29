using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text TurnTxt;
    public int Counter;
   
    public Text Timertxt;
    public float TotalTime = 90;
    public bool _isGameOVer;
    public bool _isGamestart = false;
    public Animator characterAni;
    public GameObject Startpanel;
    public OrbitingCamera OC;

    public Slider staminabar;

    public GameObject GameoverPanel;
    public GameObject tutorial;
    public GameObject InGamepanel,pausePanel;
    public Text Gameovertxt;
    public Text DescriptionTxt;
    public bool _iswinner = false;


    public Text cointxt;
    public int coins;

    

   
    public bool _ismusic = true;
    public Image MusicImage;
    public GameObject fxwater;
    public Transform fxwaterpos;


    public Audiomanager Am;


    public bool isSpirent = false;


   
    public GameObject rewardtxt;

    public bool _isHacked = false;

    public float waitingTime = 2;
    public GameObject LoadingScreen;

    public int hours = 8;
    public float second = 0;
    public int minutes = 0;

    public Text lifetimecounter;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
   
    void Start()
    {

          PlayerPrefs.SetInt("tutorial", 0);
      //  rewardtxt.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("firstplay7", 0) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("turn", 5);
           // DR.gameObject.GetComponent<DailyRewardsInterface>().resetreward();
          //  rewardtxt.gameObject.SetActive(true);
            PlayerPrefs.SetInt("firstplay7", 1);
        }
        Counter = PlayerPrefs.GetInt("turn", 5);

     hours =   PlayerPrefs.GetInt("hours",8);
     minutes =   PlayerPrefs.GetInt("minutes", 0);
     second =   PlayerPrefs.GetInt("seconds", 0);
        
    }

    void calculatetime()
    {
       if(second > 0)
        {

            second -= Time.deltaTime;


        }
        else
        {
            if(minutes > 0)
            {
                minutes -= 1;
                second = 60;
                
            }
            else
            {
                if(hours > 0)
                {
                    hours -= 1;
                    minutes = 60;
                 

                }
                else
                {
                    print("reward");
                    hours = 8;
                    minutes = 0;
                    second = 0;
                }
            }
        }

        PlayerPrefs.SetInt("hours",hours);
        PlayerPrefs.SetInt("minutes", minutes);
        PlayerPrefs.SetInt("seconds", Mathf.RoundToInt(second));
        lifetimecounter.text = hours.ToString()+":"+minutes.ToString()+":"+Mathf.RoundToInt(second).ToString();

    }
  
    // Update is called once per frame
    void Update()
    {
        calculatetime();
        if (waitingTime > 0 && LoadingScreen.active)
        {
            waitingTime -= Time.deltaTime;
        }
        else
        {
            LoadingScreen.SetActive(false);
        }
      if (TurnTxt == null)
        {
            TurnTxt = GameObject.Find("countertext").GetComponent<Text>();
           
            TurnTxt.text = Counter.ToString() +"/5";
          
        }
        if (cointxt == null)
        {
            cointxt = GameObject.Find("cointext").GetComponent<Text>();
            coins = PlayerPrefs.GetInt("coins", 0);        
            cointxt.text = coins.ToString();

        }

        if (_isGameOVer || !_isGamestart)
            return;


        if (Timertxt == null)
             {
               Timertxt = GameObject.Find("timercounter").GetComponent<Text>();
               
             }
              else
             {
          
                TotalTime -= Time.deltaTime * 2;
            
            Timertxt.text = Mathf.RoundToInt(TotalTime).ToString();
             }


       
        if (TotalTime <= 0 && !_isHacked)
        {
            _isGameOVer = true;
            gameoverFtn();
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSpirent = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSpirent = false;
        }

        if (isSpirent)
        {
            staminabar.value -= Time.deltaTime / 10;
        }
        else
        {
            staminabar.value += Time.deltaTime / 25;
        }




    }

    public void startGame()
    {
        if(Counter <= 0)
        {
            Am.onBtwrongClickSound();
            return;
        }


        Am.onBtClickSound();



        if (PlayerPrefs.GetInt("tutorial", 0) == 1)
        {
            characterAni.SetBool("startGame", true);
            Startpanel.SetActive(false);
            tutorial.SetActive(false);
            OC.enabled = true;
            _isGamestart = true;
            InGamepanel.SetActive(true);
        }
        else
        {
            tutorial.SetActive(true);
            InGamepanel.SetActive(true);
            PlayerPrefs.SetInt("tutorial", 1);
        }
    }
    public void gameoverFtn()
    {

        Am.onwalkstop();
        GameoverPanel.SetActive(true);
        _isGameOVer = true;
        _isGamestart = false;
      //  _isGamestart = false;
        if (!_iswinner)
        {
            Gameovertxt.text = "You Lose";
            DescriptionTxt.text = "Please Try Again";
            if (!_isHacked)
            {

                Counter -= 1;
             //   print(Counter);
                PlayerPrefs.SetInt("turn", Counter);
            }
            Am.onLoseSound();
            GameManager.instance.characterAni.SetBool("die", true);
        }
        else
        {
            Gameovertxt.text = "Winner";
            Am.onWinSound();
            DescriptionTxt.text ="You Got 1 Token       In you're wallet.";
            coins += 1;

            PlayerPrefs.SetInt("coins", coins);
            GameManager.instance.characterAni.SetBool("win", true);
        }

       ChickenController CC = GameObject.FindObjectOfType<ChickenController>();
        CC.chickAgent.destination = CC.transform.position;
        CC.enabled = false;
        PlayerPrefs.SetInt("turn", Counter);

    }

    public void Restart()
    {
        Am.onBtClickSound();
        Time.timeScale = 1;
        Destroy(gameObject);
        Application.LoadLevel(0);
    }

    public void Soundbt()
    {
        Am.onBtClickSound();
        if (_ismusic)
        {
            MusicImage.enabled = true;
            _ismusic = false;
        }
        else
        {
            MusicImage.enabled = false;
            _ismusic = true;
        }
    }

    public void pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
