namespace SecondTask
{
    partial class Form1
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
            btn_connect = new Button();
            btn_disconnect = new Button();
            ExecuteStrip = new MenuStrip();
            btn_execute = new ToolStripMenuItem();
            menu10 = new ToolStripMenuItem();
            menu1 = new ToolStripMenuItem();
            menu2 = new ToolStripMenuItem();
            menu3 = new ToolStripMenuItem();
            menu4 = new ToolStripMenuItem();
            menu5 = new ToolStripMenuItem();
            menu6 = new ToolStripMenuItem();
            menu7 = new ToolStripMenuItem();
            menu8 = new ToolStripMenuItem();
            menu9 = new ToolStripMenuItem();
            DataGrid = new DataGridView();
            text1 = new TextBox();
            text2 = new TextBox();
            text3 = new TextBox();
            text4 = new TextBox();
            btn_add = new Button();
            ExecuteStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).BeginInit();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(12, 399);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(340, 40);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "Подключиться к БД";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.Location = new Point(358, 398);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(353, 40);
            btn_disconnect.TabIndex = 2;
            btn_disconnect.Text = "Отключиться от БД";
            btn_disconnect.UseVisualStyleBackColor = true;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // ExecuteStrip
            // 
            ExecuteStrip.ImageScalingSize = new Size(20, 20);
            ExecuteStrip.Items.AddRange(new ToolStripItem[] { btn_execute });
            ExecuteStrip.Location = new Point(0, 0);
            ExecuteStrip.Name = "ExecuteStrip";
            ExecuteStrip.Size = new Size(1078, 28);
            ExecuteStrip.TabIndex = 4;
            ExecuteStrip.Text = "menuStrip1";
            // 
            // btn_execute
            // 
            btn_execute.DropDownItems.AddRange(new ToolStripItem[] { menu10, menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, menu9 });
            btn_execute.Enabled = false;
            btn_execute.Name = "btn_execute";
            btn_execute.Size = new Size(164, 24);
            btn_execute.Text = "Выполнить команду";
            // 
            // menu10
            // 
            menu10.Name = "menu10";
            menu10.Size = new Size(294, 26);
            menu10.Text = "Вывести все таблицы";
            menu10.Click += menu_click;
            // 
            // menu1
            // 
            menu1.Name = "menu1";
            menu1.Size = new Size(294, 26);
            menu1.Text = "Вставить новый  товар";
            menu1.Click += menu_click;
            // 
            // menu2
            // 
            menu2.Name = "menu2";
            menu2.Size = new Size(294, 26);
            menu2.Text = "Вставить новый тип товара";
            menu2.Click += menu_click;
            // 
            // menu3
            // 
            menu3.Name = "menu3";
            menu3.Size = new Size(294, 26);
            menu3.Text = "Вставить нового поставщика";
            menu3.Click += menu_click;
            // 
            // menu4
            // 
            menu4.Name = "menu4";
            menu4.Size = new Size(294, 26);
            menu4.Text = "Обновить товары";
            menu4.Click += menu_click;
            // 
            // menu5
            // 
            menu5.Name = "menu5";
            menu5.Size = new Size(294, 26);
            menu5.Text = "Обновить типы товаров";
            menu5.Click += menu_click;
            // 
            // menu6
            // 
            menu6.Name = "menu6";
            menu6.Size = new Size(294, 26);
            menu6.Text = "Обновить поставщиков";
            menu6.Click += menu_click;
            // 
            // menu7
            // 
            menu7.Name = "menu7";
            menu7.Size = new Size(294, 26);
            menu7.Text = "Удалить товар";
            menu7.Click += menu_click;
            // 
            // menu8
            // 
            menu8.Name = "menu8";
            menu8.Size = new Size(294, 26);
            menu8.Text = "Удалить тип товара";
            menu8.Click += menu_click;
            // 
            // menu9
            // 
            menu9.Name = "menu9";
            menu9.Size = new Size(294, 26);
            menu9.Text = "Удалить поставщика";
            menu9.Click += menu_click;
            // 
            // DataGrid
            // 
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid.Location = new Point(12, 31);
            DataGrid.Name = "DataGrid";
            DataGrid.RowHeadersWidth = 51;
            DataGrid.Size = new Size(1054, 328);
            DataGrid.TabIndex = 5;
            // 
            // text1
            // 
            text1.Enabled = false;
            text1.Location = new Point(12, 365);
            text1.Name = "text1";
            text1.Size = new Size(264, 27);
            text1.TabIndex = 6;
            // 
            // text2
            // 
            text2.Enabled = false;
            text2.Location = new Point(282, 365);
            text2.Name = "text2";
            text2.Size = new Size(261, 27);
            text2.TabIndex = 7;
            // 
            // text3
            // 
            text3.Enabled = false;
            text3.Location = new Point(549, 365);
            text3.Name = "text3";
            text3.Size = new Size(261, 27);
            text3.TabIndex = 8;
            // 
            // text4
            // 
            text4.Enabled = false;
            text4.Location = new Point(816, 365);
            text4.Name = "text4";
            text4.Size = new Size(250, 27);
            text4.TabIndex = 9;
            // 
            // btn_add
            // 
            btn_add.Enabled = false;
            btn_add.Location = new Point(717, 398);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(349, 40);
            btn_add.TabIndex = 10;
            btn_add.Text = "Добавить запись";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 451);
            Controls.Add(btn_add);
            Controls.Add(text4);
            Controls.Add(text3);
            Controls.Add(text2);
            Controls.Add(text1);
            Controls.Add(DataGrid);
            Controls.Add(btn_disconnect);
            Controls.Add(btn_connect);
            Controls.Add(ExecuteStrip);
            MainMenuStrip = ExecuteStrip;
            Name = "Form1";
            Text = "Склад";
            ExecuteStrip.ResumeLayout(false);
            ExecuteStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_connect;
        private Button btn_disconnect;
        private MenuStrip ExecuteStrip;
        private ToolStripMenuItem btn_execute;
        private ToolStripMenuItem menu1;
        private ToolStripMenuItem menu2;
        private ToolStripMenuItem menu3;
        private ToolStripMenuItem menu4;
        private ToolStripMenuItem menu5;
        private ToolStripMenuItem menu6;
        private ToolStripMenuItem menu7;
        private DataGridView DataGrid;
        private ToolStripMenuItem menu10;
        private ToolStripMenuItem menu8;
        private ToolStripMenuItem menu9;
        private TextBox text1;
        private TextBox text2;
        private TextBox text3;
        private TextBox text4;
        private Button btn_add;
    }
}
