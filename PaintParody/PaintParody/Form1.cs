using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PaintParody.Models;

namespace PaintParody
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Form1 (Main form) 
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // Method with drawing on pictureBox
            SetDrawingFieldSize();
            // Button with pen's color
            button5.BackColor = pen.Color;
            // Change pen size using numericUpDown
            pen.Width = (float)numericUpDown1.Value;
            // Default pictureBox color is white
            pictureBox1.BackColor = Color.White;
        }

        #region Drawing Field Settings
        // Graphics field
        Graphics graphics;
        // Storing our image here
        Bitmap map = new Bitmap(100, 100);
        // Check if mouse if pressed and holded
        bool isMousePressedAndHold = false;
        // Instance of ArrayOfPoints class. 2 points (begging and end)
        ArrayOfPoints arrayOfPoints = new ArrayOfPoints(2);
        // Instance of Pen class. Default color is black and size is 3
        Pen pen = new Pen(Color.Black, 3f);

        // Setting size of our bitmap (default is 100)
        /// <summary>
        /// SetDrawingFieldSize method
        /// </summary>
        private void SetDrawingFieldSize()
        {
            // Getting user's screen size (like 1920 x 1080)
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            // And adding this size to bitmap
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            // Making our line less sharp
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
        }

        // Mouse is pressed on pictureBox
        /// <summary>
        /// pictureBox1_MouseDown method
        /// </summary>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMousePressedAndHold = true;
        }

        // Mouse is unpressed on pictureBox
        /// <summary>
        /// pictureBox1_MouseUp method
        /// </summary>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressedAndHold = false;
            // Resetting number of our points 
            arrayOfPoints.ResetPoints();
        }

        // Mouse is moving
        /// <summary>
        /// pictureBox1_MouseMove method
        /// </summary>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // If mouse is idle, we do nothing
            if (!isMousePressedAndHold) 
            {
                // Sending mouse coordinates to label
                label3.Text = $"{e.X}, {e.Y}px";
                return;
            }

            // Adding points to array (coords from our mouse)
            arrayOfPoints.AddPointsToArray(e.X, e.Y);
            // If our array is filled with 2 points
            if (arrayOfPoints.GetAmountOfPoints() >= 2)
            {
                // Drawing line using an array
                graphics.DrawLines(pen, arrayOfPoints.ReturnPoints());
                // Adding our line to pictureBox
                pictureBox1.Image = map;
                // Adding last dot to array (so that line is not dashed, like this: - - - -)
                arrayOfPoints.AddPointsToArray(e.X, e.Y);
            }

            // Sending mouse coordinates to label
            label3.Text = $"{e.X}, {e.Y}px";
        }
        #endregion

        #region Color Picker
        // Custom color button
        /// <summary>
        /// button17_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            // Color picker window
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                // Setting custom color to pen 
                pen.Color = colorDialog1.Color;
                // Setting custom color to current color button
                button5.BackColor = pen.Color;
            }
        }

        // Universal button for changing pen color
        /// <summary>
        /// button6_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            // Group of color buttons
            pen.Color = ((Button)sender).BackColor;
            // Setting pen color using this color buttons
            button5.BackColor = pen.Color;
        }
        #endregion

        #region File Settings Buttons
        // Save file
        /// <summary>
        /// saveButton_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Save file filter
            saveFileDialog1.Filter = "PNG (*.png)|*.png";

            // File saving process
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
                MessageBox.Show("File saved successfully!", "Save status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Open file
        /// <summary>
        /// openButton_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            // Open file filter
            //openFileDialog1.Filter = "PNG (*.png)|*.png";
            //openFileDialog1.Filter = "Image Files(*.jpg)| *.jpg | (*.png) | *.png | (*.gif) | *.gif | (*.jpeg) | *.jpeg";

            // File opening process
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.BackgroundImage = img;
            }
        }

        // Clear our pictureBox
        /// <summary>
        /// clearButton_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            // Clear process
            graphics.Clear(pictureBox1.BackColor);
            // Creating new bitmap
            pictureBox1.Image = map;
        }
        #endregion

        #region Tools
        // Pencil button
        /// <summary>
        /// button1_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Size for pencil
            pen.Width = 1f;
        }

        // Brush button
        /// <summary>
        /// button3_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // Size for brush
            pen.Width = 8f;
        }

        // Size of our pen (line size) using numericUpDown
        /// <summary>
        /// numericUpDown1_ValueChanged method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Setting our size using numericUpDown value
            pen.Width = (float)numericUpDown1.Value;
        }

        // Eraser button
        /// <summary>
        /// button2_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            pen.Color = Color.White;
        }

        // Bucket button
        /// <summary>
        /// button4_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = button5.BackColor;
        }
        #endregion

        #region Shapes 
        // Line button
        /// <summary>
        /// button15_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            
        }

        // Rectangle button
        /// <summary>
        /// button16_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {

        }

        // Ellipse button
        /// <summary>
        /// button18_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {

        }

        // Text button
        /// <summary>
        /// button19_Click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.IBeam;

            string defValue = "Enter your text here";
            String drawString = Interaction.InputBox("Enter text you want to insert below.\n" +
                "And after that click on Picture Box where you want your text to be placed.", "Insert text", defValue);

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            float x = 150.0F;
            float y = 150.0F;

            // Draw string to screen.
            graphics.DrawString(drawString, drawFont, drawBrush, x, y);
        }
        #endregion

        #region Don't need
        private void button19_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button19_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button19_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
        #endregion

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
