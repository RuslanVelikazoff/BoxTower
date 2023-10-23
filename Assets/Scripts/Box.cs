using UnityEngine;

public class Box : MonoBehaviour
{
    private bool canMove;

    private float moveSpeed = 2f;
    private float minX = -2.2f;
    private float maxX = 2.2f;

    private Rigidbody2D myBody;
    private LevelUIManager levelUI;

    private bool gameOver;
    private bool firstBox;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        levelUI = FindObjectOfType<LevelUIManager>();

        myBody.gravityScale = 0f;
    }

    private void Start()
    {
        canMove = true;

        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1f;
        }

        GameplayController.instance.currentBox = this;
    }

    private void Update()
    {
        MoveBox();
    }

    private void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > maxX)
            {
                moveSpeed *= -1f;
            }
            else if (temp.x < minX)
            {
                moveSpeed *= -1f;
            }

            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    private void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
        {
            return;
        }

        if (target.gameObject.tag == "Platform")
        {
            levelUI.SetScoreText(1);
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
        if (target.gameObject.tag == "Box")
        {
            levelUI.SetScoreText(1);
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
        {
            return;
        }

        if (target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;
            AudioManager.instance.Play("Lose");


            Invoke("RestartGame", 2f);
        }
    }
}
