using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(100f * Time.deltaTime, 100f * Time.deltaTime, 100f * Time.deltaTime);
    }
}
