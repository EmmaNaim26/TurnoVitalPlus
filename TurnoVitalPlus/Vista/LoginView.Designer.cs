namespace TurnoVitalPlus.Vista
{
    partial class LoginView
    {
        private System.ComponentModel.IContainer components = null!;
        private System.Windows.Forms.Label lblCurp;
        private System.Windows.Forms.TextBox txtCurp;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCurp = new System.Windows.Forms.Label();
            this.txtCurp = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCurp
            // 
            this.lblCurp.AutoSize = true;
            this.lblCurp.Location = new System.Drawing.Point(60, 60);
            this.lblCurp.Name = "lblCurp";
            this.lblCurp.Text = "CURP:";
            // 
            // txtCurp
            // 
            this.txtCurp.Location = new System.Drawing.Point(140, 60);
            this.txtCurp.Name = "txtCurp";
            this.txtCurp.Size = new System.Drawing.Size(200, 23);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(60, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(140, 110);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(140, 160);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 35);
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginView
            // 
            this.Controls.Add(this.lblCurp);
            this.Controls.Add(this.txtCurp);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginView";
            this.Size = new System.Drawing.Size(400, 250);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
