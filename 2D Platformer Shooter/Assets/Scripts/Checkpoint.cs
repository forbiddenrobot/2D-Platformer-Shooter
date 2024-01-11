using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private AudioSource activatedSoundEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        activatedSoundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("Checkpoint_Idle"))
        {
            GameManager.instance.AddCheckpoint(transform);
            activatedSoundEffect.Play();
            animator.SetTrigger("activated");
        }
    }

    public void AnimationActivatedFinished()
    {
        animator.SetTrigger("active");
    }
}
