using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    Animator bossAnim;
    // Start is called before the first frame update
    void Start()
    {
       bossAnim = GetComponent<Animator>();
        if (bossAnim == null)
        {
            Debug.LogError("Animator not found on boss!");
        }
        else
        {
            Debug.Log("Animator found, setting Walk to true.");
            bossAnim.SetBool("Walk", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossAnim.SetBool("Walk", true);
        /*if (hatches == true)
        {
            bossAnim.SetBool("Taunt", true);
        }

        if(beam == true)
        {
            bossAnim.SetBool("Cast", true);
        }

        bossAnim.SetBool("Taunt", false);
        bossAnim.SetBool("Cast", false);*/
    }

}
