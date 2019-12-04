using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DelegationController : MonoBehaviour
{
    private CharacterAttackController character;
    private UniversalHealthController healthController;
    private CharacterAnimations animations;

    public delegate void EnemyKnocked();
    public static event EnemyKnocked EnemyIsKnocked;

    private void Awake()
    {
        character = FindObjectOfType<CharacterAttackController>();
        healthController = GameObject.FindGameObjectWithTag(Tags.ENEMY).GetComponent<UniversalHealthController>();
        animations = GameObject.FindGameObjectWithTag(Tags.ENEMY).GetComponent<CharacterAnimations>();
    }


    private void Update()
    {
        KnockEnemyOut();
    }


    void KnockEnemyOut()
    {
        if (character.ComboFinished)
        {
            if(healthController.isKnockedOut && healthController.isEnemy)
            {
                /* animations.StopAllanimations();
                 animations.KnockedOutAnim();
                 healthController.DisableEnemyScriptsAfterKnockedOut();
                 Debug.LogFormat("Enemy Knocked Out");
                 character.ComboFinished = false;
                 healthController.isKnockedOut = false;
                 */
                EnemyIsKnocked?.Invoke();

            }
        }
    } // enemy knocked out 

} // end
