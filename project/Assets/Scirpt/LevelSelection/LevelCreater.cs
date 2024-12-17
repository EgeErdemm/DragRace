using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreater : MonoBehaviour
{


    [SerializeField] private Transform PlayerPos,OpponentPos;
    [SerializeField] private GameObject[] OpponentObject;
    [SerializeField]private GameObject[] PlayerObject;

    private int opponentLevel;
    private float opponentAcceleration;


    private float baseAcceleration = 10;
    private float nextLevelCarAccelerate = 5;

    private float OpponentBonus = 5;

    [SerializeField] GameObject Usa;
    [SerializeField] GameObject French
        ;


    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        opponentLevel = (PlayerPrefs.GetInt("OpponentLevel", 1)) % 5; // mevcut shift mekanig 5lik sistemde calisir
        if (opponentLevel == 0)
            opponentLevel = 5;

        opponentAcceleration = baseAcceleration + nextLevelCarAccelerate * CarLevel() + OpponentBonus/opponentLevel;

        StartOpponent();
        StartPlayer();
        if (PlayerPrefs.GetInt("OpponentLevel", 1) >= 21)
            Usa.SetActive(true);
        if (PlayerPrefs.GetInt("OpponentLevel", 1) >= 1)
            French.SetActive(true);
    }

    private int CarLevel()
    {
        return ((PlayerPrefs.GetInt("OpponentLevel", 1) - 1) / 5);
    }

    private void StartOpponent()
    {
        GameObject opponent = Instantiate(OpponentObject[CarLevel()], OpponentPos.position, Quaternion.identity);
        opponent.TryGetComponent(out Movement_Opponent movement_Opponent);
        {
            movement_Opponent.SetAcceleration(opponentAcceleration);
            
        }
        opponent.TryGetComponent(out Shift_Opponent shift_Opponent);
        {
            shift_Opponent.SetOpponentLevel(opponentLevel);
        }
            
    }

    private void StartPlayer()
    {
        GameObject player = Instantiate(PlayerObject[PlayerCarLevel()], PlayerPos.position, Quaternion.identity);
        player.TryGetComponent(out Movement movement);
        {
            if (PlayerPrefs.GetInt("PlayerCarLevel", 0) == 0 && PlayerPrefs.GetInt("Upgrades", 0) > 2) baseAcceleration++;
            float playerAcceleration = baseAcceleration +nextLevelCarAccelerate * PlayerPrefs.GetInt("PlayerCarLevel", 0) + PlayerPrefs.GetInt("Upgrades",0);
            movement.PlayerAcceleration(playerAcceleration);
            movement.UpgradeTopSpeed(PlayerPrefs.GetInt("Upgrades", 0));
        }

    }

    private int PlayerCarLevel()
    {
        return PlayerPrefs.GetInt("PlayerCarLevel", 0);
    }

}
