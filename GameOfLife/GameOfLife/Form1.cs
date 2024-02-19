using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GameOfLife
{
    public partial class Form1 : Form
    {   // Variable Declaration
        bool[,] Universe;
        int CellsX;
        int CellsY;
        delegate int boundaryType(int i, int c, int x, int y);
        boundaryType boundary;
        int generations = 0;
        int NumberofLivingCells = 0;
        //Grid,Back and Live cell color variables.
        Color gridColor ;
        SolidBrush liveCellColor;

        


        //Time intervarl
        int timeInterval = 10;
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();

            CellsX = Properties.Settings.Default.UniverseWidth;
            CellsY = Properties.Settings.Default.UniverseHeight;
            gridColor = Properties.Settings.Default.GridColor;
            graphicsPanel.BackColor = Properties.Settings.Default.BackColor;
            liveCellColor = new SolidBrush(Properties.Settings.Default.CellColor);
            timeInterval = Properties.Settings.Default.TimeInterval;

            
           statusLabel.Text = "Generations = " + generations + "          Alive cells = " + NumberofLivingCells;
            boundary = CountNeighborsToroidal;
           
            timer.Interval = timeInterval;
         


            Universe = new bool[CellsX, CellsY];
        }
        //Tick function that is going called in the defined interval
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Start();
            NextGeneration();

        }

        //Paint event that is going to be called everytime the panel is invalidated
        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            NumberofLivingCells = 0;
            float cellWidth = (float)graphicsPanel.ClientSize.Width / (float)Universe.GetLength(0);
            float cellHeight = (float)graphicsPanel.ClientSize.Height / (float)Universe.GetLength(1);
            Pen grid = new Pen(gridColor, 0f);
            for (int y = 0; y < Universe.GetLength(1); y++)
            {
                for (int x = 0; x < Universe.GetLength(0); x++)
                {

                    RectangleF rect = RectangleF.Empty;
                    rect.X = ((float)x) * cellWidth;
                    rect.Y = ((float)y) * cellHeight;
                    rect.Width = cellWidth;
                    rect.Height = cellHeight;

                    if (Universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(liveCellColor, rect);
                        NumberofLivingCells++;
                    }

                    e.Graphics.DrawRectangle(grid, rect.X, rect.Y, cellWidth, cellHeight);
                 
                }
                statusLabel.Invalidate();
            }

            Console.Clear();


            for (int i = 0; i < Universe.GetLength(0); i++)
            {
                for (int j = 0; j < Universe.GetLength(1); j++)
                {
                    if (Universe[i, j] == true)
                        Console.Write('O');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        //Mouse click event for changing the state of  a cell;
        private void graphicsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float cellW = ((float)graphicsPanel.ClientSize.Width) / ((float)Universe.GetLength(0));
                float cellH = ((float)graphicsPanel.ClientSize.Height) / ((float)Universe.GetLength(1));

                int x = (int)(((float)e.X) / cellW);
                int y = (int)(((float)e.Y) / cellH);

                Universe[x, y] = !Universe[x, y];
                graphicsPanel.Invalidate();


            }

        }

        //Function to count the number of neighbors in a Toroidal Universe
        public int CountNeighborsToroidal(int index, int count, int x, int y)
        {

            int i = index;
            int c = count;
            int a = x;
            int b = y;

            if (c > 4)
                return c;

            if (i < 2)
            {

                if (i == -1 && (x + i == -1))
                    a = Universe.GetLength(0);
                if (i == 1 && (x + i == Universe.GetLength(0)))
                    a = -1;
                if (b - 1 == -1)
                    b = Universe.GetLength(1);
                if (Universe[a + i, b - 1] == true)
                    c++;
                if (b == Universe.GetLength(1))
                    b = 0;
                if (Universe[a + i, b] == true)
                    c++;
                if (b + 1 == Universe.GetLength(1))
                    b = -1;
                if (Universe[a + i, b + 1] == true)
                    c++;
                if (Universe[x, y] == true && i == 1)
                    c--;

                return CountNeighborsToroidal(i + 1, c, x, y);

            }
            return c;




        }

        //Function to count the number of neighbors in a Boundary type Universe
        public int CountNeighborsBoundary(int index, int count, int x, int y)
        {
            int i = index;
            int c = count;
            int a = x;
            int b = y;

            if (c > 4)
                return c;



            if (i < 2)
            {
                if (Universe[x, y] == true && i == 1)
                    c--;
                if (a + i == -1)
                    i = 0;
                if (b - 1 == -1)
                    b = 1;
                if (a + i == Universe.GetLength(0))
                    return c;
                if (Universe[a + i, b - 1] == true)
                    c++;
                if (y == 0)
                    b = 0;
                if (Universe[a + i, b] == true && b != 0)
                    c++;
                if (b + 1 == Universe.GetLength(1))
                    return CountNeighborsBoundary(i + 1, c, x, y);
                else if (Universe[a + i, b + 1] == true)
                    c++;


                return CountNeighborsBoundary(i + 1, c, x, y);


            }


            return c;

        }

        //Function that advances the universe to a next generation.
        public void NextGeneration()
        {
            bool[,] universeNext = new bool[Universe.GetLength(0), Universe.GetLength(1)];

            for (int i = 0; i < Universe.GetLength(0); i++)
            {
                for (int j = 0; j < Universe.GetLength(1); j++)
                {

                    if (Universe[i, j] == true && (boundary(-1, 0, i, j) < 2))
                        universeNext[i, j] = false;

                    if (Universe[i, j] == true && (boundary(-1, 0, i, j) > 3))
                        universeNext[i, j] = false;

                    if (Universe[i, j] == true && ((boundary(-1, 0, i, j) == 2) || (boundary(-1, 0, i, j) == 3)))
                        universeNext[i, j] = true;

                    if ((Universe[i, j] == false) && (boundary(-1, 0, i, j) == 3))
                        universeNext[i, j] = true;

                }
            }
            generations++;
            statusLabel.Text = "Generations = " + generations + "          Alive cells = " + NumberofLivingCells;
          
          
            Universe = universeNext;
            graphicsPanel.Invalidate();
        }

        // Button click events to Play,Pause,Advanced to next generation and clear the universe
        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            if (toolStripButtonPlay.Checked == false)
            {
                timer.Start();
                timer.Tick += Timer_Tick;
            }
            toolStripButtonPlay.Checked = true;
            toolStripButtonPause.Checked = false;

        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            if (toolStripButtonPlay.Checked)
            {
                timer.Stop();
            }
            toolStripButtonPlay.Checked = false;
            toolStripButtonPause.Checked = true;
            timer.Tick -= Timer_Tick;


        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            timer.Stop();
            toolStripButtonPlay.Checked = false;
            toolStripButtonPause.Checked = false;
            NextGeneration();

        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            Universe = new bool[CellsX, CellsY];
            graphicsPanel.Invalidate();
        }

        //Exchange data between the forms;
        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool[,] tempUniverse = Universe;

            modalDialog dlg = new modalDialog();
            dlg.BackgroundColor = graphicsPanel.BackColor;
            dlg.GridColor = gridColor;
            dlg.LiveCellColor = liveCellColor.Color;
            dlg.CellsX = CellsX;
            dlg.CellsY = CellsY;
            dlg.TimeInterval = timeInterval;
            if (boundary == CountNeighborsToroidal)
                dlg.Boundary = "Toroidal";
            if (boundary == CountNeighborsBoundary)
                dlg.Boundary = "Finite";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.Boundary == "Toroidal")
                    boundary = CountNeighborsToroidal;
                if (dlg.Boundary == "Finite")
                    boundary = CountNeighborsBoundary;
                graphicsPanel.BackColor = dlg.BackgroundColor;
                gridColor = dlg.GridColor;
                liveCellColor.Color = dlg.LiveCellColor;
                CellsX = dlg.CellsX;
                CellsY = dlg.CellsY;
                Universe = new bool[dlg.CellsX, dlg.CellsY];

                timer.Interval = dlg.TimeInterval;

                arrayFit(tempUniverse);
            }




            graphicsPanel.Invalidate();

        }

        //Saving dialog function
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                writer.WriteLine("!This is my comment.");

                for (int j = 0; j < Universe.GetLength(1); j++)
                {
                    for (int i = 0; i < Universe.GetLength(0); i++)
                    {
                        if (Universe[i, j] == true)
                            writer.Write('O');
                        else
                            writer.Write('.');
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
        }
        // Open Dialog fucntion
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;
            String tempuniverse = String.Empty;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);


                int mWidth = 0;
                int mHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();


                    if (row.StartsWith("!"))
                    {
                        row = string.Empty;
                        continue;
                    }

                    tempuniverse = tempuniverse + row;

                    mWidth = row.Length;
                    mHeight++;

                }
                reader.Close();

                CellsX = mWidth;
                CellsY = mHeight;

                Universe = new bool[mWidth, mHeight];
                int k = 0;
                for (int j = 0; j < mHeight; j++)
                {
                    for (int i = 0; i < mWidth; i++)
                    {
                        if (tempuniverse[k] == 'O')
                        {
                            Universe[i, j] = !Universe[i, j];
                        }

                        k++;


                    }

                }


                Console.Write("\n\n\n\n" + tempuniverse);
                graphicsPanel.Invalidate();


            }
        }


        // A method to fit a universe into a bigger universe if the amount of cells is increased
        public void arrayFit(bool[,] tempUniverse)
        {
            if ((Universe.GetLength(0) >= tempUniverse.GetLength(0)) && (Universe.GetLength(1) >= tempUniverse.GetLength(1)))
            {
                for (int i = 0; i < tempUniverse.GetLength(0); i++)
                {
                    for (int j = 0; j < tempUniverse.GetLength(1); j++)
                    {
                        Universe[i + (int)((CellsX - tempUniverse.GetLength(0)) / 2f), j + (int)((CellsY - tempUniverse.GetLength(1)) / 2f)] = tempUniverse[i, j];
                    }

                }
            }
        }
        //Code to randomize the Universe
        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            for (int i = 0; i < Universe.GetLength(0); i++)
            {
                for (int j = 0; j < Universe.GetLength(1); j++)
                {
                    if (rand.Next(0, 2) > 0)
                        Universe[i, j] = true;
                }
            }
            graphicsPanel.Invalidate();
        }
        //Code for modifying settings.
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Properties.Settings.Default.UniverseWidth = CellsX ;
            Properties.Settings.Default.UniverseHeight = CellsY ;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.BackColor = graphicsPanel.BackColor;
            Properties.Settings.Default.CellColor = liveCellColor.Color;
            Properties.Settings.Default.TimeInterval = timeInterval;
            Properties.Settings.Default.Save();
            
        }

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            CellsX = Properties.Settings.Default.UniverseWidth;
            CellsY = Properties.Settings.Default.UniverseHeight;
            gridColor = Properties.Settings.Default.GridColor;
            graphicsPanel.BackColor = Properties.Settings.Default.BackColor;
            liveCellColor = new SolidBrush(Properties.Settings.Default.CellColor);
            timeInterval = Properties.Settings.Default.TimeInterval;
            Universe = new bool[CellsX,CellsY];
            graphicsPanel.Invalidate();
            Console.Clear();

        }

       
    }
}


