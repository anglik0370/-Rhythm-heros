using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    private Animator animator = null;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PlayAnimation()
    {
        animator.SetTrigger("Fire");
    }

    private void PlayIdle()
    {
        animator.Play("Idle");
    }
}
