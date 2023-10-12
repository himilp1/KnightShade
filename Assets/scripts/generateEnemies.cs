using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateEnemies : MonoBehaviour{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int maxEnemy;
    void Start(){
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop(){
        while(enemyCount < maxEnemy){
            xPos = Random.Range(-2,15);
            zPos = Random.Range(40, 47);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}