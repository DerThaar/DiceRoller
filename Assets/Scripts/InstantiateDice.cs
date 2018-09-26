using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantiateDice : MonoBehaviour
{
	[SerializeField] GameObject d4Prefab;
	[SerializeField] GameObject d6Prefab;
	[SerializeField] GameObject d8Prefab;
	[SerializeField] GameObject d10Prefab;
	[SerializeField] GameObject d12Prefab;
	[SerializeField] GameObject d20Prefab;
	[SerializeField] TMP_InputField countOfd4;
	[SerializeField] TMP_InputField countOfd6;
	[SerializeField] TMP_InputField countOfd8;
	[SerializeField] TMP_InputField countOfd10;
	[SerializeField] TMP_InputField countOfd12;
	[SerializeField] TMP_InputField countOfd20;
	[SerializeField] Toggle toggle;
	public GameObject[] d4;
	public GameObject[] d6;
	public GameObject[] d8;
	public GameObject[] d10;
	public GameObject[] d12;
	public GameObject[] d20;
	List<GameObject[]> dice = new List<GameObject[]>();
	int parseSumm;	
	   	

	void Update()
	{
		if (toggle.isOn)
		{
			CheckIfMaxDice(countOfd4);
			CheckIfMaxDice(countOfd6);
			CheckIfMaxDice(countOfd8);
			CheckIfMaxDice(countOfd10);
			CheckIfMaxDice(countOfd12);
			CheckIfMaxDice(countOfd20); 
		}
	}

	void CheckIfMaxDice(TMP_InputField input)
	{
		int number;
		if (int.TryParse(input.text, out number))
			number = int.Parse(input.text);
		if (number > 100)
			input.text = "100";
	}

	public void ThrowDice()
	{
		d4 = new GameObject[Parse(countOfd4)];
		d6 = new GameObject[Parse(countOfd6)];
		d8 = new GameObject[Parse(countOfd8)];
		d10 = new GameObject[Parse(countOfd10)];
		d12 = new GameObject[Parse(countOfd12)];
		d20 = new GameObject[Parse(countOfd20)];

		if (parseSumm > 100 && toggle.isOn)
		{
			print("Too much dice!");
			parseSumm = 0;
			return;
		}
		parseSumm = 0;

		AddToList(d4, d6, d8, d10, d12, d20);

		dice.RemoveAll(item => item == null);
		if (dice.Count == 0)
			return;

		InstDice(d4Prefab, d4, "d4");
		InstDice(d6Prefab, d6, "d6");
		InstDice(d8Prefab, d8, "d8");
		InstDice(d10Prefab, d10, "d10");
		InstDice(d12Prefab, d12, "d12");
		InstDice(d20Prefab, d20, "d20");

		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(false);
	}

	int Parse(TMP_InputField inputField)
	{
		int number;
		if (int.TryParse(inputField.text, out number))
			number = int.Parse(inputField.text);
		else
			number = 0;

		parseSumm += number;
		return number;
	}

	void AddToList(params GameObject[][] list)
	{
		for (int i = 0; i < list.Length; i++)
		{
			if (list[i].Length != 0)
			{
				dice.Add(list[i]);
			}
		}
	}

	void InstDice(GameObject prefab, GameObject[] dice, string name)
	{
		for (int i = 0; i < dice.Length; i++)
		{
			dice[i] = Instantiate(prefab);
			dice[i].name = name;
			Material mat = dice[i].GetComponent<Renderer>().material;
			mat.color = Random.ColorHSV();
			mat.EnableKeyword("_EmissionColor");
			mat.SetColor("_EmissionColor", Random.ColorHSV());
		}
	}

	public void NewThrow()
	{
		transform.GetChild(1).gameObject.SetActive(true);
		transform.GetChild(0).gameObject.SetActive(false);

		for (int i = 0; i < dice.Count; i++)
		{
			for (int j = 0; j < dice[i].Length; j++)
			{
				Destroy(dice[i][j].gameObject);
			}
		}
		dice.Clear();
	}
}
