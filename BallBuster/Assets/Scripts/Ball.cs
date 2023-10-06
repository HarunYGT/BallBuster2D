using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] private int Num;
    [SerializeField] TextMeshProUGUI numText;
    [SerializeField] GameManager gameManager;
    [SerializeField] ParticleSystem unionEffect;
    [SerializeField] SpriteRenderer spriteRenderer;
 
    bool first = false;
    [SerializeField] private bool standartBall;

    void Start()
    {
        numText.text = Num.ToString();
        if(standartBall)
            first= true;
    }

    void SetSituation()
    {
        first = true;   
    }
    public void SetFirstSituation()
    {
        Invoke("SetSituation",2f);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Num.ToString()) && first)
        {
            unionEffect.Play();
            col.gameObject.SetActive(false);
            Num *= 2;
            gameObject.tag = Num.ToString();
            numText.text = Num.ToString();

            switch(Num)
            {
                case 4:
                    spriteRenderer.sprite = gameManager.SpriteObjects[1];
                    break;
                case 8:
                    spriteRenderer.sprite = gameManager.SpriteObjects[2];
                    break;
                case 16:
                    spriteRenderer.sprite = gameManager.SpriteObjects[3];
                    break;
                case 32:
                    spriteRenderer.sprite = gameManager.SpriteObjects[4];
                    break;
                case 64:
                    spriteRenderer.sprite = gameManager.SpriteObjects[5];
                    break;
                case 128:
                    spriteRenderer.sprite = gameManager.SpriteObjects[6];
                    break;
                case 256:
                    spriteRenderer.sprite = gameManager.SpriteObjects[7];
                    break;
                case 512:
                    spriteRenderer.sprite = gameManager.SpriteObjects[8];
                    break;
                case 1024:
                case 2048:
                    spriteRenderer.sprite = gameManager.SpriteObjects[9];
                    break; 
            }
        }
        if(gameManager.isBallTarget)
        {
            gameManager.MissionCheck(Num);
        }
        first = false;
        Invoke("SetSituation",2f);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Num.ToString()) && first)
        {
            unionEffect.Play();
            col.gameObject.SetActive(false);
            Num *= 2;
            gameObject.tag = Num.ToString();
            numText.text = Num.ToString();

            switch(Num)
            {
                case 4:
                    spriteRenderer.sprite = gameManager.SpriteObjects[1];
                    break;
                case 8:
                    spriteRenderer.sprite = gameManager.SpriteObjects[2];
                    break;
                case 16:
                    spriteRenderer.sprite = gameManager.SpriteObjects[3];
                    break;
                case 32:
                    spriteRenderer.sprite = gameManager.SpriteObjects[4];
                    break;
                case 64:
                    spriteRenderer.sprite = gameManager.SpriteObjects[5];
                    break;
                case 128:
                    spriteRenderer.sprite = gameManager.SpriteObjects[6];
                    break;
                case 256:
                    spriteRenderer.sprite = gameManager.SpriteObjects[7];
                    break;
                case 512:
                    spriteRenderer.sprite = gameManager.SpriteObjects[8];
                    break;
                case 1024:
                case 2048:
                    spriteRenderer.sprite = gameManager.SpriteObjects[9];
                    break; 
            }
        }
        if(gameManager.isBallTarget)
        {
            gameManager.MissionCheck(Num);
        }
        first = false;
        Invoke("SetSituation",2f);
    }
}
