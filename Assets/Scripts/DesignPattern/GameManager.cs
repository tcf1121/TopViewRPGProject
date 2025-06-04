using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject playerprefab;
    [SerializeField] public GameObject Canvas;
    public static Player player;
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

        GameObject playerObj = Instantiate(playerprefab);
        player = playerObj.GetComponent<Player>();
        playerObj.name = "Player";
        DontDestroyOnLoad(playerObj);
        DontDestroyOnLoad(Canvas);
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

    private void GameOver()
    {

    }

    public void EndGame()
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

    public void EnterVillage()
    {
        LoadingSceneManager.LoadScene(2);
    }

    public void EnterDungeon()
    {
        LoadingSceneManager.LoadScene(3);

    }




}
