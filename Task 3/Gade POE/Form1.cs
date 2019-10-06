using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Gade_POE
{
    public partial class Form1 : Form
    {
        GameEnigine gameEngine = new GameEnigine();
        Map map = new Map(10,4);

        public Form1()
        {
            InitializeComponent();

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            int mapXSize = 20;
            int mapYSize = 20;
            map.BattlefieldCreator(mapXSize, mapYSize);
            gameEngine.map = map;
            lblMap.Text = map.PopulateMap(map.units, map.buildings);

            gameEngine.GameLogic(map.units, map.buildings, mapXSize, mapYSize);
            rtbBuildingInfo.Text = gameEngine.buildingInfo;
            rtbInfo.Text = gameEngine.info;

        }

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

                if ((u.Health*100)/u.maxHealth < 25)
                {
                    u.combatCheck = false;
                    Random rnd = new Random();

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
                else
                if (u.xPos > closestUnit.xPos)
                {
                    u.xPos = u.xPos - u.speed;
                }else
                if (u.xPos < closestUnit.xPos)
                {
                    u.xPos = u.xPos + u.speed;
                }else
                if (u.yPos > closestUnit.yPos)
                {
                    u.yPos = u.yPos - u.speed;
                }else
                if (u.yPos < closestUnit.yPos)
                {
                    u.yPos = u.yPos + u.speed;
                }



            }

            public void CombatHandler(Unit closestUnit, Unit u, int unitAmount, Unit[] units)
            {

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
                closestUnit.Health = closestUnit.Health - u.attack;
            }

            public bool RangeCheck(Unit closestUnit, Unit u)
            {
                if (u.attackRange >= Math.Sqrt(Math.Pow((closestUnit.xPos - u.xPos), 2) + Math.Pow((closestUnit.yPos - u.yPos), 2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public Unit ClosestUnit(Unit[] units, int numUnits, Unit _u, Building [] buildings)
            {

                double distance = 0;
                int counter = 0;
                double smallestDist;
                Unit u = _u;
                Unit closestUnit = u;
                

                smallestDist = 15;
                
                for (int j = 0; j < units.Length; j++)
                {
                    if (units[counter].team != u.team)
                    {
                        distance = Math.Sqrt(Math.Pow((units[counter].xPos - u.xPos), 2) + Math.Pow((units[counter].yPos - u.yPos), 2));
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

                return closestUnit;
                
            }

            public void Death(Unit[] units, int i)
            {

                for (int k = i; k < units.Length - 1; k++)
                {
                    units[k] = units[k + 1];
                }
            }

            public string ToString(Unit u, Unit[] units, int i)
            {
                info = "";

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
                closestBuilding.HP = closestBuilding.HP - u.attack;
            }

            public bool BuildingRangeCheck(Building closestBuilding, Unit u)
            {

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
                if (u.xPos > closestBuilding.xPos)
                {
                    u.xPos = u.xPos - u.speed;
                }

                if (u.xPos < closestBuilding.xPos)
                {
                    u.xPos = u.xPos + u.speed;
                }

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

        public class Map
        {
            //CLASS VARIABLES
            public char[,] map;
            public int unitAmount;
            public int buildingAmount;
            public Unit[] units;
            public Building[] buildings;
            public int mapXSize, mapYSize;
            public int wizardAmount;

            //CLASS CONSTRUCTOR
            public Map(int _unitAmount, int _buildingAmount)
            {
                unitAmount = _unitAmount;
                buildingAmount = _buildingAmount;
            }

            //CLASS METHODS
            public void BattlefieldCreator(int _mapXSize, int _mapYSize)
            {
                mapXSize = _mapXSize;
                mapYSize = _mapYSize;
                map = new char[mapXSize, mapYSize];

                for (int i = 0; i < mapXSize; i++)
                {
                    for (int k = 0; k < mapYSize; k++)
                    {
                        map[i, k] = Convert.ToChar(".");
                    }
                }

                Random rnd = new Random();
                wizardAmount = rnd.Next(2, 6);
                units = new Unit[unitAmount + wizardAmount];

                for (int i = 0; i < unitAmount; i++)
                {

                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int unit = rnd.Next(0, 2);

                    if (map[x, y] != '.')
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            x = rnd.Next(0, mapXSize);
                            y = rnd.Next(0, mapYSize);

                            if (map[x, y] == '.')
                            {
                                k = 1000;
                            }
                        }
                    }

                    if (unit == 1 & team == 0)
                    {
                        Unit RangedUnit = new Unit(x, y, 10, 1, 3, 5, team, Convert.ToChar("R"), false, "RangedUnit");
                        map[x, y] = RangedUnit.symbol;
                        units[i] = RangedUnit;
                    }

                    if (unit == 1 & team == 1)
                    {
                        Unit RangedUnit = new Unit(x, y, 10, 1, 3, 5, team, Convert.ToChar("r"), false, "RangedUnit");
                        map[x, y] = RangedUnit.symbol;
                        units[i] = RangedUnit;
                    }

                    if (unit == 0 & team == 0)
                    {
                        Unit MeleeUnit = new Unit(x, y, 20, 1, 2, 1, team, Convert.ToChar("M"), false, "MeleeUnit");
                        map[x, y] = MeleeUnit.symbol;
                        units[i] = MeleeUnit;
                    }

                    if (unit == 0 & team == 1)
                    {
                        Unit MeleeUnit = new Unit(x, y, 20, 1, 2, 1, team, Convert.ToChar("m"), false, "MeleeUnit");
                        map[x, y] = MeleeUnit.symbol;
                        units[i] = MeleeUnit;

                    }
                }

                buildings = new Building[buildingAmount];
                

                for (int j = 0; j < buildings.Length; j++)
                {
                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int buildingType = rnd.Next(0, 2);
                    

                    if (buildingType == 0)
                    {
                        ResourceBuilding B = new ResourceBuilding(x, y, 100, team, 'O');
                        map[x, y] = B.Symbol ;
                        buildings[j] = B;
                    }
                    else if (buildingType == 1)
                    {
                        FactoryBuilding B = new FactoryBuilding(x, y, 100, team, 'F');
                        map[x, y] = B.Symbol;
                        buildings[j] = B;
                    }
                    
                }

                for (int j = 0; j < wizardAmount ; j++)
                {

                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int unit = rnd.Next(0, 2);

                    if (map[x, y] != '.')
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            x = rnd.Next(0, mapXSize);
                            y = rnd.Next(0, mapYSize);

                            if (map[x, y] == '.')
                            {
                                k = 1000;
                            }
                        }
                    }
                    Unit WizardUnit = new Unit(x, y, 10, 1, 2, 1, 3, 'W', false, "WizardUnit");
                    map[x, y] = WizardUnit.symbol;
                    units[unitAmount + j] = WizardUnit;
                }
            }

            public string PopulateMap(Unit[] units, Building[] buildings)
            {
                string mapLayout = "";

                for (int k = 0; k < units.Length; k++)
                {

                    if (units[k].Health > 0)
                    {
                        map[units[k].xPos, units[k].yPos] = units[k].symbol;
                    }
                    else
                    {
                        map[units[k].xPos, units[k].yPos] = '.';
                    }
                }

                for (int j = 0; j < mapXSize; j++)
                {
                    for (int l = 0; l < mapYSize; l++)
                    {
                        mapLayout += map[j, l];
                    }
                    mapLayout = mapLayout + "\n";
                }

                return mapLayout;
            }

            public void UpdatePosition(Unit u, int oldX, int oldY)
            {

                map[u.xPos, u.yPos] =   u.symbol;
                map[oldX, oldY] = '.';
            }


        }
        class GameEnigine
        {
            public int roundCheck;
            public string info;
            Unit closestUnit;
            public int x, y, i;
            public Map map;
            public int temp;
            int mapXSize, mapYSize;
            public bool buildingCheck;
            Building closestBuilding;
            public string buildingInfo;



            public void GameLogic(Unit[] units, Building [] buildings, int _mapXSize, int _mapYSize)
            {
                mapXSize = _mapXSize;
                mapYSize = _mapYSize;

                info = "";
                buildingInfo = "";
                if (roundCheck > 0)
                {
                    for (i = 0; i < units.Length - map.wizardAmount ; i++)
                    {
                        Unit u = (Unit)units[i];

                        for (int k = 0; k < buildings.Length; k++)
                        {
                            if (u.team != buildings[k].team)
                            {
                                buildingCheck = true;
                            }
                        }

                        x = u.xPos;
                        y = u.yPos;

                        if (u.Health <= 0)
                        {
                            u.Death(units, i);
                        }
                        else if (buildingCheck == true)
                        {
                            closestBuilding = u.ClosestBuilding(u, buildings);
                            if (u.BuildingRangeCheck(closestBuilding, u) == true)
                            {
                                u.combatCheck = true;
                                u.BuildingCombat(closestBuilding, u);
                                buildingCheck = false;
                            }
                            else
                            {
                                u.MoveToBuilding(u, closestBuilding);
                                map.UpdatePosition(u, x, y);
                                buildingCheck = false;
                                u.combatCheck = false;
                            }
                        }
                        else
                        {
                            closestUnit = u.ClosestUnit(units, units.Length, u, buildings);
                            if (u.RangeCheck(closestUnit, u) == true)
                            {
                                u.combatCheck = true;
                                u.CombatHandler(closestUnit, u, map.unitAmount, units);
                            }
                            else
                            {
                                u.MoveUnit(u, closestUnit, mapXSize, mapYSize);
                                map.UpdatePosition(u, x, y);
                                
                            }    
                        }

                        info += u.ToString(u, units, i);
                    }

                    for (int k = 0; k < buildings.Length; k++)
                    {
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "Form1+ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;
                            if (B.HP > 0)
                            {
                                buildingInfo += B.ToString(buildings, B);
                                if (temp > 4)
                                {
                                    temp = 4;
                                    B.GenerateResources();
                                }
                            }
                            else
                            {
                                B.Death(B, k,buildings);
                            }
                            
                        }
                        if (buildingType == "Form1+FactoryBuilding")
                        {
                            FactoryBuilding B = (FactoryBuilding)b;
                            if (B.HP > 0)
                            {
                                buildingInfo += B.ToString(buildings, B);
                                
                            }else
                            {
                                B.Death(B, k, buildings);
                            }
                        }
                    }

                    for (int h = 0; h < map.wizardAmount; h++)
                    {
                        Unit u = (Unit)units[map.unitAmount + h];

                        x = u.xPos;
                        y = u.yPos;

                        if ((u.Health *100/ u.maxHealth) > 50)
                        {
                            closestUnit = u.ClosestUnit(units, units.Length, u, buildings);
                            if (u.RangeCheck(closestUnit, u) == true)
                            {
                                u.combatCheck = true;
                                u.CombatHandler(closestUnit, u, map.unitAmount, units);
                            }
                            else
                            {
                                u.MoveUnit(u, closestUnit, mapXSize, mapYSize);
                                map.UpdatePosition(u, x, y);

                            }
                        }
                    }
                }
                else
                {
                    for (i = 0; i < units.Length; i++)
                    {
                        Unit u = (Unit)units[i];
                        info += u.ToString(u, units, i);
                    }

                    for (int k = 0; k < buildings.Length; k++)
                    {
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "Form1+ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;
                            buildingInfo += B.ToString(buildings, B);
                        }

                        if (buildingType == "Form1+FactoryBuilding")
                        {
                            FactoryBuilding B = (FactoryBuilding)b;
                            buildingInfo += B.ToString(buildings, B);
                        }
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Timer.Enabled = true;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameEngine.roundCheck += 1;
            gameEngine.temp += 1;
            rtbInfo.Clear();
            gameEngine.GameLogic(map.units , map.buildings, map.mapXSize, map.mapYSize);
            rtbInfo.Text = gameEngine.info;
            rtbBuildingInfo.Text = gameEngine.buildingInfo;
            lblMap.Text = map.PopulateMap(map.units, map.buildings);
            lblScore.Text = "Round : " + gameEngine.roundCheck;
            

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Timer.Enabled = false;
            Timer.Stop();
        }



        abstract public class Building
        {
            //CLASS VARIABLES 
            public int xPos;
            public int yPos;
            public int HP;
            public int maxHP;
            public int team;
            public char symbol;


            //CLASS CONSTRUCTOR
            public Building(int _xPos, int _yPos, int _HP, int _team, char _symbol)
            {
                xPos = _xPos;
                yPos = _yPos;
                HP = _HP;
                team = _team;
                symbol = _symbol;

            }

            //CLASS METHODS
            public abstract void save();
            public abstract void Death(Building b, int k, Building[] buildings);
            public abstract string ToString(Building[] buildings, Building b);     
        }
    
        class ResourceBuilding : Building
        {
            //CLASS VARIABLES
            public string resourceType = "Wood";
            public int resourcesGenerated = 0;
            public int generatedPerRound = 2;
            public int remainingResources = 100;

            //CLASS CONSTRUCTOR
            public ResourceBuilding(int _xPos, int _yPos, int _HP, int _team , char _symbol) : base(_xPos, _yPos, _HP, _team , _symbol)
            {
            
            }

            public int XPos { get => base.xPos; set => base.xPos = value; }
            public int YPos { get => base.yPos; set => base.yPos = value; }
            public int HP { get => base.HP; set => base.HP = value; }
            public int MaxHP { get => base.maxHP; }
            public int Team { get => base.team; set => base.team = value; }
            public char Symbol { get => base.symbol; set => base.symbol = value; }

            //CLASS METHODS
            public override void Death(Building B,int k, Building[] buildings)
            {
                for (int i = k; i < buildings.Length - 1; i++)
                {
                    buildings[i] = buildings[i + 1];
                }

            }

            public override void save()
            {

            }

            public void GenerateResources()
            {
                resourceType = "WOOD";
                resourcesGenerated += generatedPerRound;
                remainingResources -= generatedPerRound;
            }

            public override string ToString(Building [] buildings, Building b)
            {
                string info = "ResourceBuidling " + "\n" + "____________" + "\n" + "HP : " + b.HP + "\n" + "Team : " + b.team + "\n" + "Symbol : " + b.symbol + "\n" + "\n";
                return info;
            }
        }


        class FactoryBuilding : Building
        {
            //CLASS VARIABLES
            public int unitType;
            public int productionSpeed;
            public int spawnPointX, spawnPointY;

            //CLASS CONSTRUCTOR
            public FactoryBuilding(int _xPos, int _yPos, int _HP, int _team, char _symbol) : base(_xPos, _yPos, _HP, _team, _symbol)
            {
                
            }

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
                if (this.HP < 0)
                {
                    
                }
            }

            public override void save()
            {

            }

            public Unit SpawnUnit()
            {
                spawnPointX = xPos;
                spawnPointY = YPos + 1;
                Unit u = new Unit(0, 0, 0, 0, 0, 0, 0, '0', false, "Unit");

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
                string info = "FactoryBuilding " + "\n" + "____________" + "\n" + "HP : " + b.HP + "\n" +  "Team : "+ b.team + "\n" + "Symbol : " + b.symbol + "\n"+ "\n";
                return info;
            }
        }
    }
}
