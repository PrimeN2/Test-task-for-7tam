using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Infrastructure
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private GameObject _loadingScreen;

		public void LoadScene(string name)
		{
			TurnLoadingScreenOn();

			AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(name);
			sceneLoading.completed += TurnLoadingScreenOff;
		}

		private void TurnLoadingScreenOn()
		{
			_loadingScreen.SetActive(true);
		}

		private void TurnLoadingScreenOff()
		{
			_loadingScreen.SetActive(false);
		}

		private void TurnLoadingScreenOff(AsyncOperation operation)
		{
			_loadingScreen.SetActive(false);
		}
	}
}
