using TMPro;
using UnityEngine;

namespace Project.UI
{
	public class VictoryMenu : MonoBehaviour
	{
		[SerializeField] private TMP_Text _winnerLabel;
		[SerializeField] private TMP_Text _coinsLabel;

		public void TurnOn(string name, int winnersCoins)
		{
			gameObject.SetActive(true);
			_winnerLabel.text = $"{name} won!";
			_coinsLabel.text = $"Coins: {winnersCoins}";
		}
	}
}