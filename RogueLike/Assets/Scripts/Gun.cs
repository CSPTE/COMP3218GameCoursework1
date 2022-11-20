using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    public float damage = 10f;
    public float range = 100f;
    public Camera fspCam;
    public ParticleSystem muzzleFlash;
    public PlayerMotor playerMotor;
    public GameObject impactEffect;
    public float impactForce = 30f;
    public float aimSpeed = 10f;
    public Animator anim;
    private GameObject child;
    private float timestamp;

    public AudioSource gunSound;

    //private bool isAiming = false;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerMotor.isGunActivated) return;

        if(Input.GetMouseButtonDown(0)){
            Shoot();
            timestamp = Time.time + 0.4f;
        }

        Aim(Input.GetMouseButton(1));
    

        //isAiming = false;
    }

    void Shoot(){
        muzzleFlash.Play();
        //anim.Play("Base Layer.Fire", 0, 0.25f);
        RaycastHit hit;

        if(Physics.Raycast(fspCam.transform.position, fspCam.transform.forward, out hit, range)){
            //it hits good
            Target target = hit.transform.GetComponent<Target>();

            if(target != null){
                target.TakeDamage(damage);
            }

            /*if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }*/

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            gunSound.Play();
        }
    }

    void Aim(bool isAiming){
        var start = transform.localPosition;
        var aimpos = transform.localPosition;

        //TODO decrease the mouse sensitivity and the speed

        if(isAiming){
            transform.localPosition = new Vector3(0f, start.y, start.z);
        } else {
            transform.localPosition = new Vector3(0.1f, start.y, start.z);
        }
         
    }
}
