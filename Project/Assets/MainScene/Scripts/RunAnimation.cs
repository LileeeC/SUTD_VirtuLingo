using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTalking(GameObject g)
    {
        Animator anim;
        anim = g.GetComponent<Animator>();
        anim.Play("Talk");
    }
}
