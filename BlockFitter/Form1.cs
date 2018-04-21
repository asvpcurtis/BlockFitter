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
            string configFilename = "../../config.json";
            string json = System.IO.File.ReadAllText(configFilename);
            Config config = JsonConvert.DeserializeObject<Config>(json);
            IBlockFittingHeuristic heuristic;
            IBlockFitter hillClimber;
            switch (config.Heuristic)
            {
                case "IntersectingPieces":
                    heuristic = new IntersectingPiecesHeuristic();
                    break;
                case "SpaceUncovered":
                    heuristic = new SpaceUncoveredHeuristic();
                    break;
                case "SpaceCohesion":
                    heuristic = new SpaceCohesionHeuristic();
                    break;
                default:
                    throw new FormatException();
            }
            switch (config.HillClimbStrategy)
            {
                case "SimpleHillClimbing":
                    hillClimber = new HillClimbingBlockFitter(heuristic);
                    break;
                case "GeneticAlgorithm":
                    hillClimber = new GeneticBlockFitter(heuristic, 100);
                    break;
                case "SimulatedAnnealing":
                    hillClimber = new AnnealingBlockFitter(heuristic);
                    break;
                default:
                    throw new FormatException();
            }
            /*
            double intersectingSum = 0;
            double coveredSum = 0;
            double manhattanSum = 0;
            for (int i = 0; i < 25; i++)
            {
                State solution = hillClimber.Climb(config.Problem, 10000);
                intersectingSum += new IntersectingPiecesHeuristic().Evaluate(solution);
                coveredSum += new SpaceUncoveredHeuristic().Evaluate(solution);
                manhattanSum += new SpaceCohesionHeuristic().Evaluate(solution);
            }
            intersectingSum /= 25;
            coveredSum /= 25;
            manhattanSum /= 25;
            Console.WriteLine($"intersect={intersectingSum}");
            Console.WriteLine($"covered={coveredSum}");
            Console.WriteLine($"manhattan={manhattanSum}");
            */
            
            State solution = hillClimber.Climb(config.Problem, 10000);
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
