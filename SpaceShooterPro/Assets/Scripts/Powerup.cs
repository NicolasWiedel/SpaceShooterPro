using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed  = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when we leave the Screen, destry thie object
        if(transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // OnTriggerCollision
    // only be collectable by the player (Tags)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Kollision mit dem Spieler!");
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.ActivateTrippleShot();
            }
        }
    }
}
