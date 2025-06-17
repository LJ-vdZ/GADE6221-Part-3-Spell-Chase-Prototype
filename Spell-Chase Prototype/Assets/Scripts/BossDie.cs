using System.Collections;
using UnityEngine;

public class BossDie : Boss
{
    Animator bossAnim2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossAnim2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (ObstaclePassedScore.score >= 100)
        {
            bossAnim2.SetBool("Walk", false);
            bossAnim2.SetBool("Cast", false);
            bossAnim2.SetBool("BossDie", true);
            StartCoroutine(DestroyTimer());
        } 
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy (gameObject);
    }
}
