using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("LEVEL BASIC OBJECTS")]
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject pot;
    [SerializeField] private GameObject potEnlarge;
    [SerializeField] private GameObject[] spawnSomethingPositions;
    [SerializeField] private AudioSource[] Voices;
    [SerializeField] private ParticleSystem[] Effects;

    [Header("UI OBJECTS")]
    [SerializeField] private Image[] DutyImages;
    [SerializeField] private Sprite MissionCompletedSprite;
    [SerializeField] private int dutyCount;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI WinLevelCount;
    [SerializeField] private TextMeshProUGUI LoseLevelCount;
    private int scoredBall;
    private string LevelName;

    private float fingerPosX;
    void Start()
    {
        LevelName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < dutyCount; i++)
        {
            DutyImages[i].gameObject.SetActive(true);
        }
        //Invoke("SpawnSomething",3f);
    }

    void SpawnSomething()
    {
        int randomNumber = Random.Range(0, spawnSomethingPositions.Length -1);
        
        potEnlarge.transform.position = spawnSomethingPositions[randomNumber].transform.position;
        potEnlarge.SetActive(true);
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {

            /*if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10));

                switch (touch.phase)
                {
                    case  TouchPhase.Began:
                        fingerPosX = touchPosition.x - platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (touchPosition.x - fingerPosX > -1.15 && touchPosition.x - fingerPosX < 1.15)
                        {
                            platform.transform.position = Vector3.Lerp(platform.transform.position,new Vector3
                            (touchPosition.x - fingerPosX,platform.transform.position.y
                                ,platform.transform.position.z),5f);
                        }
                        break;
                }
            }*/
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(platform.transform.position.x > -1.15)
                    platform.transform.position = Vector3.Lerp(platform.transform.position,new Vector3(platform.transform.position.x - .3f,platform.transform.position.y
                        ,platform.transform.position.z),0.50f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if(platform.transform.position.x < 1.15)
                    platform.transform.position = Vector3.Lerp(platform.transform.position,new Vector3(platform.transform.position.x + .3f,platform.transform.position.y
                        ,platform.transform.position.z),0.50f);
            }
        }
        
    }

    public void Basket(Vector3 pos)
    {
        scoredBall++;
        DutyImages[scoredBall - 1].sprite = MissionCompletedSprite;
        Effects[0].transform.position = pos;
        Effects[0].gameObject.SetActive(true);
        Voices[1].Play();
        if (scoredBall == dutyCount)
        {
            Win();
        }

        if (scoredBall == 1)
        {
            SpawnSomething();
        }
    }

    public void Lose()
    {
        Voices[2].Play();
        Panels[2].SetActive(true);
        LoseLevelCount.text = "Level : " + LevelName;
        Time.timeScale = 0;
    }

    void Win()
    {
        Voices[3].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        WinLevelCount.text = "Level : " + LevelName;
        Time.timeScale = 0;

    }
    public void EnlargePot(Vector3 pos)
    {
        Effects[1].transform.position = pos;
        Effects[1].gameObject.SetActive(true);
        Voices[0].Play();
        pot.transform.localScale = new Vector3(55f, 55f, 55f);
    }

    public void ButtonOperations(string value)
    {
        switch (value)
        {
            case "Stop":
                Time.timeScale = 0;
                Panels[0].SetActive(true);
                break;
            case "Continue":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Settings":
                break;
            case "Retry":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
                
        }
    }
}
