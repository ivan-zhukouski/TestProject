using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoading : MonoBehaviour
    {
        private AsyncOperation _gamingLevel;
        
        private const int _maxCountLoadingLevel = 15;


        private void Start()
        {
            StartCoroutine(AsyncLoading());
        }
        
        private IEnumerator AsyncLoading()
        {
            LoadingAsyncScene();
            
            while (_gamingLevel.progress < 1)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        
        private void LoadingAsyncScene()
        {
            if (PlayerPrefs.GetInt("Level") <= _maxCountLoadingLevel)
            {
                LoadLevel();
            }
        }

        private void LoadLevel()
        {
            _gamingLevel = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level"));
        }
    }
}
