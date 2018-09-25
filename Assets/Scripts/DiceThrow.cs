using UnityEngine;

public class DiceThrow : MonoBehaviour
{
	[SerializeField] float maxRandom;	


	void Start()
	{
		Rigidbody rB = GetComponent<Rigidbody>();

		float torqueX = Random.Range(-maxRandom, maxRandom);
		float torqueY = Random.Range(-maxRandom, maxRandom);
		float torqueZ = Random.Range(-maxRandom, maxRandom);
		Vector3 torque = new Vector3(torqueX, torqueY, torqueZ);
		rB.AddTorque(torque);

		float forceX = Random.Range(-maxRandom, maxRandom);
		float forceY = Random.Range(-maxRandom, maxRandom);
		float forceZ = Random.Range(-maxRandom, maxRandom);
		Vector3 force = new Vector3(forceX, forceY, forceZ);
		rB.AddForce(force);
	}	
}
