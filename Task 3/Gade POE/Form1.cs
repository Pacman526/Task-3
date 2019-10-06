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

            map.BattlefieldCreator();
            gameEngine.map = map;
            lblMap.Text = map.PopulateMap(map.units, map.buildings);

            gameEngine.GameLogic(map.units, map.buildings);
            
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


            public void MoveUnit(Unit u, Unit closestUnit)
            {
                for (int i = 0; i < (u.speed); i++)
                {
                    if (((u.Health * 100) / u.maxHealth) <= 25)
                    {
                        if (Math.Abs(u.xPos - closestUnit.xPos) > Math.Abs(u.yPos - closestUnit.yPos))
                        {
                            if ((u.yPos + 1) >= 20 | (u.yPos - 1) <= 0)
                            {
                                if (u.xPos > closestUnit.xPos)
                                {
                                    if (u.xPos + 1 >= 20)
                                    {
                                        u.xPos -= 1;
                                    }
                                    else
                                    {
                                        u.xPos += 1;
                                    }
                                }
                                else
                                {
                                    if (u.xPos - 1 <= 0)
                                    {
                                        u.xPos += 1;
                                    }
                                    else
                                    {
                                        u.xPos -= 1;
                                    }
                                }
                            }
                            else
                            {
                                if (u.yPos > closestUnit.yPos)
                                {
                                    u.yPos += 1;
                                }
                                else
                                {
                                    u.yPos -= 1;
                                }
                            }

                        }
                        else if (Math.Abs(u.xPos - closestUnit.xPos) < Math.Abs(u.yPos - closestUnit.yPos))
                        {
                            if ((u.xPos + 1) >= 20 | (u.xPos - 1) <= 0)
                            {
                                if (u.yPos > closestUnit.yPos)
                                {
                                    if (u.yPos + 1 >= 20)
                                    {
                                        u.yPos -= 1;
                                    }
                                    else
                                    {
                                        u.yPos += 1;
                                    }
                                }
                                else
                                {
                                    if (u.yPos - 1 <= 0)
                                    {
                                        u.yPos += 1;
                                    }
                                    else
                                    {
                                        u.yPos -= 1;
                                    }
                                }
                            }
                            else
                            {
                                if (u.xPos > closestUnit.xPos)
                                {
                                    u.xPos += 1;
                                }
                                else
                                {
                                    u.xPos -= 1;
                                }
                            }

                        }
                    }
                    else
                    {
                        if (Math.Abs(u.xPos - closestUnit.xPos) < Math.Abs(u.yPos - closestUnit.yPos))
                        {

                            if ((u.yPos + 1) >= 20 | (u.yPos - 1) <= 0)
                            {
                                if (u.xPos > closestUnit.xPos)
                                {
                                    if (u.xPos - 1 <= 0)
                                    {
                                        u.xPos += 1;
                                    }
                                    else
                                    {
                                        u.xPos -= 1;
                                    }
                                }
                                else
                                {
                                    if (u.yPos + 1 >= 20)
                                    {
                                        u.xPos -= 1;
                                    }
                                    else
                                    {
                                        u.yPos += 1;
                                    }
                                }
                            }
                            else
                            {
                                if (u.yPos > closestUnit.yPos)
                                {
                                    u.yPos -= 1;
                                }
                                else
                                {
                                    u.yPos += 1;
                                }
                            }

                        }
                        else if (Math.Abs(u.xPos - closestUnit.xPos) < Math.Abs(u.yPos - closestUnit.yPos))
                        {
                            if ((u.xPos + 1) >= 20 | (u.xPos - 1) <= 0)
                            {
                                if (u.yPos > closestUnit.yPos)
                                {
                                    if (u.yPos - 1 <= 0)
                                    {
                                        u.yPos += 1;
                                    }
                                    else
                                    {
                                        u.yPos -= 1;
                                    }

                                }
                                else
                                {
                                    if (u.yPos + 1 >= 20)
                                    {
                                        u.yPos -= 1;
                                    }
                                    else
                                    {
                                        u.yPos += 1;
                                    }
                                }
                            }
                            else
                            {
                                if (u.xPos > closestUnit.xPos)
                                {
                                    u.xPos -= 1;
                                }
                                else
                                {
                                    u.xPos += 1;
                                }
                            }
                        }
                    }
                }



            }

            public void CombatHandler(Unit closestUnit, Unit u)
            {
                closestUnit.Health -= u.attack;
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

            public Unit ClosestUnit(Unit[] units, int numUnits, Unit u)
            {

                double distance = 0;
                int counter = 0;
                double smallestDist;
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

        }

        public class Map
        {
            //CLASS VARIABLES
            public char[,] map = new char[20, 20];
            public int unitAmount;
            public int buildingAmount;
            public Unit[] units;
            public Building[] buildings;

            //CLASS CONSTRUCTOR
            public Map(int _unitAmount, int _buildingAmount)
            {
                unitAmount = _unitAmount;
                buildingAmount = _buildingAmount;
            }

            //CLASS METHODS
            public void BattlefieldCreator()
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        map[i, k] = Convert.ToChar(".");
                    }
                }

                Random rnd = new Random();
                units = new Unit[unitAmount];

                for (int i = 0; i < unitAmount; i++)
                {

                    int x = rnd.Next(0, 20);
                    int y = rnd.Next(0, 20);
                    int team = rnd.Next(0, 2);
                    int unit = rnd.Next(0, 2);

                    if (map[x, y] != '.')
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            x = rnd.Next(0, 20);
                            y = rnd.Next(0, 20);

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
                    int x = rnd.Next(0, 20);
                    int y = rnd.Next(0, 20);
                    int team = rnd.Next(0, 2);
                    int buildingType = rnd.Next(0, 2);
                    

                    if (buildingType == 0)
                    {
                        ResourceBuilding B = new ResourceBuilding(x, y, 25, team, 'O');
                        map[x, y] = B.Symbol ;
                        buildings[j] = B;
                    }
                    else if (buildingType == 1)
                    {
                        FactoryBuilding B = new FactoryBuilding(x, y, 25, team, 'F');
                        map[x, y] = B.Symbol;
                        buildings[j] = B;
                    }
                    
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

                    }
                }

                for (int j = 0; j < 20; j++)
                {
                    for (int l = 0; l < 20; l++)
                    {
                        mapLayout += map[j, l];
                    }
                    mapLayout = mapLayout + "\n";
                }

                return mapLayout;
            }

            public void UpdatePosition(int i, int oldX, int oldY)
            {

                map[units[i].xPos, units[i].yPos] = units[i].symbol;
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

            

            public void GameLogic(Unit[] units, Building [] buildings)
            {

                info = "";
                if (roundCheck > 0)
                {
                    for (i = 0; i < units.Length; i++)
                    {
                        Unit u = (Unit)units[i];

                        x = u.xPos;
                        y = u.yPos;

                        if (u.Health <= 0)
                        {
                            u.Death(units, i);
                        }
                        else
                        {
                            closestUnit = u.ClosestUnit(units, units.Length, u);
                            if (u.RangeCheck(closestUnit, u) == true)
                            {
                                u.combatCheck = true;
                                u.CombatHandler(closestUnit, u);
                            }
                            else
                            {
                                u.MoveUnit(u, closestUnit);
                                map.UpdatePosition(i, x, y);

                            }

                            info += u.ToString(u, units, i);
                        }
                    }

                    for (int k = 0; k < buildings.Length; k++)
                    {
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;

                            if (B.HP < 0)
                            {
                                B.Death(B);
                            }else
                            {
                                info += B.ToString(buildings, B);
                                if (temp > 4)
                                {
                                    temp = 4;
                                    B.GenerateResources();
                                }
                            }
                            
                        }
                        
                        if (buildingType == "FactoryBuilding")
                        {
                            FactoryBuilding B = (FactoryBuilding)b;
                            if (B.HP < 0)
                            {
                                B.Death(B);
                            }else
                            {
                                info += B.ToString(buildings, B);
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
            gameEngine.GameLogic(map.units , map.buildings);
            rtbInfo.Text = gameEngine.info;
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
            protected int xPos;
            protected int yPos;
            protected int HP;
            protected int maxHP;
            protected int team;
            protected char symbol;


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
            public abstract void Death(Building b);
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
            public override void Death(Building B)
            {
                
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
                string info = "ResourceBuidling " + "\n" + "____________" + "\n" + "HP : " + this.HP + "\n" + "Team : " + this.team + "\n" + "Symbol : " + this.symbol;
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
            public override void Death(Building B)
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
                string info = "FactoryBuilding " + "\n" + "____________" + "\n" + "HP : " + this.HP + "\n" +  "Team : "+ this.team + "\n" + "Symbol : " + this.symbol;
                return info;
            }
        }
    }
}
