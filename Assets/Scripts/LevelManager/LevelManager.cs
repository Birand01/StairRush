using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    [Header("Level Objects & Obstacles")]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material obstaclesMaterial;
    [SerializeField] Material brickMaterial;
    [SerializeField] Material playerMaterial;
    [SerializeField] Material basketMaterial;
    //[SerializeField] Image progressbarFillImage;
    [Space]
    [Header("Level Colors")]
    [SerializeField] Color groundColor;
    [SerializeField] Color obstacleColor;
    [SerializeField] Color playerColor;
    [SerializeField] Color basketColor;
    //[SerializeField] Color progressbarColor;
    [SerializeField] Color brickColor;


    private void Awake()
    {
        UpdateLevelColor();
    }

    private void OnEnable()
    {
        BrickStack.OnPlayerVictoryAnimation += LoadNextLevel;
        Obstacle.OnLevelRestart += RestartLevel;
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation += LoadNextLevel;
    }


    /// <summary>
    /// LOADING NEXT LEVEL
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    IEnumerator LoadNextLevelCoroutine()
    {
        
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        int lastLevel = SceneManager.GetActiveScene().buildIndex;
        if (lastLevel == 2)
        {
            SceneManager.LoadScene(0);
        }
    }


    //-----------------------------------------------------------------------------

    /// <summary>
    /// RESTART LEVEL
    /// </summary>
    public void RestartLevel()
    {
       StartCoroutine(RestartLevelCoroutine());

       
    }
    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //-----------------------------------------------------------------------------------

    /// <summary>
    ///  UPDATING LEVEL OBJECTS' COLOR
    /// </summary>
    private void UpdateLevelColor()
    {
        groundMaterial.color = groundColor;
        playerMaterial.color = playerColor;
        basketMaterial.color = basketColor;
       // progressbarFillImage.color = progressbarColor;
        obstaclesMaterial.color=obstacleColor;
        brickMaterial.color=brickColor;

    }

    private void OnValidate()
    {
        UpdateLevelColor();
    }




    private void OnDisable()
    {
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation -= LoadNextLevel;
        BrickStack.OnPlayerVictoryAnimation -= LoadNextLevel;
        Obstacle.OnLevelRestart -= RestartLevel;
    }

   
}
