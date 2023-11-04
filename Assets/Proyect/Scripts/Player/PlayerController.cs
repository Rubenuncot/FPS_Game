using System.Collections;
using System.Collections.Generic;
using Proyect.Scripts.Models;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    public static bool CanSpawnEnemies { get; set; }
    public static PlayerController Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _player = new Player();
    }

    /*
     * This method is called when the player takes an object
     * and it applies some buff.
     */
    public void BufFNerf(float percent, string type, int timeOutSeconds, bool buff = true)
    {
        _player.BuffNerf(buff, percent, type);
        StartCoroutine(WaitForNerf(timeOutSeconds, percent, type));
    }

    public void TakeDamage(float damage, string type, int timeOutSeconds = 0)
    {
        if (type == "toxic")
        {
            StartCoroutine(LifeInTime(timeOutSeconds, damage, false));
        }
        else
        {
            _player.TakeDamage(damage);
        }
    }

    public bool TakeHeal(float heal, int timeOutSeconds = 0)
    {
        if (_player.CanHealth)
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
    private IEnumerator LifeInTime(int timeOutSeconds, float data, bool type)
    {
        int actualTime = 0;
        if (type)
        {
            while (actualTime == timeOutSeconds)
            {
                _player.Healing(data);
                actualTime += 1;
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            while (actualTime == timeOutSeconds)
            {
                _player.TakeDamage(data);
                actualTime += 1;
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    
    /*
     * This methos is called to wait for seconds til the nerf is over.
     */
    private IEnumerator WaitForNerf(int timeOutSeconds, float percent, string type)
    {
        yield return new WaitForSeconds(timeOutSeconds);
        _player.BuffNerf(false, percent, type);
    }
    
    public void Awake()
    {
        Assert.IsNull(Instance, $"Multiple instances of {nameof(Instance)} detected. This should not happen.");
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Player created");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}