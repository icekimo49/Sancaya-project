using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Dash_Tutorial : MonoBehaviour
{
    [SerializeField] public Animator animator;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        animator.SetBool("dashTutorial", true);
    }
}

