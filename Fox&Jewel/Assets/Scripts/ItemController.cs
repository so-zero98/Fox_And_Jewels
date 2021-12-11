using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem()
    {
        animator.SetTrigger("getItem");
        Destroy(gameObject, 0.3f);
        gameObject.tag = "getItem";
    }
}
