using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private bool _WantToPlayHard = false;



    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [SerializeField]
    private GameObject _enemyMultyShotPrefab;

    [SerializeField]
    private float _yLaserOffset = -3.05f;

    [SerializeField]
    private float _yMultyLaserOffset = -0.6f;

    [SerializeField]
    private bool _isMultyShotActive = false;

    [SerializeField]
    private float _CoolDownSecondsMin = 3.0f;
    [SerializeField]
    private float _CoolDownSecondsMax = 7.0f;

    private bool _isFiringStopped = false;



    [SerializeField]
    private Player _player;

    private AudioManager _audioManager;

    private AudioSource _audioSource;

    private Animator _animator;
    private GameManager _gameManager;


    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        

        if (_player == null)
        {
            Debug.LogError("_player is null!");
        }


        if (_audioManager == null)
        {
            Debug.LogError("_audioManager is Null!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("_audioSource on the enemy is Null!");
        }

        if (_audioManager != null && _audioSource != null)
        {
            _audioSource.clip = _audioManager._explosionSoundClip;

        }

        if (_gameManager = null)
        {
            Debug.LogError("_gameManager on Enemy is Null!");

        }



        if (_animator == null)
        {
            Debug.LogError("_animator is null!");
        }

        StartCoroutine(FireLaser());

    }

    // Update is called once per frame
    void Update()
    {

        
        
        
        //you could also use Vector3.down
        transform.Translate(new Vector3(0,-1,0) * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {

            float randomX = Random.Range(-9.5f, +9.5f);
            transform.position = new Vector3(randomX, 7, 0);


            if (_WantToPlayHard) //enemy gets faster every time it makes it to the bottom
            {
                _speed = _speed + 1;
            }
        } 



    }

   
    //actually this is private by default, so private is not really required
    //"other" is just a variable. It could also be "idiotThatCollidedWithMe"
    private void OnTriggerEnter2D(Collider2D other)
    {





        if (other.tag == "Player")
        {
           // Player player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                _player.Damage();
            }

            EnemyDeath();
        }

        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);


            if (_player != null)
                {
                    _player.AddtoScore(10);
                }

            EnemyDeath();
            
        }

      


    }


    
    
    private IEnumerator FireLaser()
    {

        while (!_isFiringStopped)
        {
            if (_isMultyShotActive)
            {

                Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + _yMultyLaserOffset, 0);
                GameObject enemyLaser = Instantiate(_enemyMultyShotPrefab, offsetPosition, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
               
                foreach (Laser laser in lasers)
                { 
                laser.SetLaserToEnemy();
                
                }
            }
            else
            {
                Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + _yLaserOffset, 0);
                GameObject enemyLaser = Instantiate(_enemyLaserPrefab, offsetPosition, Quaternion.identity);
                Laser laser = enemyLaser.GetComponent<Laser>();
                laser.SetLaserToEnemy();
            }


            if (_audioManager != null && _audioSource != null)
            {
                _audioSource.Play();
            }

            yield return new WaitForSeconds(Random.Range(_CoolDownSecondsMin, _CoolDownSecondsMax));

        }


    }


    private void EnemyDeath() {

        _speed = 0;
       StopShooting();


        if (_audioManager != null && _audioSource != null)
        {
            _audioSource.Play();
        }

        _animator.SetTrigger("OnEnemyDeath");
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.8f);

    }

    public void StopShooting() {
        _isFiringStopped = true;
    }



}
