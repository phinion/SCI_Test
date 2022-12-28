using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IHealth
{
    public int Health { get; set; }

    // Add locomotion

    // Moves the character
    protected abstract void Move();

    // Allows the character to Jump
    protected abstract void Jump();

    public void TakeDamage(int amount)
    {
        Health -= amount;
        DeathCheck();
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Die()
    {
        //
    }

    public void DeathCheck()
    {
        if(Health <= 0)
        {
            Die();
        }
    }

}
