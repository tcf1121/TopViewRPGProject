using Cinemachine;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    void Awake()
    {
        GameManager.player.gameObject.transform.position = new Vector3(0, 2, 0);
        _camera.Follow = GameObject.Find("Player").transform;
    }
}
