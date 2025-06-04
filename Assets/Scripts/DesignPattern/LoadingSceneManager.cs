using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static int nextScene;
    [SerializeField] Slider progressBar;
    public static Coroutine loadCor;

    private void Start()
    {
        loadCor = StartCoroutine(LoadScene());
    }



    public static void LoadScene(int sceneNum)
    {
        nextScene = sceneNum;
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    if (nextScene == 2)
                    {
                        GameManager.player.playerStatus.OnDownBar();
                    }
                    else if (nextScene == 3)
                    {

                    }

                    StopCoroutine(loadCor);
                    loadCor = null;
                    yield break;

                }
            }
        }

    }
}
