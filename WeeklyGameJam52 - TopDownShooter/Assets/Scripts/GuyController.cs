using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyController : MonoBehaviour {
    public float speed = 5f;
    public Gun weaponSlot1, weaponSlot2, weaponSlot3;
    public Transform gunPos;


    Gun currentGun;

    float fireNewTime = 0;
    Transform gunMuzzle;
    GameObject gunGun;

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;

        //temporary gunSetup
        ChangeWeapon(1);

	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Shooting();
        ChoosingWeapon();
	}


    void ChoosingWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {

            ChangeWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            ChangeWeapon(3);
        }


    }


    void ChangeWeapon(int weaponNum) {
        if (gunGun != null)
        {
            Destroy(gunGun.gameObject);
            gunGun = null;
        }

        Debug.Log("Change Weapon to Weapon #" + weaponNum);
        if (weaponNum == 1)
        {
            gunGun = Instantiate(weaponSlot1.gunObject, gunPos.position, gunPos.rotation);
            gunMuzzle = gunGun.GetComponentInChildren<Muzzle>().transform;
            gunGun.transform.parent = transform;
            currentGun = weaponSlot1;

        }
        else if (weaponNum == 2) {

            gunGun = Instantiate(weaponSlot2.gunObject, gunPos.position, gunPos.rotation);
            gunMuzzle = gunGun.GetComponentInChildren<Muzzle>().transform;
            gunGun.transform.parent = transform;
            currentGun = weaponSlot2;

        }
        else if (weaponNum == 3)
        {

            gunGun = Instantiate(weaponSlot3.gunObject, gunPos.position, gunPos.rotation);
            gunMuzzle = gunGun.GetComponentInChildren<Muzzle>().transform;
            gunGun.transform.parent = transform;
            currentGun = weaponSlot3;

        }

    }





    void Shooting() {
        if (currentGun != null)
        {
            if (currentGun.isAutomatic)
            {
                if (Input.GetButton("Fire1"))
                {


                    if (Time.time > fireNewTime + (1 / currentGun.fireRate))
                    {
                        Debug.Log("Fire with " + currentGun.gunName + " for " + currentGun.damage);
                        GameObject bullet = Instantiate(currentGun.bullet, gunMuzzle.position, gunMuzzle.rotation);
                        Bullet bulletScript = bullet.GetComponent<Bullet>();
                        bulletScript.SetDamage(currentGun.damage, currentGun.bulletSpeed);


                        fireNewTime = Time.time;
                    }
                }
            }
            else {
                if (Input.GetButtonDown("Fire1")) {

                    if (Time.time > fireNewTime + (1 / currentGun.fireRate))
                    {
                        Debug.Log("Fire with  " + currentGun.gunName + " for " + currentGun.damage);
                       


                            GameObject bullet = Instantiate(currentGun.bullet, gunMuzzle.position, gunMuzzle.rotation);
                            Bullet bulletScript = bullet.GetComponent<Bullet>();
                            bulletScript.SetDamage(currentGun.damage, currentGun.bulletSpeed);
                        

                        fireNewTime = Time.time;
                    }
                }
            }
        }
    }


    void Movement() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePos.Normalize();
        float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

    }
}
