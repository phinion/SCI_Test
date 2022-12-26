using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
	// Moves the character
	protected abstract void Move();

	// Allows the character to Jump
	protected abstract void Jump();

}
