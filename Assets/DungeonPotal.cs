using UnityEngine;

public class DungeonPotal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //다음 층으로 이동
        if (other.gameObject.CompareTag("Pick"))
        {
            LoadingSceneManager.LoadScene(3);
        }

    }
}
