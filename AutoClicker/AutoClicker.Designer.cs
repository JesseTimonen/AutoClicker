namespace AutoClicker
{
    partial class AutoClicker
    {
        private System.ComponentModel.IContainer components = null;

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
            this.MouseButton = new System.Windows.Forms.Button();
            this.KeyboardButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.iterationLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedTextbox = new System.Windows.Forms.TextBox();
            this.timeTimer = new System.Windows.Forms.Timer(this.components);
            this.iterationTimer = new System.Windows.Forms.Timer(this.components);
            this.patternTextbox = new System.Windows.Forms.TextBox();
            this.patternLabel = new System.Windows.Forms.Label();
            this.modeButton = new System.Windows.Forms.Button();
            this.patternSeperatorLabel = new System.Windows.Forms.Label();
            this.patternSeperatorTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MouseButton
            // 
            this.MouseButton.Location = new System.Drawing.Point(0, -1);
            this.MouseButton.Name = "MouseButton";
            this.MouseButton.Size = new System.Drawing.Size(92, 36);
            this.MouseButton.TabIndex = 0;
            this.MouseButton.Text = "Mouse";
            this.MouseButton.UseVisualStyleBackColor = true;
            this.MouseButton.Click += new System.EventHandler(this.MouseButton_Click);
            // 
            // KeyboardButton
            // 
            this.KeyboardButton.Location = new System.Drawing.Point(89, -1);
            this.KeyboardButton.Name = "KeyboardButton";
            this.KeyboardButton.Size = new System.Drawing.Size(92, 36);
            this.KeyboardButton.TabIndex = 1;
            this.KeyboardButton.Text = "Keyboard";
            this.KeyboardButton.UseVisualStyleBackColor = true;
            this.KeyboardButton.Click += new System.EventHandler(this.KeyboardButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(372, 36);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(131, 55);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Enabled = false;
            this.endButton.Location = new System.Drawing.Point(372, 121);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(131, 55);
            this.endButton.TabIndex = 3;
            this.endButton.Text = "End";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(30, 212);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(112, 17);
            this.timeLabel.TabIndex = 4;
            this.timeLabel.Text = "Time: 0 seconds";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(369, 212);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(52, 17);
            this.status.TabIndex = 6;
            this.status.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(416, 212);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 17);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "OFF";
            // 
            // iterationLabel
            // 
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Location = new System.Drawing.Point(30, 195);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(82, 17);
            this.iterationLabel.TabIndex = 8;
            this.iterationLabel.Text = "Iterations: 0";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(30, 55);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(104, 17);
            this.speedLabel.TabIndex = 10;
            this.speedLabel.Text = "Clicks / Second";
            // 
            // speedTextbox
            // 
            this.speedTextbox.Location = new System.Drawing.Point(33, 75);
            this.speedTextbox.Name = "speedTextbox";
            this.speedTextbox.Size = new System.Drawing.Size(110, 22);
            this.speedTextbox.TabIndex = 11;
            // 
            // timeTimer
            // 
            this.timeTimer.Interval = 1000;
            this.timeTimer.Tick += new System.EventHandler(this.timeTimer_Tick);
            // 
            // iterationTimer
            // 
            this.iterationTimer.Interval = 1000;
            this.iterationTimer.Tick += new System.EventHandler(this.iterationTimer_Tick);
            // 
            // patternTextbox
            // 
            this.patternTextbox.Location = new System.Drawing.Point(33, 129);
            this.patternTextbox.Multiline = true;
            this.patternTextbox.Name = "patternTextbox";
            this.patternTextbox.Size = new System.Drawing.Size(265, 54);
            this.patternTextbox.TabIndex = 12;
            this.patternTextbox.Visible = false;
            // 
            // patternLabel
            // 
            this.patternLabel.AutoSize = true;
            this.patternLabel.Location = new System.Drawing.Point(30, 109);
            this.patternLabel.Name = "patternLabel";
            this.patternLabel.Size = new System.Drawing.Size(54, 17);
            this.patternLabel.TabIndex = 13;
            this.patternLabel.Text = "Pattern";
            this.patternLabel.Visible = false;
            // 
            // modeButton
            // 
            this.modeButton.Location = new System.Drawing.Point(152, 71);
            this.modeButton.Name = "modeButton";
            this.modeButton.Size = new System.Drawing.Size(65, 30);
            this.modeButton.TabIndex = 15;
            this.modeButton.Text = "Mode";
            this.modeButton.UseVisualStyleBackColor = true;
            this.modeButton.Click += new System.EventHandler(this.modeButton_Click);
            // 
            // patternSeperatorLabel
            // 
            this.patternSeperatorLabel.AutoSize = true;
            this.patternSeperatorLabel.Location = new System.Drawing.Point(227, 195);
            this.patternSeperatorLabel.Name = "patternSeperatorLabel";
            this.patternSeperatorLabel.Size = new System.Drawing.Size(71, 17);
            this.patternSeperatorLabel.TabIndex = 16;
            this.patternSeperatorLabel.Text = "Seperator";
            this.patternSeperatorLabel.Visible = false;
            // 
            // patternSeperatorTextbox
            // 
            this.patternSeperatorTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patternSeperatorTextbox.Location = new System.Drawing.Point(230, 215);
            this.patternSeperatorTextbox.MaxLength = 1;
            this.patternSeperatorTextbox.Name = "patternSeperatorTextbox";
            this.patternSeperatorTextbox.Size = new System.Drawing.Size(68, 24);
            this.patternSeperatorTextbox.TabIndex = 17;
            this.patternSeperatorTextbox.Text = ",";
            this.patternSeperatorTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.patternSeperatorTextbox.Visible = false;
            // 
            // AutoClicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 267);
            this.Controls.Add(this.patternSeperatorTextbox);
            this.Controls.Add(this.patternSeperatorLabel);
            this.Controls.Add(this.modeButton);
            this.Controls.Add(this.patternLabel);
            this.Controls.Add(this.patternTextbox);
            this.Controls.Add(this.speedTextbox);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.iterationLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.status);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.KeyboardButton);
            this.Controls.Add(this.MouseButton);
            this.Name = "AutoClicker";
            this.Text = "AutoClicker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button MouseButton;
        private System.Windows.Forms.Button KeyboardButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label iterationLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TextBox speedTextbox;
        private System.Windows.Forms.Timer timeTimer;
        private System.Windows.Forms.Timer iterationTimer;
        private System.Windows.Forms.TextBox patternTextbox;
        private System.Windows.Forms.Label patternLabel;
        private System.Windows.Forms.Button modeButton;
        private System.Windows.Forms.Label patternSeperatorLabel;
        private System.Windows.Forms.TextBox patternSeperatorTextbox;
    }
}