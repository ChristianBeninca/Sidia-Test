using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string charName;
    public int health;
    public int attack;
    public int idPosition;

    public Player(string name, int hp, int ap, int pos)
    {
        charName = name;
        health = hp;
        attack = ap;
        idPosition = pos;
    }
}
