using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _anim;
    [SerializeField]
    private GameObject _explosionPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>(); // ---> Tag'i Player olan nesneyi bul Componentlerine ulaş.
       
        if (_player == null)
        {
            Debug.LogError("The Player is Null");
        }

        _anim = GetComponent<Animator>(); // ---> Animator zaten Enemy'nin içinde oşduğu için ona ulaşmak için direkt GetComponent'i kullanıyoruz.

        if (_anim == null)
        {
            Debug.LogError("_anim is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -8.0f) // ---> y düzleminde -8.0f'den küçük ise tekrar başladığı yere dönecek..
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0); // ---> x düzleminde rastgele yerlerden gelecek.
             
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // ---> Lazer ya da karakterler birbirine çarparsa yok olacak.
    {
        if(other.tag == "Player") // ---> Player'a herhangi bir obje çarparsa Enemy'i' yok et.
        {
            Player player = other.transform.GetComponent<Player>();// ---> Lives'ı etkinleştirmek için foksyon çağırma metodu
            
            if (player != null) // ---> Nesne Player'a eşit değilse Damage al.
            {
                player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity); // ---> Astreoid lazer ile çarpıştığında exploionPrefab'i çağır.
            
            Destroy(this.gameObject); // ---> Astreoid'i yok et.
            //_anim.SetTrigger("OnEnemyDeath"); // ---> Enemy'e Player çarptığında Destroyed anim devreye girecek.
            //_speed = 0.5f;
            //Destroy(this.gameObject, 2.8f);
        }

        if(other.tag == "Laser") // ---> Laser herhangi bir şeye çarparsa lazer'i yok et
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10); // ---> Tag'i Player olan nesneyi bul Componentlerine ulaştık ve fonksyonu kullanabildik.
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity); // ---> Astreoid lazer ile çarpıştığında exploionPrefab'i çağır.
            Destroy(other.gameObject); // ---> Lazer'i yok et.
            Destroy(this.gameObject); // ---> Astreoid'i yok et.
            //_anim.SetTrigger("OnEnemyDeath"); // ---> Enemy'e Lazer çarptığında Destroyed anim devreye girecek.
            //_speed = 0.5f;
            //Destroy(this.gameObject, 2.8f); // ---> Lazer bir şeye çarptığı zaman lazer'i de yok et.
        }


    }

}
