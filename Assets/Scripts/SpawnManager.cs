using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUps;



    private bool _stopSpawning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());

    }

    IEnumerator SpawnEnemyRoutine()
    {
        

        yield return new WaitForSeconds(4.0f);

        while (_stopSpawning == false)
        {

            float randomX = Random.Range(-9.5f, +9.5f);
            Vector3 spawnPosition = new Vector3(randomX, 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
 
            yield return new WaitForSeconds(5.0f);
            
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-9.4f, +9.4f);
            float randomTime = Random.Range(+3.0f, +7.0f);

            Vector3 SpawnPosition = new Vector3(randomX, 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerup], SpawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(randomTime);
        }
    
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
