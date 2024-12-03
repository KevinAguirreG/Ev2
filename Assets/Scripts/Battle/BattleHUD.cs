using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text pokemonName;
    public Text pokemonLevel;
    public HealthBar healthBar;
    public Text pokemonHealth;

    private Pokemon _pokemon;

    public void setPokemonData(Pokemon pokemon)
    {
        _pokemon = pokemon;

        pokemonName.text = pokemon.Base.Name;
        pokemonLevel.text = $"Lv {pokemon.Level}";
        UpdatePokemonData();
    }

    public void UpdatePokemonData()
    {
        healthBar.SetHP((float)_pokemon.HP / _pokemon.MaxHP);
        pokemonHealth.text = $"{_pokemon.HP}/{_pokemon.MaxHP}";
    }
}

