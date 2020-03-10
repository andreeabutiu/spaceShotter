using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

     private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -5.5f)
        {
             transform.position = new Vector3(Random.Range(-11.5f,11.5f),5.5f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "player")
         {
             Player player = other.transform.GetComponent<Player>();

             if (player != null)
             {
                 player.Damage();
             }
             
            Destroy(this.gameObject);
             

             
             
         }

         if (other.tag == "laser")
         {
               Destroy(other.gameObject);

               if (_player != null)
               {
                   _player.addScore();
               }

               Destroy(this.gameObject);
         }
    }

}
