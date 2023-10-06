using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void PlayEffect()
    {
        gameManager.BoxDestructEffect(transform.position);
        gameObject.SetActive(false);
    }
}
