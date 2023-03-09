using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 8.0f; // ---> Lazer'in gidiş hızı.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.up * _speed * Time.deltaTime); // ---> Lazer'in gidiş yönü ve hızı ayarı.

       if(transform.position.y > 8f)
       {
            if(transform.parent != null) // ---> Üst klasör null'a eşit değilse.
            {
                Destroy(transform.parent.gameObject); // ---> Parent'i yok et.
            }
            Destroy(this.gameObject); // ---> Lazer ekrandan çıktığı anda siliniyor.
       }
    }
}
