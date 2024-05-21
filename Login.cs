using ProtectedPapyrus;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjectPapyrus
{
    public partial class Login : Form
    {
        private SqlConnection connection;

        public Login()
        {
            InitializeComponent();
            control.SetIntial(this);
            textBox3.TextChanged += textBox3_TextChanged;

            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;Connect Timeout=30;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
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

        private Guid userID = Guid.Empty;

        private void label4_Click(object sender, EventArgs e)
        {
            pnlLogo.Dock = DockStyle.Right;
            pnlSignup.Visible = true;

            // Use the class-level userID
            userID = Guid.NewGuid();

            string insertQuery = "INSERT INTO [user] (userID) VALUES (@userID)";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.UniqueIdentifier) { Value = userID });
                insertCommand.ExecuteNonQuery();
            }
        }



        private bool VerifyPassword(string enteredPassword, string storedPassword, string salt)
        {
            string enteredPasswordHash = HashPassword(enteredPassword, salt);
            return storedPassword.Equals(enteredPasswordHash);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string enteredPassword = textBox2.Text; // The password entered by the user

                string selectQuery = "SELECT userID , Password, PasswordSalt FROM user_info WHERE Name = @Name";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Name", username);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            string passwordSalt = reader["PasswordSalt"].ToString();
                            userID = reader.GetGuid(reader.GetOrdinal("UserID"));

                            if (userID != Guid.Empty)
                            {
                            UserManager.SetUserID(userID);
                            }
                            // Verify the entered password against the stored password
                            if (VerifyPassword(enteredPassword, storedPassword, passwordSalt))
                            {
                                this.Hide();
                                var menu = new Menu();
                                menu.Show();
                            }
                            else
                            {
                                MessageBox.Show("Sign-in failed. Incorrect password.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
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

        private string GenerateRandomSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];

                saltBytes.CopyTo(saltedPasswordBytes, 0);
                passwordBytes.CopyTo(saltedPasswordBytes, saltBytes.Length);

                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private async Task<bool> CheckUsernameAvailabilityAsync(string username)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;Connect Timeout=30;"))
            {
                await connection.OpenAsync();

                string checkQuery = "SELECT COUNT(*) FROM user_info WHERE Name = @Name";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Name", username);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    return count == 0;
                }
            }
        }

        private void ValidatePassword()
        {
            string password = textBox5.Text;
            string confirmPassword = textBox6.Text;

            bool doPasswordsMatch = (password == confirmPassword);
            bool isPasswordValid = IsPasswordValid(password);

            if (isPasswordValid)
            {
                passCon.Visible = false;
            }
            else
            {
                passCon.Visible = true;
                passCon.Text = "Must Contain upper_case, lower_case , digit , special";
                passCon.ForeColor = Color.Red; 
            }
            if (!doPasswordsMatch)
            {
                passwordStatusLabel.Visible = !doPasswordsMatch;
                passwordStatusLabel.Text = "Passwords do not match.";
                passwordStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                passwordStatusLabel.Visible = false;
            }

            EnableSignupButtonIfConditionsMet();
        }


        private bool IsPasswordValid(string password)
        {
            return password.Any(char.IsDigit) &&
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(c => "!@#$%^&*()-_+=<>? ".Contains(c));

        }

        private void EnableSignupButtonIfConditionsMet()
        {
            bool areOtherConditionsMet = true; 
            signup.Visible = areOtherConditionsMet;
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ValidateUsername();
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
               // lblConPass.Visible = true;
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


                string username = textBox3.Text;
                string password = textBox5.Text;
                string confirmPassword = textBox6.Text;
                string masterPassword = textBox4.Text;


                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                }


                bool isValidMasterPassword = true;

                int minimumMasterPasswordLength = 8;
                bool hasUppercaseMaster = masterPassword.Any(char.IsUpper);
                bool hasDigitMaster = masterPassword.Any(char.IsDigit);
                bool hasSpecialCharMaster = masterPassword.Any(c => !char.IsLetterOrDigit(c));

                if (masterPassword.Length < minimumMasterPasswordLength)
                {
                    MessageBox.Show("Master Password must be at least 8 characters long.");
                    isValidMasterPassword = false;
                }

                if (!hasUppercaseMaster)
                {
                    MessageBox.Show("Master Password must contain at least one uppercase letter.");
                    isValidMasterPassword = false;
                }

                if (!hasDigitMaster)
                {
                    MessageBox.Show("Master Password must contain at least one digit.");
                    isValidMasterPassword = false;
                }

                if (!hasSpecialCharMaster)
                {
                    MessageBox.Show("Master Password must contain at least one special character.");
                    isValidMasterPassword = false;
                }

                if (!isValidMasterPassword)
                {
                    return;
                }

                string passwordSalt = GenerateRandomSalt();


                string hashedPassword = HashPassword(password, passwordSalt);


                string masterPasswordSalt = GenerateRandomSalt();


                string hashedMasterPassword = HashPassword(masterPassword, masterPasswordSalt);

                    string insertQuery = "INSERT INTO user_info (Name, Password, Master_Password, PasswordSalt, MasterPasswordSalt , userID) VALUES (@Name, @Password, @MasterPassword, @PasswordSalt, @MasterPasswordSalt, @userID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", username);
                        insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                        insertCommand.Parameters.AddWithValue("@MasterPassword", hashedMasterPassword);
                        insertCommand.Parameters.AddWithValue("@PasswordSalt", passwordSalt);
                        insertCommand.Parameters.AddWithValue("@MasterPasswordSalt", masterPasswordSalt);
                        insertCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.UniqueIdentifier) { Value = userID });

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Registration failed.");
                        }
                    }
            }
        }

        private async void ValidateUsername()
        {
            string username = textBox3.Text;

            bool isUsernameAvailable = await CheckUsernameAvailabilityAsync(username);

            if (isUsernameAvailable)
            {
                availabilityLabel.Visible = true;
                availabilityLabel.Text = "Username available!";
                availabilityLabel.ForeColor = Color.Green;
                signup.Visible = true;
            }
            else
            {
                availabilityLabel.Visible = true;
                availabilityLabel.Text = "Username already taken.";
                availabilityLabel.ForeColor = Color.Red;
                signup.Visible = false;
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
            ValidatePassword();

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
            ValidatePassword();

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

        
        private void verifybtn_Click(object sender, EventArgs e)
        {
 
                string username = fgetUser.Text;
                string masterPassword = fgetMasterPw.Text;
            

                string selectQuery = "SELECT Password, Master_Password, PasswordSalt, MasterPasswordSalt FROM user_info WHERE Name = @Name";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Name", username);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            string storedMasterPassword = reader["Master_Password"].ToString();
                            string passwordSalt = reader["PasswordSalt"].ToString();
                            string masterPasswordSalt = reader["MasterPasswordSalt"].ToString();

                            string enteredMasterPasswordHash = HashPassword(masterPassword, masterPasswordSalt);

                            Continue.Visible = true;
                            Verify.Visible = false;
                            // Verify that the entered master password matches the stored master password
                            if (enteredMasterPasswordHash.Equals(storedMasterPassword))
                            {
                                
                                panel22.Visible = false;
                                panel20.Visible = false;
                                newPw.Visible = true;
                                conNewPw.Visible = true;
                                label16.Text = "Welcome back, Chief";
                                Continue.Visible = true;
      
                            }
                            else
                            {
                                MpNomatch.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                }
        }

        private void Verify_Click(object sender, EventArgs e)
        {
            string username = fgetUser.Text;
            string newPassword = fgetNewPw.Text;
            string confirmPassword = fgetConPw.Text;

            if (newPassword != confirmPassword)
            {
               
                pnlLogo.Dock = DockStyle.Left;
                pnlForget.Visible = true;
                fgetPwNm.Visible = true;
                MessageBox.Show("New passwords do not match.");
                return;

            }


            string newPasswordSalt = GenerateRandomSalt();

            string hashedPassword = HashPassword(newPassword, newPasswordSalt);

            string updateQuery = "UPDATE user_info SET Password = @Password, PasswordSalt = @PasswordSalt WHERE Name = @Name";
;
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    updateCommand.Parameters.AddWithValue("@PasswordSalt", newPasswordSalt);
                    updateCommand.Parameters.AddWithValue("@Name", username);

                    int rowsAffected = updateCommand.ExecuteNonQuery();


                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("reset success!");
                        pnlLogo.Dock = DockStyle.Left;
                        pnlLogin.Visible = true;
                        pnlForget.Visible = false;
                        pnlForget.Refresh();
                    }
                    else
                    {
                        fgetPwNm.Visible = true;
                    }
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

        private void lblPassword_Click(object sender, EventArgs e)
        {
            if(lblPassword == null)
            {
                lblPassword.Visible = true;
            }
            else { lblPassword.Visible = false; }
        }

        private void lblConPass_Click(object sender, EventArgs e)
        {
            if(lblConPass == null)
            {
                lblConPass.Visible = true;
            }
            else lblConPass.Visible = false;
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
