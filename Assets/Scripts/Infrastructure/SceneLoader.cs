using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Infrastructure
{
	public class SceneLoader : MonoBehaviourPunCallbacks
	{
		public event Action OnSceneLoaded;

		[SerializeField] private GameObject _loadingScreen;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void LoadScene(string name)
		{
			_loadingScreen.SetActive(true);

			StartCoroutine(WaitForSceneLoad(SceneManager.LoadSceneAsync(name)));
		}

		public override void OnJoinedRoom()
		{
			PhotonNetwork.LoadLevel(GlobalNames.GameScene);
			StartCoroutine(WaitForNetworkSceneLoad());

		}
		private IEnumerator WaitForNetworkSceneLoad()
		{
			while (PhotonNetwork.LevelLoadingProgress != 1)
			{
				yield return null;
			}

			OnSceneLoaded?.Invoke();
		}

		private IEnumerator WaitForSceneLoad(AsyncOperation sceneLoading)
		{
			sceneLoading.allowSceneActivation = false;
			yield return new WaitForSeconds(1f);
			sceneLoading.allowSceneActivation = true;

			while (!sceneLoading.isDone)
			{
				yield return null;
			}

			_loadingScreen.SetActive(false);
			OnSceneLoaded?.Invoke();
		}
	}
}
