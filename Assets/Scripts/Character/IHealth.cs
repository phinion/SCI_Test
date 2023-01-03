using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
	// Health variable
	int Health { get;}

	// function to take damage
	void TakeDamage(int amount);
	// function to heal health
	void Heal(int amount);

	// function to kill character
	void Die();
}
