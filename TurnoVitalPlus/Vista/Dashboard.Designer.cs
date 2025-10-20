namespace TurnoVitalPlus.Vista
{
    partial class Dashboard
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnSpaces;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnLogout;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnSpaces = new System.Windows.Forms.Button();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(30, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(196, 21);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Bienvenido, [Usuario]";
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(30, 70);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(150, 40);
            this.btnSchedule.TabIndex = 1;
            this.btnSchedule.Text = "Ver Horarios";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // btnSpaces
            // 
            this.btnSpaces.Location = new System.Drawing.Point(30, 120);
            this.btnSpaces.Name = "btnSpaces";
            this.btnSpaces.Size = new System.Drawing.Size(150, 40);
            this.btnSpaces.TabIndex = 2;
            this.btnSpaces.Text = "Ver Espacios";
            this.btnSpaces.UseVisualStyleBackColor = true;
            this.btnSpaces.Click += new System.EventHandler(this.btnSpaces_Click);
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(30, 170);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(150, 40);
            this.btnRequest.TabIndex = 3;
            this.btnRequest.Text = "Solicitar Turno";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Firebrick;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(30, 230);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 40);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Cerrar Sesión";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.btnSpaces);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.lblUserName);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
