using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText; // ---> Score variable'ı atandı daha sonra değişebilmesi için.
    [SerializeField]
    private Image _LivesImg; // ---> Lives imglarının değişkeni.
    [SerializeField]
    private Sprite[] _liveSprites; // ---> 4 tane Can img olduğu için direkt olarak array içine alacaz.
    [SerializeField]
    private Text _gameOverText; // ---> GameOver Text'in değişkeni.
    [SerializeField]
    private Text _restartGameText;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
       _scoreText.text = "Score : " + 0;  // ---> UIManager'ın içindeki text'e ulaşabilmek için _scoreText.text yazdık.
       _gameOverText.gameObject.SetActive(false);
       _restartGameText.gameObject.SetActive(false);
       _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

       if (_gameManager == null)
       {
            Debug.LogError("Game Manager is null");
       }
    }

    public void UpdateScore(int playerScore) // ---> Score'un sürekli güncellenmesi gerekiyor. Bu yüzden yeni Fonksyon atadık.
    {
        _scoreText.text = "Score : " + playerScore.ToString(); 
    }  

    public void UpdateLives(int currentLives) // ---> Resmi 0-1-2-3 ile değiştirdiğimiz yer.
    {
        _LivesImg.sprite = _liveSprites[currentLives]; // ---> Resmin indexlerinin değiştiği yer.

        if (currentLives == 0)
        {
            StartCoroutine(ShowGameOverTextAfterDelay());
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        StartCoroutine(ShowGameOverTextAfterDelay());
        _gameManager.GameOver();
        // _gameOverText.gameObject.SetActive(true); // ---> CurrentLives 0'a eşit olduğu anda GameOverText'i görünür yap.
        // _restartGameText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine() // ---> Öldükten sonraki GameOver yazısını loop'a sokacaz.
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShowGameOverTextAfterDelay() // ---> Game Over yazısı 2 saniye sonra gelecek.
    
    {
        yield return new WaitForSeconds(2f);
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
    }
}
