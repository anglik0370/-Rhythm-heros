using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject uI = null;    //UI

    [SerializeField]
    private Slider sensitivityBar = null;    //감도 조절 바
    [SerializeField]
    private Slider musicVolumBar = null;    //음악 조절 바
    [SerializeField]
    private Slider gunSoundBar = null;    //총소리 조절 바

    [SerializeField]
    private AudioSource gunSound = null;    //총소리 효과음

    [SerializeField]
    private GameObject optionUI = null;    //설정UI
    [SerializeField]
    private GameObject pauseUI = null;  //일시정지UI

    public bool pauseEnable = false;    //일시정지인지 아닌지 체크
    private bool optionEnable = false;  //옵션창 열려있는지 아닌지 체크

    [SerializeField]
    private GameManager gameManager = null;
    void Start()
    {
        sensitivityBar.value = StaticManager.Instance.rotateSpeed;
        gunSound.volume = StaticManager.Instance.gunSound;
        gunSoundBar.value = StaticManager.Instance.gunSound;
        gameManager.music[StaticManager.Instance.stageNumber].volume = StaticManager.Instance.musicVolum;
        musicVolumBar.value = StaticManager.Instance.musicVolum;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //일시정지
        {
            if (pauseEnable == false)  //일시정지가 아닐때
            {
                Pause();
            }
            else  //일시정지일때
            {
                if (optionEnable == true)
                {
                    BackToPause();
                }
                else 
                {
                    BackToGame();
                }
            }
        }
    }

    public void UpdatdSensitivity()    //감도 연동
    {
        StaticManager.Instance.rotateSpeed = sensitivityBar.value;
        sensitivityBar.value = StaticManager.Instance.rotateSpeed;
    }

    public void UpdatdGunVolume()   //총소리 연동
    {
        StaticManager.Instance.gunSound = gunSoundBar.value;
        gunSound.volume = StaticManager.Instance.gunSound;
    }

    public void UpdatdMusicVolume()    //음악소리 연동
    {
        StaticManager.Instance.musicVolum = musicVolumBar.value;
        gameManager.music[StaticManager.Instance.stageNumber].volume = StaticManager.Instance.musicVolum;
    }

    public void BackToPause()   //옵션창 닫기 (일시정지 창으로 돌아가기)
    {
        CloseOption();
        optionEnable = false;
        gameManager.Save();
    }

    public void BackToGame()
    {
        pauseEnable = false;
        pauseUI.SetActive(false);
        uI.SetActive(true);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameManager.music[StaticManager.Instance.stageNumber].Play();
    }

    public void Pause()
    {
        pauseEnable = true;
        uI.SetActive(false);
        pauseUI.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameManager.music[StaticManager.Instance.stageNumber].Pause();
    }

    public void OpenOption()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(true);
        optionEnable = true;
    }

    public void CloseOption()
    {
        pauseUI.SetActive(true);
        optionUI.SetActive(false);
    }
}
