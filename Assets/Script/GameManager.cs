using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting
{
    public float rotateSpeed = 400;    //카메라 회전속도
    public float musicVolum = 0.5f;     //음악 볼륨
    public float gunSound = 0.4f;   //총소리 볼륨
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PauseManager pauseManager = null;
    [SerializeField]
    private StageManager stageManager = null;

    public List<AudioSource> music = null;    //배경음악 효과음, 퍼즈메니저에서 볼륨조절을 해야 해서 퍼블릭

    [SerializeField]
    private GameObject target = null;    //과녁
    [SerializeField]
    private Text scoreText = null;    //점수 텍스트

    public bool canFire = false;    //카운트 다운동안 발사 못하게 하는 스위치 역할

    [SerializeField]
    private GameObject countUI = null;  //카운트다운 UI
    [SerializeField]
    private Text countText = null;  //카운트다운 텍스트

    [SerializeField]
    private GameObject comboUI = null;  //콤보UI
    [SerializeField]
    private Text comboText = null;  //콤보 텍스트

    private Setting setting;

    private string jsonString = "";
    private string filePath = "";

    private void Awake()
    {
        setting = new Setting();
        filePath = string.Concat(Application.persistentDataPath, "/", "SaveSetting.txt");

        Load();
    }

    void Start()
    {
        Time.timeScale = 0f;

        StaticManager.Instance.miss = 0;
        StaticManager.Instance.combo = 0;
        StaticManager.Instance.maxCombo = 0;
        StaticManager.Instance.score = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartGame();
    }

    void Update()
    {
        scoreText.text = StaticManager.Instance.score.ToString();

        if (StaticManager.Instance.miss >= 50)
        {
            SceneManager.LoadScene(1);  //미스가 20개 넘어가면 게임오버
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (StaticManager.Instance.combo >= 10)
        {
            OnCombo();
        }
    }

    private void _SpawnTarget()     //과녁 랜덤하게 생성
    {
        float randomX = Random.Range(12, -11);
        float randomY = Random.Range(6, -5);

        Instantiate(target, new Vector3(randomX, randomY, 1.5f), Quaternion.Euler(0, 180, 0));
    }

    public void SpawnTarget(float sec)   //과녁 생성 싱크 맞추기
    {
        Invoke("_SpawnTarget", sec - 1);
    }

    private void StartGame() //게임 시작 전 과정
    {
        Time.timeScale = 1f;

        countUI.SetActive(true);

        Invoke("PlayMusic", 2f);
        Invoke(string.Concat("Stage",StaticManager.Instance.stageNumber.ToString()), 2f);

        Invoke("TimerThree", 0.5f);
        Invoke("TimerTwo", 1f);
        Invoke("TimerOne", 1.5f);
        Invoke("TimerGO", 2f);
        Invoke("TimerEnd", 2.5f);
    }

    private void Stage0()
    {
        stageManager.Stage1();
    }

    private void Stage1()
    {
        stageManager.Stage2();
    }

    private void Stage2()
    {
        stageManager.Stage2_Insane();
    }

    private void PlayMusic()    //배경음악 플레이
    {
        music[StaticManager.Instance.stageNumber].Play();
    }

    private void OnCombo()
    {
        comboUI.SetActive(true);
        comboText.text = StaticManager.Instance.combo.ToString() + "Combo!";
    }

    public void ResetCombo()
    {
        comboUI.SetActive(false);
        StaticManager.Instance.combo = 0;
    }

    public void Save()
    {
        setting.rotateSpeed = StaticManager.Instance.rotateSpeed;
        setting.musicVolum = StaticManager.Instance.musicVolum;
        setting.gunSound = StaticManager.Instance.gunSound;

        jsonString = JsonUtility.ToJson(setting);
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        byte[] data = Encoding.UTF8.GetBytes(jsonString);

        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    public void Load()
    {
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        jsonString = Encoding.UTF8.GetString(data);
        setting = JsonUtility.FromJson<Setting>(jsonString);

        StaticManager.Instance.rotateSpeed = setting.rotateSpeed;
        StaticManager.Instance.musicVolum = setting.musicVolum;
        StaticManager.Instance.gunSound = setting.gunSound;
    }

    public void QuitGame()    //게임 종료
    {
        Application.Quit();
    }

    private void TimerOne()    //카운트다운 1
    {
        countText.text = "1";
    }
    private void TimerTwo()    //카운트다운 2
    {
        countText.text = "2";
    }
    private void TimerThree()    //카운트다운 3
    {
        countText.text = "3";
    }
    private void TimerGO()    //카운트다운 GO
    {
        countText.text = "GO!";
    }
    private void TimerEnd()    //카운트다운 종료
    {
        pauseManager.uI.SetActive(true);
        countUI.SetActive(false);
        canFire = true;
    }
}
