using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOPanel : MonoBehaviour
{
    [SerializeField] private GameObject player, spawner, panelManagerGO, guideTMP;
    [SerializeField] private TextMeshPro scoreValueTM;
    [SerializeField] private TextMeshProUGUI highScoreTMP;
    [SerializeField] private string saveValueName;

    private ObstaclesBehaviour obstaclesBehaviour;
    private PanelManager panelManager;
    private PlayerControls playerControls;
    private SpawnBehaviour spawnBehaviour;

    private void Awake()
    {
        this.initObjects();
    }

    private void initObjects()
	{
        spawnBehaviour = spawner.GetComponent<SpawnBehaviour>();
        playerControls = player.GetComponent<PlayerControls>();
        panelManager = panelManagerGO.GetComponent<PanelManager>();
    }

    public void gameOver(GameObject[] _obstacles, int _speed, bool _disable)
	{
        panelManager.gameOver();
        this.checkIfRecord();

        foreach (GameObject obstacle in _obstacles)
        {
            obstaclesBehaviour = obstacle.GetComponent<ObstaclesBehaviour>();

            if (!obstaclesBehaviour)
            {
                continue;
            }

            obstaclesBehaviour.speedPub = _speed;

            highScoreTMP.text = PlayerPrefs.GetInt("RecordValue").ToString();
        }

        playerControls.enabled = _disable;
        spawnBehaviour.enabled = _disable;
        guideTMP.SetActive(_disable);

        
    }

    private void checkIfRecord()
	{
        int currentValue = int.Parse(scoreValueTM.text);
        int savedRecord = PlayerPrefs.GetInt(saveValueName);

        if(currentValue > savedRecord)
		{
            PlayerPrefs.SetInt(saveValueName, currentValue);
		}
	}
}
