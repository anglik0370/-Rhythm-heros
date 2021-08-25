using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 스크립트는 여러가지 변수의 연동을 위해 작성한 싱글톤 스크립트임
public class StaticManager : MonoBehaviour
{
    private static StaticManager _instance;

    public static StaticManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(StaticManager)) as StaticManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public int score = 0;   //점수
    public int miss = 0;   //미스 갯수
    public int combo = 0;  //콤보 수
    public int maxCombo = 0;   //최대 콤보

    public float rotateSpeed = 400;    //카메라 회전속도
    public float musicVolum = 0.5f;     //음악 볼륨
    public float gunSound = 0.4f;   //총소리 볼륨

    public int stageNumber = 0;

    public int scene = 0;  //0일땐 스타트 1일땐 스테이지 선택

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (combo > maxCombo) maxCombo = combo;
    }
}
