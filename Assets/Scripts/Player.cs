using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int healthPoints;
    int attackPoints;
    string name;

    public Player(string name, int hp, int ap )
    {
        healthPoints = hp;
        attackPoints = ap;
        this.name = name;
    }
}
