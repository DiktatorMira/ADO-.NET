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
            but_execute1 = new ToolStripMenuItem();
            menu1 = new ToolStripMenuItem();
            menu2 = new ToolStripMenuItem();
            menu3 = new ToolStripMenuItem();
            menu4 = new ToolStripMenuItem();
            menu5 = new ToolStripMenuItem();
            menu6 = new ToolStripMenuItem();
            menu7 = new ToolStripMenuItem();
            menu8 = new ToolStripMenuItem();
            menu9 = new ToolStripMenuItem();
            menu10 = new ToolStripMenuItem();
            menu11 = new ToolStripMenuItem();
            menu12 = new ToolStripMenuItem();
            but_execute2 = new ToolStripMenuItem();
            menu13 = new ToolStripMenuItem();
            menu14 = new ToolStripMenuItem();
            menu15 = new ToolStripMenuItem();
            menu16 = new ToolStripMenuItem();
            menu17 = new ToolStripMenuItem();
            menu18 = new ToolStripMenuItem();
            menu19 = new ToolStripMenuItem();
            menu20 = new ToolStripMenuItem();
            menu21 = new ToolStripMenuItem();
            menu22 = new ToolStripMenuItem();
            menu23 = new ToolStripMenuItem();
            DataGrid = new DataGridView();
            btn_add = new Button();
            text1 = new TextBox();
            text2 = new TextBox();
            text3 = new TextBox();
            text4 = new TextBox();
            menu24 = new ToolStripMenuItem();
            ExecuteStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).BeginInit();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(12, 519);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(340, 40);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "Подключиться к БД";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.Location = new Point(354, 519);
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
            ExecuteStrip.Items.AddRange(new ToolStripItem[] { but_execute1, but_execute2 });
            ExecuteStrip.Location = new Point(0, 0);
            ExecuteStrip.Name = "ExecuteStrip";
            ExecuteStrip.Size = new Size(1078, 28);
            ExecuteStrip.TabIndex = 4;
            ExecuteStrip.Text = "menuStrip1";
            // 
            // but_execute1
            // 
            but_execute1.DropDownItems.AddRange(new ToolStripItem[] { menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, menu9, menu10, menu11, menu12 });
            but_execute1.Enabled = false;
            but_execute1.Name = "but_execute1";
            but_execute1.Size = new Size(255, 24);
            but_execute1.Text = "Выполнить команду (1е задание)";
            // 
            // menu1
            // 
            menu1.Name = "menu1";
            menu1.Size = new Size(511, 26);
            menu1.Text = "Вывести БД";
            menu1.Click += menu_click;
            // 
            // menu2
            // 
            menu2.Name = "menu2";
            menu2.Size = new Size(511, 26);
            menu2.Text = "Вывести все типы канцтоваров";
            menu2.Click += menu_click;
            // 
            // menu3
            // 
            menu3.Name = "menu3";
            menu3.Size = new Size(511, 26);
            menu3.Text = "Вывести всех менеджеров";
            menu3.Click += menu_click;
            // 
            // menu4
            // 
            menu4.Name = "menu4";
            menu4.Size = new Size(511, 26);
            menu4.Text = "Вывести канцтовары с макс количеством единиц";
            menu4.Click += menu_click;
            // 
            // menu5
            // 
            menu5.Name = "menu5";
            menu5.Size = new Size(511, 26);
            menu5.Text = "Вывести канцтовары с мин количеством единиц";
            menu5.Click += menu_click;
            // 
            // menu6
            // 
            menu6.Name = "menu6";
            menu6.Size = new Size(511, 26);
            menu6.Text = "Вывести канцтовары с мин себестоимостью";
            menu6.Click += menu_click;
            // 
            // menu7
            // 
            menu7.Name = "menu7";
            menu7.Size = new Size(511, 26);
            menu7.Text = "Вывести канцтовары с макс себестоимостью";
            menu7.Click += menu_click;
            // 
            // menu8
            // 
            menu8.Name = "menu8";
            menu8.Size = new Size(511, 26);
            menu8.Text = "Вывести канцтовары заданного типа";
            menu8.Click += menu_click;
            // 
            // menu9
            // 
            menu9.Name = "menu9";
            menu9.Size = new Size(511, 26);
            menu9.Text = "Вывести канцтовары, проданные конкретным менеджером";
            menu9.Click += menu_click;
            // 
            // menu10
            // 
            menu10.Name = "menu10";
            menu10.Size = new Size(511, 26);
            menu10.Text = "Вывести канцтовары, купленные конкретной фирмой";
            menu10.Click += menu_click;
            // 
            // menu11
            // 
            menu11.Name = "menu11";
            menu11.Size = new Size(511, 26);
            menu11.Text = "Вывести самую недавнюю продажу";
            menu11.Click += menu_click;
            // 
            // menu12
            // 
            menu12.Name = "menu12";
            menu12.Size = new Size(511, 26);
            menu12.Text = "Вывести среднее кол-во товаров по каждому типу";
            menu12.Click += menu_click;
            // 
            // but_execute2
            // 
            but_execute2.DropDownItems.AddRange(new ToolStripItem[] { menu13, menu14, menu15, menu16, menu17, menu18, menu19, menu20, menu21, menu22, menu23, menu24 });
            but_execute2.Enabled = false;
            but_execute2.Name = "but_execute2";
            but_execute2.Size = new Size(255, 24);
            but_execute2.Text = "Выполнить команду (2е задание)";
            // 
            // menu13
            // 
            menu13.Name = "menu13";
            menu13.Size = new Size(320, 26);
            menu13.Text = "Вставка нового канцтовара";
            menu13.Click += menu_click;
            // 
            // menu14
            // 
            menu14.Name = "menu14";
            menu14.Size = new Size(320, 26);
            menu14.Text = "Вставка нового типа канцтовара";
            menu14.Click += menu_click;
            // 
            // menu15
            // 
            menu15.Name = "menu15";
            menu15.Size = new Size(320, 26);
            menu15.Text = "Вставка нового менеджера";
            menu15.Click += menu_click;
            // 
            // menu16
            // 
            menu16.Name = "menu16";
            menu16.Size = new Size(320, 26);
            menu16.Text = "Вставка новой фирмы";
            menu16.Click += menu_click;
            // 
            // menu17
            // 
            menu17.Name = "menu17";
            menu17.Size = new Size(320, 26);
            menu17.Text = "Обновить канцтовары";
            menu17.Click += menu_click;
            // 
            // menu18
            // 
            menu18.Name = "menu18";
            menu18.Size = new Size(320, 26);
            menu18.Text = "Обновить фирмы";
            menu18.Click += menu_click;
            // 
            // menu19
            // 
            menu19.Name = "menu19";
            menu19.Size = new Size(320, 26);
            menu19.Text = "Обновить менеджеров";
            menu19.Click += menu_click;
            // 
            // menu20
            // 
            menu20.Name = "menu20";
            menu20.Size = new Size(320, 26);
            menu20.Text = "Обновить тип канцтоваров";
            menu20.Click += menu_click;
            // 
            // menu21
            // 
            menu21.Name = "menu21";
            menu21.Size = new Size(320, 26);
            menu21.Text = "Удалить канцтовар";
            menu21.Click += menu_click;
            // 
            // menu22
            // 
            menu22.Name = "menu22";
            menu22.Size = new Size(320, 26);
            menu22.Text = "Удалить менеджера";
            menu22.Click += menu_click;
            // 
            // menu23
            // 
            menu23.Name = "menu23";
            menu23.Size = new Size(320, 26);
            menu23.Text = "Удалить тип канцтовара";
            menu23.Click += menu_click;
            // 
            // DataGrid
            // 
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid.Location = new Point(12, 31);
            DataGrid.Name = "DataGrid";
            DataGrid.RowHeadersWidth = 51;
            DataGrid.Size = new Size(1054, 451);
            DataGrid.TabIndex = 5;
            // 
            // btn_add
            // 
            btn_add.Enabled = false;
            btn_add.Location = new Point(713, 519);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(353, 40);
            btn_add.TabIndex = 6;
            btn_add.Text = "Добавить запись";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // text1
            // 
            text1.Enabled = false;
            text1.Location = new Point(12, 488);
            text1.Name = "text1";
            text1.Size = new Size(263, 27);
            text1.TabIndex = 7;
            // 
            // text2
            // 
            text2.Enabled = false;
            text2.Location = new Point(281, 488);
            text2.Name = "text2";
            text2.Size = new Size(260, 27);
            text2.TabIndex = 8;
            // 
            // text3
            // 
            text3.Enabled = false;
            text3.Location = new Point(547, 488);
            text3.Name = "text3";
            text3.Size = new Size(244, 27);
            text3.TabIndex = 9;
            // 
            // text4
            // 
            text4.Enabled = false;
            text4.Location = new Point(797, 488);
            text4.Name = "text4";
            text4.Size = new Size(269, 27);
            text4.TabIndex = 10;
            // 
            // menu24
            // 
            menu24.Name = "menu24";
            menu24.Size = new Size(320, 26);
            menu24.Text = "Удалить фирму";
            menu24.Click += menu_click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 571);
            Controls.Add(text4);
            Controls.Add(text3);
            Controls.Add(text2);
            Controls.Add(text1);
            Controls.Add(btn_add);
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
        private DataGridView DataGrid;
        private ToolStripMenuItem but_execute1;
        private ToolStripMenuItem but_execute2;
        private ToolStripMenuItem menu1;
        private ToolStripMenuItem menu2;
        private ToolStripMenuItem menu3;
        private ToolStripMenuItem menu4;
        private ToolStripMenuItem menu5;
        private ToolStripMenuItem menu6;
        private ToolStripMenuItem menu7;
        private ToolStripMenuItem menu8;
        private ToolStripMenuItem menu9;
        private ToolStripMenuItem menu10;
        private ToolStripMenuItem menu11;
        private ToolStripMenuItem menu12;
        private Button btn_add;
        private TextBox text1;
        private TextBox text2;
        private TextBox text3;
        private TextBox text4;
        private ToolStripMenuItem menu13;
        private ToolStripMenuItem menu14;
        private ToolStripMenuItem menu15;
        private ToolStripMenuItem menu16;
        private ToolStripMenuItem menu17;
        private ToolStripMenuItem menu18;
        private ToolStripMenuItem menu19;
        private ToolStripMenuItem menu20;
        private ToolStripMenuItem menu21;
        private ToolStripMenuItem menu22;
        private ToolStripMenuItem menu23;
        private ToolStripMenuItem menu24;
    }
}
