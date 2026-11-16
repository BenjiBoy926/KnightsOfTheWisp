using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    private Battle _battle;

    public void Prepare(Battle battle)
    {
        _battle = battle;
        _battle.ClearFactions();
        RegisterAllCharacters();
        RegisterAllSquads();
    }

    public void Begin()
    {
        // Clear history
        _battle.StartPlayerTurn();
        RecordInitialState();
    }

    private void RegisterAllCharacters()
    {
        Character[] characters = GetComponentsInChildren<Character>();
        foreach (var character in characters)
        {
            _battle.Register(character);
        }
    }

    private void RegisterAllSquads()
    {
        Squad[] squads = GetComponentsInChildren<Squad>();
        foreach (var squad in squads)
        {
            _battle.Register(squad);
        }
    }

    private void RecordInitialState()
    {
        _battle.RecordInitialState();
    }
}