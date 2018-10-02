using UnityEngine;

public class DiceNumber : MonoBehaviour
{
	[SerializeField] LayerMask layerMask;
	public GameObject[] rayTarget;
	public int Number = 0;
	int diceSides;
	int childNumber;
	int force = 1;
	float time;
	bool checkedNumber;
	Rigidbody rB;


	void Start()
	{
		rB = GetComponent<Rigidbody>();
		diceSides = transform.childCount;
		rayTarget = new GameObject[diceSides];

		for (int i = 0; i < rayTarget.Length; i++)
		{
			rayTarget[i] = transform.GetChild(childNumber).gameObject;
			childNumber += 1;
		}
	}

	void Update()
	{
		if (!checkedNumber)
		{
			if (rB.IsSleeping())
				rB.WakeUp();

			for (int i = 0; i < rayTarget.Length; i++)
			{
				Debug.DrawRay(rayTarget[i].transform.position, rayTarget[i].transform.up * 0.08f);
				if (Physics.Raycast(rayTarget[i].transform.position, rayTarget[i].transform.up, 0.08f, layerMask))
				{
					if (rB.velocity.magnitude <= 0.1f)
					{
						if (Number == 0)
						{
							Number = int.Parse(rayTarget[i].name);
							checkedNumber = true;
						}
					}
				}
				else if (rB.velocity.magnitude <= 0.05f)
				{
					time += Time.deltaTime;
					if (time >= 1.5f)
					{
						Vector3 vec = new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force));
						rB.AddForce(vec, ForceMode.Impulse);
						force++;
						time = 0;
					}
				}
			}
		}		
	}
}
