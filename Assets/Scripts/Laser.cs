using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 9f;

    private bool _isEnemyLaser = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isEnemyLaser)
        {
            MoveUp();

        }

        else
        {
            MoveDown();

        }
    

    }


    private void MoveUp() {

        transform.Translate(new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 8)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0);

            }
            else
            {

                Destroy(this.gameObject, 0);
            }
        }
    }


    private void MoveDown() {

        transform.Translate(new Vector3(0, -1, 0) * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -10.5)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject, 0);

            }
            else
            {

                Destroy(this.gameObject, 0);
            }
        }

    }
    
    public void SetLaserToEnemy() {
        _isEnemyLaser = true;    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
               
                player.Damage();
            }
                

        }
    }


}
