using System;



namespace Gade_POE
{
    public class Unit
        {
            //CLASS VARIABLES 
            public int xPos, yPos, Health, maxHealth, speed, attack, attackRange;
            public int team;
            public char symbol;
            public bool combatCheck;
            public string info;
            public int count;
            public string name;
            public bool buildingCheck;


            //CLASS CONSTRUCTOR
            public Unit(int _xPos, int _yPos, int _health, int _speed, int _attack, int _attackRange, int _team, char _symbol, bool _combatCheck, string _name)
            {
                xPos = _xPos;
                yPos = _yPos;
                Health = _health;
                maxHealth = _health;
                speed = _speed;
                attack = _attack;
                attackRange = _attackRange;
                team = _team;
                symbol = _symbol;
                combatCheck = _combatCheck;
                name = _name;
            }

            //CLASS METHODS
            
            public void save()
            {

            }

            public void MoveUnit(Unit u, Unit closestUnit, int mapXSize, int mapYSize)
            {
            //unit movement
            Random rnd = new Random();
            if (u.name == "WizardUnit" && ((u.Health * 100) / u.maxHealth < 50))
            {
                int temp = rnd.Next(0, 4);
                if (temp == 0)
                {
                    if (u.xPos - u.speed < 0)
                    {

                    }
                    else
                        u.xPos = u.xPos - u.speed;
                }
                else if (temp == 1)
                {
                    if (u.yPos - u.speed < 0)
                    {

                    }
                    else
                        u.yPos = u.yPos - u.speed;
                }
                else if (temp == 2)
                {
                    if (u.xPos + u.speed > mapXSize)
                    {

                    }
                    else
                        u.xPos = u.xPos + u.speed;
                }
                else if (temp == 3)
                {
                    if (u.yPos + u.speed > mapYSize)
                    {

                    }
                    else
                        u.yPos = u.yPos + u.speed;
                }
            }
            else
            if ((u.Health*100)/u.maxHealth < 25)
                {
                //movement when running away
                u.combatCheck = false;
                    
                    int temp = rnd.Next(0, 4);
                    if (temp == 0)
                    {
                        if (u.xPos - u.speed < 0)
                        {

                        }
                        else
                            u.xPos = u.xPos - u.speed;
                    }
                    else if(temp == 1)
                    {
                        if (u.yPos - u.speed < 0)
                        {

                        }
                        else
                            u.yPos = u.yPos - u.speed;
                    }
                    else if (temp == 2)
                    {
                        if (u.xPos + u.speed > mapXSize)
                        {

                        }
                        else
                            u.xPos = u.xPos + u.speed;
                    }
                    else if (temp == 3)
                    {
                        if (u.yPos + u.speed > mapYSize)
                        {

                        }
                        else
                            u.yPos = u.yPos + u.speed;
                    }
                }
                else  //movement when moving to target
                    for (int k = 0; k < u.speed; k++)
                    {
                        if (u.xPos > closestUnit.xPos)
                        {
                            u.xPos = u.xPos - 1;
                        }
                        else
                        if (u.xPos < closestUnit.xPos)
                        {
                            u.xPos = u.xPos + 1;
                        }
                        else
                        if (u.yPos > closestUnit.yPos)
                        {
                            u.yPos = u.yPos - 1;
                        }
                        else
                        if (u.yPos < closestUnit.yPos)
                        {
                            u.yPos = u.yPos + 1;
                        }
                    }
            }

            public void CombatHandler(Unit closestUnit, Unit u, int unitAmount, Unit[] units)
            {
                //wizard combat
                if (u.name == "WizardUnit")
                {
                    for (int i = 0; i < unitAmount ; i++)
                    {
                        if (units[i].xPos == u.xPos && units[i].yPos == u.yPos || units[i].xPos -1 == u.xPos && units[i].yPos == u.yPos || units[i].xPos  == u.xPos && units[i].yPos -1 == u.yPos || units[i].xPos + 1 == u.xPos && units[i].yPos == u.yPos || units[i].xPos == u.xPos && units[i].yPos + 1 == u.yPos
                            || units[i].xPos - 1 == u.xPos && units[i].yPos -1 == u.yPos || units[i].xPos - 1 == u.xPos && units[i].yPos + 1 == u.yPos || units[i].xPos + 1 == u.xPos && units[i].yPos +1 == u.yPos || units[i].xPos + 1 == u.xPos && units[i].yPos -1 == u.yPos)
                        {
                            units[i].Health -= u.attack;
                        }
                    }
                }
                else
                //unit combat
                closestUnit.Health = closestUnit.Health - u.attack;
            }

            public bool RangeCheck(Unit closestUnit, Unit u, Unit [] wizards)
            {
                //checks that target is in units range
                if (u.attackRange >= Math.Sqrt(Math.Pow((closestUnit.xPos - u.xPos), 2) + Math.Pow((closestUnit.yPos - u.yPos), 2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public Unit ClosestUnit(Unit[] units, int numUnits, Unit _u, Building [] buildings, Unit[] wizards)
            {
                //finds the closest unit to the unit
                double distance = 0;
                int counter = 0;
                double smallestDist;
                Unit u = _u;
                Unit closestUnit = u;
                

                smallestDist = 15;

            if (u.name== "WizardUnit")
            {
                for (int h = 0; h < units.Length; h++)
                {

                    if (units[h].team != u.team && units[h].Health > 0)
                    {
                        distance = Math.Sqrt(Math.Pow((units[h].xPos - u.xPos), 2) + Math.Pow((units[h].yPos - u.yPos), 2)); //dist formula
                        if (distance < smallestDist)
                        {
                            smallestDist = distance;
                            closestUnit = units[h];
                        }
                    }
                }
                return closestUnit;

            }
             else   
                for (int j = 0; j < units.Length; j++)
                {
                    
                    if (units[counter].team != u.team && units[counter].Health > 0)
                    {
                        distance = Math.Sqrt(Math.Pow((units[counter].xPos - u.xPos), 2) + Math.Pow((units[counter].yPos - u.yPos), 2)); //dist formula
                        if (distance < smallestDist)
                        {
                            smallestDist = distance;
                            closestUnit = units[j];
                        }
                        counter += 1;
                    }
                    else
                    {
                        counter += 1;
                    }
                }

                for (int i = 0; i < wizards.Length; i++)
                {
                    distance = Math.Sqrt(Math.Pow((wizards[i].xPos - u.xPos), 2) + Math.Pow((wizards[i].yPos - u.yPos), 2)); //dist formula
                    if (distance < smallestDist)
                    {
                    smallestDist = distance;
                    closestUnit = wizards[i];
                    }
                }       

                return closestUnit;
                
            }

            public void Death(Unit[] units, int i)
            {
                //when a unit dies
                for (int k = i; k < units.Length - 1; k++)
                {
                    units[k] = units[k + 1];
                }
            }

            public string ToString(Unit u, Unit[] units, int i)
            {

                info = "";
                //creates string with unit info
                if (u.Health <= 0)
                {

                }
                else
                {
                    info += u.name + "\n" + "____________" + "\n" + "Hp : " + u.Health + "\n" + "Damage : " + u.attack + "\n" + "Team : " + (u.team + 1) + "\n" + "In Combat : " + u.combatCheck + "\n" + "Symbol: " + u.symbol; ;
                    info = info + "\n" + "\n";
                }

                return info;
            }


            public Building ClosestBuilding(Unit _u, Building[] buildings )
            {
                //finds closest building
                double distance = 0;
                int counter = 0;
                double smallestDist;
                Unit u = _u;
                Building closestBuilding = buildings[1];
                smallestDist = 15;


                for (int j = 0; j < buildings.Length; j++)
                {
                    if (buildings[counter].HP   > 0)
                    {
                        if (buildings[counter].team != u.team)
                        {
                            distance = Math.Sqrt(Math.Pow((buildings[counter].xPos - u.xPos), 2) + Math.Pow((buildings[counter].yPos - u.yPos), 2));
                            if (distance < smallestDist)
                            {
                                smallestDist = distance;
                                closestBuilding = buildings[counter];
                            }
                            counter += 1;
                        }
                        else
                        {
                            counter += 1;
                        }

                    }
                       
                }

                return closestBuilding;
            }


            public void BuildingCombat(Building closestBuilding, Unit u)
            {
                //when a unit attacks a building
                closestBuilding.HP = closestBuilding.HP - u.attack;
            }

            public bool BuildingRangeCheck(Building closestBuilding, Unit u)
            {
                //checks that building is in units range
                if (u.attackRange >= Math.Sqrt(Math.Pow((closestBuilding.xPos - u.xPos), 2) + Math.Pow((closestBuilding.yPos - u.yPos), 2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void MoveToBuilding(Unit u, Building closestBuilding)
            {
            //moves unit closer to the building
            for (int i = 0; i < u.speed; i++)
            {

                if (u.xPos > closestBuilding.xPos)
                {
                    u.xPos = u.xPos - u.speed;
                }
                else
                if (u.xPos < closestBuilding.xPos)
                {
                    u.xPos = u.xPos + u.speed;
                }
                else
                if (u.yPos > closestBuilding.yPos)
                {
                    u.yPos = u.yPos - u.speed;
                }

                if (u.yPos < closestBuilding.yPos)
                {
                    u.yPos = u.yPos + u.speed;
                }

            }
            
            }

    }
    
}
