using UnityEngine;

public class HPbar : MonoBehaviour
{
	public Transform Player;

	Vector3 Offset;

	void Start()
	{
		Offset = transform.position - Player.position;
	}

	void LateUpdate()
	{
		transform.position = Player.position + Offset;
	}
}