using ProtectedPapyrus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPapyrus
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            control.SetIntial(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "Enter Username";
                    textBox1.ForeColor = Color.Gray;
                    return;
                }
                textBox1.ForeColor = Color.White;
                pwInvalid.Visible = false;
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            try
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Enter Password";
                    textBox2.ForeColor = Color.Gray;
                    return;
                }
                textBox2.ForeColor = Color.White;
                pwInvalid.Visible = false;
            }
            catch { }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            pnlLogo.Dock = DockStyle.Right;
            pnlForget.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

            pnlLogo.Dock = DockStyle.Right;
            pnlSignup.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Username") // get username from database
            {
                usInvalid.Visible = true;
                return;
            }
            if (textBox2.Text == "Enter Password") // get password from database
            {
                pwInvalid.Visible = true;
                return;
            }
            else
            {
                this.Hide();
                var menu = new Menu();
                menu.Show();
            }
        }

        private void haveAccount_Click(object sender, EventArgs e)
        {

            pnlLogo.Dock = DockStyle.Left;
            pnlSignup.Visible = false;
        }

        private void signupBack_Click(object sender, EventArgs e)
        {
            pnlLogo.Dock = DockStyle.Left;
            pnlSignup.Visible = false;
        }

        private void signup_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == "Enter Username") // get username from database
            {
                lblUsername.Visible = true;
                return;
            }
            if (textBox4.Text == "Enter Master Password") // get username from database
            {
                lblmp.Visible = true;
                return;
            }
            if (textBox5.Text == "Enter Password") // get username from database
            {
                lblPassword.Visible = true;
                return;

            }
            if (textBox6.Text == "Confirm Password") // get username from database
            {
                lblConPass.Visible = true;
                return;
            }

            if (textBox5.Text != textBox6.Text)
            {
                lblNoMatch.Visible = true;
                return;
            }
            else
            {
                //New user created
                pnlLogo.Dock = DockStyle.Left;
                pnlSignup.Visible = false;
            }
        }

        

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.SelectAll();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    textBox5.ForeColor = Color.Gray;
                    lblPassword.Visible = true;
                    lblNoMatch.Visible = false;
                    return;
                }
                textBox5.ForeColor = Color.White;
                textBox5.UseSystemPasswordChar = true;
                lblNoMatch.Visible = false;
                lblPassword.Visible = false;
            }
            catch { }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "")
                {
                    textBox6.ForeColor = Color.Gray;
                    lblConPass.Visible = true;
                    lblNoMatch.Visible = false;
                    return;
                }
                textBox6.ForeColor = Color.White;
                textBox6.UseSystemPasswordChar = true;
                lblNoMatch.Visible = false;
                lblConPass.Visible = false;
            }
            catch { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = true;
            try
            {
                if (textBox4.Text == "")
                {
                    textBox4.ForeColor = Color.Gray;
                    lblmp.Visible = true;
                    return;
                }

                textBox4.ForeColor = Color.White;
                lblmp.Visible = false;
            }
            catch { }
        }
        int click = 0;  
        private void verifybtn_Click(object sender, EventArgs e)
        {
            if (click == 0)
            {
                //if verify success
                click++;
                panel22.Visible = false;
                panel20.Visible = false;
                newPw.Visible = true;
                conNewPw.Visible = true;
                label16.Text = "Welcome back, Chief";
            }
            else
            {
                try
                {
                    if (fgetConPw.Text == fgetNewPw.Text)
                    {
                        click--;
                        pnlLogo.Dock = DockStyle.Left;
                        pnlForget.Visible = false;
                        fgetPwNm.Visible = false;
                    }
                    else
                    {
                        fgetPwNm.Visible = true;
                    }
                }
                catch { }
            }
        }

        private void fgetMasterPw_Click(object sender, EventArgs e)
        {
            fgetMasterPw.SelectAll();
        }

        private void fgetMasterPw_TextChanged(object sender, EventArgs e)
        {
            fgetMasterPw.UseSystemPasswordChar = true;
            try
            {
                if (fgetMasterPw.Text == "")
                {
                    fgetMasterPw.ForeColor = Color.Gray;
                    fgetlblMP.Visible = true;
                    return;
                }
                fgetMasterPw.ForeColor = Color.White;
                fgetlblMP.Visible = false;
            }
            catch { }
        }

        private void fgetUser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fgetUser.Text == "")
                {
                    fgetUser.ForeColor = Color.Gray;
                    fgetlbl1.Visible = true;
                    return;
                }
                fgetUser.ForeColor = Color.White;
                fgetlbl1.Visible = false;
            }
            catch { }
        }

        private void fgetNewPw_Click(object sender, EventArgs e)
        {
            fgetNewPw.SelectAll();
        }

        private void fgetNewPw_TextChanged(object sender, EventArgs e)
        {
            fgetNewPw.UseSystemPasswordChar = true;
            try
            {
                if (fgetNewPw.Text == "")
                {
                    fgetNewPw.ForeColor = Color.Gray;
                    fgetlbl1.Visible = true;
                    return;
                }
                fgetNewPw.ForeColor = Color.White;
                fgetlbl1.Visible = false;
            }
            catch { }
        }

        private void fgetConPw_Click(object sender, EventArgs e)
        {
            fgetConPw.SelectAll(); 
        }

        private void fgetConPw_TextChanged(object sender, EventArgs e)
        {
            fgetConPw.UseSystemPasswordChar = true;
            try
            {
                if (fgetConPw.Text == "")
                {
                    fgetConPw.ForeColor = Color.Gray;
                    fgetPwNm.Visible = true;
                    return;
                }
                fgetConPw.ForeColor = Color.White;
                fgetPwNm.Visible = false;
            }
            catch { }
        }

        private void fgetBack_Click(object sender, EventArgs e)
        {

            pnlLogo.Dock = DockStyle.Left;
            pnlForget.Visible = false;
        }

        int mov, movX, movY;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
            this.Opacity = 0.7;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            mov = 0;
            this.Opacity = 1;
        }
    }
}
