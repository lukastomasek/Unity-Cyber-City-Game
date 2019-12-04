using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Walk(bool walk)
    {
        anim.StopPlayback();
        anim.SetBool(AnimTags.WALK_BOOL, walk);
        
    }

    public void Punch1()
    {
        anim.SetTrigger(AnimTags.PUNCH_1);
    }

    public void Punch2()
    {
        anim.SetTrigger(AnimTags.PUNCH_2);
    }

    public void Punch3()
    {
        anim.SetTrigger(AnimTags.PUNCH_3);
    }

    public void QuakeKick()
    {
        anim.SetTrigger(AnimTags.KICK);
    }

    public void RegularHit()
    {
        anim.StopPlayback();
        anim.SetTrigger(AnimTags.HIT);
    }

    public void DeadAnims(int deadAnim)
    {
        if (deadAnim == 0)
            anim.SetTrigger(AnimTags.DEAD_1);
        if (deadAnim == 1)
            anim.SetTrigger(AnimTags.DEAD_2);
    }

    public void ViperDeadAnim()
    {
        anim.StopPlayback();
        anim.SetTrigger(AnimTags.DEAD_2);
    }

    public void SpecialAttack()
    {
        anim.SetTrigger(AnimTags.SPECIAL_ATTACK);
    }

    public void KnockedOutAnim()
    {
        anim.SetTrigger(AnimTags.KNOCKED_OUT);
    }

    public void StopAllanimations()
    {
        anim.StopPlayback();
    }

    public void EnemyShoot()
    {
        anim.SetTrigger(AnimTags.SHOOT);
    }


    public void BlockingAnim(bool canBlock)
    {
        anim.SetBool(AnimTags.BLOCKING, canBlock);
    }

    public void FreezeAnim()
    {
        anim.speed = 0f;
    }

    public void UnFreezeAnim()
    {
        anim.speed = 1.0f;
    }

} // end
