using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationHandler
{
    private Animator animator;

    public CharacterAnimationHandler(Animator _animator)
    {
        animator = _animator;
    }

    public bool GetInAirBool => animator.GetBool("InAir");
    public float GetWalkValue => animator.GetFloat("Walk");

    public void SetHurtTrigger()
    {
        animator.SetTrigger("Hurt");
    }

    public void SetInAirBool(bool _value)
    {
        animator.SetBool("InAir", _value);
    }

    public void SetWalkValue(float _value)
    {
        if (GetWalkValue != _value)
        {
            animator.SetFloat("Walk", _value);
        }
    }
}
