using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class Level
{
    public List<InitialBrick> Bricks { get; }
    
    public String Name { get; set; } 
    
    public int CountTypes { get; } // Кол-во типов
    
    public int Complexity { get; set; } // Уровень сложности 

    public Level(String name, List<InitialBrick> bricks, int countTypes, int complexity)
    {
        Name = name;
        Bricks = bricks;
        CountTypes = countTypes;
        Complexity = complexity;
    }
}
