namespace Grimoire
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            files_listBox = new ListBox();
            files_groupBox = new GroupBox();
            sortButton = new Button();
            removeButton = new Button();
            selectButton = new Button();
            services_groupBox = new GroupBox();
            syncButton = new Button();
            renameButton = new Button();
            input_groupBox = new GroupBox();
            number_label = new Label();
            animeName_label = new Label();
            numberNumericUpDown = new NumericUpDown();
            nameTextBox = new TextBox();
            label1 = new Label();
            modeComboBox = new ComboBox();
            progressBar1 = new ProgressBar();
            files_groupBox.SuspendLayout();
            services_groupBox.SuspendLayout();
            input_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // files_listBox
            // 
            files_listBox.BackColor = Color.FromArgb(0, 0, 170);
            files_listBox.BorderStyle = BorderStyle.FixedSingle;
            files_listBox.ForeColor = Color.LightGray;
            files_listBox.FormattingEnabled = true;
            files_listBox.HorizontalScrollbar = true;
            files_listBox.Location = new Point(36, 15);
            files_listBox.Margin = new Padding(4);
            files_listBox.Name = "files_listBox";
            files_listBox.RightToLeft = RightToLeft.No;
            files_listBox.SelectionMode = SelectionMode.MultiExtended;
            files_listBox.Size = new Size(367, 534);
            files_listBox.TabIndex = 0;
            // 
            // files_groupBox
            // 
            files_groupBox.BackColor = Color.Transparent;
            files_groupBox.Controls.Add(sortButton);
            files_groupBox.Controls.Add(removeButton);
            files_groupBox.Controls.Add(selectButton);
            files_groupBox.ForeColor = Color.LightGray;
            files_groupBox.Location = new Point(756, 15);
            files_groupBox.Margin = new Padding(4);
            files_groupBox.Name = "files_groupBox";
            files_groupBox.Padding = new Padding(4);
            files_groupBox.Size = new Size(257, 138);
            files_groupBox.TabIndex = 1;
            files_groupBox.TabStop = false;
            files_groupBox.Text = "Fájlok kezelése";
            // 
            // sortButton
            // 
            sortButton.FlatAppearance.MouseDownBackColor = Color.White;
            sortButton.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            sortButton.FlatStyle = FlatStyle.Flat;
            sortButton.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            sortButton.ForeColor = Color.LightGray;
            sortButton.Location = new Point(8, 101);
            sortButton.Margin = new Padding(4);
            sortButton.Name = "sortButton";
            sortButton.Size = new Size(242, 29);
            sortButton.TabIndex = 2;
            sortButton.Text = "[ Rendezés epizódszám szerint ]";
            sortButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            removeButton.FlatAppearance.MouseDownBackColor = Color.White;
            removeButton.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            removeButton.FlatStyle = FlatStyle.Flat;
            removeButton.ForeColor = Color.LightGray;
            removeButton.Location = new Point(8, 65);
            removeButton.Margin = new Padding(4);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(242, 29);
            removeButton.TabIndex = 1;
            removeButton.Text = "[ Kijelölt törlése ]";
            removeButton.UseVisualStyleBackColor = true;
            // 
            // selectButton
            // 
            selectButton.FlatAppearance.MouseDownBackColor = Color.White;
            selectButton.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            selectButton.FlatStyle = FlatStyle.Flat;
            selectButton.ForeColor = Color.LightGray;
            selectButton.Location = new Point(8, 28);
            selectButton.Margin = new Padding(4);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(242, 29);
            selectButton.TabIndex = 0;
            selectButton.Text = "[ Fájlok hozzáadása ]";
            selectButton.UseVisualStyleBackColor = true;
            // 
            // services_groupBox
            // 
            services_groupBox.BackColor = Color.Transparent;
            services_groupBox.Controls.Add(syncButton);
            services_groupBox.Controls.Add(renameButton);
            services_groupBox.ForeColor = Color.LightGray;
            services_groupBox.Location = new Point(756, 161);
            services_groupBox.Margin = new Padding(4);
            services_groupBox.Name = "services_groupBox";
            services_groupBox.Padding = new Padding(4);
            services_groupBox.Size = new Size(257, 101);
            services_groupBox.TabIndex = 3;
            services_groupBox.TabStop = false;
            services_groupBox.Text = "Szolgáltatások";
            // 
            // syncButton
            // 
            syncButton.FlatAppearance.MouseDownBackColor = Color.White;
            syncButton.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            syncButton.FlatStyle = FlatStyle.Flat;
            syncButton.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
            syncButton.ForeColor = Color.LightGray;
            syncButton.Location = new Point(8, 65);
            syncButton.Margin = new Padding(4);
            syncButton.Name = "syncButton";
            syncButton.Size = new Size(242, 29);
            syncButton.TabIndex = 1;
            syncButton.Text = "< Szinkronizálás indítása >";
            syncButton.UseVisualStyleBackColor = true;
            // 
            // renameButton
            // 
            renameButton.FlatAppearance.MouseDownBackColor = Color.White;
            renameButton.FlatAppearance.MouseOverBackColor = Color.DarkCyan;
            renameButton.FlatStyle = FlatStyle.Flat;
            renameButton.ForeColor = Color.LightGray;
            renameButton.Location = new Point(8, 28);
            renameButton.Margin = new Padding(4);
            renameButton.Name = "renameButton";
            renameButton.Size = new Size(242, 29);
            renameButton.TabIndex = 0;
            renameButton.Text = "< Átnevezés indítása >";
            renameButton.UseVisualStyleBackColor = true;
            // 
            // input_groupBox
            // 
            input_groupBox.BackColor = Color.Transparent;
            input_groupBox.Controls.Add(number_label);
            input_groupBox.Controls.Add(animeName_label);
            input_groupBox.Controls.Add(numberNumericUpDown);
            input_groupBox.Controls.Add(nameTextBox);
            input_groupBox.Controls.Add(label1);
            input_groupBox.Controls.Add(modeComboBox);
            input_groupBox.ForeColor = Color.LightGray;
            input_groupBox.Location = new Point(756, 270);
            input_groupBox.Margin = new Padding(4);
            input_groupBox.Name = "input_groupBox";
            input_groupBox.Padding = new Padding(4);
            input_groupBox.Size = new Size(257, 204);
            input_groupBox.TabIndex = 4;
            input_groupBox.TabStop = false;
            input_groupBox.Text = "Bemenetek";
            // 
            // number_label
            // 
            number_label.AutoSize = true;
            number_label.Location = new Point(8, 144);
            number_label.Margin = new Padding(4, 0, 4, 0);
            number_label.Name = "number_label";
            number_label.Size = new Size(162, 19);
            number_label.TabIndex = 6;
            number_label.Text = "Számozás megadása";
            // 
            // animeName_label
            // 
            animeName_label.AutoSize = true;
            animeName_label.Location = new Point(8, 24);
            animeName_label.Margin = new Padding(4, 0, 4, 0);
            animeName_label.Name = "animeName_label";
            animeName_label.Size = new Size(144, 19);
            animeName_label.TabIndex = 1;
            animeName_label.Text = "Fájlnév megadás";
            // 
            // numberNumericUpDown
            // 
            numberNumericUpDown.BackColor = Color.LightGray;
            numberNumericUpDown.Location = new Point(8, 167);
            numberNumericUpDown.Margin = new Padding(4);
            numberNumericUpDown.Name = "numberNumericUpDown";
            numberNumericUpDown.Size = new Size(154, 26);
            numberNumericUpDown.TabIndex = 5;
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nameTextBox
            // 
            nameTextBox.BackColor = Color.LightGray;
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.ForeColor = Color.Black;
            nameTextBox.Location = new Point(8, 47);
            nameTextBox.Margin = new Padding(4);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(241, 26);
            nameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 86);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(144, 19);
            label1.TabIndex = 8;
            label1.Text = "Szinkron típusa";
            // 
            // modeComboBox
            // 
            modeComboBox.BackColor = Color.LightGray;
            modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeComboBox.FlatStyle = FlatStyle.Popup;
            modeComboBox.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            modeComboBox.ForeColor = Color.Black;
            modeComboBox.FormattingEnabled = true;
            modeComboBox.Location = new Point(8, 109);
            modeComboBox.Margin = new Padding(4);
            modeComboBox.Name = "modeComboBox";
            modeComboBox.Size = new Size(241, 23);
            modeComboBox.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(411, 523);
            progressBar1.Margin = new Padding(4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(602, 29);
            progressBar1.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 170);
            ClientSize = new Size(1029, 570);
            Controls.Add(progressBar1);
            Controls.Add(input_groupBox);
            Controls.Add(services_groupBox);
            Controls.Add(files_groupBox);
            Controls.Add(files_listBox);
            Font = new Font("Consolas", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            ForeColor = Color.LightGray;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Grimoire";
            files_groupBox.ResumeLayout(false);
            services_groupBox.ResumeLayout(false);
            input_groupBox.ResumeLayout(false);
            input_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox files_listBox;
        private GroupBox files_groupBox;
        private Button sortButton;
        private Button removeButton;
        private Button selectButton;
        private GroupBox services_groupBox;
        private Button syncButton;
        private Button renameButton;
        private GroupBox input_groupBox;
        private TextBox nameTextBox;
        private Label animeName_label;
        private NumericUpDown numberNumericUpDown;
        private Label number_label;
        private ProgressBar progressBar1;
        private ComboBox modeComboBox;
        private Label label1;
    }
}
