namespace Kifasoft.WordamentSolver
{
  partial class Form1
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
      this.components = new System.ComponentModel.Container();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.buttonSolver = new System.Windows.Forms.Button();
      this.buttonNextBest = new System.Windows.Forms.Button();
      this.buttonNextShortest = new System.Windows.Forms.Button();
      this.timerShowAnimation = new System.Windows.Forms.Timer(this.components);
      this.labelCurrentWord = new System.Windows.Forms.Label();
      this.labelResultFound = new System.Windows.Forms.Label();
      this.buttonStopGeneration = new System.Windows.Forms.Button();
      this.listBoxFoundWord = new System.Windows.Forms.ListBox();
      this.buttonRecordTopLeft = new System.Windows.Forms.Button();
      this.buttonRecordBR = new System.Windows.Forms.Button();
      this.buttonStartBackDrive = new System.Windows.Forms.Button();
      this.labelCoordinate = new System.Windows.Forms.Label();
      this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
      this.listBoxLanguage = new System.Windows.Forms.ListBox();
      this.listBoxProposed = new System.Windows.Forms.ListBox();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 4;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 42);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(218, 203);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // buttonSolver
      // 
      this.buttonSolver.Location = new System.Drawing.Point(12, 251);
      this.buttonSolver.Name = "buttonSolver";
      this.buttonSolver.Size = new System.Drawing.Size(94, 23);
      this.buttonSolver.TabIndex = 1;
      this.buttonSolver.Text = "Start Generation";
      this.buttonSolver.UseVisualStyleBackColor = true;
      this.buttonSolver.Click += new System.EventHandler(this.buttonSolver_Click);
      // 
      // buttonNextBest
      // 
      this.buttonNextBest.Location = new System.Drawing.Point(12, 280);
      this.buttonNextBest.Name = "buttonNextBest";
      this.buttonNextBest.Size = new System.Drawing.Size(94, 23);
      this.buttonNextBest.TabIndex = 3;
      this.buttonNextBest.Text = "Next Best";
      this.buttonNextBest.UseVisualStyleBackColor = true;
      this.buttonNextBest.Click += new System.EventHandler(this.buttonNextBest_Click);
      // 
      // buttonNextShortest
      // 
      this.buttonNextShortest.Location = new System.Drawing.Point(112, 280);
      this.buttonNextShortest.Name = "buttonNextShortest";
      this.buttonNextShortest.Size = new System.Drawing.Size(94, 23);
      this.buttonNextShortest.TabIndex = 4;
      this.buttonNextShortest.Text = "Next Shortest";
      this.buttonNextShortest.UseVisualStyleBackColor = true;
      this.buttonNextShortest.Click += new System.EventHandler(this.buttonNextShortest_Click);
      // 
      // timerShowAnimation
      // 
      this.timerShowAnimation.Interval = 500;
      this.timerShowAnimation.Tick += new System.EventHandler(this.timerShowAnimation_Tick);
      // 
      // labelCurrentWord
      // 
      this.labelCurrentWord.AutoSize = true;
      this.labelCurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelCurrentWord.Location = new System.Drawing.Point(12, 9);
      this.labelCurrentWord.Name = "labelCurrentWord";
      this.labelCurrentWord.Size = new System.Drawing.Size(166, 25);
      this.labelCurrentWord.TabIndex = 100;
      this.labelCurrentWord.Text = "labelCurrentWord";
      // 
      // labelResultFound
      // 
      this.labelResultFound.AutoSize = true;
      this.labelResultFound.Location = new System.Drawing.Point(354, 18);
      this.labelResultFound.Name = "labelResultFound";
      this.labelResultFound.Size = new System.Drawing.Size(35, 13);
      this.labelResultFound.TabIndex = 101;
      this.labelResultFound.Text = "label1";
      // 
      // buttonStopGeneration
      // 
      this.buttonStopGeneration.Location = new System.Drawing.Point(112, 251);
      this.buttonStopGeneration.Name = "buttonStopGeneration";
      this.buttonStopGeneration.Size = new System.Drawing.Size(94, 23);
      this.buttonStopGeneration.TabIndex = 2;
      this.buttonStopGeneration.Text = "Stop Generation";
      this.buttonStopGeneration.UseVisualStyleBackColor = true;
      this.buttonStopGeneration.Click += new System.EventHandler(this.buttonStopGeneration_Click);
      // 
      // listBoxFoundWord
      // 
      this.listBoxFoundWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.listBoxFoundWord.FormattingEnabled = true;
      this.listBoxFoundWord.Location = new System.Drawing.Point(252, 42);
      this.listBoxFoundWord.Name = "listBoxFoundWord";
      this.listBoxFoundWord.Size = new System.Drawing.Size(244, 368);
      this.listBoxFoundWord.TabIndex = 104;
      this.listBoxFoundWord.SelectedIndexChanged += new System.EventHandler(this.listBoxFoundWord_SelectedIndexChanged);
      // 
      // buttonRecordTopLeft
      // 
      this.buttonRecordTopLeft.Location = new System.Drawing.Point(12, 309);
      this.buttonRecordTopLeft.Name = "buttonRecordTopLeft";
      this.buttonRecordTopLeft.Size = new System.Drawing.Size(94, 23);
      this.buttonRecordTopLeft.TabIndex = 5;
      this.buttonRecordTopLeft.Text = "Record TL";
      this.buttonRecordTopLeft.UseVisualStyleBackColor = true;
      this.buttonRecordTopLeft.Click += new System.EventHandler(this.buttonRecordTopLeft_Click);
      // 
      // buttonRecordBR
      // 
      this.buttonRecordBR.Location = new System.Drawing.Point(112, 309);
      this.buttonRecordBR.Name = "buttonRecordBR";
      this.buttonRecordBR.Size = new System.Drawing.Size(94, 23);
      this.buttonRecordBR.TabIndex = 6;
      this.buttonRecordBR.Text = "Record BR";
      this.buttonRecordBR.UseVisualStyleBackColor = true;
      this.buttonRecordBR.Click += new System.EventHandler(this.buttonRecordBR_Click);
      // 
      // buttonStartBackDrive
      // 
      this.buttonStartBackDrive.Location = new System.Drawing.Point(13, 339);
      this.buttonStartBackDrive.Name = "buttonStartBackDrive";
      this.buttonStartBackDrive.Size = new System.Drawing.Size(93, 23);
      this.buttonStartBackDrive.TabIndex = 7;
      this.buttonStartBackDrive.Text = "Start Back Drive";
      this.buttonStartBackDrive.UseVisualStyleBackColor = true;
      this.buttonStartBackDrive.Click += new System.EventHandler(this.buttonStartBackDrive_Click);
      // 
      // labelCoordinate
      // 
      this.labelCoordinate.AutoSize = true;
      this.labelCoordinate.Location = new System.Drawing.Point(12, 367);
      this.labelCoordinate.Name = "labelCoordinate";
      this.labelCoordinate.Size = new System.Drawing.Size(35, 13);
      this.labelCoordinate.TabIndex = 108;
      this.labelCoordinate.Text = "label1";
      // 
      // numericUpDownSpeed
      // 
      this.numericUpDownSpeed.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numericUpDownSpeed.Location = new System.Drawing.Point(113, 341);
      this.numericUpDownSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.numericUpDownSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownSpeed.Name = "numericUpDownSpeed";
      this.numericUpDownSpeed.Size = new System.Drawing.Size(93, 20);
      this.numericUpDownSpeed.TabIndex = 109;
      this.numericUpDownSpeed.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
      // 
      // listBoxLanguage
      // 
      this.listBoxLanguage.FormattingEnabled = true;
      this.listBoxLanguage.Location = new System.Drawing.Point(252, 9);
      this.listBoxLanguage.Name = "listBoxLanguage";
      this.listBoxLanguage.Size = new System.Drawing.Size(96, 30);
      this.listBoxLanguage.TabIndex = 110;
      this.listBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.listBoxLanguage_SelectedIndexChanged);
      // 
      // listBoxProposed
      // 
      this.listBoxProposed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listBoxProposed.FormattingEnabled = true;
      this.listBoxProposed.Location = new System.Drawing.Point(502, 42);
      this.listBoxProposed.Name = "listBoxProposed";
      this.listBoxProposed.Size = new System.Drawing.Size(229, 368);
      this.listBoxProposed.TabIndex = 111;
      this.listBoxProposed.SelectedIndexChanged += new System.EventHandler(this.listBoxProposed_SelectedIndexChanged);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(743, 430);
      this.Controls.Add(this.listBoxProposed);
      this.Controls.Add(this.listBoxLanguage);
      this.Controls.Add(this.numericUpDownSpeed);
      this.Controls.Add(this.labelCoordinate);
      this.Controls.Add(this.buttonStartBackDrive);
      this.Controls.Add(this.buttonRecordBR);
      this.Controls.Add(this.buttonRecordTopLeft);
      this.Controls.Add(this.listBoxFoundWord);
      this.Controls.Add(this.buttonStopGeneration);
      this.Controls.Add(this.labelResultFound);
      this.Controls.Add(this.labelCurrentWord);
      this.Controls.Add(this.buttonNextShortest);
      this.Controls.Add(this.buttonNextBest);
      this.Controls.Add(this.buttonSolver);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button buttonSolver;
    private System.Windows.Forms.Button buttonNextBest;
    private System.Windows.Forms.Button buttonNextShortest;
    private System.Windows.Forms.Timer timerShowAnimation;
    private System.Windows.Forms.Label labelCurrentWord;
    private System.Windows.Forms.Label labelResultFound;
    private System.Windows.Forms.Button buttonStopGeneration;
    private System.Windows.Forms.ListBox listBoxFoundWord;
    private System.Windows.Forms.Button buttonRecordTopLeft;
    private System.Windows.Forms.Button buttonRecordBR;
    private System.Windows.Forms.Button buttonStartBackDrive;
    private System.Windows.Forms.Label labelCoordinate;
    private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
    private System.Windows.Forms.ListBox listBoxLanguage;
    private System.Windows.Forms.ListBox listBoxProposed;
  }
}

