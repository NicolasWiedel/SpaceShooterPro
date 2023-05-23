using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (it, float, bool, string)
    // every variale has an name
    // optional value assiged
    [SerializeField]
    private float _speed = 5f;
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool isTrippleShotActive = false;
    [SerializeField]
    private bool isSpeedBoostActive = false;
    [SerializeField]
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    // Update is called once per frame
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
       
        

        //if (transform.position.y >= 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, 0);
        //}
        //else if (transform.position.y <= -3.8f)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //}

        // Das selbe, wie das obere if - Statement
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -3.8f, 0),
            0);

        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if (isTrippleShotActive)
        {
            Instantiate(_trippleShotPrefab,
                transform.position,
                Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab,
               transform.position + new Vector3(0, 1.05f, 0),
               Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (!isShieldActive)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
            if (_lives < 1)
            {
                _spawnManager.OnPlayerDead();
                Destroy(this.gameObject);
            }
        }
        else if (isShieldActive)
        {
            isShieldActive = false;
            _shield.SetActive(false);
        }
        
    }
    public void ActivateTrippleShot()
    {
        // trippleShotActive becomes true
        isTrippleShotActive = true;
        // start the powerup coroutine for tripple shot
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    // IEnumerator TrippleShotPowerDownRoutine
    // wait 5 seconds
    // set trippleShotActive to false
    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTrippleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerupRoutine());
    }

    IEnumerator SpeedBoostPowerupRoutine()
    {
        _speed *= _speedMultiplier;
        yield return new WaitForSeconds(10.0f);
        isSpeedBoostActive = false;
        _speedMultiplier /= _speedMultiplier;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddEnemyKillScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
