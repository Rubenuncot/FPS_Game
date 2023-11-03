using System.Collections;
using System.Collections.Generic;
using Proyect.Scripts.Models;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;

    public PlayerController Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
    }

    /*
     * This method is called when the player takes an object
     * and it applies some buff.
     */
    void BufFNerf(float percent, string type, int timeOutSeconds, bool buff = true)
    {
        player.BuffNerf(buff, percent, type);
        StartCoroutine(WaitForNerf(timeOutSeconds, percent, type));
    }

    void TakeDamage(float damage, string type, int timeOutSeconds = 0)
    {
        if (type == "toxic")
        {
            StartCoroutine(LifeInTime(timeOutSeconds, damage, false));
        }
        else
        {
            player.TakeDamage(damage);
        }
    }

    bool TakeHeal(float heal, int timeOutSeconds = 0)
    {
        if (player.CanHealth)
        {
            StartCoroutine(LifeInTime(timeOutSeconds, heal, false));
            return true;
        }
        return false;
    }

    /*
     * This method is called to take damage or heal in some time.
     * data is the amount of damage or healing.
     * If type is true, the player is healing, otherwise is taking damage
     */
    IEnumerator LifeInTime(int timeOutSeconds, float data, bool type)
    {
        int actualTime = 0;
        if (type)
        {
            while (actualTime == timeOutSeconds)
            {
                player.Healing(data);
                actualTime += 1;
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            while (actualTime == timeOutSeconds)
            {
                player.TakeDamage(data);
                actualTime += 1;
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    
    /*
     * This methos is called to wait for seconds til the nerf is over.
     */
    IEnumerator WaitForNerf(int timeOutSeconds, float percent, string type)
    {
        yield return new WaitForSeconds(timeOutSeconds);
        player.BuffNerf(false, percent, type);
    }
}