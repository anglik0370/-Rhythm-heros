using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> viewStage = null;

    [SerializeField]
    private List<AudioSource> bgm = null;

    [SerializeField]
    private Animator hands = null;

    [SerializeField]
    private GameObject startCan = null;

    private void Start()
    {
        StaticManager.Instance.stageNumber = 0;
        Time.timeScale = 1f;
        hands.ResetTrigger("Move");
        hands.ResetTrigger("StopMove");
    }
    private void Update()
    {
        if(StaticManager.Instance.scene == 0)
        {
            startCan.SetActive(true);
        }
        else
        {
            startCan.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) Next();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Prev();
    }

    public void StagePreviewStart()
    {
        hands.SetTrigger("Move");
        bgm[StaticManager.Instance.stageNumber].Play();
    }

    public void StagePreviewEnd()
    {
        hands.SetTrigger("StopMove");
        bgm[StaticManager.Instance.stageNumber].Stop();
    }

    public void StartGame()
    {
        hands.ResetTrigger("Move");
        hands.ResetTrigger("StopMove");
        SceneManager.LoadScene(2);
    }

    public void Next()
    {
        StaticManager.Instance.stageNumber++;

        for (int i = 0; i < 3; i++)
        {
            if (StaticManager.Instance.stageNumber == i)
            {
                viewStage[i].SetActive(true);
            }
            else
            {
                viewStage[i].SetActive(false);
            }
        }
    }

    public void Prev()
    {
        StaticManager.Instance.stageNumber--;

        for (int i = 0; i < 3; i++)
        {
            if (StaticManager.Instance.stageNumber == i)
            {
                viewStage[i].SetActive(true);
            }
            else
            {
                viewStage[i].SetActive(false);
            }
        }
    }
}
