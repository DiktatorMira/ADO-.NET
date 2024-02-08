namespace FirstTask {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
            ListBox = new ListBox();
            but_connect = new Button();
            but_disconnect = new Button();
            but_execute = new Button();
            SuspendLayout();
            // 
            // ListBox
            // 
            ListBox.FormattingEnabled = true;
            ListBox.Location = new Point(12, 12);
            ListBox.Name = "ListBox";
            ListBox.Size = new Size(368, 164);
            ListBox.TabIndex = 0;
            // 
            // but_connect
            // 
            but_connect.Location = new Point(12, 182);
            but_connect.Name = "but_connect";
            but_connect.Size = new Size(368, 39);
            but_connect.TabIndex = 1;
            but_connect.Text = "Подключиться к БД";
            but_connect.UseVisualStyleBackColor = true;
            but_connect.Click += but_connect_Click;
            // 
            // but_disconnect
            // 
            but_disconnect.Location = new Point(12, 227);
            but_disconnect.Name = "but_disconnect";
            but_disconnect.Size = new Size(368, 39);
            but_disconnect.TabIndex = 2;
            but_disconnect.Text = "Отключиться от БД";
            but_disconnect.UseVisualStyleBackColor = true;
            but_disconnect.Click += but_disconnect_Click;
            // 
            // but_execute
            // 
            but_execute.Enabled = false;
            but_execute.Location = new Point(12, 272);
            but_execute.Name = "but_execute";
            but_execute.Size = new Size(368, 39);
            but_execute.TabIndex = 3;
            but_execute.Text = "Выполнить команду";
            but_execute.UseVisualStyleBackColor = true;
            but_execute.Click += but_execute_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 324);
            Controls.Add(but_execute);
            Controls.Add(but_disconnect);
            Controls.Add(but_connect);
            Controls.Add(ListBox);
            Name = "Form1";
            Text = "Продукты";
            ResumeLayout(false);
        }

        #endregion

        private ListBox ListBox;
        private Button but_connect;
        private Button but_disconnect;
        private Button but_execute;
    }
}
