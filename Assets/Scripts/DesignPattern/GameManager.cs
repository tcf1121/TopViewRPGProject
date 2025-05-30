using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static Dungeon Dungeon;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // [Header("UI")]
    // [SerializeField] private GameObject startPanel;
    // [SerializeField] private Button startBtn;
    // [SerializeField] private Button endBtn;

    private void Awake()
    {
        SetSingleton();
        player = GameObject.Find("Player");
        Dungeon = GameObject.Find("DungeonManager").GetComponent<Dungeon>();
        // 테스트용 추가
        testGameStart();
    }
    private void SetSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {

    }

    private void testGameStart()
    {
        Dungeon.Init();
    }

    private void GameOver()
    {

    }

    private void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
    }

    private void Update()
    {

    }


}
