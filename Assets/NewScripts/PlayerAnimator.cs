using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Player player; // 拖入Player脚本所在的GameObject

    private const string IsWalkingKey = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetBool(IsWalkingKey, player.IsWalking());
    }


}

