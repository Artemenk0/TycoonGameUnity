using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuUI : MonoBehaviour
{
	public GameObject loadingScreen; 
    
    public GameObject settingsPanel;

    public Slider progressBar;       

    private IEnumerator LoadSceneAsync(int sceneName)
	{
	    loadingScreen.SetActive(true);
	    
	    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
	    operation.allowSceneActivation = false;

	    float minLoadTime = 1.5f; 
	    float elapsedTime = 0f;

	    while (!operation.isDone)
	    {
	        float progress = Mathf.Clamp01(operation.progress / 0.9f);
	        progressBar.value = progress;

	        elapsedTime += Time.deltaTime;

	        if (operation.progress >= 0.9f && elapsedTime >= minLoadTime)
	        {
	            operation.allowSceneActivation = true;
	        }

	        yield return null;
	    }
	}

    public void PlayButton()
    {
    	StartCoroutine(LoadSceneAsync(1));
    }

    public void SettingsButton()
    {
    	settingsPanel.SetActive(true);
    }
}
