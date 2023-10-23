using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public BoxSpawner boxSpawner;

    [HideInInspector]public Box currentBox;

    public CameraFollow cameraScript;
    private int moveCount;

    public static GameplayController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        boxSpawner.SpawnBox();
    }

    private void Update()
    {
        DetectInput();
    }

    private void DetectInput()
    {
        #region PC

        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }

        #endregion

        #region Android

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                currentBox.DropBox();
            }
        }

        #endregion
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 1f);
    }

    private void NewBox()
    {
        boxSpawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount++;

        if (moveCount == 3)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 2f;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
