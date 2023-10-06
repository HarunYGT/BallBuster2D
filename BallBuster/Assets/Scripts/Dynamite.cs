using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private int Num;
    [SerializeField] TextMeshProUGUI numText;
    [SerializeField] GameManager gameManager;
    List<Collider2D> colliders= new List<Collider2D>();
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Num.ToString()))
        {
            UsePower();
        }
    }
    void UsePower()
    {
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers=true
        };

        Physics2D.OverlapBox(transform.position,transform.localScale*2,20f,contactFilter2D,colliders);

        gameManager.BoomEfeect(transform.position);
        gameObject.SetActive(false);
        foreach (var item in colliders)
        {
            if(item.gameObject.CompareTag("Box"))
            {
                item.GetComponent<Box>().PlayEffect();
            }
            else
                item.gameObject.GetComponent<Rigidbody2D>().AddForce(90 * new Vector2(0,6),ForceMode2D.Force);
        }
    }
}
