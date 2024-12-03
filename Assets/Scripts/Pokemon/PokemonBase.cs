using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Nuevo Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] private int id;

    [SerializeField] private string name;

    public string Name => name;

    [TextArea][SerializeField] private string description;

    public string Description => description;


    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    public Sprite FrontSprite => frontSprite;
    public Sprite BackSprite => backSprite;

    [SerializeField] private PokemonType type1;
    public PokemonType Type1 => type1;


    [SerializeField] private PokemonType type2;
    public PokemonType Type2 => type2;


    //Stats
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    [SerializeField] private int attack;
    public int Attack => attack;
    [SerializeField] private int defense;
    public int Defense => defense;
    [SerializeField] private int spAttack;
    public int SpAttack => spAttack;
    [SerializeField] private int spDefense;
    public int SpDefense => spDefense;
    [SerializeField] private int speed;
    public int Speed => speed;

    [SerializeField] private List<LearnableMove> learnableMoves;

    public List<LearnableMove> LearnableMoves => learnableMoves;

}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Figth,
    Ice,
    Poison,
    Ground,
    Fly,
    Psychic,
    Rock,
    Bug,
    Ghost,
    Dragon,
    Dark,
    Steel
}

[Serializable] 
public class LearnableMove
{
    [SerializeField] private MoveBase _move;
    public MoveBase Move => _move;

    [SerializeField] private int level;
    public int Level => level;
}
