using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Team _playerTeam;
    [SerializeField] Team _enemyTeam;

    [SerializeField] SelectEntity _selectEntity;


    bool _isPlayerTurn;


    // Start is called before the first frame update
    void Start()
    {
        _isPlayerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerTurn)
        {

            if (_selectEntity._isSelecting)
            {
                if (_selectEntity._isPlayerTeam)
                {
                    Entity player = _playerTeam.SelectEntity();
                    //afficher les compétences du joueur
                }
                else
                {
                    Entity enemy = _enemyTeam.SelectEntity();
                }
            }
        }
    }
}
