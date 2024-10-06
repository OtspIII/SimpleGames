using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //I can use a static variable to make this available
      //from anywhere in my code
    public static GameManager Me;
    
    //The prefab for the monster that spawns
    public EnemyScript EnemyPrefab;
    //How long until the next monster spawns?
    public float SpawnTimer=0;
    //Once a monster spawns, how long until the next?
    public float SpawnMaxTimer=3;
    //Where do the monsters spawn?
    public Vector3 EnemySpawnPos;

    //A link to the player
    public PlayerScript Player;
    //Keep a list of all the monsters
    public List<EnemyScript> AllEnemies;
    
    void Start()
    {
        //Register myself to the static variable
        GameManager.Me = this;
    }

    void Update()
    {
        //The spawn timer always counts down in real time
        SpawnTimer -= Time.deltaTime;
        //When it hits 0. . .
        if (SpawnTimer <= 0)
        {
            //Reset it
            SpawnTimer = SpawnMaxTimer;
            //Spawn an enemy at the EnemySpawnPos position
            EnemyScript e = Instantiate(EnemyPrefab,
                EnemySpawnPos, Quaternion.identity);
            //50/50 chance you turn the enemy around
            if(Random.Range(0,1f) < 0.5f)
                e.TurnAround();
        }
    }
}
