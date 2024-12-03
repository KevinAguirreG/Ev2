using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon 
{
    private PokemonBase _base;
    public PokemonBase Base => _base;   

    private int _level;
    public int Level
    {
        get => _level; set => _level = value;
    }

    private List<Move> _moves;
    public List<Move> Moves
    {
        get => _moves;
        set => _moves = value;
    }

    private int _hp;

    public int HP
    {
        get => _hp;
        set => _hp = value;
    }

    public Pokemon(PokemonBase pokemonBase, int pokemonLevel)
    {
        _base = pokemonBase;
        _level = pokemonLevel;

        _hp = MaxHP;

        _moves = new List<Move>();

        foreach(var move in _base.LearnableMoves)
        {
            if(move.Level <= _level)
            {
                _moves.Add(new Move(move.Move));
            }

            if(_moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int MaxHP => Mathf.FloorToInt((_base.MaxHP* _level) /20.0f)+10;
    public int Attack => Mathf.FloorToInt((_base.Attack*_level)/100.0f)+2;
    public int Defense => Mathf.FloorToInt((_base.Defense*_level)/100.0f)+1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack*_level)/100.0f)+1;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense*_level)/100.0f)+1;
    public int Speed => Mathf.FloorToInt((_base.Speed*_level)/100.0f)+1;


    public bool ReciveDamage( Pokemon attacker, Move move)
    {
        float modifiers = Random.Range(0.85f, 1.0f);
        float baseDamage = (2*attacker.Level/5f+2)*move.Base.Power*(attacker.Attack/(float)Defense)/50f+2;
        int totalDamage = Mathf.FloorToInt(baseDamage * modifiers);

        HP -= totalDamage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
            return false;
    }

    public Move RandomMove()
    {
        int randId = Random.Range(0, Moves.Count);
        return Moves[randId];
    }
}
