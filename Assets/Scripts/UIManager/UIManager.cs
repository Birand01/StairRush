using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [SerializeField] ParticleSystem levelEndParticle;
    [SerializeField] TMP_Text scoreText, currentLevelText, nextLevelText, levelFailText, levelSuccessText;
    [SerializeField] Image progressFillBarImage;
    [SerializeField] Transform playerCurrentPosition, levelEndPosition;
    [Space]
    private int scoreAmount;
    [SerializeField] int sceneOffset;

    private void OnEnable()
    {
        BrickStack.OnPlayerBrickAmountZero += PlayEndLevelParticle;
        BrickStack.OnPlayerBrickAmountZero += LevelSuccessTextVisibility;
        CoinManager.OnCoinsCollect += ScoreToAdd;
        Obstacle.OnPlayerRagdoll += LevelFailTextVisibility;
        scoreAmount = 0;
        scoreText.text = scoreAmount.ToString();
       
    }
    private void Awake()
    {
        progressFillBarImage.fillAmount = 0;
       
    }
    private void PlayEndLevelParticle()
    {
        levelEndParticle.Play();
    }
   
    private void LevelFailTextVisibility()
    {
        levelFailText.gameObject.SetActive(true);
    }

    private void LevelSuccessTextVisibility()
    {
        levelSuccessText.gameObject.SetActive(true);
    }

    private void Start()
    {
        SetLevelProgressText();
        if (PlayerPrefs.HasKey("Gold"))
        {
            CoinManager.Instance.CoinCount = PlayerPrefs.GetInt("Gold");
            scoreText.text = CoinManager.Instance.CoinCount.ToString();
        }
    }
    private void Update()
    {
        UpdateProgressLevel();
    }
    private void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

    }

    private void UpdateProgressLevel()
    {
        float val =((float)playerCurrentPosition.position.z/levelEndPosition.position.z);
        progressFillBarImage.DOFillAmount(val, 0.1f);
    }


    private void ScoreToAdd(int value)
    {
       
        scoreText.text = CoinManager.Instance.CoinCount.ToString();
    }

    private void OnDisable()
    {
        BrickStack.OnPlayerBrickAmountZero -= LevelSuccessTextVisibility;
        Obstacle.OnPlayerRagdoll -= LevelFailTextVisibility;
        BrickStack.OnPlayerBrickAmountZero -= PlayEndLevelParticle;
        CoinManager.OnCoinsCollect -= ScoreToAdd;
    }
}
