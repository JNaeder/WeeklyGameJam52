using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health;
	public float speed;
	public float damageToPlayer;
	public int points;
	public Transform healthBarGreen;
	public GameObject[] drops;


	float healthPerc, startHealthNum;

	Transform target;

	// Use this for initialization
	void Start () {
		startHealthNum = health;
		target = FindObjectOfType<GuyController>().transform;

		GameManager.numberOfEnemiesLeft++;
	}
	
	// Update is called once per frame
	void Update () {
		SetUpHealth();
		FollowPlayer();

	}


	void SetUpHealth(){
		healthPerc = health / startHealthNum;

		Vector3 healthBarScale = healthBarGreen.localScale;
		healthBarScale.x = healthPerc;
		healthBarGreen.localScale = healthBarScale;
	}


	public void TakeDamage(float damage){
		health -= damage;
		if(health <= 0){
			Death();

		}
	}


	void Death()
	{
		GameManager.numberOfEnemiesLeft--;
		GameManager.score += points;
		Destroy(gameObject);

		DropItems();
	}

	void DropItems(){
		int randNum = Random.Range(1, 4);
		for (int i = 0; i < randNum; i++){


		}

		Vector2 randomDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		float randomSpeed = Random.Range(1f, 3f);

	}

	void FollowPlayer(){
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			GuyController guy = collision.gameObject.GetComponent<GuyController>();
			guy.TakeDamage(damageToPlayer);
		}
	}
}
