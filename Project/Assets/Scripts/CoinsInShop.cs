using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinsInShop : MonoBehaviour 
{
	[SerializeField] private Text text;

	private void Start()
	{
		text.text = GameManagerScript.playerMoney.ToString();
	}
}
