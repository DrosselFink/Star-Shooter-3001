using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _PowerUpId;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.6f)
        {
            Destroy(this.gameObject);
        }
 

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

                

                switch (_PowerUpId)
                {
                    case 0:     // "TrippleShotPowerUp"
                        player.ActivateTrippleShot();
                        break;
                    case 1:  // "SpeedPowerUp"
                        player.ActivateSpeedBoost();
                        break;
                    case 2:  //"ShieldPowerUp"
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Whoops, no defined behaviour for this PowerUp");
                            break;

                }

            }

            Destroy(this.gameObject);
        }
    }
}
