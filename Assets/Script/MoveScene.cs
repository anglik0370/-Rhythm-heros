using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private int scene = 0;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
        StaticManager.Instance.scene = 0;
    }

    public void BackToSelect()
    {
        SceneManager.LoadScene(0);
        StaticManager.Instance.scene = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
