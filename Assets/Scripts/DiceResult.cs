using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceResult : MonoBehaviour
{
	TextMeshProUGUI resultText;
	TMP_Dropdown diceResults;
	InstantiateDice Id;
	GameObject[] d4;
	GameObject[] d6;
	GameObject[] d8;
	GameObject[] d10;
	GameObject[] d12;
	GameObject[] d20;
	bool gotActiveDice;
	int gotResults;


	void Start()
	{
		resultText = GetComponent<TextMeshProUGUI>();
		diceResults = transform.GetChild(1).GetComponent<TMP_Dropdown>();		
		diceResults.gameObject.SetActive(false);
		Id = transform.parent.parent.GetComponent<InstantiateDice>();
	}

	void Update()
	{
		if (!gotActiveDice)
		{
			d4 = Id.d4;
			d6 = Id.d6;
			d8 = Id.d8;
			d10 = Id.d10;
			d12 = Id.d12;
			d20 = Id.d20;

			GetActiveDice("D4 Result", d4);
			GetActiveDice("D6 Result", d6);
			GetActiveDice("D8 Result", d8);
			GetActiveDice("D10 Result", d10);
			GetActiveDice("D12 Result", d12);
			GetActiveDice("D20 Result", d20);
			gotActiveDice = true;
		}

		if (gotResults < 6)
		{
			bool d4Bool = false;
			bool d6Bool = false;
			bool d8Bool = false;
			bool d10Bool = false;
			bool d12Bool = false;
			bool d20Bool = false;

			d4Bool = GetNumbers(d4, d4Bool);
			d6Bool = GetNumbers(d6, d6Bool);
			d8Bool = GetNumbers(d8, d8Bool);
			d10Bool = GetNumbers(d10, d10Bool);
			d12Bool = GetNumbers(d12, d12Bool);
			d20Bool = GetNumbers(d20, d20Bool);

			if (d4Bool && d6Bool && d8Bool && d10Bool && d12Bool && d20Bool)
			{
				GetResults(d4, "D4 Result");
				GetResults(d6, "D6 Result");
				GetResults(d8, "D8 Result");
				GetResults(d10, "D10 Result");
				GetResults(d12, "D12 Result");
				GetResults(d20, "D20 Result");
			}
		}
	}

	void GetActiveDice(string name, GameObject[] dice)
	{
		if (gameObject.name == name && dice.Length != 0)
			diceResults.gameObject.SetActive(true);
	}

	bool GetNumbers(GameObject[] dice, bool diceBool)
	{
		if (dice.Length > 0)
		{
			for (int i = 0; i < dice.Length; i++)
			{
				if (dice[i].GetComponent<DiceNumber>().Number == 0)
					return diceBool = false;
			}
			diceBool = true;
		}
		else
			diceBool = true;

		return diceBool;
	}

	void GetResults(GameObject[] dice, string name)
	{
		if (gameObject.name == name)
		{
			if (dice.Length > 0)
			{
				int diceResult = 0;
				List<string> results = new List<string>(dice.Length);
				for (int i = 0; i < dice.Length; i++)
				{
					int val = dice[i].GetComponent<DiceNumber>().Number;
					diceResult += val;
					results.Add($"{val}");
				}

				resultText.text = $"= { diceResult.ToString()}";
				diceResults.AddOptions(results);
			}
			else
				resultText.text = "";
		}

		gotResults++;
	}

	void OnDisable()
	{
		resultText.text = "";
		diceResults.ClearOptions();
		diceResults.gameObject.SetActive(false);
		gotResults = 0;
		gotActiveDice = false;
	}
}