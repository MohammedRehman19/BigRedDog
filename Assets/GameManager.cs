using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Text Gameovertxt;
    public bool _iswinner = false;


    public Text cointxt;
    public int coins;

    public GameObject tutorial;


    public Sprite onMusic, offMusic;
    public bool _ismusic = true;
    public Image MusicImage;
    public GameObject fxwater;
    public Transform fxwaterpos;
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
      //  PlayerPrefs.SetInt("tutorial", 0);
    }

  
    // Update is called once per frame
    void Update()
    {
        
      if (TurnTxt == null)
        {
            TurnTxt = GameObject.Find("countertext").GetComponent<Text>();
            Counter = PlayerPrefs.GetInt("turn", 5);
            if (Counter <= 0)
            {
                Counter = 5;
            }
            TurnTxt.text = "5/" + Counter.ToString();
          
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
            TotalTime -= Time.deltaTime*2;
            Timertxt.text = "Timer = " + Mathf.RoundToInt(TotalTime);
             }


       
        if (TotalTime <= 0)
        {
            _isGameOVer = true;
            gameoverFtn();
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            staminabar.value -= Time.deltaTime /15;
        }
        
    }

    public void startGame()
    {
        if (PlayerPrefs.GetInt("tutorial", 0) == 1)
        {
            characterAni.SetBool("startGame", true);
            Startpanel.SetActive(false);
            tutorial.SetActive(false);
            OC.enabled = true;
            _isGamestart = true;
        }
        else
        {
            tutorial.SetActive(true);
            PlayerPrefs.SetInt("tutorial", 1);
        }
    }
    public void gameoverFtn()
    {
        Counter -= 1;
        PlayerPrefs.SetInt("turn", Counter);
        GameoverPanel.SetActive(true);
        _isGameOVer = true;
        _isGamestart = false;
      //  _isGamestart = false;
        if (!_iswinner)
        {
            Gameovertxt.text = "Game Over";
            GameManager.instance.characterAni.SetBool("die", true);
        }
        else
        {
            Gameovertxt.text = "You Win and you got 1 token";
         
            coins += 1;

            PlayerPrefs.SetInt("coins", coins);
            GameManager.instance.characterAni.SetBool("win", true);
        }

       ChickenController CC = GameObject.FindObjectOfType<ChickenController>();
        CC.chickAgent.destination = CC.transform.position;
        CC.enabled = false;


    }

    public void Restart()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        Application.LoadLevel(0);
    }

    public void Soundbt()
    {
        if (_ismusic)
        {
            MusicImage.sprite = offMusic;
            _ismusic = false;
        }
        else
        {
            MusicImage.sprite = onMusic;
            _ismusic = true;
        }
    }
}
