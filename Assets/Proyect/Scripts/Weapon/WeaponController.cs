using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour
{
    [Header("General")] public float fireRange = 200;
    public Transform cameraPlayerTransform;

    [Header("Shoot Parameters")] public LayerMask hittableLayers;

    public float recoilForce = 4f;
    public Animator animator;
    [SerializeField] 
    private ParticleSystem flashParticles;
    [SerializeField] 
    private ParticleSystem bulletParticles;

    private Vector3 weaponPosition;
    private bool reacoilActive = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        weaponPosition = transform.position;
        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Recoil();
        RaycastHit hitInfo;
        bool hitObject = Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hitInfo,
            fireRange, hittableLayers);
        if (hitObject)
        {
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                
            }
            //Aquí se hace lo que sea que vaya a hacer cuando se haya colisionado con algún objeto
        }

        RecoverPosition();
    }

    void Recoil()
    {
        flashParticles.Play();
        bulletParticles.Play();
        animator.Play("RecoilAnimation");
    }

    void RecoverPosition()
    {
        animator.SetBool("Recoil", false);
    }
}