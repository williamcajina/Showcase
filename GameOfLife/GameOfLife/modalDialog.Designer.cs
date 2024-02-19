namespace GameOfLife
{
    partial class modalDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.numericUpDownTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUniverseHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUniverseWidth = new System.Windows.Forms.NumericUpDown();
            this.labelCellHeight = new System.Windows.Forms.Label();
            this.labelCellWidth = new System.Windows.Forms.Label();
            this.labelMilliseconds = new System.Windows.Forms.Label();
            this.tabPageView = new System.Windows.Forms.TabPage();
            this.labelLiveCellColor = new System.Windows.Forms.Label();
            this.labelBackColor = new System.Windows.Forms.Label();
            this.labelGridColor = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonLiveCellColor = new System.Windows.Forms.Button();
            this.buttonBackColor = new System.Windows.Forms.Button();
            this.buttonGridColor = new System.Windows.Forms.Button();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.groupBoxBoundary = new System.Windows.Forms.GroupBox();
            this.radioButtonFinite = new System.Windows.Forms.RadioButton();
            this.radioButtonToroidal = new System.Windows.Forms.RadioButton();
            this.groupBoxResults.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniverseHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniverseWidth)).BeginInit();
            this.tabPageView.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.groupBoxBoundary.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.buttonCancel);
            this.groupBoxResults.Controls.Add(this.buttonOK);
            this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxResults.Location = new System.Drawing.Point(0, 217);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(284, 45);
            this.groupBoxResults.TabIndex = 0;
            this.groupBoxResults.TabStop = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(203, 16);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(6, 16);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageView);
            this.tabControl1.Controls.Add(this.tabPageAdvanced);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 217);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.numericUpDownTimeInterval);
            this.tabPageGeneral.Controls.Add(this.numericUpDownUniverseHeight);
            this.tabPageGeneral.Controls.Add(this.numericUpDownUniverseWidth);
            this.tabPageGeneral.Controls.Add(this.labelCellHeight);
            this.tabPageGeneral.Controls.Add(this.labelCellWidth);
            this.tabPageGeneral.Controls.Add(this.labelMilliseconds);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(276, 191);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTimeInterval
            // 
            this.numericUpDownTimeInterval.Location = new System.Drawing.Point(185, 23);
            this.numericUpDownTimeInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTimeInterval.Name = "numericUpDownTimeInterval";
            this.numericUpDownTimeInterval.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownTimeInterval.TabIndex = 1;
            // 
            // numericUpDownUniverseHeight
            // 
            this.numericUpDownUniverseHeight.Location = new System.Drawing.Point(185, 99);
            this.numericUpDownUniverseHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownUniverseHeight.Name = "numericUpDownUniverseHeight";
            this.numericUpDownUniverseHeight.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownUniverseHeight.TabIndex = 1;
            // 
            // numericUpDownUniverseWidth
            // 
            this.numericUpDownUniverseWidth.Location = new System.Drawing.Point(185, 62);
            this.numericUpDownUniverseWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownUniverseWidth.Name = "numericUpDownUniverseWidth";
            this.numericUpDownUniverseWidth.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownUniverseWidth.TabIndex = 1;
            // 
            // labelCellHeight
            // 
            this.labelCellHeight.AutoSize = true;
            this.labelCellHeight.Location = new System.Drawing.Point(9, 101);
            this.labelCellHeight.Name = "labelCellHeight";
            this.labelCellHeight.Size = new System.Drawing.Size(131, 13);
            this.labelCellHeight.TabIndex = 0;
            this.labelCellHeight.Text = "Height of Universe in Cells";
            // 
            // labelCellWidth
            // 
            this.labelCellWidth.AutoSize = true;
            this.labelCellWidth.Location = new System.Drawing.Point(9, 64);
            this.labelCellWidth.Name = "labelCellWidth";
            this.labelCellWidth.Size = new System.Drawing.Size(128, 13);
            this.labelCellWidth.TabIndex = 0;
            this.labelCellWidth.Text = "Width of Universe in Cells";
            // 
            // labelMilliseconds
            // 
            this.labelMilliseconds.AutoSize = true;
            this.labelMilliseconds.Location = new System.Drawing.Point(9, 25);
            this.labelMilliseconds.Name = "labelMilliseconds";
            this.labelMilliseconds.Size = new System.Drawing.Size(139, 13);
            this.labelMilliseconds.TabIndex = 0;
            this.labelMilliseconds.Text = "Time Interval in Milliseconds";
            // 
            // tabPageView
            // 
            this.tabPageView.Controls.Add(this.labelLiveCellColor);
            this.tabPageView.Controls.Add(this.labelBackColor);
            this.tabPageView.Controls.Add(this.labelGridColor);
            this.tabPageView.Controls.Add(this.buttonReset);
            this.tabPageView.Controls.Add(this.buttonLiveCellColor);
            this.tabPageView.Controls.Add(this.buttonBackColor);
            this.tabPageView.Controls.Add(this.buttonGridColor);
            this.tabPageView.Location = new System.Drawing.Point(4, 22);
            this.tabPageView.Name = "tabPageView";
            this.tabPageView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageView.Size = new System.Drawing.Size(276, 191);
            this.tabPageView.TabIndex = 1;
            this.tabPageView.Text = "View";
            this.tabPageView.UseVisualStyleBackColor = true;
            // 
            // labelLiveCellColor
            // 
            this.labelLiveCellColor.AutoSize = true;
            this.labelLiveCellColor.Location = new System.Drawing.Point(85, 125);
            this.labelLiveCellColor.Name = "labelLiveCellColor";
            this.labelLiveCellColor.Size = new System.Drawing.Size(74, 13);
            this.labelLiveCellColor.TabIndex = 2;
            this.labelLiveCellColor.Text = "Live Cell Color";
            // 
            // labelBackColor
            // 
            this.labelBackColor.AutoSize = true;
            this.labelBackColor.Location = new System.Drawing.Point(85, 70);
            this.labelBackColor.Name = "labelBackColor";
            this.labelBackColor.Size = new System.Drawing.Size(92, 13);
            this.labelBackColor.TabIndex = 2;
            this.labelBackColor.Text = "Background Color";
            // 
            // labelGridColor
            // 
            this.labelGridColor.AutoSize = true;
            this.labelGridColor.Location = new System.Drawing.Point(85, 20);
            this.labelGridColor.Name = "labelGridColor";
            this.labelGridColor.Size = new System.Drawing.Size(53, 13);
            this.labelGridColor.TabIndex = 2;
            this.labelGridColor.Text = "Grid Color";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(198, 162);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // buttonLiveCellColor
            // 
            this.buttonLiveCellColor.BackColor = System.Drawing.Color.Black;
            this.buttonLiveCellColor.Location = new System.Drawing.Point(18, 120);
            this.buttonLiveCellColor.Name = "buttonLiveCellColor";
            this.buttonLiveCellColor.Size = new System.Drawing.Size(27, 23);
            this.buttonLiveCellColor.TabIndex = 0;
            this.buttonLiveCellColor.UseVisualStyleBackColor = false;
            this.buttonLiveCellColor.Click += new System.EventHandler(this.buttonGridColor_Click);
            // 
            // buttonBackColor
            // 
            this.buttonBackColor.BackColor = System.Drawing.Color.White;
            this.buttonBackColor.Location = new System.Drawing.Point(18, 65);
            this.buttonBackColor.Name = "buttonBackColor";
            this.buttonBackColor.Size = new System.Drawing.Size(27, 23);
            this.buttonBackColor.TabIndex = 0;
            this.buttonBackColor.UseVisualStyleBackColor = false;
            this.buttonBackColor.Click += new System.EventHandler(this.buttonGridColor_Click);
            // 
            // buttonGridColor
            // 
            this.buttonGridColor.BackColor = System.Drawing.Color.DimGray;
            this.buttonGridColor.Location = new System.Drawing.Point(18, 15);
            this.buttonGridColor.Name = "buttonGridColor";
            this.buttonGridColor.Size = new System.Drawing.Size(27, 23);
            this.buttonGridColor.TabIndex = 0;
            this.buttonGridColor.UseVisualStyleBackColor = false;
            this.buttonGridColor.Click += new System.EventHandler(this.buttonGridColor_Click);
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.groupBoxBoundary);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Size = new System.Drawing.Size(276, 191);
            this.tabPageAdvanced.TabIndex = 2;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBoxBoundary
            // 
            this.groupBoxBoundary.Controls.Add(this.radioButtonFinite);
            this.groupBoxBoundary.Controls.Add(this.radioButtonToroidal);
            this.groupBoxBoundary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBoundary.Location = new System.Drawing.Point(0, 0);
            this.groupBoxBoundary.Name = "groupBoxBoundary";
            this.groupBoxBoundary.Size = new System.Drawing.Size(276, 191);
            this.groupBoxBoundary.TabIndex = 0;
            this.groupBoxBoundary.TabStop = false;
            this.groupBoxBoundary.Text = "Boudary Type";
            // 
            // radioButtonFinite
            // 
            this.radioButtonFinite.AutoSize = true;
            this.radioButtonFinite.Location = new System.Drawing.Point(9, 76);
            this.radioButtonFinite.Name = "radioButtonFinite";
            this.radioButtonFinite.Size = new System.Drawing.Size(50, 17);
            this.radioButtonFinite.TabIndex = 0;
            this.radioButtonFinite.Tag = "efwfdf";
            this.radioButtonFinite.Text = "Finite";
            this.radioButtonFinite.UseVisualStyleBackColor = true;
            // 
            // radioButtonToroidal
            // 
            this.radioButtonToroidal.AutoSize = true;
            this.radioButtonToroidal.Checked = true;
            this.radioButtonToroidal.Location = new System.Drawing.Point(9, 38);
            this.radioButtonToroidal.Name = "radioButtonToroidal";
            this.radioButtonToroidal.Size = new System.Drawing.Size(63, 17);
            this.radioButtonToroidal.TabIndex = 0;
            this.radioButtonToroidal.TabStop = true;
            this.radioButtonToroidal.Tag = "0";
            this.radioButtonToroidal.Text = "Toroidal";
            this.radioButtonToroidal.UseVisualStyleBackColor = true;
            // 
            // modalDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBoxResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "modalDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.groupBoxResults.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniverseHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUniverseWidth)).EndInit();
            this.tabPageView.ResumeLayout(false);
            this.tabPageView.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.groupBoxBoundary.ResumeLayout(false);
            this.groupBoxBoundary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownUniverseHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownUniverseWidth;
        private System.Windows.Forms.Label labelCellHeight;
        private System.Windows.Forms.Label labelCellWidth;
        private System.Windows.Forms.Label labelMilliseconds;
        private System.Windows.Forms.TabPage tabPageView;
        private System.Windows.Forms.Label labelLiveCellColor;
        private System.Windows.Forms.Label labelBackColor;
        private System.Windows.Forms.Label labelGridColor;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonLiveCellColor;
        private System.Windows.Forms.Button buttonBackColor;
        private System.Windows.Forms.Button buttonGridColor;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.GroupBox groupBoxBoundary;
        private System.Windows.Forms.RadioButton radioButtonFinite;
        private System.Windows.Forms.RadioButton radioButtonToroidal;
    }
}