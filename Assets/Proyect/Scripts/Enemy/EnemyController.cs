using System.Collections;
using System.Collections.Generic;
using Proyect.Scripts.Models;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Enemy enemy; 
    public EnemyController Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        enemy = new Enemy();
    }

    void TakeDamage(float damage, string type, int timeOutSeconds)
    {
        if(type == "toxic")
        {
            StartCoroutine(TimeTakingData(timeOutSeconds, damage));
        }
        else
        {
            enemy.GetDamage(damage);
        }
    }

    void BuffNerf(float percent, int timeOutSeconds, bool buff = true)
    {
        enemy.BuffNerf(buff, percent);
        StartCoroutine(WaitForNerf(timeOutSeconds, percent));
    }

    IEnumerator TimeTakingData(int timeOutSeconds, float data)
    {
        int actualTime = 0;
        while (actualTime == timeOutSeconds)
        {
            enemy.GetDamage(data);
            actualTime++;
            yield return new WaitForSeconds(0.5f);

        }
    }

    IEnumerator WaitForNerf(int timeOutSeconds, float percent)
    {
        yield return new WaitForSeconds(timeOutSeconds);
        enemy.BuffNerf(false, percent);
    }
}
