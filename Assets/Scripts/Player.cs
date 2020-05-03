using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _speedBoost = 5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _trippleShotPrefab;

    [SerializeField]
    private float _yLaserOffset = 1.05f;

    [SerializeField]
    private float _yTrippleLaserOffset = 0.6f;

    [SerializeField]
    private float _fireRate = 0.15f;

    private float _canFire = -1.0f;

    private SpawnManager _spawnManager;

    private bool _isTrippleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;


    [SerializeField]
    private GameObject _thruster;

    [SerializeField]
    private int _score;


    private UIManager _uiManager;

    private GameManager _gameManager;

    private AudioManager _audioManager;

    private AudioSource _audioSource;

   private AudioSource _audioSourcePowerUp;

    private Animator _animator;





    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
         _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSourcePowerUp = GameObject.Find("PowerUp_Sound").GetComponent<AudioSource>();

        _animator = GetComponent<Animator>();
       

        // _shieldVisualizer = transform.GetChild(0).gameObject;



        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is Null!!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("_uiManager is Null!");

        }

        if (_gameManager == null)
        {
            Debug.LogError("_gameManager is Null!");
        }


        if (_audioManager == null)
        {
            Debug.LogError("_audioManager is Null!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("_audioSource on the player is Null!");
        }

       else if (_audioManager != null && _audioSource != null)
        {
            _audioSource.clip = _audioManager._laserSoundClip;
        }


        if (_audioSourcePowerUp == null)
        {
            Debug.LogError("_audioSourcePowerUp on the player is Null!");
        }

       else if (_audioManager != null && _audioSourcePowerUp != null)
        {
            _audioSourcePowerUp.clip = _audioManager._powerUpSoundClip;
        }



        if (_animator == null)
        {
            Debug.LogError("_animator on Player is null!");
        }


    }

    // Update is called once per frame. Each Second has 60 frames
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();

        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        transform.Translate(direction * _speed * Time.deltaTime);



        //Keep Player within boundries:

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));

        if (transform.position.x > 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;



        if (_isTrippleShotActive)
        {

            Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + _yTrippleLaserOffset, 0);
            Instantiate(_trippleShotPrefab, offsetPosition, Quaternion.identity);

        }
        else {
            Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + _yLaserOffset, 0);
            Instantiate(_laserPrefab, offsetPosition, Quaternion.identity);
        }


        if (_audioManager != null && _audioSource != null)
        {
            _audioSource.Play();
        }



    }


    public void Damage()
    {
        if (_isShieldActive)
        {
            if (_shieldVisualizer != null)
            {
                _shieldVisualizer.SetActive(false);
            }
            _isShieldActive = false;
            return;
        }


        if (_lives > 0)
        {
            _lives--;
            _uiManager.UpDateLivesDisplay(_lives);
        }

        if (_lives == 2)
        {
            if (_leftEngine != null)
            {
                _leftEngine.SetActive(true);
            }
        
        }

        else if (_lives == 1)
        {
            if (_rightEngine != null)
            {
                _rightEngine.SetActive(true);
            }

        }


        if (_lives < 1)
        {
            StartCoroutine(PlayerDeathRoutine());
        }
    }


    public void ActivateTrippleShot()
    {
        _isTrippleShotActive = true;
        StartCoroutine(StopTrippleShotAfterTimeRoutine(3.0f));
        PlayPowerUpSound();

    }

    IEnumerator StopTrippleShotAfterTimeRoutine(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        _isTrippleShotActive = false;
    }


    public void ActivateSpeedBoost()
    {
        _isSpeedBoostActive = true;
        _speed = _speed + _speedBoost;
        StartCoroutine(StopSpeedBoostAfterTimeRoutine(5.0f));
        PlayPowerUpSound();
    }


    IEnumerator StopSpeedBoostAfterTimeRoutine(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        _speed = _speed - _speedBoost;
        _isSpeedBoostActive = false;
    }

    IEnumerator PlayerDeathRoutine()
    {

        _animator.SetTrigger("OnPlayerDeath");
        

    

        if (_shieldVisualizer != null)
        {
            Destroy(_shieldVisualizer);
        }


        yield return new WaitForSeconds(0.3f);

        _speed = 0;


        if (_thruster != null)
        {
            Destroy(_thruster);     
        }



        if (_leftEngine != null)
        {
            Destroy(_leftEngine);
        }


        if (_rightEngine != null)
        {
            Destroy(_rightEngine);
        }

        yield return new WaitForSeconds(3.0f);
        _spawnManager.OnPlayerDeath();
        _uiManager.GameOverSequence();
        _gameManager.GameOver();

        Destroy(this.gameObject);
    }

    public void ActivateShield()
    {
        _isShieldActive = true;

        if (_shieldVisualizer != null)
        {
            _shieldVisualizer.SetActive(true);
        }
        PlayPowerUpSound();
    }

    public void AddtoScore(int points)
    {
        _score = _score + points;

        if (_uiManager != null)
        {
            _uiManager.UpDateScoreDisplay(_score);
        }


    }

  private void PlayPowerUpSound()
    {

 

        if (_audioManager != null && _audioSourcePowerUp != null)
        {

            _audioSourcePowerUp.Play();

            //alternative Option:
           // AudioSource.PlayClipAtPoint(_audioManager._powerUpSoundClip, transform.position, 1.0F);
        }

    }

    






}
