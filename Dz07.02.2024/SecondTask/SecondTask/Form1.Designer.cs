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
            ListBox = new ListBox();
            btn_connect = new Button();
            btn_disconnect = new Button();
            btn_execute = new Button();
            SuspendLayout();
            // 
            // ListBox
            // 
            ListBox.FormattingEnabled = true;
            ListBox.Location = new Point(12, 12);
            ListBox.Name = "ListBox";
            ListBox.Size = new Size(534, 204);
            ListBox.TabIndex = 0;
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(12, 222);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(534, 40);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "Подключиться к БД";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.Location = new Point(12, 268);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(534, 40);
            btn_disconnect.TabIndex = 2;
            btn_disconnect.Text = "Отключиться от БД";
            btn_disconnect.UseVisualStyleBackColor = true;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // btn_execute
            // 
            btn_execute.Enabled = false;
            btn_execute.Location = new Point(12, 314);
            btn_execute.Name = "btn_execute";
            btn_execute.Size = new Size(534, 40);
            btn_execute.TabIndex = 3;
            btn_execute.Text = "Выполнить команду";
            btn_execute.UseVisualStyleBackColor = true;
            btn_execute.Click += btn_execute_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 374);
            Controls.Add(btn_execute);
            Controls.Add(btn_disconnect);
            Controls.Add(btn_connect);
            Controls.Add(ListBox);
            Name = "Form1";
            Text = "Склад";
            ResumeLayout(false);
        }

        #endregion

        private ListBox ListBox;
        private Button btn_connect;
        private Button btn_disconnect;
        private Button btn_execute;
    }
}
