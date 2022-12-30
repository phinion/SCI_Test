using UnityEngine;

public class TestScript : MonoBehaviour
{
	private readonly int a = 2;
	private const int b = 5;
	private Vector3[] positions = new Vector3[5];

	void Awake()
	{
		for (int i = 0; i < 5; i++)
		{
			positions[i] = Vector3.zero;
		}
	}

	void Start()
	{
		Debug.Log(a + " x " + b + " = " + (a * b));
	}
}