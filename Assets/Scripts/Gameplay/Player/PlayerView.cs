using TMPro;
using UnityEngine;

namespace Project.Gameplay
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _nameLabel;

		public void SetName(string name)
		{
			_nameLabel.text = name;
		}
	}
}