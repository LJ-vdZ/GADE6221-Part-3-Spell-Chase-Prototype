using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    Animator bossAnim;
    public float maxTime = 4f;
    public float TimeCountdown ;
    // Start is called before the first frame update
    void Start()
    {
        TimeCountdown = maxTime;
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
        }*/

        TimeCountdown -= Time.deltaTime;

        if(TimeCountdown <= 0f)
        {
            TimeCountdown = maxTime;
            bossAnim.SetBool("Walk", false);
            bossAnim.SetBool("Cast", true);
        }
        else
        {
            bossAnim.SetBool("Cast", false);

        }

        //bossAnim.SetBool("Taunt", false);
    }

}
