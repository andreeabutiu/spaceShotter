using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public or private reference
    //data type(int, float, bool, strings)
    //every variable has a name
    //optional value assigned
    [SerializeField]
     private float _speed = 3.5f;
     [SerializeField]
     private float _fireRate = 0.5f;
     private float _canFire = -1f;

    [SerializeField]
     private GameObject _laserPrefab;

    [SerializeField]
     private int _lives = 3;

     private SpawnManager _spawnManager;

    [SerializeField]
    private int _score; 
     
     private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position(0,0,0)
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    
       if(_spawnManager == null)
       {
           Debug.LogError("The Spawn Manager is NULL");
       }

       if(_uiManager == null)
       {
           Debug.LogError("The UI Manager is NULL");
       }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time> _canFire))
        {
            FireLaser();
        }
    }
    void CalculateMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * horizontalInput * _speed* Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed* Time.deltaTime);
        //transform.Translate(new Vector3(1,0,0)) * 5 * real time
        //16frame/s ... 1m in the real word ..1m/frame  16m/s
        //convert to 1m/s
        //Time.deltaTime is going to do the conversion process

         Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
         transform.Translate(direction * _speed * Time.deltaTime);

 
         transform.position = new Vector3(Mathf.Clamp(transform.position.x,-10f,10f), Mathf.Clamp(transform.position.y,-4f, 0f), 0);
  
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
    }

    public void Damage()
    {
       _lives=_lives-1 ;

       _uiManager.UpdateLives(_lives);

       if (_lives < 1)
       {
           _spawnManager.OnPlayerDeath();
           Destroy(this.gameObject);
       }
    }

    public void addScore()
    {
        _score=_score+10;
        _uiManager.UpdateScore(_score);
    }

    
}
