using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public Text txtScore;
	public int totalScore = 0;

	public Image imgHPBar;

	private static GameUI instance = null;

	void Start()
	{
		if (instance == null) instance = this;

		totalScore = PlayerPrefs.GetInt("TOTAL_SCORE", 0);
		DisplayScore(0);
	}

	public static GameUI Instance
	{
		get
		{
			if (instance == null)
			{
				return null;
			}

			return instance;
		}
	}

	public void DisplayScore(int score)
	{
		totalScore += score;
		txtScore.text = "SCORE <color=#ff0000>" + totalScore.ToString() + "</color>";

		PlayerPrefs.SetInt("TOTAL_SCORE", totalScore);
	}
}
