using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScrpit : MonoBehaviour
{
    [SerializeField]
    private float maxTimer = 1f;
    private float destroyTimer = 0f;

    [SerializeField]
    private GameManager gameManager = null;
    [SerializeField]
    private StaticManager staticManager = null;

    private float xScale = 0;
    private float yScale = 0;
    private float zScale = 0;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        staticManager = FindObjectOfType<StaticManager>();
    }

    void Update()
    {
        xScale += Time.deltaTime * 2;
        yScale += Time.deltaTime * 2;
        zScale += Time.deltaTime * 2;

        destroyTimer += Time.deltaTime;

        if (xScale >= 1 && yScale >= 1 && zScale >= 1)
        {
            xScale = 1;
            yScale = 1;
            zScale = 1;
        }

        gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);

        if (destroyTimer >= maxTimer)
        {
            if (staticManager.score - 10 <= 0)
            {
                staticManager.score = 0;
            }
            else
            {
                staticManager.score -= 10;
            }
            staticManager.miss++;
            gameManager.ResetCombo();
            Destroy(gameObject);
        }
    }
}
