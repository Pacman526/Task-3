﻿using System;



namespace Gade_POE
{
    class FactoryBuilding : Building
        {
            //CLASS VARIABLES
            public int unitType;
            public int productionSpeed;
            public int spawnPointX, spawnPointY;
            Unit u;

            //CLASS CONSTRUCTOR
            public FactoryBuilding(int _xPos, int _yPos, int _HP, int _team, char _symbol) : base(_xPos, _yPos, _HP, _team, _symbol)
            {
                
            }
            //Class Accessors
            public int XPos { get => base.xPos; set => base.xPos = value; }
            public int YPos { get => base.yPos; set => base.yPos = value; }
            public int HP { get => base.HP; set =>base. HP = value; }
            public int MaxHP { get => base.maxHP;}
            public int Team { get => base.team; set => base.team = value; }
            public char Symbol { get => base.symbol; set => base.symbol = value; }

            public int ProductionSpeed { get => productionSpeed; }
            
            //CLASS METHODS
            public override void Death(Building B, int k, Building[] buildings)
            {
                //when a building is destroyed
                if (this.HP < 0)
                {
                    
                }
            }

            public override void save()
            {

            }

            public Unit SpawnUnit(FactoryBuilding B)
            {
                //spawns units
                spawnPointX = B.xPos;
                spawnPointY = B.yPos + 1;

                Random rnd = new Random();
                unitType = rnd.Next(0, 2);

                if (spawnPointY > 20)
                {
                    spawnPointY -= 2; 
                }

                if (unitType == 0)
                {
                    u = new Unit(spawnPointX, spawnPointY, 20, 1, 2, 1, Team, Convert.ToChar("M"), false, "MeleeUnit");

                }
                else if (unitType == 0)
                {
                    u = new Unit(spawnPointX, spawnPointY, 10, 1, 3, 5, Team, Convert.ToChar("R"), false, "RangedUnit");

                }
                return u;
            }
            public override string ToString(Building[] buildings, Building b)
            {
            
                //string with building info
                string info = "FactoryBuilding " + "\n" + "____________" + "\n" + "HP : " + b.HP + "\n" +  "Team : "+ b.team + "\n" + "Symbol : " + b.symbol + "\n"+ "\n";
                return info;
            }
        }
    
}
