using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLifeOOP
{
    public partial class MainWindow : Form
    {
        private ITerrain terrain;

        private PatternTerrainDecorator patternDecorator;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private int cellSize = 20;

        private bool showOnlyPatterns = false;
        private CheckBox showPatternsCheckbox;
        private Panel patternsPanel;

        private IWorldFactory currentFactory;
        private ICellFactory cellFactory;

        public MainWindow()
        {
            InitializeComponent();

            this.Text = "Game of Life";
            this.ClientSize = new Size(500, 400);
            this.DoubleBuffered = true;

            ComboBox comboBox = new ComboBox();

            comboBox.Items.AddRange(new string[] { "Classic", "Colonies" });
            comboBox.SelectedIndex = 0;

            comboBox.SelectedIndexChanged += (s, e) => OnModeChanged(comboBox.SelectedItem.ToString());
            comboBox.Dock = DockStyle.Top;

            this.Controls.Add(comboBox);

            OnModeChanged("Classic");

            timer.Interval = 500;
            timer.Tick += (s, e) =>
            {
                terrain.Update();
                this.Invalidate();
                patternsPanel.Invalidate();
            };
            timer.Start();

            this.Paint += MainWindow_Paint;

            // Паттерны
            showPatternsCheckbox = new CheckBox();
            showPatternsCheckbox.Text = "Show only patterns";
            showPatternsCheckbox.Dock = DockStyle.Top;

            showPatternsCheckbox.CheckedChanged += (s, e) =>
            {
                showOnlyPatterns = showPatternsCheckbox.Checked;
                Invalidate();
            };

            this.Controls.Add(showPatternsCheckbox);

            // Панель для паттернов
            patternsPanel = new Panel();
            patternsPanel.Width = 120;
            patternsPanel.Dock = DockStyle.Right;
            patternsPanel.BackColor = Color.LightGray;
            this.Controls.Add(patternsPanel);

            patternsPanel.Paint += (s, e) =>
            {
                int y = 5;
                foreach (var kvp in Patterns.Counts)
                {
                    e.Graphics.DrawString($"{kvp.Key}: {kvp.Value}",
                                        this.Font, Brushes.Black, 5, y);
                    y += 20;
                }
            };

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
            {
                var baseTerrain = new Terrain(20, 20, cellFactory);
                patternDecorator = new PatternTerrainDecorator(baseTerrain);
                terrain = patternDecorator;

            }
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
                    bool isPatternCell =
                    patternDecorator != null &&
                    patternDecorator.PatternCells.Contains((x, y));

                    Brush brush;

                    if (showOnlyPatterns && !isPatternCell)
                    {
                        brush = Brushes.Gray;
                    }
                    else
                    {
                        brush = cells[x, y].Type switch
                        {
                            CellType.White => Brushes.White,
                            CellType.Black => Brushes.Black,
                            _ => Brushes.Gray
                        };
                    }

                    g.FillRectangle(
                        brush,
                        x * cellSize,
                        y * cellSize,
                        cellSize - 1,
                        cellSize - 1
                );
                }
        }
    }
}
