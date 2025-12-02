using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        // Vector de populație
        int[] populatie = new int[] { 2000000, 4000000, 300000, 40000000 };
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

            // Inițializare virus_pow
            virus_pow[0] = new NumarStruct { x = "COVID", y = 1000 };
            virus_pow[1] = new NumarStruct { x = "Ebola", y = 3000 };
            virus_pow[2] = new NumarStruct { x = "H1N1", y = 1500 };
            virus_pow[3] = new NumarStruct { x = "Rubeola", y = 2000 };
            virus_pow[4] = new NumarStruct { x = "Ciuma Neagra", y = 10000 };
            this.Load += Form2_Load;

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
            nodes.Add(new Node(1000, 300, "Romania"));//0
            nodes.Add(new Node(300, 50, "Bulgaria"));//1
            nodes.Add(new Node(200, 150, "Serbia"));//2
            nodes.Add(new Node(250, 400, "Ungaria"));//3
            nodes.Add(new Node(400, 400, "Ucraina"));//4
            nodes.Add(new Node(200, 400, "Moldova"));//5
            nodes.Add(new Node(500, 400, "Grecia"));//6
            nodes.Add(new Node(450, 400, "Slovacia"));//7
            nodes.Add(new Node(200, 450, "Slovenia"));//8
            nodes.Add(new Node(250, 450, "Polonia"));//9
            nodes.Add(new Node(250, 450, "Germania"));//10
            nodes.Add(new Node(250, 450, "Cehia"));//11
            nodes.Add(new Node(250, 450, "Austria"));//12
            nodes.Add(new Node(250, 450, "Croatia"));//13
            nodes.Add(new Node(250, 450, "Elvetia"));//14
            nodes.Add(new Node(250, 450, "Italia"));//15
            nodes.Add(new Node(250, 450, "Bosnia"));//16


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
                g.FillEllipse(nodeBrush, node.X - radius, node.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(Pens.Black, node.X - radius, node.Y - radius, radius * 2, radius * 2);

                // Numele nodului deasupra
                SizeF textSize = g.MeasureString(node.Name, font);
                float textX = node.X - textSize.Width / 2;
                float textY = node.Y - radius - textSize.Height - 2;
                g.DrawString(node.Name, font, Brushes.Black, textX, textY);
            }
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

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen edgePen = new Pen(Color.Black, 1);
            Brush nodeBrush = Brushes.LightGreen;
            Font font = new Font("Arial", 10, FontStyle.Bold);

            // Desenează muchiile
            foreach (Edge edge in edges)
            {
                g.DrawLine(edgePen,
                           edge.From.X, edge.From.Y,
                           edge.To.X, edge.To.Y);
            }

            // Desenează nodurile
            int radius = 20;
            foreach (Node node in nodes)
            {
                g.FillEllipse(nodeBrush, node.X - radius, node.Y - radius, radius * 2, radius * 2);
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

        public Node(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
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
