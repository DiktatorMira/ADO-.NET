namespace Dz19._02._2023
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
            connection.Close();
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGrid1 = new DataGridView();
            but_create = new Button();
            btn_add = new Button();
            DataGrid2 = new DataGridView();
            DataGrid3 = new DataGridView();
            btn_delete = new Button();
            btn_update = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGrid1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGrid2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGrid3).BeginInit();
            SuspendLayout();
            // 
            // DataGrid1
            // 
            DataGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid1.Location = new Point(12, 12);
            DataGrid1.Name = "DataGrid1";
            DataGrid1.RowHeadersWidth = 51;
            DataGrid1.Size = new Size(996, 230);
            DataGrid1.TabIndex = 0;
            // 
            // but_create
            // 
            but_create.Location = new Point(12, 690);
            but_create.Name = "but_create";
            but_create.Size = new Size(251, 52);
            but_create.TabIndex = 1;
            but_create.Text = "Создание таблиц";
            but_create.UseVisualStyleBackColor = true;
            but_create.Click += but_create_Click;
            // 
            // btn_add
            // 
            btn_add.Location = new Point(269, 690);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(246, 52);
            btn_add.TabIndex = 2;
            btn_add.Text = "Добавление записей";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // DataGrid2
            // 
            DataGrid2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid2.Location = new Point(12, 248);
            DataGrid2.Name = "DataGrid2";
            DataGrid2.RowHeadersWidth = 51;
            DataGrid2.Size = new Size(996, 228);
            DataGrid2.TabIndex = 3;
            // 
            // DataGrid3
            // 
            DataGrid3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid3.Location = new Point(12, 482);
            DataGrid3.Name = "DataGrid3";
            DataGrid3.RowHeadersWidth = 51;
            DataGrid3.Size = new Size(996, 202);
            DataGrid3.TabIndex = 4;
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(521, 690);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(230, 52);
            btn_delete.TabIndex = 5;
            btn_delete.Text = "Удаление записей";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // btn_update
            // 
            btn_update.Location = new Point(757, 690);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(251, 52);
            btn_update.TabIndex = 6;
            btn_update.Text = "Обновление записей";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 754);
            Controls.Add(btn_update);
            Controls.Add(btn_delete);
            Controls.Add(DataGrid3);
            Controls.Add(DataGrid2);
            Controls.Add(btn_add);
            Controls.Add(but_create);
            Controls.Add(DataGrid1);
            Name = "Form1";
            Text = "Хранилище";
            ((System.ComponentModel.ISupportInitialize)DataGrid1).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGrid2).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGrid3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DataGrid1;
        private Button but_create;
        private Button btn_add;
        private DataGridView DataGrid2;
        private DataGridView DataGrid3;
        private Button btn_delete;
        private Button btn_update;
    }
}
