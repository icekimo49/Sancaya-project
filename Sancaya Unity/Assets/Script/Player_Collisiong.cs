using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisiong : MonoBehaviour
{

    [SerializeField] public Animator animator;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        animator.SetBool("diTempat", true);
    }
}
