using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private float _rotation_speed = 30f;

    [SerializeField]
    private GameObject _explosion_prefab;

    private GameObject _explosion_instance;

    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();


        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is Null!!");
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        //vector.forward
        transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * _rotation_speed, Space.Self);
   //     transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

     //   if (transform.position.y < -6.0f)
     //   {
     //       transform.position = new Vector3(Random.Range(-9f, +9f), 7.8f, 0);
     //   }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _explosion_instance = Instantiate(_explosion_prefab, transform.position, Quaternion.identity);
            
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);


        
        }
    }

   
    //instantiate explosion
    //clean up explosion after 3 seconds
}
