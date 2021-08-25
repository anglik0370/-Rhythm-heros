using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    [SerializeField]
    private PauseManager pauseManager = null;

    private RaycastHit hit;
    private float maxDistance = 50f;
    [SerializeField]
    private LayerMask plus = 0;
    [SerializeField]
    private LayerMask minus = 0;

    [SerializeField]
    private GameManager gameManager = null;
    [SerializeField]
    private GunScript gun = null;

    float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && pauseManager.pauseEnable == false && fireTimer >= 0.2f && gameManager.canFire == true)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    private void Fire()
    {
        gun.PlaySound();
        gun.PlayAnimation();

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, plus))
        {
            StaticManager.Instance.combo++;
            StaticManager.Instance.score += 100;
            Destroy(hit.collider.gameObject);
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            StaticManager.Instance.miss++;
            gameManager.ResetCombo();

            if (StaticManager.Instance.score - 10 <= 0)
            {
                StaticManager.Instance.score = 0;
            }
            else
            {
                StaticManager.Instance.score -= 50;
            }
        }
    }
}
