using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BlockFitter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string configFilename = "../../config.json";
            string json = System.IO.File.ReadAllText(configFilename);
            Config config = JsonConvert.DeserializeObject<Config>(json);
            //foreach(Unit u in config.Container.Units)
            //{
            //    Console.WriteLine($"({u.X}, {u.Y})");
            //}
            foreach(BlockShape bs in config.Pieces)
            {
                Console.WriteLine("Piece");
                foreach(Unit u in bs.Units)
                {
                    Console.WriteLine($"({u.X}, {u.Y})");
                }
            }
        }

    }
}
