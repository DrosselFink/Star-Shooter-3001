using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;


    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;


    // Start is called before the first frame update
    void Start()
    {
        
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void UpDateScoreDisplay(int scoreToDisplay)
    {
        _scoreText.text = "Score: " + scoreToDisplay;
    }

    public void UpDateLivesDisplay(int livesToDisplay)
    {
        _livesImg.sprite = _liveSprites[livesToDisplay];
    }

    public void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine()) ;
    }

    IEnumerator GameOverFlickerRoutine()
    { int failsave =  10;
        while (failsave > 0)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            failsave--;
        }
    }

}
