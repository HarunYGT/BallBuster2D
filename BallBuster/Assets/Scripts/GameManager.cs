using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Level Settings")]

    public Sprite[] SpriteObjects;
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private TextMeshProUGUI remainBallText;
    int remainBall;
    int poolIndex;

    // --- CannonBall System. ---///
    [Header("Cannonball System")]
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject ballSocket;
    [SerializeField] private GameObject nextBall;
    GameObject choosenBall;

    void Start()
    {
        remainBall = Balls.Length;
        GetBall(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.CompareTag("Ground"))
                {
                    Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    cannon.transform.position = Vector2.MoveTowards(cannon.transform.position,
                        new Vector2(MousePosition.x, cannon.transform.position.y), 30 * Time.deltaTime);
                }
            }
        }       
        if (Input.GetMouseButtonUp(0))
        {
            choosenBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            choosenBall.transform.parent = null;
            choosenBall.GetComponent<Ball>().SetFirstSituation();
            GetBall(false);
        }
    }
    void GetBall(bool firstSetup)
    {
        if (firstSetup)
        {
            Balls[poolIndex].transform.SetParent(cannon.transform);
            Balls[poolIndex].transform.position = ballSocket.transform.position;
            Balls[poolIndex].SetActive(true);
            choosenBall = Balls[poolIndex];

            poolIndex++;
            Balls[poolIndex].transform.position = nextBall.transform.position;
            Balls[poolIndex].SetActive(true);
            remainBallText.text = remainBall.ToString();
        }
        else
        {
            if (Balls.Length != 0)
            {
                Balls[poolIndex].transform.SetParent(cannon.transform);
                Balls[poolIndex].transform.position = ballSocket.transform.position;
                Balls[poolIndex].SetActive(true);
                choosenBall = Balls[poolIndex];
                remainBall--;
                remainBallText.text = remainBall.ToString();
                if (poolIndex == Balls.Length - 1)
                {
                    Debug.Log("Finish");
                }
                else
                {
                    poolIndex++;
                    Balls[poolIndex].transform.position = nextBall.transform.position;
                    Balls[poolIndex].SetActive(true);
                    remainBallText.text = remainBall.ToString();
                }
            }
        }

    }
}
