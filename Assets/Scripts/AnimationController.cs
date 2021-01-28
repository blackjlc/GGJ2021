using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AnimationController : MonoBehaviour
{
    private bool isRight = true;

    // referenes
    public Animator animator;
    private int moveAniID;
    private int moveXAniID;
    private int moveSpeedAniID;
    private int carryAniID;
    private int pickKeyAniID;
    private int danceAniID;

    private void Awake()
    {
        Assert.IsNotNull(animator, "Animator should not be null!!");
        moveAniID = Animator.StringToHash("move");
        moveXAniID = Animator.StringToHash("move_x");
        moveSpeedAniID = Animator.StringToHash("move_speed");
        carryAniID = Animator.StringToHash("carry");
        pickKeyAniID = Animator.StringToHash("pick_key");
        danceAniID = Animator.StringToHash("dance");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="speedX"></param>
    /// <param name="speed">0 for idle. 1 for walk. 2 for dash</param>
    public void HandleAnimation(float speedX, float speed)
    {
        if (speedX != 0)
            isRight = speedX > 0;
        animator.SetFloat(moveXAniID, isRight ? 1f : -1f);
        animator.SetFloat(moveSpeedAniID, speed);
    }
}
