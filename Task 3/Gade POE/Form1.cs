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
        Map map = new Map(10, 4);

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
            gameEngine.GameLogic(map.units, map.buildings, map.mapXSize, map.mapYSize);
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

    }
    
}
