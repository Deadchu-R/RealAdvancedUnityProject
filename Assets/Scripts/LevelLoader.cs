using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text progressText;
     public void LoadLevel(int sceneIndex)
     {

         StartCoroutine(LoadAsync(sceneIndex));

     }

     IEnumerator LoadAsync(int sceneIndex)
     {
         AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);
            
         while (!operation.isDone)
         {
             float progress = Mathf.Clamp01(operation.progress  / 0.9f);
             loadingBar.value = progress;
             progressText.text = progress * 100f + "%";

             yield return null;
         }
     }
}
