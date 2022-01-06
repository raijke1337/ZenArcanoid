using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameController : MonoBehaviour
{
    // singleton
    public static GameController MainController;
    private void Awake()
    {
        if (MainController != null) { MainController = null; }
        MainController = this;
    }    
    // stop editor
    public void QuitGame()
    {
        Debug.Log("Quit game at level" + Level);
        EditorApplication.isPlaying = false;
    }

    // game objects
    [SerializeField]
    private BallController BallPrefab;
    private BallController _ball;
    private SpawnerComp _spawner;
    private Transform _playerPivot;
    public Transform GetPivot => _playerPivot;
    public Transform GetCubesPool { get; private set; }


    //game variables
    [SerializeField]
    private int Level;
    private int Score;
    public bool IsGamePaused { get; private set; }
    public bool IsGameStarted { get; private set; }
    [SerializeField, Tooltip("Max speed. Boosted by bouncing"), Range(0, 100)]
    float MaxSpeed = 20;

    // for controls
    public Vector2 InputValue { get; private set; }

    // settings
    [SerializeField, Tooltip("Lives player gets"), Range(1, 5)]
    private int Lives = 3;
    [SerializeField, Tooltip("Number of cubes to spawn")]
    private SpawnerTask SpawnSettings;
    
    // controls
    private DefaultControls _controls;

    private void OnEnable()
    {
        GetCubesPool = GetComponentsInChildren<Transform>().First(t => t.name == "CubesPool");
        _spawner = FindObjectOfType<SpawnerComp>();
        _playerPivot = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _controls = new DefaultControls();
        SetupControls(true);
        SetupItems();


        IsGameStarted = false;
        IsGamePaused = false;
    }
    private void OnDisable()
    {
        SetupControls(false);
    }
    // subs unsubs


    // sets walls and everythingelse without proper settings to "solid"
    private void SetupItems()
    {
        var list = FindObjectsOfType<BouncyItemComponent>();
        foreach (var item in list)
        {
            if (item.GetGameItemType() == ObjectType.Unset)
            {
                item.SetItemType(ObjectType.Solid);
            }    
        }
        if (list.Length > 0)
        {
            Debug.LogWarning($"Total {list.Length} objects not set proprly");
        }
    }
    void SpawnStuff(SpawnerTask task)
    {
        _spawner.SpawnCubes(task);
    }
    void StartGame()
    {
        SpawnStuff(SpawnSettings);
        _ball = Instantiate(BallPrefab);
        _ball.CollisionEvent += _ball_CollisionEvent;
        _ball.BallSpeed = 1f;
        IsGameStarted = true;
    }

    private void _ball_CollisionEvent(Collision item, BouncyItemComponent comp)
    {
        //Debug.Log($"Ball collided with {item.gameObject.name} which is a {comp.GetGameItemType()}");
        var type = comp.GetGameItemType();
        GeneralCollision(item);

        if (type == ObjectType.Point)
        {
            Score++;
            Destroy(comp.gameObject);
        }
        if (type == ObjectType.Passthrough)
        {
            ApplyBonus();
            Destroy(comp.gameObject);
        }
    }

    private void GeneralCollision(Collision item)
    {
        _ball.RecalcDir(item);
        if (_ball.BallSpeed < MaxSpeed)
        {
            _ball.BallSpeed += 0.5f;
        }
    }

    private void ApplyBonus()
    {
        // some logic here
    }

    // pause is also used in UIcontroller
    public event GameEventHandler PausePressedEventForUI;
    private void Pause_performed(CallbackContext obj)
    {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        PausePressedEventForUI?.Invoke(IsGamePaused);
    }

    private void SpaceBar_performed(CallbackContext obj)
    {
        if (IsGamePaused) return;
        if (!IsGameStarted) { StartGame(); }
        else { Tilt(); }
    }

    private void Update()
    {
        if (IsGameStarted & !IsGamePaused)
        {
            InputValue = _controls.Platform.WASD.ReadValue<Vector2>();
        }
    }


    void Tilt()
    {
        Debug.Log("Relax...");
    }
    void SetupControls(bool isstart)
    {
        if (isstart)
        {
            _controls.Platform.Enable();
            _controls.Platform.SpaceBar.performed += SpaceBar_performed;
            _controls.Platform.Pause.performed += Pause_performed;
        }
        else
        {
            _controls.Platform.Disable();
            _controls.Platform.SpaceBar.performed -= SpaceBar_performed;
            _controls.Platform.Pause.performed -= Pause_performed;
            _controls.Dispose();
        }
    }

    #region TODO
    // rebuild level
    void NextLevel()
    {
        Level++;
        //IsGameStarted = false;
        //ResetBall();
        //SpawnCubes();
        Debug.Log("Now entering level: " + Level);
    }
    // also destroy cubes
    public void RestartLevel()
    {
        Level--;

        //ResetPlatf();
        //ResetBall();
        NextLevel();
        //PauseMenuPanel.SetActive(false);
        //IsGameStarted = false;
        IsGamePaused = true;
        Time.timeScale = 1;
    }
    void ResetPlatf()
    {
        //player1.transform.position = new Vector3(0, 0, player1.transform.position.z);
        //p1Rigid.velocity = Vector3.zero;
        //p1_InputVector2 = Vector2.zero;
    }
    #endregion


}
