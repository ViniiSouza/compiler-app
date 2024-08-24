namespace CompiladorApp
{
    partial class Compilador
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compilador));
            menuPanel = new Panel();
            newButton = new Button();
            openButton = new Button();
            saveButton = new Button();
            copyButton = new Button();
            pasteButton = new Button();
            cutButton = new Button();
            compileButton = new Button();
            teamButton = new Button();
            splitContainer = new SplitContainer();
            lineNumberRtb = new LineNumberRTB();
            messagesTextBox = new TextBox();
            statusBarPanel = new Panel();
            statusBarLabel = new Label();
            menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            statusBarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuPanel
            // 
            menuPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            menuPanel.BackColor = SystemColors.Control;
            menuPanel.Controls.Add(newButton);
            menuPanel.Controls.Add(openButton);
            menuPanel.Controls.Add(saveButton);
            menuPanel.Controls.Add(copyButton);
            menuPanel.Controls.Add(pasteButton);
            menuPanel.Controls.Add(cutButton);
            menuPanel.Controls.Add(compileButton);
            menuPanel.Controls.Add(teamButton);
            menuPanel.Location = new Point(2, 12);
            menuPanel.MinimumSize = new Size(900, 70);
            menuPanel.Name = "menuPanel";
            menuPanel.Size = new Size(900, 100);
            menuPanel.TabIndex = 0;
            // 
            // newButton
            // 
            newButton.Image = (Image)resources.GetObject("newButton.Image");
            newButton.ImageAlign = ContentAlignment.TopCenter;
            newButton.Location = new Point(0, 0);
            newButton.Name = "newButton";
            newButton.Size = new Size(104, 83);
            newButton.TabIndex = 0;
            newButton.Text = "Novo [Ctrl+N]";
            newButton.TextAlign = ContentAlignment.BottomCenter;
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += newButton_Click;
            // 
            // openButton
            // 
            openButton.Image = (Image)resources.GetObject("openButton.Image");
            openButton.ImageAlign = ContentAlignment.TopCenter;
            openButton.Location = new Point(104, 0);
            openButton.Name = "openButton";
            openButton.Size = new Size(104, 83);
            openButton.TabIndex = 1;
            openButton.Text = "Abrir [Ctrl+O]";
            openButton.TextAlign = ContentAlignment.BottomCenter;
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // saveButton
            // 
            saveButton.Image = (Image)resources.GetObject("saveButton.Image");
            saveButton.ImageAlign = ContentAlignment.TopCenter;
            saveButton.Location = new Point(208, 0);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(104, 83);
            saveButton.TabIndex = 2;
            saveButton.Text = "Salvar [Ctrl+S]";
            saveButton.TextAlign = ContentAlignment.BottomCenter;
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // copyButton
            // 
            copyButton.Image = (Image)resources.GetObject("copyButton.Image");
            copyButton.ImageAlign = ContentAlignment.TopCenter;
            copyButton.Location = new Point(312, 0);
            copyButton.Name = "copyButton";
            copyButton.Size = new Size(104, 83);
            copyButton.TabIndex = 3;
            copyButton.Text = "Copiar [Ctrl+C]";
            copyButton.TextAlign = ContentAlignment.BottomCenter;
            copyButton.UseVisualStyleBackColor = true;
            copyButton.Click += copyButton_Click;
            // 
            // pasteButton
            // 
            pasteButton.Image = (Image)resources.GetObject("pasteButton.Image");
            pasteButton.ImageAlign = ContentAlignment.TopCenter;
            pasteButton.Location = new Point(416, 0);
            pasteButton.Name = "pasteButton";
            pasteButton.Size = new Size(104, 83);
            pasteButton.TabIndex = 4;
            pasteButton.Text = "Colar [Ctrl+V]";
            pasteButton.TextAlign = ContentAlignment.BottomCenter;
            pasteButton.UseVisualStyleBackColor = true;
            pasteButton.Click += pasteButton_Click;
            // 
            // cutButton
            // 
            cutButton.Image = (Image)resources.GetObject("cutButton.Image");
            cutButton.ImageAlign = ContentAlignment.TopCenter;
            cutButton.Location = new Point(520, 0);
            cutButton.Name = "cutButton";
            cutButton.Size = new Size(104, 83);
            cutButton.TabIndex = 5;
            cutButton.Text = "Recortar [Ctrl+X]";
            cutButton.TextAlign = ContentAlignment.BottomCenter;
            cutButton.UseVisualStyleBackColor = true;
            cutButton.Click += cutButton_Click;
            // 
            // compileButton
            // 
            compileButton.Image = (Image)resources.GetObject("compileButton.Image");
            compileButton.ImageAlign = ContentAlignment.TopCenter;
            compileButton.Location = new Point(624, 0);
            compileButton.Name = "compileButton";
            compileButton.Size = new Size(104, 83);
            compileButton.TabIndex = 6;
            compileButton.Text = "Compilar [F7]";
            compileButton.TextAlign = ContentAlignment.BottomCenter;
            compileButton.UseVisualStyleBackColor = true;
            compileButton.Click += compileButton_Click;
            // 
            // teamButton
            // 
            teamButton.Image = (Image)resources.GetObject("teamButton.Image");
            teamButton.ImageAlign = ContentAlignment.TopCenter;
            teamButton.Location = new Point(728, 0);
            teamButton.Name = "teamButton";
            teamButton.Size = new Size(104, 83);
            teamButton.TabIndex = 7;
            teamButton.Text = "Equipe [F1]";
            teamButton.TextAlign = ContentAlignment.BottomCenter;
            teamButton.UseVisualStyleBackColor = true;
            teamButton.Click += teamButton_Click;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(2, 118);
            splitContainer.MinimumSize = new Size(900, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(lineNumberRtb);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(messagesTextBox);
            splitContainer.Panel2.RightToLeft = RightToLeft.No;
            splitContainer.Size = new Size(900, 441);
            splitContainer.SplitterDistance = 246;
            splitContainer.TabIndex = 2;
            // 
            // lineNumberRtb
            // 
            lineNumberRtb.BackColor = SystemColors.Window;
            lineNumberRtb.BorderStyle = BorderStyle.Fixed3D;
            lineNumberRtb.Location = new Point(0, 0);
            lineNumberRtb.Name = "lineNumberRtb";
            lineNumberRtb.Size = new Size(898, 244);
            lineNumberRtb.TabIndex = 0;
            // 
            // messagesTextBox
            // 
            messagesTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messagesTextBox.Location = new Point(0, 0);
            messagesTextBox.Multiline = true;
            messagesTextBox.Name = "messagesTextBox";
            messagesTextBox.ReadOnly = true;
            messagesTextBox.ScrollBars = ScrollBars.Vertical;
            messagesTextBox.Size = new Size(900, 191);
            messagesTextBox.TabIndex = 1;
            // 
            // statusBarPanel
            // 
            statusBarPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusBarPanel.Controls.Add(statusBarLabel);
            statusBarPanel.Location = new Point(0, 577);
            statusBarPanel.MinimumSize = new Size(900, 0);
            statusBarPanel.Name = "statusBarPanel";
            statusBarPanel.Size = new Size(900, 25);
            statusBarPanel.TabIndex = 3;
            // 
            // statusBarLabel
            // 
            statusBarLabel.AutoSize = true;
            statusBarLabel.Location = new Point(3, 3);
            statusBarLabel.Name = "statusBarLabel";
            statusBarLabel.Size = new Size(0, 15);
            statusBarLabel.TabIndex = 0;
            // 
            // Compilador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 600);
            Controls.Add(statusBarPanel);
            Controls.Add(splitContainer);
            Controls.Add(menuPanel);
            Name = "Compilador";
            Text = "Compilador";
            KeyDown += Compilador_KeyDown;
            menuPanel.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            statusBarPanel.ResumeLayout(false);
            statusBarPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel menuPanel;
        private SplitContainer splitContainer;
        private Button newButton;
        private Button teamButton;
        private Button compileButton;
        private Button cutButton;
        private Button pasteButton;
        private Button copyButton;
        private Button saveButton;
        private Button openButton;
        private TextBox messagesTextBox;
        private Panel statusBarPanel;
        private Label statusBarLabel;
        private LineNumberRTB lineNumberRtb;
    }
}
