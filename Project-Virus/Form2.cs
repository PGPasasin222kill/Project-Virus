using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_Virus
{

    public partial class Form2 : Form
    {
        // Structură pentru virusuri
        struct NumarStruct
        {
            public string x; // Numele virusului
            public int y;    // Puterea virusului
        }
        struct TaraStruct
        {
            public string Nume;
            public long Populatie;
            public double Rezistenta; // 0 = fără rezistență, 1 = foarte rezistentă
        }

        TaraStruct[] populatie_tara = new TaraStruct[]
        {
    new TaraStruct { Nume = "Romania",  Populatie = 19000000, Rezistenta = 0.30 },
    new TaraStruct { Nume = "Bulgaria", Populatie = 7000000,    Rezistenta = 0.20 },
    new TaraStruct { Nume = "Serbia",   Populatie = 6700000,  Rezistenta = 0.25 },
    new TaraStruct { Nume = "Ungaria",  Populatie = 9700000,  Rezistenta = 0.40 },
    new TaraStruct { Nume = "Ucraina",  Populatie = 41000000, Rezistenta = 0.15 },
    new TaraStruct { Nume = "Moldova",  Populatie = 2600000,  Rezistenta = 0.10 },
    new TaraStruct { Nume = "Grecia",   Populatie = 10700000, Rezistenta = 0.35 },
    new TaraStruct { Nume = "Germania", Populatie = 5500000,  Rezistenta = 0.50 },
    new TaraStruct { Nume = "Cehia",    Populatie = 2100000,  Rezistenta = 0.45 },
    new TaraStruct { Nume = "Austria",  Populatie = 38000000, Rezistenta = 0.60 },
    new TaraStruct { Nume = "Croatia",  Populatie = 2500000,  Rezistenta = 0.30 },
    new TaraStruct { Nume = "Elvetia",  Populatie = 2200000,  Rezistenta = 0.55 },
    new TaraStruct { Nume = "Italia",   Populatie = 80000,    Rezistenta = 0.20 },
    new TaraStruct { Nume = "Bosnia",   Populatie = 200000,   Rezistenta = 0.25 }
        };
        // Vector de populație

        // Vector de structuri pentru virusuri
        NumarStruct[] virus_pow = new NumarStruct[5];

        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();

        // Variabile pentru mutarea nodurilor cu mouse-ul
        private Node selectedNode = null;
        private int offsetX, offsetY;


        public Form2()
        {
            

            // Activează double buffering pentru a elimina flicker
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            InitializeComponent();
            textBoxT.KeyDown += textBoxT_KeyDown;

            // Inițializare virus_pow
            virus_pow[0] = new NumarStruct { x = "COVID", y = 1000 };
            virus_pow[1] = new NumarStruct { x = "Ebola", y = 3000 };
            virus_pow[2] = new NumarStruct { x = "H1N1", y = 1500 };
            virus_pow[3] = new NumarStruct { x = "Rubeola", y = 2000 };
            virus_pow[4] = new NumarStruct { x = "Ciuma Neagra", y = 10000 };
            this.Load += Form2_Load;
            comboBox1.Items.AddRange(populatie_tara.Select(t => t.Nume).ToArray());
            comboBoxVirus.Items.AddRange(new string[]
        {
            virus_pow[0].x,
            virus_pow[1].x,
            virus_pow[2].x,
            virus_pow[3].x,
            virus_pow[4].x
        });

            // Conectează Paint și evenimente mouse
            this.Paint += Form2_Paint;
            this.MouseDown += Form2_MouseDown;
            this.MouseMove += Form2_MouseMove;
            this.MouseUp += Form2_MouseUp;

            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            // Noduri
            nodes.Add(new Node(564, 507, "Romania"));//0
            nodes.Add(new Node(558, 644, "Bulgaria"));//1
            nodes.Add(new Node(436, 571, "Serbia"));//2
            nodes.Add(new Node(396, 463, "Ungaria"));//3
            nodes.Add(new Node(643, 302, "Ucraina"));//4
            nodes.Add(new Node(649, 417, "Moldova",13));//5
            nodes.Add(new Node(468, 759, "Grecia"));//6
            nodes.Add(new Node(381, 375, "Slovacia",15));//7
            nodes.Add(new Node(200, 450, "Slovenia", 12));//8
            nodes.Add(new Node(393, 251, "Polonia"));//9
            nodes.Add(new Node(176, 271, "Germania"));//10
            nodes.Add(new Node(259, 420, "Cehia"));//11
            nodes.Add(new Node(276, 333, "Austria"));//12
            nodes.Add(new Node(302, 498, "Croatia", 12));//13
            nodes.Add(new Node(61, 433, "Elvetia", 15));//14
            nodes.Add(new Node(155, 609, "Italia"));//15
            nodes.Add(new Node(340, 572, "Bosnia"));//16
            nodes.Add(new Node(614, 153, "Belarus"));//17
            nodes.Add(new Node(704, 64, "Rusia"));//18
            nodes.Add(new Node(694, 760, "Turcia"));//19
            nodes.Add(new Node(501, 93, "Lituania",15));//20
            nodes.Add(new Node(539, 34, "Latvia",10));//21



            // Muchii
            edges.Add(new Edge(nodes[1], nodes[0]));
            edges.Add(new Edge(nodes[2], nodes[0]));
            edges.Add(new Edge(nodes[3], nodes[0]));
            edges.Add(new Edge(nodes[4], nodes[0]));
            edges.Add(new Edge(nodes[5], nodes[0]));
            edges.Add(new Edge(nodes[6], nodes[1]));
            edges.Add(new Edge(nodes[7], nodes[3]));
            edges.Add(new Edge(nodes[8], nodes[3]));
            edges.Add(new Edge(nodes[9], nodes[7]));
            edges.Add(new Edge(nodes[9], nodes[4]));
            edges.Add(new Edge(nodes[5], nodes[4]));
            edges.Add(new Edge(nodes[7], nodes[4]));
            edges.Add(new Edge(nodes[2], nodes[1]));
            edges.Add(new Edge(nodes[3], nodes[2]));
            edges.Add(new Edge(nodes[10], nodes[9]));
            edges.Add(new Edge(nodes[10], nodes[11]));
            edges.Add(new Edge(nodes[11], nodes[12]));
            edges.Add(new Edge(nodes[11], nodes[7]));
            edges.Add(new Edge(nodes[8], nodes[13]));
            edges.Add(new Edge(nodes[2], nodes[13]));
            edges.Add(new Edge(nodes[10], nodes[12]));
            edges.Add(new Edge(nodes[12], nodes[3]));
            edges.Add(new Edge(nodes[12], nodes[7]));
            edges.Add(new Edge(nodes[12], nodes[8]));
            edges.Add(new Edge(nodes[14], nodes[15]));
            edges.Add(new Edge(nodes[14], nodes[10]));
            edges.Add(new Edge(nodes[14], nodes[12]));
            edges.Add(new Edge(nodes[14], nodes[15]));
            edges.Add(new Edge(nodes[16], nodes[2]));
            edges.Add(new Edge(nodes[16], nodes[13]));
            edges.Add(new Edge(nodes[3], nodes[13]));
            edges.Add(new Edge(nodes[8], nodes[15]));
            edges.Add(new Edge(nodes[9], nodes[11]));
            edges.Add(new Edge(nodes[18], nodes[4]));
            edges.Add(new Edge(nodes[18], nodes[17]));
            edges.Add(new Edge(nodes[17], nodes[4]));
            edges.Add(new Edge(nodes[17], nodes[9]));
            edges.Add(new Edge(nodes[21], nodes[20]));
            edges.Add(new Edge(nodes[21], nodes[17]));
            edges.Add(new Edge(nodes[21], nodes[18]));
            edges.Add(new Edge(nodes[20], nodes[17]));
            edges.Add(new Edge(nodes[20], nodes[9]));
            edges.Add(new Edge(nodes[19], nodes[1]));
            edges.Add(new Edge(nodes[19], nodes[6]));
            LoadGraph();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen edgePen = new Pen(Color.Black, 3);
            Brush nodeBrush = Brushes.LightGreen;
            Font font = new Font("Bold", 10);

            // Desenează muchiile
            foreach (Edge edge in edges)
            {
                g.DrawLine(edgePen, edge.From.X, edge.From.Y, edge.To.X, edge.To.Y);
            }

            int radius = 20;
            foreach (Node node in nodes)
            {
                // Nodul
                g.FillEllipse(nodeBrush, node.X - radius, node.Y - radius, radius * 3, radius * 2);
                g.DrawEllipse(Pens.Black, node.X - radius, node.Y - radius, radius * 2, radius * 2);

                // Numele nodului deasupra
                SizeF textSize = g.MeasureString(node.Name, font);
                float textX = node.X - textSize.Width / 2;
                float textY = node.Y - radius - textSize.Height - 2;
                g.DrawString(node.Name, font, Brushes.Black, textX, textY);
            }
        }
        private Color Lerp(Color a, Color b, double t)
        {
            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int bC = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(r, g, bC);
        }

        private Color GetInfectionColor(double infectie) // infectie între 0 și 1
        {
            infectie = Math.Max(0, Math.Min(1, infectie));
            double t = Math.Pow(infectie, 2.2);

            if (t <= 0.10)
                return Lerp(Color.FromArgb(0, 180, 0), Color.FromArgb(120, 200, 0), t / 0.10);
            if (t <= 0.30)
                return Lerp(Color.FromArgb(120, 200, 0), Color.FromArgb(255, 255, 0), (t - 0.10) / 0.20);
            if (t <= 0.60)
                return Lerp(Color.FromArgb(255, 255, 0), Color.FromArgb(255, 128, 0), (t - 0.30) / 0.30);
            if (t <= 0.95)
                return Lerp(Color.FromArgb(255, 128, 0), Color.FromArgb(180, 0, 0), (t - 0.60) / 0.35);
            if (t < 1.0)
                return Lerp(Color.FromArgb(180, 0, 0), Color.Black, (t - 0.95) / 0.05);

            return Color.Black;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            int radius = 20;
            foreach (var node in nodes)
            {
                int dx = e.X - node.X;
                int dy = e.Y - node.Y;
                if (dx * dx + dy * dy <= radius * radius)
                {
                    selectedNode = node;
                    offsetX = dx;
                    offsetY = dy;
                    break;
                }
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedNode != null && e.Button == MouseButtons.Left)
            {
                selectedNode.X = e.X - offsetX;
                selectedNode.Y = e.Y - offsetY;
                this.Invalidate(); // redesenează form-ul fără flicker
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveGraph();
        }

        private int nodeRadius = 15;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen edgePen = new Pen(Color.Black, 1);
            Brush nodeBrush = Brushes.LightGreen;
            Font font = new Font("Arial", 10, FontStyle.Bold);

            // Muchii
            foreach (Edge edge in edges)
            {
                g.DrawLine(edgePen, edge.From.X, edge.From.Y, edge.To.X, edge.To.Y);
            }

            // Noduri
            foreach (Node node in nodes)
            {
                int radius = node.Radius;

                double procent = 0;
                if (procentCurentDict.ContainsKey(node.Name))
                    procent = procentCurentDict[node.Name];

                double infectie = procent / 100.0;
                Color nodeColor = GetInfectionColor(infectie);

                using (Brush nodeBrush2 = new SolidBrush(nodeColor))
                {
                    g.FillEllipse(nodeBrush2, node.X - radius, node.Y - radius, radius * 2, radius * 2);
                }

                g.DrawEllipse(Pens.Black, node.X - radius, node.Y - radius, radius * 2, radius * 2);

                SizeF textSize = g.MeasureString(node.Name, font);
                float textX = node.X - textSize.Width / 2;
                float textY = node.Y - radius - textSize.Height - 2;

                g.DrawString(node.Name, font, Brushes.Black, textX, textY);
            }

        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int radius = 20;

            foreach (var node in nodes)
            {
                int dx = e.X - node.X;
                int dy = e.Y - node.Y;

                if (dx * dx + dy * dy <= radius * radius)
                {
                    selectedNode = node;
                    offsetX = dx;
                    offsetY = dy;
                    break;
                }
            }
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedNode != null && e.Button == MouseButtons.Left)
            {
                selectedNode.X = e.X - offsetX;
                selectedNode.Y = e.Y - offsetY;

                pictureBox1.Invalidate(); // redesenează graful
            }
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
        }
        private void LoadGraph()
        {
            if (!File.Exists("graf.txt"))
                return;

            var lines = File.ReadAllLines("graf.txt");

            foreach (string line in lines)
            {
                var parts = line.Split(':');
                string name = parts[0];

                var pos = parts[1].Split(',');
                int x = int.Parse(pos[0]);
                int y = int.Parse(pos[1]);

                // găsește nodul cu acest nume
                var node = nodes.Find(n => n.Name == name);
                if (node != null)
                {
                    node.X = x;
                    node.Y = y;
                }
            }

            this.Invalidate(); // redesenează nodurile
        }
        private void textBoxT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
        double t;
        private Dictionary<string, double> procentCurentDict = new Dictionary<string, double>();
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Alege o țară!");
                return;
            }

            if (comboBoxVirus.SelectedIndex == -1)
            {
                MessageBox.Show("Alege un virus!");
                return;
            }

            int indexTara = comboBox1.SelectedIndex;
            int indexVirus = comboBoxVirus.SelectedIndex;

            if (!double.TryParse(textBoxT.Text, out double tNou) || tNou <= 0)
            {
                MessageBox.Show("Scrie un număr valid pentru t!");
                return;
            }

            double t = tNou;

            // Folosim structura populatie_tara
            long totalPopulatie = populatie_tara[indexTara].Populatie;
            double rez = populatie_tara[indexTara].Rezistenta;
            double k = (virus_pow[indexVirus].y / 50000.0) * (1 - rez);
            double infectati = totalPopulatie * (1 - Math.Exp(-k * t));
            double procent1 = (double)(infectati / totalPopulatie) * 100;

            if (procent1 > 100) procent1 = 100;

            textBoxProcent.Text = $"{procent1:F2}%";
            procentCurentDict[populatie_tara[indexTara].Nume] = procent1;
            pictureBox1.Invalidate();

        }

        private void textBoxProcent_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void SaveGraph()
        {
            using (StreamWriter sw = new StreamWriter("graf.txt"))
            {
                foreach (Node node in nodes)
                {
                    sw.WriteLine($"{node.Name}:{node.X},{node.Y}");
                }
            }
        }

    }
}

// Clase auxiliare
public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Name { get; set; }
    public int Radius { get; set; }

    public Node(int x, int y, string name, int radius = 20)
    {
        X = x;
        Y = y;
        Name = name;
        Radius = radius;  // Setare corectă
    }
}


public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
    }
