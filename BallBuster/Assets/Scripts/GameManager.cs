using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;


[Serializable]
public class Targets
{
    public Sprite targetSprite;
    public int ballValue;
    public GameObject misComplete;
    public string targetType;
}
[Serializable]
public class Targets_UI
{
    public GameObject target;
    public Image targetSprite;
    public TextMeshProUGUI ballValue;
    public GameObject misComplete;
}

public class GameManager : MonoBehaviour
{
    [Header("Level Settings")]

    public Sprite[] SpriteObjects;
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private TextMeshProUGUI remainBallText;
    int remainBall;
    int poolIndex;

    [Header("Other Settings")]
    [SerializeField] private ParticleSystem boomEffect;
    [SerializeField] private ParticleSystem[] boxDestructionEffects;
    int boxDestrucIndex;


    // --- CannonBall System. ---///
    [Header("Cannonball System")]
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject ballSocket;
    [SerializeField] private GameObject nextBall;
    GameObject choosenBall;


    // --- Mission System --- //
    [Header("Mission Operations")]
    [SerializeField] private List<Targets_UI> targets_UI;
    [SerializeField] private List<Targets> targets;
    int BallValue, BoxValue, SumMissionNum;
    bool isBoxTarget;
    public bool isBallTarget;


    void Start()
    {

        remainBall = Balls.Length;
        GetBall(true);
        SumMissionNum = targets.Count;
        for (int i = 0; i < targets.Count; i++)
        {
            targets_UI[i].target.SetActive(true);
            targets_UI[i].targetSprite.sprite = targets[i].targetSprite;
            targets_UI[i].ballValue.text = targets[i].ballValue.ToString();
            if (targets[i].targetType == "Ball")
            {
                isBallTarget = true;
                BallValue = targets[i].ballValue;
            }
            else if (targets[i].targetType == "Box")
            {
                isBoxTarget = true;
                BoxValue = targets[i].ballValue;
            }
        }
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
                if (poolIndex != Balls.Length - 1)
                {
                    poolIndex++;
                    Balls[poolIndex].transform.position = nextBall.transform.position;
                    Balls[poolIndex].SetActive(true);
                }
                else
                {
                    poolIndex++;
                    Balls[poolIndex].transform.position = nextBall.transform.position;
                    Balls[poolIndex].SetActive(true);
                    remainBallText.text = remainBall.ToString();
                }
            }
            if (remainBall == 0)
            {
                Invoke("CheckResult", 3f);
            }
        }

    }
    public void BoomEfeect(Vector2 Pos)
    {
        boomEffect.gameObject.transform.position = Pos;
        boomEffect.gameObject.SetActive(true);
        boomEffect.Play();
    }
    public void BoxDestructEffect(Vector2 Pos)
    {
        boxDestructionEffects[boxDestrucIndex].gameObject.transform.position = Pos;
        boxDestructionEffects[boxDestrucIndex].gameObject.SetActive(true);
        boxDestructionEffects[boxDestrucIndex].Play();
        if (isBoxTarget)
        {
            BoxValue--;
            if (BoxValue == 0)
            {
                targets_UI[1].misComplete.SetActive(true);
                SumMissionNum--;
                if (SumMissionNum == 0)
                {
                    Won();
                }
            }
        }
        if (boxDestrucIndex == boxDestructionEffects.Length - 1)
            boxDestrucIndex = 0;
        else
            boxDestrucIndex++;

    }
    public void MissionCheck(int num)
    {
        if (num == BallValue)
        {
            targets_UI[0].misComplete.SetActive(true);
            SumMissionNum--;
            if (SumMissionNum == 0)
            {
                Won();
            }
        }
    }
    void Won()
    {
        Debug.Log("Win");
    }
    void Lose()
    {
        Debug.Log("Lose");
    }
    void CheckResult()
    {
        if (SumMissionNum == 0)
            Won();
        else
            Lose();
    }
}
