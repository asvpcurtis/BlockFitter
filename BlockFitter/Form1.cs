using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string configFilename = "../../config2.json";
            string json = System.IO.File.ReadAllText(configFilename);
            Config config = JsonConvert.DeserializeObject<Config>(json);
            IBlockFitter hillClimber = new GeneticBlockFitter(new SpaceUncoveredHeuristic(), 100);
            //IBlockFitter hillClimber = new HillClimbingBlockFitter(new SpaceUncoveredHeuristic());
            State solution = hillClimber.Climb(config.Problem, 1000);
            //State solution = config.Problem.GetRandomState(new Random());
            foreach (BlockShape bs in solution.pieces)
            {
                Console.WriteLine("Piece");
                foreach (Unit u in bs.Units)
                {
                    Console.WriteLine($"({u.X}, {u.Y})");
                }
            }

            this.pbxResult.SizeChanged += (s, e) =>
            {
                DrawState(solution);
            };
            DrawState(solution);
        }
        private SolidBrush RandomColorBrush (Random r)
        {
            int red = r.Next(256);
            int green = r.Next(256);
            int blue = r.Next(256);
            return new SolidBrush(Color.FromArgb(red, green, blue));
        }

        public void DrawState(State state)
        {
            int xOffset = state.container.Left();
            int yOffset = state.container.Top();
            int containerWidth = state.container.Right() - xOffset + 1;
            int containerHeight = state.container.Bottom() - yOffset + 1;
            float xRatio = pbxResult.Width / containerWidth;
            float yRatio = pbxResult.Height / containerHeight;
            float blockSize = Math.Min(xRatio, yRatio);
            Rectangle UnitRectangle(Unit u)
            {
                return new Rectangle((int)((u.X - xOffset) * blockSize),
                    (int)((u.Y - yOffset) * blockSize), (int)blockSize, (int)blockSize);
            }
            if (pbxResult.Width <= 0 || pbxResult.Height <= 0)
            {
                return;
            }
            Bitmap buffer = new Bitmap(pbxResult.Width, pbxResult.Height);
            Random r = new Random();
            Pen outlinePen = new Pen(Color.Black);
            SolidBrush containerBrush = new SolidBrush(Color.White);

            using (Graphics g = Graphics.FromImage(buffer))
            {
                g.Clear(Color.DarkGray);
                foreach (Unit u in state.container.Units)
                {
                    Rectangle ur = UnitRectangle(u);
                    g.FillRectangle(containerBrush, ur);
                    g.DrawRectangle(outlinePen, ur);
                }
                foreach (BlockShape bs in state.pieces)
                {
                    SolidBrush pieceBrush = RandomColorBrush(r);
                    foreach (Unit u in bs.Units)
                    {
                        Rectangle ur = UnitRectangle(u);
                        g.FillRectangle(pieceBrush, ur);
                        g.DrawRectangle(outlinePen, ur);
                    }
                }
            }
            pbxResult.Image = buffer;
        }
    }
}
