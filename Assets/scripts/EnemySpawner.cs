using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;

    CameraScript camScript;

    float cooldownTimer;

    public GameHandler gamehandlerScript;

    // Use this for initialization
    void Start()
    {
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        gamehandlerScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameHandler>();
        cooldownTimer = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer == 0)
            SpawnEnemy();

        Cooldown();
    }

    void Cooldown()
    {
        float destinationValue = 2;

        if (gamehandlerScript.score >= 500)
        {
            destinationValue = 1.25f;
        }

        if (cooldownTimer > 0 && cooldownTimer < destinationValue)
            cooldownTimer += Time.deltaTime;
        else if (cooldownTimer >= destinationValue)
            cooldownTimer = 0;
    }

    void SpawnEnemy()
    {

        int selectedEnemyType = Random.Range(0, MaximumRange());

        Bounds Enemybounds = enemyTypes[selectedEnemyType].GetComponent<BoxCollider2D>().bounds;

        Vector2 spawnRange = new Vector2(camScript.camSize.y - (Enemybounds.size.y * 2), -camScript.camSize.y + (Enemybounds.size.y * 2));

        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(spawnRange.y, spawnRange.x));

        GameObject createdEnemy = Instantiate(enemyTypes[selectedEnemyType]);
        createdEnemy.transform.position = spawnPosition;

        cooldownTimer += Time.deltaTime;
    }

    int MaximumRange()
    {
        if (gamehandlerScript.score < 60)
            return 1;
        else if (gamehandlerScript.score >= 60 && gamehandlerScript.score < 140)
            return 2;
        else if (gamehandlerScript.score >= 140 && gamehandlerScript.score < 250)
            return 3;
        else if (gamehandlerScript.score >= 250)
            return 4;

        return 0;
    }
}
