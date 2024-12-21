using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastScript : MonoBehaviour
{
    public float RotationBorder = 45.0f;
    public float RotationCurrent = 0.0f;

    public int Ammo = 2;

    public GameObject Shotgun;
    public ParticleSystem Muzzle;

    public AudioSource Sounds;
    public AudioClip Fire;
    public AudioClip Reload;

    public bool ReloadCooldown;
    public bool FireCooldown;

    public Text AmmoText;
    public Text HPText;
    public int AmmoDisplay;
    public int HPDisplay;


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = Shotgun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AmmoText.text = "Ammo: " + AmmoDisplay + "/2";
        HPText.text = "Lives: " + HPDisplay;
        Ray Mouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit MouseDetect = new RaycastHit();

        // (Left & Right Camera Movement) \\
        float MouseX = Input.GetAxis("Mouse X") * 3.0f; // How quick your mouse can move (3.0f)
        RotationCurrent += MouseX;
        RotationCurrent = Mathf.Clamp(RotationCurrent, -RotationBorder, RotationBorder);
        transform.localRotation = Quaternion.Euler(19.661f, RotationCurrent, 0);
        // -------------------------------- \\ 

        if (Physics.Raycast(Mouse, out MouseDetect)) 
        {
            GameObject CurrentObject = MouseDetect.collider.gameObject;

            if (Input.GetMouseButtonDown(0) && !FireCooldown) 
            {
                if (Ammo == 0 && !ReloadCooldown) 
                {
                    StartCoroutine(ReloadTime(0.9f));
                }

                else if (Ammo > 0)
                {
                    if (CurrentObject.CompareTag("Robot"))
                    {
                        Debug.Log("Robot Shot");
                        Destroy(CurrentObject);
                        StartCoroutine(ShotTime(0.3f));
                    }
                    else 
                    {
                       StartCoroutine(ShotTime(0.3f));
                    }
                    
                }
                
                // Muzzle.Play();
                // animator.SetTrigger("GunFire");

                

            }
        }
    }

    private IEnumerator ReloadTime(float WaitTime) 
    {
        Sounds.PlayOneShot(Reload);
        animator.SetTrigger("Reloading");
        ReloadCooldown = true;
        yield return new WaitForSeconds(WaitTime);
        ReloadCooldown = false;
        Ammo = 2;
        AmmoDisplay += 2;
    }

    private IEnumerator ShotTime(float WaitTime)
    {
        //yield return new WaitForSeconds(WaitTime);
        Muzzle.Play();
        animator.SetTrigger("GunFire");
        Sounds.PlayOneShot(Fire);
        FireCooldown = true;
        Ammo -= 1;
        AmmoDisplay -= 1;
        yield return new WaitForSeconds(WaitTime);
        FireCooldown = false;
    }
}
