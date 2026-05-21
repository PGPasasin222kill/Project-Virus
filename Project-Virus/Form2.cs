using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
             new TaraStruct { Nume = "Bosnia",   Populatie = 200000,   Rezistenta = 0.25 },
             new TaraStruct { Nume = "Belarus",   Populatie = 2200000,  Rezistenta = 0.55 },
             new TaraStruct { Nume = "Polonia",   Populatie = 38000000, Rezistenta = 0.35 },
             new TaraStruct { Nume = "Lituania",  Populatie = 2800000,  Rezistenta = 0.25 },
             new TaraStruct { Nume = "Latvia",    Populatie = 1900000,  Rezistenta = 0.25 },
             new TaraStruct { Nume = "Rusia",     Populatie = 80000000, Rezistenta = 0.20 },
             new TaraStruct { Nume = "Turcia",    Populatie = 85000000, Rezistenta = 0.55 },
             new TaraStruct { Nume = "Slovacia",     Populatie = 2450000, Rezistenta = 0.70 },
             new TaraStruct { Nume = "Slovenia",    Populatie = 8500000,  Rezistenta = 0.35 }
        };

        // Vector de structuri pentru virusuri
        NumarStruct[] virus_pow = new NumarStruct[5];

        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();

        // Variabile pentru mutarea nodurilor cu mouse-ul
        private Node selectedNode = null;
        private int offsetX, offsetY;

        // STRUCTURI PENTRU ADJACENȚĂ & MATRICE
        Dictionary<string, List<(string neigh, int weight)>> adjacencyWeighted =
            new Dictionary<string, List<(string neigh, int weight)>>();

        int[,] MatrixAdj;
        int[,] MatrixWeight;

        // Infectare / vizual
        List<string> infected = new List<string>();
        int RiskThreshold = 5; // prag: muchiile cu Weight >= transmit

        // procent curent (pentru partea ta deja existentă)
        private Dictionary<string, double> procentCurentDict = new Dictionary<string, double>();

        // *** NOUTĂȚI ANIMAȚIE / SUNET ***
        private SoundPlayer infectSound; // preîncarcă sunetul și îl refolosim

        public Form2()
        {
            // activează double buffering
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
            this.MouseDown += Form2_MouseDown;
            this.MouseMove += Form2_MouseMove;
            this.MouseUp += Form2_MouseUp;

            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            InitSounds();
        }

        // SUNNET loading
        private void InitSounds()
        {
            try
            {
                using (UnmanagedMemoryStream ums = Properties.Resources.Infect_Effect)
                {
                    byte[] buffer = new byte[ums.Length];
                    ums.Read(buffer, 0, buffer.Length);
                    infectSound = new SoundPlayer(new MemoryStream(buffer));
                    infectSound.Load();
                }
            }
            catch
            {
                infectSound = null;
            }
        }

        // Safe play
        private void PlayInfectSound()
        {
            try
            {
                infectSound?.Play();
            }
            catch
            {
                
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            infectSound?.Stop();
            infectSound?.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Noduri 
            nodes.Add(new Node(564, 507, "Romania"));//0
            nodes.Add(new Node(558, 644, "Bulgaria"));//1
            nodes.Add(new Node(436, 571, "Serbia"));//2
            nodes.Add(new Node(396, 463, "Ungaria"));//3
            nodes.Add(new Node(643, 302, "Ucraina"));//4
            nodes.Add(new Node(649, 417, "Moldova", 13));//5
            nodes.Add(new Node(468, 759, "Grecia"));//6
            nodes.Add(new Node(381, 375, "Slovacia", 15));//7
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
            nodes.Add(new Node(501, 93, "Lituania", 15));//20
            nodes.Add(new Node(539, 34, "Latvia", 10));//21

            // Muchii
            edges.Add(new Edge(nodes[1], nodes[0], 4));   // BG - RO (trafic moderat)
            edges.Add(new Edge(nodes[2], nodes[0], 6));   // SRB - RO (risc)
            edges.Add(new Edge(nodes[3], nodes[0], 7));   // HUN - RO (trafic intens)
            edges.Add(new Edge(nodes[4], nodes[0], 3));   // UKR - RO (risc mic)
            edges.Add(new Edge(nodes[5], nodes[0], 5));   
            edges.Add(new Edge(nodes[6], nodes[1], 4));   
            edges.Add(new Edge(nodes[7], nodes[3], 2));
            edges.Add(new Edge(nodes[8], nodes[3], 3));
            edges.Add(new Edge(nodes[9], nodes[7], 5));
            edges.Add(new Edge(nodes[9], nodes[4], 6));
            edges.Add(new Edge(nodes[5], nodes[4], 2));
            edges.Add(new Edge(nodes[7], nodes[4], 3));
            edges.Add(new Edge(nodes[2], nodes[1], 6));
            edges.Add(new Edge(nodes[3], nodes[2], 5));
            edges.Add(new Edge(nodes[10], nodes[9], 8));
            edges.Add(new Edge(nodes[10], nodes[11], 7));
            edges.Add(new Edge(nodes[11], nodes[12], 6));
            edges.Add(new Edge(nodes[11], nodes[7], 4));
            edges.Add(new Edge(nodes[8], nodes[13], 3));
            edges.Add(new Edge(nodes[2], nodes[13], 4));
            edges.Add(new Edge(nodes[10], nodes[12], 5));
            edges.Add(new Edge(nodes[12], nodes[3], 6));
            edges.Add(new Edge(nodes[12], nodes[7], 4));
            edges.Add(new Edge(nodes[12], nodes[8], 3));
            edges.Add(new Edge(nodes[14], nodes[15], 7));
            edges.Add(new Edge(nodes[14], nodes[10], 6));
            edges.Add(new Edge(nodes[14], nodes[12], 5));
            edges.Add(new Edge(nodes[16], nodes[2], 3));
            edges.Add(new Edge(nodes[16], nodes[13], 4));
            edges.Add(new Edge(nodes[3], nodes[13], 4));
            edges.Add(new Edge(nodes[8], nodes[15], 5));
            edges.Add(new Edge(nodes[9], nodes[11], 6));
            edges.Add(new Edge(nodes[18], nodes[4], 4));
            edges.Add(new Edge(nodes[18], nodes[17], 5));
            edges.Add(new Edge(nodes[17], nodes[4], 6));
            edges.Add(new Edge(nodes[17], nodes[9], 4));
            edges.Add(new Edge(nodes[21], nodes[20], 3));
            edges.Add(new Edge(nodes[21], nodes[17], 3));
            edges.Add(new Edge(nodes[21], nodes[18], 4));
            edges.Add(new Edge(nodes[20], nodes[17], 3));
            edges.Add(new Edge(nodes[20], nodes[9], 4));
            edges.Add(new Edge(nodes[19], nodes[1], 2));
            edges.Add(new Edge(nodes[19], nodes[6], 3));

            // Construcție inițială a listei și matricei
            BuildAdjData();

            LoadGraph();
        }

        // ----- DESENARE FORM (păstrat, dar folosește node.Color) -----
        private Color Lerp(Color a, Color b, double t)
        {
            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int bC = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(r, g, bC);
        }

        private Color GetInfectionColor(double t) // infectie între 0 și 1
        {
            if (t < 0) t = 0;
            if (t > 1) t = 1;


            if (t <= 0.05) return Color.FromArgb(0, 180, 0);      // verde închis
            if (t <= 0.10) return Color.FromArgb(60, 200, 0);     // verde lime
            if (t <= 0.15) return Color.FromArgb(120, 220, 0);    // galben-verzui
            if (t <= 0.20) return Color.FromArgb(180, 230, 0);    // galben aprins
            if (t <= 0.25) return Color.FromArgb(255, 255, 0);    // galben
            if (t <= 0.30) return Color.FromArgb(255, 200, 0);    // portocaliu deschis
            if (t <= 0.35) return Color.FromArgb(255, 160, 0);    // portocaliu
            if (t <= 0.40) return Color.FromArgb(255, 120, 0);    // portocaliu închis
            if (t <= 0.45) return Color.FromArgb(255, 80, 0);     // roșu-portocaliu
            if (t <= 0.50) return Color.FromArgb(240, 50, 0);     // roșu aprins
            if (t <= 0.55) return Color.FromArgb(220, 0, 0);      // roșu intens
            if (t <= 0.60) return Color.FromArgb(200, 0, 0);      // roșu închis
            if (t <= 0.65) return Color.FromArgb(180, 0, 20);     // roșu-vinețiu
            if (t <= 0.70) return Color.FromArgb(160, 0, 40);     // roșu-vinețiu închis
            if (t <= 0.75) return Color.FromArgb(140, 0, 60);     // vișiniu
            if (t <= 0.80) return Color.FromArgb(120, 0, 80);     // vișiniu închis
            if (t <= 0.85) return Color.FromArgb(100, 0, 100);    // mov-roșu
            if (t <= 0.90) return Color.FromArgb(80, 0, 120);     // mov închis
            if (t <= 0.95) return Color.FromArgb(40, 0, 160);     // violet închis
            return Color.Black;                                    // 100%
        }

        // ----- MOUSE HANDLING pe form (permite mutare noduri) -----
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

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveGraph();
        }

        // ----- PictureBox (folosit pentru vizualizare noduri colorate) -----
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen edgePen = new Pen(Color.Black, 1);
            Font font = new Font("Arial", 10, FontStyle.Bold);

            // Draw edges with weights
            /*foreach (Edge edge in edges)
            {
                g.DrawLine(edgePen, edge.From.X, edge.From.Y, edge.To.X, edge.To.Y);
                var mx = (edge.From.X + edge.To.X) / 2;
                var my = (edge.From.Y + edge.To.Y) / 2;
                g.DrawString(edge.Weight.ToString(), new Font("Arial", 8), Brushes.DarkBlue, mx, my);
            }*/

            // Noduri
            foreach (Node node in nodes)
            {
                int radius = node.Radius;

                // Dacă avem procent calculat pentru un node, colorăm pe baza lui:
                double procent = 0;
                if (procentCurentDict.ContainsKey(node.Name))
                    procent = procentCurentDict[node.Name];

                double infectie = procent / 100.0;
                Color nodeColorByPercent = GetInfectionColor(infectie);

                // Prioritate: nod.Color (infectat manual) -> colorarea bazată pe procent -> default
                Color finalColor = node.Color;
                if (finalColor == DefaultNodeColor) // dacă nu e setat infectat
                {
                    finalColor = nodeColorByPercent;
                }

                using (Brush nodeBrush2 = new SolidBrush(finalColor))
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

        // ----- Load/Save poziții noduri în fisier -----
        private void LoadGraph()
        {
            if (!File.Exists("graf.txt"))
                return;

            var lines = File.ReadAllLines("graf.txt");

            foreach (string line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length < 2) continue;
                string name = parts[0];

                var pos = parts[1].Split(',');
                if (pos.Length < 2) continue;
                if (!int.TryParse(pos[0], out int x)) continue;
                if (!int.TryParse(pos[1], out int y)) continue;

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

        // ----- TextBox Enter handler pentru t -----
        private void textBoxT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        // ----- Funcția originală de calcul procent (păstrată) -----
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
            MessageBox.Show("PROCENTUL DE INFECRARE: " + $"{procent1:F2}%");
            pictureBox1.Invalidate();
        }

        private double ComputePercentForNode(string nodeName)
        {
            // gasim index tara in populatie_tara după nume
            int indexTara = Array.FindIndex(populatie_tara, p => p.Nume == nodeName);
            if (indexTara < 0) return 0.0;

            // virus selectat (folosim comboBoxVirus current; dacă nu e selectat, luăm index 0)
            int indexVirus = comboBoxVirus.SelectedIndex >= 0 ? comboBoxVirus.SelectedIndex : 0;

            // citim t (dacă nu e valid, folosim 1)
            if (!double.TryParse(textBoxT.Text, out double t) || t <= 0) t = 1.0;

            long totalPopulatie = populatie_tara[indexTara].Populatie;
            double rez = populatie_tara[indexTara].Rezistenta;
            double k = (virus_pow[indexVirus].y / 50000.0) * (1 - rez);
            double infectati = totalPopulatie * (1 - Math.Exp(-k * t));
            double procent1 = (double)(infectati / totalPopulatie) * 100.0;
            if (procent1 > 100.0) procent1 = 100.0;
            return procent1;
        }

        // ----- FUNCȚII NOI: Construire adj / matrice, afișare matrice ----- //
        private void BuildAdjData()
        {
            adjacencyWeighted.Clear();

            foreach (var node in nodes)
                adjacencyWeighted[node.Name] = new List<(string neigh, int weight)>();

            int n = nodes.Count;
            MatrixAdj = new int[n, n];
            MatrixWeight = new int[n, n];

            foreach (var edge in edges)
            {
                // listă ponderată
                adjacencyWeighted[edge.From.Name].Add((edge.To.Name, edge.Weight));
                adjacencyWeighted[edge.To.Name].Add((edge.From.Name, edge.Weight)); // neorientat

                // matrice
                int i = nodes.IndexOf(edge.From);
                int j = nodes.IndexOf(edge.To);

                if (i >= 0 && j >= 0)
                {
                    MatrixAdj[i, j] = 1;
                    MatrixAdj[j, i] = 1;

                    MatrixWeight[i, j] = edge.Weight;
                    MatrixWeight[j, i] = edge.Weight;
                }
            }
        }

        // ============================
        // *** NOUTĂȚI: ANIMAȚII ***
        // Particule care se mișcă de la un nod la altul folosind coordonatele din nodes
        // și explozie (cercuri) la nodul țintă.
        // ============================
        private async Task TravelParticlesBetweenNodes(string fromName, string toName)
        {
            var fromNode = nodes.Find(n => n.Name == fromName);
            var toNode = nodes.Find(n => n.Name == toName);
            if (fromNode == null || toNode == null) return;

            // obținem coordonatele relative la pictureBox (evenimentele mouse foloseau coordonate picturebox)
            // nodes.X/Y sunt folosite pentru desen direct în pictureBox Paint, deci le putem folosi pe acelea
            int steps = 30;
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                for (int i = 0; i <= steps; i++)
                {
                    float t = i / (float)steps;
                    int x = (int)(fromNode.X + (toNode.X - fromNode.X) * t);
                    int y = (int)(fromNode.Y + (toNode.Y - fromNode.Y) * t);

                    // desenăm mai multe particule ușoare (mic punct + halo)
                    int size = 10;
                    Rectangle rect = new Rectangle(x - size / 2, y - size / 2, size, size);
                    g.FillEllipse(Brushes.OrangeRed, rect);
                    await Task.Delay(0);

                    // curățare simplă a particulei prin redrawing pictureBox (Invalidate)
                    // pentru a evita ștergerea întregului UI prea des, redăm doar baza
                    pictureBox1.Invalidate(); // repaint permanent (paint va desena muchii/noduri)
                }
            }
        }

        private async Task ExplosionEffectOnNode(string nodeName)
        {
            var node = nodes.Find(n => n.Name == nodeName);
            if (node == null) return;

            int cx = node.X;
            int cy = node.Y;

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                for (int r = 10; r <= 60; r += 10)
                {
                    using (Pen p = new Pen(Color.FromArgb(200, Color.Red), 3))
                    {
                        g.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);
                    }
                    await Task.Delay(80);
                    pictureBox1.Invalidate();
                }
            }
            pictureBox1.Invalidate();
        }

        // ----- INFECTARE (BFS) cu greutăți (vizual, nod cu nod) -----
        // Observație: folosește animații async pentru particule/explozie
        private async Task SpreadInfectionAsync(string start)
        {
            if (!adjacencyWeighted.ContainsKey(start))
            {
                MessageBox.Show($"Nodul {start} nu există în graf.");
                return;
            }

            infected.Clear();
            Queue<(string node, double time)> q = new Queue<(string node, double time)>();
            Dictionary<string, double> visitedTime = new Dictionary<string, double>();

            if (!double.TryParse(textBoxT.Text, out double tMax) || tMax <= 0) tMax = 1.0;

            visitedTime[start] = 0;
            infected.Add(start);

            procentCurentDict[start] = ComputePercentForNode(start);
            pictureBox1.Invalidate();
            await Task.Delay(300);

            q.Enqueue((start, 0));

            PlayInfectSound();
            await ExplosionEffectOnNode(start);

            while (q.Count > 0)
            {
                var (current, currentTime) = q.Dequeue();

                foreach (var (neigh, weight) in adjacencyWeighted[current])
                {
                    // Timpul necesar pentru a trece virusul prin muchie
                    double deltaTime = weight * 2.0; // greutăți mai mari → propagare mai lentă
                    double arrivalTime = currentTime + deltaTime;

                    if (arrivalTime <= tMax && (!visitedTime.ContainsKey(neigh) || arrivalTime < visitedTime[neigh]))
                    {
                        visitedTime[neigh] = arrivalTime;
                        infected.Add(neigh);

                        procentCurentDict[neigh] = ComputePercentForNode(neigh);

                        await TravelParticlesBetweenNodes(current, neigh);

                        // redăm sunet și explozie la destinație
                        PlayInfectSound();
                        await ExplosionEffectOnNode(neigh);

                        pictureBox1.Invalidate();
                        await Task.Delay(1500); // mic pauză după explozie
                        q.Enqueue((neigh, arrivalTime));
                    }
                }
            }

            MessageBox.Show("Infectate: " + string.Join(", ", infected));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string start = "Romania";
            if (comboBox1.SelectedIndex >= 0)
                start = comboBox1.SelectedItem.ToString();
            if (adjacencyWeighted == null || adjacencyWeighted.Count == 0)
                BuildAdjData();
            var _ = SpreadInfectionAsync(start);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var node in nodes)
            {
                node.Color = DefaultNodeColor;
            }
            procentCurentDict.Clear();
            pictureBox1.Invalidate();
        }

        private void buttonlog_Click(object sender, EventArgs e)
        {
            if (textlogin.Text == "Savant")
            {
                textlogin.Visible = false;
                buttonlog.Visible = false;
                label1.Visible = false;
                button4.Visible = true;
            }
            else
            {
                MessageBox.Show("Acces interzis!");
                textlogin.Visible = false;
                buttonlog.Visible = false;
                label1.Visible = false;
                //button4.Visible = true;
            }
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            textlogin.Visible = true;
            buttonlog.Visible = true;
            label1.Visible = true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        // variabila folosită pentru culoare default
        private static readonly Color DefaultNodeColor = Color.LightGreen;
    }

    // ----- Clase auxiliare ----- //
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; } = Color.LightGreen; // culoare implicită

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
        public int Weight { get; set; } // risc / trafic / rată infectare

        public Edge(Node from, Node to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
