using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLifeOOP
{
    public partial class MainWindow : Form
    {
        private Terrain terrain;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int cellSize = 20;
        private IWorldFactory currentFactory;
        private ICellFactory cellFactory;

        public MainWindow()
        {
            InitializeComponent();

            this.Text = "Game of Life";
            this.ClientSize = new Size(400, 400);
            this.DoubleBuffered = true;

            ComboBox comboBox = new ComboBox();
            comboBox.Items.AddRange(new string[] { "Classic", "Colonies" });
            comboBox.SelectedIndex = 0; 
            comboBox.SelectedIndexChanged += (s, e) => OnModeChanged(comboBox.SelectedItem.ToString());
            comboBox.Dock = DockStyle.Top;
            this.Controls.Add(comboBox);

            OnModeChanged("Classic");

            timer.Interval = 500;
            timer.Tick += (s, e) => { terrain.Update(); this.Invalidate(); };
            timer.Start();

            this.Paint += MainWindow_Paint;
        }

        private void OnModeChanged(string mode)
        {
            currentFactory = mode switch
            {
                "Classic" => new ClassicWorldFactory(),
                "Colonies" => new ColoniesWorldFactory(),
                _ => throw new NotImplementedException()
            };

            var strategies = currentFactory.CreateStrategies();
            cellFactory = currentFactory.CreateCellFactory(strategies);

            if (terrain == null)
                terrain = new Terrain(20, 20, cellFactory);
            else
                terrain.Reinitialize(cellFactory);

            Randomize(mode); 
        }

        private void Randomize(string mode)
        {
            var rnd = new Random();
            CellType RandomType()
            {
                return mode switch
                {
                    "Classic" => rnd.Next(2) == 0 ? CellType.Empty : CellType.White,
                    "Colonies" => rnd.Next(3) switch
                    {
                        0 => CellType.Empty,
                        1 => CellType.White,
                        _ => CellType.Black
                    },
                    _ => CellType.Empty
                };
            }

            for (int x = 0; x < terrain.GetCells().GetLength(0); x++)
            for (int y = 0; y < terrain.GetCells().GetLength(1); y++)
                terrain.GetCells()[x, y] = cellFactory.Create(x, y, RandomType());
        }


        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var cells = terrain.GetCells();

            for (int x = 0; x < cells.GetLength(0); x++)
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                Brush brush = cells[x, y].Type switch
                {
                    CellType.White => Brushes.White,
                    CellType.Black => Brushes.Black,
                    _ => Brushes.Gray
                };
                g.FillRectangle(brush, x * cellSize, y * cellSize, cellSize - 1, cellSize - 1);
            }
        }
    }
}
