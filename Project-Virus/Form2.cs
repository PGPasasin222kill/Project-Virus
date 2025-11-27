using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Virus
{
    public partial class Form2 : Form
    {
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
            this.Load += Form2_Load;

            // Conectează Paint și evenimente mouse
            this.Paint += Form2_Paint;
            this.MouseDown += Form2_MouseDown;
            this.MouseMove += Form2_MouseMove;
            this.MouseUp += Form2_MouseUp;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Noduri
            nodes.Add(new Node(400, 300, "Romania"));
            nodes.Add(new Node(300, 50, "Bulgaria"));
            nodes.Add(new Node(200, 150, "Serbia"));
            nodes.Add(new Node(250, 400, "Ungaria"));

            // Muchii
            edges.Add(new Edge(nodes[0], nodes[1]));
            edges.Add(new Edge(nodes[2], nodes[0]));
            edges.Add(new Edge(nodes[3], nodes[0]));
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen edgePen = new Pen(Color.Black, 2);
            Brush nodeBrush = Brushes.LightBlue;
            Font font = new Font("Arial", 12);

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

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
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
}
