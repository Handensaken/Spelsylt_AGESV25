using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player1;
    public GameObject player2;

    [Header("SpawnPoints")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    [Header("Win Round")]
    public TextMeshProUGUI winText;
    public float respawnDeley;
    [Header("Win Game")]
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private TextMeshProUGUI _winGameText;

    [Header("Points Gui")]
    public TextMeshProUGUI player1PointsText;
    public TextMeshProUGUI player2PointsText;
    private float _Player1Points = 0;
    private float _Player2Points = 0;

    void Start()
    {
        GameEventsManager.instance.OnPlayerDeath += OnPlayerDeath;
        SetPointsText();
    }
    void OnDisable()
    {
        GameEventsManager.instance.OnPlayerDeath -= OnPlayerDeath;
    }
    void OnPlayerDeath(GameObject player)
    {
        if (player2 != player)
        {
            _Player2Points++;
            winText.text = player2.name + " won this game";
        }
        else
        {
            _Player1Points++;
            winText.text = player1.name + " won this game";
        }
        SetPointsText();
        if (_Player1Points == 3 || _Player2Points == 3)
        {
            WinGame();
        }
        else
        {
            Invoke(nameof(Respawn), 3f);
        }
    }
    void Respawn()
    {
        winText.text = "";
        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);

        SetPosition();
    }
    private void WinGame()
    {
        if (_winMenu != null)
        {
            _winMenu.SetActive(true);
        }

        if (_Player1Points == 3)
        {
            if (_winGameText != null)
            {
                _winGameText.text = player1.name + " Won This Game!";
            }
        }
        else if (_Player2Points == 3)
        {
            if (_winGameText != null)
            {
                _winGameText.text = player2.name + " Won This Game!";
            }
        }
    }
    void SetPointsText()
    {
        if (player1PointsText != null)
        {
            player1PointsText.text = _Player1Points.ToString();
        }
        if (player2PointsText != null)
        {
            player2PointsText.text = _Player2Points.ToString();
        }
    }
    void SetPosition()
    {
        player1.transform.position = spawnPoint1.position;
        player2.transform.position = spawnPoint2.position;
    }
}
