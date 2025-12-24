using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLifeOOP
{
    public partial class MainWindow : Form
    {
        private ITerrain terrain;
        private Terrain baseTerrain;

        private PatternTerrainDecorator patterns;
        private StatisticsTerrainDecorator stats;

        private IWorldFactory currentFactory;
        private ICellFactory cellFactory;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private const int cellSize = 20;

        private bool drawOnlyPatterns = false;
        private CheckBox drawCellsCheckbox;

        private ColonyManager? colonyManager;

        public MainWindow()
        {
            InitializeComponent();

            Text = "Game of Life";
            ClientSize = new Size(650, 400);
            DoubleBuffered = true;

            var comboBox = new ComboBox
            {
                Dock = DockStyle.Top
            };
            comboBox.Items.AddRange(new[] { "Classic", "Colonies" });
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += (_, __) =>
                OnModeChanged(comboBox.SelectedItem!.ToString()!);

            Controls.Add(comboBox);

            drawCellsCheckbox = new CheckBox
            {
                Text = "Draw patterns only",
                Dock = DockStyle.Top
            };

            drawCellsCheckbox.CheckedChanged += (_, __) =>
            {
                drawOnlyPatterns = drawCellsCheckbox.Checked;
                Invalidate();
            };

            Controls.Add(drawCellsCheckbox);

            OnModeChanged("Classic");

            timer.Interval = 500;
            timer.Tick += (_, __) =>
            {
                terrain.Update();
                colonyManager?.UpdateColonies();
                patterns.RecalculatePatterns();
                Invalidate();
            };
            timer.Start();

            Paint += MainWindow_Paint;
        }

        private void OnModeChanged(string mode)
        {
            currentFactory = mode switch
            {
                "Classic" => new ClassicWorldFactory(),
                "Colonies" => new ColoniesWorldFactory(),
                _ => throw new NotImplementedException()
            };

            if (mode=="Colonies")
                colonyManager = new ColonyManager(baseTerrain);
            else
                colonyManager = null;

            var strategies = currentFactory.CreateStrategies();
            cellFactory = currentFactory.CreateCellFactory(strategies);

            if (terrain == null)
            {
                baseTerrain = new Terrain(20, 20, cellFactory);
                patterns = new PatternTerrainDecorator(baseTerrain);
                stats = new StatisticsTerrainDecorator(patterns, patterns);

                terrain = stats;

            }
            else
                baseTerrain.Reinitialize(cellFactory);

            Randomize(mode);

            patterns.RecalculatePatterns();
        }

        private void Randomize(string mode)
        {
            var rnd = new Random();

            CellType RandomType() => mode switch
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

            var cells = terrain.GetCells();

            for (int x = 0; x < cells.GetLength(0); x++)
                for (int y = 0; y < cells.GetLength(1); y++)
                    cells[x, y] = cellFactory.Create(x, y, RandomType());
        }

        private void MainWindow_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.DarkGray);

            if (drawOnlyPatterns) patterns.Draw(e.Graphics, drawOnlyPatterns);
            else stats.Draw(e.Graphics);

            var s = stats.Stats;

            int offsetX = terrain.GetCells().GetLength(0) * cellSize + 10;
            int offsetY = 50;


            string text =
            $"White: {s.WhiteCount} ({s.WhiteDelta:+0.##;-0.##;0}%)\n" +
            $"Black: {s.BlackCount} ({s.BlackDelta:+0.##;-0.##;0}%)\n\n" +
            $"Update: {s.UpdateTime.TotalMilliseconds:F1} ms\n" +
            $"Scan:   {s.ScanTime.TotalMilliseconds:F1} ms\n\n";

            if (s.PatternCounts.Count > 0)
            {
                text += "Patterns:\n";

                foreach (var (type, count) in s.PatternCounts)
                {
                    text += $"  {type}: {count}\n";
                }
            }

            e.Graphics.DrawString(
                text,
                Font,
                Brushes.Black,
                offsetX,
                offsetY
            );
        }

    }
}
