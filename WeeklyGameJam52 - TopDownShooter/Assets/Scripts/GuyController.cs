using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuyController : MonoBehaviour {
	public int health = 5;
    public float speed = 5f;
	public Ammo[] ammo;
	public Transform gun;
    public Transform gunMuzzle;

	public Text ammo1num, ammo2num, ammo3num;
	public Image[] ammoBGHighlight;


	int currentAmmoIndex;
	public int[] ammoNum;

	Ammo currentAmmo;

    Camera cam;
	public SpriteRenderer guySP;
	public Transform gunTrans;
	public Sprite leftRightSprite, upDownSprite;

	// Use this for initialization
	void Start () {
        cam = Camera.main;

		currentAmmo = ammo[0];
		UpdateAmmoHighlight(0);
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Shooting();
        ChoosingWeapon();
		UpdateUI();
	}


    void ChoosingWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			currentAmmoIndex = 0;
			UpdateAmmoHighlight(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {

			currentAmmoIndex = 1;
			UpdateAmmoHighlight(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

			currentAmmoIndex = 2;
			UpdateAmmoHighlight(2);
        }


    }


    





    void Shooting() {


                if (Input.GetButtonDown("Fire1"))
                {


			    if (ammoNum[currentAmmoIndex] > 0)
                    {
				
				    GameObject bullet = Instantiate(ammo[currentAmmoIndex].bulletToSpawn , gunMuzzle.position, gunMuzzle.rotation);
                        Bullet bulletScript = bullet.GetComponent<Bullet>();
				        bulletScript.SetDamage(currentAmmo.damage, currentAmmo.bulletSpeed);
				ammoNum[currentAmmoIndex]--;
                    }
                }
            }
            
   


    void Movement() {
		//Move Player
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
		transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;
		if(h < 0){
			guySP.flipX = true;         
		} else if(h > 0) {
			guySP.flipX = false;
		}
		if(v != 0 && h == 0){
			guySP.sprite = upDownSprite;
			if(v < 0){
				guySP.flipY = true;
			} else {
				guySP.flipY = false;
			}
		} else {
			guySP.sprite = leftRightSprite;

		}



		//RotateGun
		Vector3 gunScale = gunTrans.localScale;
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		//Debug.Log("Player Position " + transform.position + " Mouse Pos is " + mousePos);




		if(Input.mousePosition.x < (Screen.width/2)){
			mousePos.Normalize();
            float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			gun.rotation = Quaternion.Euler(0, 0, rotZ - 90);
			gunScale.x = -1;         
		} else {
			mousePos.Normalize();
            float rotZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			gun.rotation = Quaternion.Euler(0, 0, rotZ - 90);
			gunScale.x = 1;         
		}

		gunTrans.localScale = gunScale;

    }


	void UpdateUI(){
		ammo1num.text = ammoNum[0].ToString();
		ammo2num.text = ammoNum[1].ToString();
		ammo3num.text = ammoNum[2].ToString();


	}

	void UpdateAmmoHighlight(int ammoIndex){
		for (int i = 0; i < ammoBGHighlight.Length; i++){
			ammoBGHighlight[i].gameObject.SetActive(false);
		}

		ammoBGHighlight[ammoIndex].gameObject.SetActive(true);

	}

	public void TakeDamage(float newDamage){
		health -= (int)newDamage;

	}
}
