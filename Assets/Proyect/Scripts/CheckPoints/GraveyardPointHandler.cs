using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardPointHandler : MonoBehaviour
{
    [SerializeField] private Transform directionalLightObject;
    [SerializeField] private Transform spotLightObject;
    private Light lightDirectional;
    private Light lightSpot;

    void Start()
    {
        lightDirectional = directionalLightObject.GetComponent<Light>();
        lightSpot = spotLightObject.GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lightDirectional.color = new Color(0, 0, 0, 1);
            lightSpot.gameObject.SetActive(true);
        }
    }
}
