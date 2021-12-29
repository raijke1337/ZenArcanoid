using System.Collections;
using System.Collections.Generic;
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
       
    // controls
    private DefaultControls _controls;
    private BallController _balls;
    private UIController _UIctrl;

    // game objects
    private List<BouncyItemComponent> _bouncers;
    private List<SpawnerComp> _spawners;
    private Transform _playerPivot;
    public Transform GetPlatform => _playerPivot.GetChild(1);


    //game variables
    [SerializeField]
    private int Level;
    public bool IsGamePaused { get; private set; }
    public bool IsGameStarted { get; private set; }
    //private const float AngleToRotatePlatformInSpace = 45f;
    // rotate 45 degrees 45*8 = 360
    //private bool IsPivotBusy;


    // for platform rotation around pivot in Y
    //private Vector3 _startingRotation;
    //private float _angleX;
    //private Quaternion _desiredRotation;

    // for controls
    [SerializeField]
    private float RotationSpeed = 3f;
    private Vector2 _currentInputVector = new Vector2();

    // settings
    [SerializeField, Tooltip("Lives player gets"), Range(1, 5)]
    private int Lives = 3;
    [SerializeField, Tooltip("Number of cubes to spawn")]
    private SpawnerTask SpawnSettings;

           
    
    private void OnEnable()
    {
        UpdateBouncers();
        // todo add log
        _controls = new DefaultControls();
        SetupControls(true);
        FindSpawners();
        _playerPivot = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        IsGameStarted = false;
        IsGamePaused = false;
    }
    private void OnDisable()
    {
        SetupControls(false);
    }

    // update/init list of bouncers and subscribe to collision events
    void UpdateBouncers()
    {
        //if (_bouncers == null)
        //{
            _bouncers = new List<BouncyItemComponent>
            {
                FindObjectOfType<BouncyItemComponent>()
            };
       // }
        if (_bouncers.Count == 0) EditorApplication.isPaused = true;
    }
    // find spawner empties
    void FindSpawners()
    {
        _spawners = new List<SpawnerComp>();
        _spawners.Add(FindObjectOfType<SpawnerComp>());
    }
    // uses _spawners
    void SpawnStuff(SpawnerTask task)
    {
        foreach (var spawner in _spawners)
        {
            spawner.SpawnCubes(task);
        }
        UpdateBouncers();
    }

    void StartGame()
    {
        if (_playerPivot == null)
        {
            Debug.LogError("Set 'player' tag on platform");
            return;
        }
        SpawnStuff(SpawnSettings);
        IsGameStarted = true;
    }

    // controls here

    // subs unsubs
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


    private void Update()
    {
        if (IsGameStarted)
        {
            _currentInputVector = _controls.Platform.WASD.ReadValue<Vector2>();
            PivotMove();
        }
    }


    // pause is also used in UIcontroller
    public event GameEventHandler PausePressedEventForUI;
    private void Pause_performed(CallbackContext obj)
    {
        PausePressedEventForUI?.Invoke();
        if (!IsGamePaused)
        {
            Time.timeScale = 0f;
        }
        if (IsGamePaused)
        {
            Time.timeScale = 1f;
        }
        IsGamePaused = !IsGamePaused;
    }

    public event GameEventHandler SpaceBarPressedEvent;
    private void SpaceBar_performed(CallbackContext obj)
    {
        if (IsGamePaused) return;
        SpaceBarPressedEvent?.Invoke();
        if (!IsGameStarted) { StartGame(); }
        else { Tilt(); }
    }

    private void PivotMove()
    {

    }


         
    //private void Right_performed(CallbackContext obj)
    //{
    //    if (IsPivotBusy) return;
    //    _angleX += obj.ReadValue<float>() * AngleToRotatePlatformInSpace;
    //    _desiredRotation = Quaternion.Euler(0f, _angleX, 0f);
    //    StartCoroutine(RotateTransform(_desiredRotation,_playerPivot));
    //}

    //private IEnumerator RotateTransform(Quaternion desired, Transform tra)
    //{
    //    IsPivotBusy = true;
    //    float timer = 0;
    //    while (timer < RotationSpeed)
    //    {
    //        timer += Time.deltaTime;
    //        tra.localRotation = Quaternion.Lerp(tra.localRotation, desired, Time.deltaTime);
    //        yield return null;
    //    }
    //    IsPivotBusy = false;
    //    yield return null;
    //}



    void Tilt()
    {
        Debug.Log("Relax...");
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
        foreach (var c in _bouncers)
        {
            Destroy(c);
        }
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
