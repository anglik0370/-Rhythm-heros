using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManageer : MonoBehaviour
{
    [SerializeField]
    private Image gradeImage = null;

    [SerializeField]
    private Sprite sImage = null;
    [SerializeField]
    private Sprite aImage = null;
    [SerializeField]
    private Sprite bImage = null;
    [SerializeField]
    private Sprite cImage = null;
    [SerializeField]
    private Sprite dImage = null;

    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text missText = null;
    [SerializeField]
    private Text maxComboText = null;

    void Update()
    {
        if (StaticManager.Instance.miss == 0) gradeImage.sprite = sImage;
        else if (StaticManager.Instance.miss > 0 && StaticManager.Instance.miss < 11) gradeImage.sprite = aImage;
        else if (StaticManager.Instance.miss > 10 && StaticManager.Instance.miss < 21) gradeImage.sprite = bImage;
        else if (StaticManager.Instance.miss > 20 && StaticManager.Instance.miss < 31) gradeImage.sprite = cImage;
        else if (StaticManager.Instance.miss > 30 && StaticManager.Instance.miss < 41) gradeImage.sprite = dImage;

        scoreText.text = StaticManager.Instance.score.ToString();
        missText.text = StaticManager.Instance.miss.ToString();
        maxComboText.text = StaticManager.Instance.maxCombo.ToString();
    }
}
