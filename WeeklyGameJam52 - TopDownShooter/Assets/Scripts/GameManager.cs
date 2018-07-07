using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int wave;

	public Text scoreNum;

	int numberOfEnemies;

	public static int numberOfEnemiesLeft = 0;
	public static int score;


	EnemySpawn[] enemySpawns;

	// Use this for initialization
	void Start () {
		enemySpawns = FindObjectsOfType<EnemySpawn>();
        

	}
	
	// Update is called once per frame
	void Update () {
		scoreNum.text = score.ToString();


		if(numberOfEnemiesLeft <= 0){
			NewWave();

		}
	}

	void NewWave(){
		wave++;
		numberOfEnemies = wave * 2;
		StartCoroutine(SpawnEnemies());
	}


	IEnumerator SpawnEnemies(){
		for (int i = 0; i < numberOfEnemies; i++){
			int randomSpawnPos = Random.Range(0, enemySpawns.Length);
			enemySpawns[randomSpawnPos].SpawnNextEnemy();
			yield return new WaitForSeconds(1f);
		}

		yield break;
	}


}
