using ProtectedPapyrus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MimeMapping;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;


namespace ProjectPapyrus
{
    public partial class Menu : Form
    {
        private SqlConnection connection;

        // private readonly NoteService noteService = new NoteService();

        public Menu()
        {
            InitializeComponent();

            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;MultipleActiveResultSets=True;User ID=reflexorigin;Password=waytoGO.1;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            BeautifyDataGridView(GridView);
            BeautifyHaveDataGridView(HaveKeyDataGrid);
        }

        private void BeautifyHaveDataGridView(DataGridView dataGridView)
        {
            Color indigo = Color.FromArgb(75, 0, 130);
            Color darkViolet = Color.DarkViolet;

            // Set the alternating row styles
            DataGridViewCellStyle altRowStyle = dataGridView.AlternatingRowsDefaultCellStyle;
            altRowStyle.BackColor = darkViolet; // DarkViolet for odd rows
            altRowStyle.ForeColor = Color.White;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    // Even rows: Indigo
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = indigo;
                    dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }

            // Set the header styles
            DataGridViewCellStyle headerStyle = dataGridView.ColumnHeadersDefaultCellStyle;
            headerStyle.BackColor = indigo; // Use indigo for the header background
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Nova Square", 12F, FontStyle.Bold); // Standard system font, larger size
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.Padding = new Padding(10, 5, 10, 5);

            // Set the selection background color
            DataGridViewCellStyle selectionStyle = dataGridView.DefaultCellStyle;
            selectionStyle.SelectionBackColor = Color.FromArgb(184, 134, 11); // Dark Goldenrod
            selectionStyle.SelectionForeColor = Color.White;

            // Disable user-resizing of columns
            dataGridView.AllowUserToResizeColumns = false;

            // Add a subtle border for better separation
            dataGridView.BorderStyle = BorderStyle.FixedSingle;

            // Enhance grid lines
            dataGridView.GridColor = Color.Silver;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add rounded corners to the header
            dataGridView.EnableHeadersVisualStyles = false;

            // Add a drop shadow effect
            dataGridView.Margin = new Padding(0, 0, 0, 5); // Margin for the entire DataGridView
            dataGridView.Paint += (sender, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, dataGridView.Bounds.X + 4, dataGridView.Bounds.Y + 4, dataGridView.Bounds.Width - 8, dataGridView.Bounds.Height - 8);
                }
            };
        }





        private void BeautifyDataGridView(DataGridView dataGridView)
        {
            // Set the background color
            dataGridView.BackgroundColor = Color.FromArgb(255, 248, 196); // Light Gold

            // Set the header styles
            DataGridViewCellStyle headerStyle = dataGridView.ColumnHeadersDefaultCellStyle;
            headerStyle.BackColor = Color.FromArgb(255, 215, 0); // Gold
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Nova Square", 12F, FontStyle.Bold); // Standard system font, larger size
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.Padding = new Padding(10, 5, 10, 5);

            // Set the row styles
            DataGridViewCellStyle rowStyle = dataGridView.RowsDefaultCellStyle;
            rowStyle.BackColor = Color.White;
            rowStyle.ForeColor = Color.DimGray; // Slightly darker text color

            // Set the alternating row styles
            DataGridViewCellStyle altRowStyle = dataGridView.AlternatingRowsDefaultCellStyle;
            altRowStyle.BackColor = Color.FromArgb(255, 248, 196); // Light Gold

            // Set the selection background color
            DataGridViewCellStyle selectionStyle = dataGridView.DefaultCellStyle;
            selectionStyle.SelectionBackColor = Color.FromArgb(184, 134, 11); // Dark Goldenrod
            selectionStyle.SelectionForeColor = Color.White;

            // Disable user-resizing of columns
            dataGridView.AllowUserToResizeColumns = false;

            // Add a subtle border for better separation
            dataGridView.BorderStyle = BorderStyle.FixedSingle;

            // Enhance grid lines
            dataGridView.GridColor = Color.Silver;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Add rounded corners to the header
            dataGridView.EnableHeadersVisualStyles = false;

            // Add a drop shadow effect
            dataGridView.Margin = new Padding(0, 0, 0, 5); // Margin for the entire DataGridView
            dataGridView.Paint += (sender, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, dataGridView.Bounds.X + 4, dataGridView.Bounds.Y + 4, dataGridView.Bounds.Width - 8, dataGridView.Bounds.Height - 8);
                }
            };
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
                Environment.Exit(1);
        }
        int clicks = 0;
        private void btnChoices_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
            if (clicks % 2 == 0)
            {
                clicks++;
                btnChoices.BackgroundImage = Properties.Resources.choice2;

            }
            else
            {
                clicks--;
                btnChoices.BackgroundImage = Properties.Resources.choice;

            }
        }
        bool sidebarExpand = false;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 50)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 250)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }
        int mov, movX, movY;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
            this.Opacity = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            control.DoMaximize(this, btn);
        }

        private Guid actualNoteID = Guid.Empty;

        private void New_Click(object sender, EventArgs e)
        {

            save.Text = "Save";
            btnCopy.Text = "Copy";
            panelNew.Visible = true;
            outKey.Visible = false;
            btnCopy.Visible = false;
            panelView.Visible = false;
            pnlOkView.Visible = false;
            panelCalendar.Visible = false;
            PanelcalenderVerifySuccess.Visible = false;
            if (viewBar.Height > 150)
            {
                viewBarTransition.Start();
            }

            actualNoteID = Guid.NewGuid(); // Generate a new Guid

            string insertQuery = "INSERT INTO creation (NoteID) VALUES (@NoteID)";

            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = actualNoteID });
                insertCommand.ExecuteNonQuery();
            }
            
        }

        private void FutureReset()
        {
            FutureTextBox.Clear();
            Calendar_viewTitle.Clear();
            Calendar_viewAttach.Items.Clear();
        }

        private void FutureSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Calendar_viewTitle.Text))
            {
                MessageBox.Show("Title cannot be empty.");
                return;
            }

            Guid senderUserID = UserManager.UserID;
            sendDatePick.Value = DateTime.Now;
            DateTime scheduleDate = sendDatePick.Value;

            byte[] newEncryptionKey = GenerateRandomBytes(32); // Assuming a 256-bit key
            byte[] newIV = GenerateRandomBytes(16); // Assuming a 128-bit IV

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;"))
            {
                connection.Open();
                SqlTransaction transaction = null;

                try
                {
                    transaction = connection.BeginTransaction();

                    // Encrypt the note content
                    string encryptedNoteContent = AesEncryptionHelper.EncryptString(
                        FutureTextBox.Text,
                        newEncryptionKey,
                        newIV
                    );

                    // Insert note details into ScheduledNotes table
                    using (SqlCommand insertCommand = new SqlCommand("INSERT INTO ScheduledNotes (NoteID, SenderUserID, RecipientUserID, NoteTitle, NoteContent, ScheduledDate, IsSent, EncryptionKey, IV) VALUES (@NoteID, @SenderUserID, @RecipientUserID, @NoteTitle, @NoteContent, @ScheduledDate, @IsSent, @EncryptionKey, @IV)", connection, transaction))
                    {
                        Guid noteId = Guid.NewGuid();
                        insertCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = noteId });
                        insertCommand.Parameters.Add(new SqlParameter("@SenderUserID", SqlDbType.UniqueIdentifier) { Value = senderUserID });
                        insertCommand.Parameters.Add(new SqlParameter("@RecipientUserID", SqlDbType.UniqueIdentifier) { Value = recipientUserID });
                        insertCommand.Parameters.Add(new SqlParameter("@NoteTitle", SqlDbType.NVarChar) { Value = Calendar_viewTitle.Text });
                        insertCommand.Parameters.Add(new SqlParameter("@NoteContent", SqlDbType.NVarChar) { Value = encryptedNoteContent });
                        insertCommand.Parameters.Add(new SqlParameter("@ScheduledDate", SqlDbType.DateTime) { Value = scheduleDate });
                        insertCommand.Parameters.Add(new SqlParameter("@IsSent", SqlDbType.Bit) { Value = false });
                        insertCommand.Parameters.Add(new SqlParameter("@EncryptionKey", SqlDbType.VarBinary) { Value = newEncryptionKey });
                        insertCommand.Parameters.Add(new SqlParameter("@IV", SqlDbType.VarBinary) { Value = newIV });

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string base64EncryptionKey = Convert.ToBase64String(newEncryptionKey);
                            FutureEncKey.Text = base64EncryptionKey;
                        }
                        FutureEncKey.Visible = true;
                        FutureCopy.Visible = true;
                        transaction.Commit();
                        FutureCancel.Visible = false;
                        FutureSave.Visible = false;
                        FutureAttach.Visible = false;
                        FutureReset();
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"An error occurred while saving the note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void VerifyReset()
        {
            NoteText.Clear();
            NoteTT.Clear();
            attachmentQueue.Clear();
            new_attach.Items.Clear();
        }

        private void verifybtn_Click(object sender, EventArgs e)
        {
            outKey.Text = "Encryption Key Here";
            outKey.Visible = true;
            btnCopy.Visible = true;

            if (actualNoteID == Guid.Empty)
            {
                MessageBox.Show("No Note to save.");
                return;
            }

            if (string.IsNullOrWhiteSpace(NoteTT.Text))
            {
                MessageBox.Show("Title cannot be empty.");
                return;
            }

            Guid senderUserID = UserManager.UserID;

            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        byte[] newEncryptionKey = GenerateRandomBytes(32); // Assuming a 256-bit key
                        byte[] newIV = GenerateRandomBytes(16); // Assuming a 128-bit IV

                        using (SqlCommand insertKeyCommand = new SqlCommand("INSERT INTO EncryptionKeys (NoteID, EncryptionKey, IV) VALUES (@NoteID, @EncryptionKey, @IV)", connection, transaction))
                        {
                            insertKeyCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = actualNoteID });
                            insertKeyCommand.Parameters.Add(new SqlParameter("@EncryptionKey", SqlDbType.VarBinary) { Value = newEncryptionKey });
                            insertKeyCommand.Parameters.Add(new SqlParameter("@IV", SqlDbType.VarBinary) { Value = newIV });

                            insertKeyCommand.ExecuteNonQuery();
                        }

                        string encryptedNoteContent = AesEncryptionHelper.EncryptString(
                            NoteText.Text,
                            newEncryptionKey,
                            newIV
                        );

                        using (SqlCommand insertCommand = new SqlCommand("INSERT INTO Notes (NoteID, NoteContent, CreatedDate, userID, NoteTitle) VALUES (@NoteID, @NoteContent, GETDATE(), @userID, @NoteTitle)", connection, transaction))
                        {
                            insertCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = actualNoteID });
                            insertCommand.Parameters.Add(new SqlParameter("@NoteContent", SqlDbType.NVarChar) { Value = encryptedNoteContent });
                            insertCommand.Parameters.Add(new SqlParameter("@userID", SqlDbType.UniqueIdentifier) { Value = senderUserID });
                            insertCommand.Parameters.Add(new SqlParameter("@NoteTitle", SqlDbType.NVarChar) { Value = NoteTT.Text });

                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                string base64EncryptionKey = Convert.ToBase64String(newEncryptionKey);
                                outKey.Text = base64EncryptionKey;
                            }
                            else
                            {
                                MessageBox.Show("Save operation failed.");
                            }
                        }
                        if (attachmentQueue.Any())
                        {
                            Upload(this, EventArgs.Empty);
                        }

                        transaction.Commit();
                        NoteText.Clear();
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("An error occurred while saving the note: " + ex.Message);
                    }
                }
            }
        }

        private byte[] GenerateRandomBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(outKey.Text);
            btnCopy.Text = "Copied";

            Home.PerformClick();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelNew.Visible = false;
            panelView.Visible = false;
            pnlOkView.Visible = false;
            panelCalendar.Visible = true;
            if (viewBar.Height > 150)
            {
                viewBarTransition.Start();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            viewBarTransition.Start();
            viewSave.Text = "Save";
           
            panelNew.Visible = false;
            panelView.Visible = false;
            pnlOkView.Visible = false;
            panelCalendar.Visible = false;
            PanelcalenderVerifySuccess.Visible = false;
        }
        bool viewBarExpand = false;
        private void viewBarTransition_Tick(object sender, EventArgs e)
        {
            if (viewBarExpand == false)
            {
                viewBar.Height += 10;
                if (viewBar.Height >= 151)
                {
                    viewBarTransition.Stop();
                    viewBarExpand = true;
                }

            }
            else
            {
                viewBar.Height -= 10;
                if (viewBar.Height <= 51)
                {
                    viewBarTransition.Stop();
                    viewBarExpand = false;
                }
            }
        }

        private void Home_Click(object sender, EventArgs e)
        {
            panelNew.Visible = false;
            panelView.Visible = false;
            pnlOkView.Visible = false;
            panelCalendar.Visible = false;
            PanelcalenderVerifySuccess.Visible = false;
            AtcName.Items.Clear();
            new_attach.Items.Clear();
            inputKey.Clear();
            if (viewBar.Height > 150)
            {
                viewBarTransition.Start();
            }
            RefreshAllControls(this);
        }

        private void RefreshAllControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = -1;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
              
                if (c.HasChildren)
                {
                    RefreshAllControls(c);
                }
            }
        }


        private bool isBtnByKeyClicked = false;

        private void btnByKey_Click(object sender, EventArgs e)
        {
            try
            {
                panelView.Visible = true;
                pnlViewAction.Visible = true;
                GridView.Visible = false;
                HaveKeyDataGrid.Visible = true;

                Guid recipientUserID = UserManager.UserID;

                using (SqlCommand command = new SqlCommand("SELECT * FROM RecipientScheduledNotes WHERE RecipientUserID = @UserID;", connection))
                {
                    command.Parameters.AddWithValue("@UserID", recipientUserID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (isBtnByKeyClicked)
                            {
                                GridView.Rows.Clear();
                            }

                            while (reader.Read())
                            {
                                Guid noteID = reader.GetGuid(0);
                                string guidString = noteID.ToString();
                                int rowIndex = GridView.Rows.Add();

                                GridView.Rows[rowIndex].Cells["ID"].Value = guidString;
                                GridView.Rows[rowIndex].Cells["NoteTitle"].Value = reader["NoteTitle"];
                                GridView.Rows[rowIndex].Cells["CreatedDate"].Value = reader["CreatedDate"];
                                GridView.Rows[rowIndex].Cells["FileName"].Value = reader["FileName"];
                            }

                            GridView.CellDoubleClick += GridView_CellDoubleClick;

                            isBtnByKeyClicked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnLogout_Click(object sender, EventArgs e)
        {
            //if database disconnect success then
            this.Hide();
            var login = new Login();
            login.Show();
        }



        private bool isChooseClicked = false;

        private void Choose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlOkView.Refresh();
                panelView.Visible = true;
                pnlViewAction.Visible = false;
                GridView.Visible = true;
                Continue.Visible = false;
                Con_Pro.Visible = true;
                HaveKeyDataGrid.Visible = false;

                Guid userID = UserManager.UserID;

                using (SqlCommand command = new SqlCommand("SELECT * FROM NotesAttach WHERE userID = @UserID;", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (isChooseClicked)
                            {
                                GridView.Rows.Clear();
                                inputKey.Clear();
                            }

                            while (reader.Read())
                            {
                                Guid noteID = reader.GetGuid(0);
                                string guidString = noteID.ToString();
                                int rowIndex = GridView.Rows.Add();

                                GridView.Rows[rowIndex].Cells["ID"].Value = guidString;
                                GridView.Rows[rowIndex].Cells["NoteTitle"].Value = reader["NoteTitle"];
                                GridView.Rows[rowIndex].Cells["CreatedDate"].Value = reader["CreatedDate"];
                                GridView.Rows[rowIndex].Cells["FileName"].Value = reader["FileName"];
                            }

                            GridView.CellDoubleClick += GridView_CellDoubleClick;

                            isChooseClicked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private string noteID;

        private void GridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int targetColumnIndex = GridView.Columns["ID"].Index;

            if (e.ColumnIndex == targetColumnIndex && e.RowIndex >= 0)
            {
                noteID = GridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                pnlViewAction.Visible = true;
            }
        }
        
        private void Con_Pro_Click(object sender, EventArgs e)
        {
            panelView.Visible = false;
            pnlOkView.Visible = true;

            string userProvidedKey = inputKey.Text;
 
            List<string> attachmentFileNames;
            // Convert noteID to Guid
            Guid noteIDGuid = Guid.Parse(noteID);

            bool hasAccess = CheckAccessByNoteID(noteIDGuid, userProvidedKey, out string decryptedNoteContent, out attachmentFileNames);

            if (hasAccess)
            {
                ViewRichBox.Text = decryptedNoteContent;
                
            }
            else
            {
                MessageBox.Show("Invalid key. Access denied.");
            }
        }

        private bool CheckAccessByNoteID(Guid noteID, string userProvidedKey, out string decryptedNoteContent, out List<string> attachmentFileNames)
        {
            attachmentFileNames = new List<string>();
            decryptedNoteContent = null;  // Initialize with a default value

            using (SqlCommand keyCommand = new SqlCommand("SELECT EncryptionKey, IV FROM EncryptionKeys WHERE NoteID = @NoteID", connection))
            {
                keyCommand.Parameters.AddWithValue("@NoteID", noteID);

                using (SqlDataReader keyReader = keyCommand.ExecuteReader())
                {
                    if (keyReader.Read())
                    {
                        byte[] storedKeyBytes = (byte[])keyReader["EncryptionKey"];
                        byte[] iv = (byte[])keyReader["IV"];

                        string storedKeyBase64 = Convert.ToBase64String(storedKeyBytes);
                        string providedKey = userProvidedKey.Trim();

                        if (storedKeyBase64 == providedKey)
                        {
                            decryptedNoteContent = DecryptNoteContent(storedKeyBytes, iv);

                            using (SqlCommand attachmentCommand = new SqlCommand("SELECT FileName FROM Attachments WHERE NoteID = @NoteID", connection))
                            {
                                attachmentCommand.Parameters.AddWithValue("@NoteID", noteID);

                                using (SqlDataReader attachmentReader = attachmentCommand.ExecuteReader())
                                {
                                    while (attachmentReader.Read())
                                    {
                                        string fileName = attachmentReader.GetString(0);
                                        AtcName.Text = fileName;
                                        AtcName.Items.Add(fileName);
                                        attachmentFileNames.Add(fileName);
                                    }
                                }
                            }

                            return true;
                        }
                    }
                }
            }

            return false;
        }




        private string DecryptNoteContent(byte[] encryptionKey, byte[] iv)
        {
            string encryptedNoteContent;

            using (SqlCommand noteContentCommand = new SqlCommand("SELECT NoteTitle,NoteContent FROM Notes WHERE NoteID = @NoteID", connection))
            {
                noteContentCommand.Parameters.AddWithValue("@NoteID", noteID); // Add this line to include noteID

                using (SqlDataReader noteContentReader = noteContentCommand.ExecuteReader())
                {
                    if (noteContentReader.Read())
                    {
                        encryptedNoteContent = noteContentReader.GetString(1);
                        ViewTitle.Text = noteContentReader.GetString(0);
                        return AesEncryptionHelper.DecryptString(encryptedNoteContent, encryptionKey, iv);
                    }
                }
            }

            return null;
        }


        private bool CheckAccess(byte[] providedKey, byte[] iv, out Guid matchingNoteID, out string noteContent, out List<string> attachmentFileNames)
        {
            matchingNoteID = Guid.Empty;
            noteContent = null;
            attachmentFileNames = new List<string>();

            using (SqlCommand keyCommand = new SqlCommand("SELECT NoteID, IV FROM EncryptionKeys WHERE EncryptionKey = @UserProvidedKey", connection))
            {
                keyCommand.Parameters.AddWithValue("@UserProvidedKey", providedKey);

                using (SqlDataReader keyReader = keyCommand.ExecuteReader())
                {
                    if (keyReader.Read())
                    {
                        matchingNoteID = keyReader.GetGuid(0);
                        //byte[] dbIV = Convert.ToBase64String(keyReader.GetString(1));
                        //iv = dbIV;

                    }
                }
            }

            if (matchingNoteID != Guid.Empty)
            {
                using (SqlCommand noteContentCommand = new SqlCommand("SELECT NoteContent FROM Notes WHERE NoteID = @NoteID", connection))
                {
                    noteContentCommand.Parameters.AddWithValue("@NoteID", matchingNoteID);

                    using (SqlDataReader noteContentReader = noteContentCommand.ExecuteReader())
                    {
                        if (noteContentReader.Read())
                        {
                            string encryptedNoteContent = noteContentReader.GetString(0);
                            string keyString = Convert.ToBase64String(providedKey);
                            string ivString = Convert.ToBase64String(iv);

                            noteContent = AesEncryptionHelper.DecryptString(encryptedNoteContent, providedKey, iv);
                        }
                    }
                }

                using (SqlCommand attachmentCommand = new SqlCommand("SELECT FileName FROM Attachments WHERE NoteID = @NoteID", connection))
                {
                    attachmentCommand.Parameters.AddWithValue("@NoteID", matchingNoteID);

                    using (SqlDataReader attachmentReader = attachmentCommand.ExecuteReader())
                    {
                        while (attachmentReader.Read())
                        {
                            string fileName = attachmentReader.GetString(0);
                            new_attach.Text = fileName;
                            new_attach.Items.Add(fileName);
                            attachmentFileNames.Add(fileName);
                        }
                    }
                }
            }

            return !string.IsNullOrEmpty(noteContent) && attachmentFileNames.Count > 0;
        }
        
        private void Continue_Click(object sender, EventArgs e)
        {
           
        }


        private void viewSave_Click(object sender, EventArgs e)
        {
            viewSave.Text = "Saved";
            Guid senderUserID = UserManager.UserID;

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;Connect Timeout=30;"))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand keyCommand = new SqlCommand("SELECT EncryptionKey, IV FROM EncryptionKeys WHERE NoteID = @NoteID", connection, transaction))
                        {
                            keyCommand.Parameters.AddWithValue("@NoteID", noteID);

                            using (SqlDataReader keyReader = keyCommand.ExecuteReader())
                            {
                                if (keyReader.Read())
                                {
                                    byte[] storedKeyBytes = (byte[])keyReader["EncryptionKey"];
                                    byte[] iv = (byte[])keyReader["IV"];

                                    string encryptedNoteContent = AesEncryptionHelper.EncryptString(
                                        ViewRichBox.Text,
                                        storedKeyBytes,
                                        iv
                                    );

                                    keyReader.Close();

                                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Notes SET NoteContent = @NoteContent WHERE NoteID = @NoteID", connection, transaction))
                                    {
                                        updateCommand.Parameters.Add(new SqlParameter("@NoteContent", SqlDbType.NVarChar) { Value = encryptedNoteContent });
                                        updateCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = Guid.Parse(noteID) });

                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (attachmentQueue.Any())
                            {
                                Upload(this, EventArgs.Empty);
                            }
                            transaction.Commit();
                            Home.PerformClick();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private List<Attachment> attachmentQueue = new List<Attachment>();

        public class Attachment
        {
            public Guid NoteID { get; set; }
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
            public string ContentType { get; set; } 

            public Attachment(Guid noteID, string fileName, byte[] fileContent , string contentType)
            {
                NoteID = noteID;
                FileName = fileName;
                ContentType = contentType; 
                FileContent = fileContent;
            }
        }



        private void UploadAttachment(Guid noteID, string fileName, byte[] fileContent , string contentType)
        {
            Attachment attachment = new Attachment(noteID, fileName, fileContent, contentType); 
            attachmentQueue.Add(attachment);
            new_attach.Items.Add(fileName);
        }

        private void UploadFutureAttachmet(Guid noteID, string fileName, byte[] fileContent, string contentType)
        {
            Attachment attachment = new Attachment(noteID, fileName, fileContent, contentType);
            attachmentQueue.Add(attachment);
            Calendar_viewAttach.Items.Add(fileName);
        }

        private void UploadViewAttach(Guid noteID, string fileName, byte[] fileContent, string contentType)
        {
            Attachment attachment = new Attachment(noteID, fileName, fileContent, contentType);
            attachmentQueue.Add(attachment);
            AtcName.Items.Add(fileName);
        }

        private void Upload(object sender, EventArgs e)
        {
            if (attachmentQueue.Count == 0)
            {
                MessageBox.Show("There are no attachments to upload.");
                return;
            }

            Guid userID = UserManager.UserID;
               
            try
            {
                foreach (Attachment attachment in attachmentQueue)
                {

                    Guid noteID = actualNoteID;

                    string insertQuery = "INSERT INTO Attachments (NoteID, FileName, FileContent, ContentType , userID) VALUES (@NoteID, @FileName, @FileContent, @ContentType , @userID)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = noteID });
                        command.Parameters.AddWithValue("@FileName", attachment.FileName);
                        command.Parameters.Add(new SqlParameter("@FileContent", SqlDbType.VarBinary) { Value = attachment.FileContent });
                        command.Parameters.AddWithValue("@ContentType", attachment.ContentType);
                        command.Parameters.AddWithValue("@userID", userID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Failed to save the attachment.");
                        }
                    }
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while uploading the attachments: " + ex.Message);
                }
                finally
                {
                    attachmentQueue.Clear();
                }
            }


        private void UploadFuture(object sender, EventArgs e)
        {
            if (attachmentQueue.Count == 0)
            {
                MessageBox.Show("There are no attachments to upload.");
                return;
            }

            //Guid userID = UserManager.UserID;

            try
            {
                foreach (Attachment attachment in attachmentQueue)
                {

                    Guid noteID = actualNoteID;

                    string insertQuery = "INSERT INTO ScheduledNotes (NoteID, FileName, FileContent, ContentType) VALUES (@NoteID, @FileName, @FileContent, @ContentType)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = noteID });
                        command.Parameters.AddWithValue("@FileName", attachment.FileName);
                        command.Parameters.Add(new SqlParameter("@FileContent", SqlDbType.VarBinary) { Value = attachment.FileContent });
                        command.Parameters.AddWithValue("@ContentType", attachment.ContentType);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Failed to save the attachment.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while uploading the attachments: " + ex.Message);
            }
            finally
            {
                attachmentQueue.Clear();
            }
        }


        private void ViewCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(outKey.Text);
            btnCopy.Text = "Copied";
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            if (viewBar.Height > 150)
            {
                viewBarTransition.Start();
            }
            if(!File.Exists("HomePage.html"))
            {
                File.WriteAllBytes("HomePage.html", Properties.Resources.HomePage);
            }
            if (!File.Exists("About.html"))
            {
                File.WriteAllBytes("About.html", Properties.Resources.about);
            }
            Process.Start("HomePage.html");
            //string mainFilePath = Path.Combine(Application.StartupPath, "C:\\Users\\Kibria\\Documents\\ProjectPapyrus1\\Resources\\HomePage.html");
           // string secondPageFilePath = Path.Combine(Application.StartupPath, "C:\\Users\\Kibria\\Documents\\ProjectPapyrus1\\Resources\\about.html");

            //OpenInDefaultBrowser(mainFilePath);
        }

       /* private void OpenInDefaultBrowser(string filePath)
        {
            Process.Start(filePath);
        }*/

        private void addAtch_Click(object sender, EventArgs e)
        {

        }

        private void NewAttach_Click(object sender, EventArgs e)
        {
            
        }

        private void Menu_Load_1(object sender, EventArgs e)
        {

        }

        private void NewAttach_Click_2(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Open Attachment";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.CheckFileExists = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(openFileDialog.FileName))
                        {
                            byte[] fileContent = File.ReadAllBytes(openFileDialog.FileName);

                            string fileName = openFileDialog.SafeFileName;

                            Guid noteID = actualNoteID; // Use actualNoteID directly as a Guid

                            string contentType = MimeUtility.GetMimeMapping(fileName);

                            UploadAttachment(noteID, openFileDialog.SafeFileName, fileContent, contentType);
                        }
                        else
                        {
                            MessageBox.Show("The attachment file does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtcBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFileName = AtcName.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedFileName))
            {
                var documentViewer = new DocumentViewer(selectedFileName);

                documentViewer.Show();
            }
        }

        private void inputKey_Click(object sender, EventArgs e)
        {
            inputKey.SelectAll();
        }

        private void RecipientBox_Click(object sender, EventArgs e)
        {
            RecipientBox.SelectAll();
        }

        private Guid? recipientUserID;

        private async void Future_Proceed_Click(object sender, EventArgs e)
        {
            try
            {

                panelCalendar.Visible = false;
                PanelcalenderVerifySuccess.Visible = true;
                string recipientUsername = RecipientBox.Text;

                recipientUserID = await GetUserIdByUsernameAsync(recipientUsername);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving user ID:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async void RecipientBox_TextChanged(object sender, EventArgs e)
        {
            string recipientUsername = RecipientBox.Text;

            try
            {
                bool isUsernameValid = await IsUsernameValidAsync(recipientUsername);

                if (isUsernameValid)
                {
                    futureLabel.Visible = true;
                    Future_Proceed.Visible = false;
                }
                else
                {
                    futureLabel.Visible = false;
                    futureLabel.Text = "No user found.";
                    Future_Proceed.Visible = true;
                }
            }
            catch (Exception ex)
            {
                futureLabel.Visible = false;
                Future_Proceed.Visible = false;
                MessageBox.Show($"An error occurred while checking the username:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> IsUsernameValidAsync(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;User ID=reflexorigin;Password=waytoGO.1;"))
                {
                    await connection.OpenAsync();

                    string checkUsernameQuery = "SELECT COUNT(*) FROM user_info WHERE Name = @Username"; 
                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@Username", username);

                        object result = await checkUsernameCommand.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);

                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking the username:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<Guid?> GetUserIdByUsernameAsync(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;User ID=reflexorigin;Password=waytoGO.1;"))
                {
                    await connection.OpenAsync();

                    string getUserIdQuery = "SELECT userID FROM user_info WHERE Name = @Username"; 
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@Username", username);

                        object result = await getUserIdCommand.ExecuteScalarAsync();

                        if (result != null && Guid.TryParse(result.ToString(), out Guid resultGuid))
                        {
                            return resultGuid;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving user ID:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }




        private void FutureTextBox_TextChanged(object sender, EventArgs e)
        {
           /* try
            {
                Guid senderUserID = UserManager.UserID;

                if (recipientUserID.HasValue)
                {
                    string noteContent = FutureTextBox.Text;
                    DateTime sendDate = sendDatePick.Value;

                    noteService.SaveNoteAndScheduleSending(senderUserID, recipientUserID.Value, noteContent, sendDate);

                    FutureTextBox.Clear();
                    sendDatePick.Value = DateTime.Now;

                    MessageBox.Show("Note saved and scheduled for sending.");
                }
                else
                {
                    MessageBox.Show("Recipient user ID not available. Please check the username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the note:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           */
        }

        private void FutureCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(FutureEncKey.Text);
            Home.PerformClick();
        }

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewEdit_Click(object sender, EventArgs e)
        {
            viewSave.Visible = true;
            ViewATC.Visible = true;
            ViewCancel.Visible = true;
            ViewEdit.Visible = false;
            ViewExit.Visible = false;
        }

        private void ViewExit_Click(object sender, EventArgs e)
        {
            Home.PerformClick();
            RefreshAllControls(this);
        }

        private void FutureAttach_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Open Attachment";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.CheckFileExists = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(openFileDialog.FileName))
                        {
                            byte[] fileContent = File.ReadAllBytes(openFileDialog.FileName);

                            string fileName = openFileDialog.SafeFileName;

                            Guid noteID = actualNoteID; // Use actualNoteID directly as a Guid

                            string contentType = MimeUtility.GetMimeMapping(fileName);

                            UploadFutureAttachmet(noteID, openFileDialog.SafeFileName, fileContent, contentType);
                        }
                        else
                        {
                            MessageBox.Show("The attachment file does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewATC_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Open Attachment";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.CheckFileExists = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(openFileDialog.FileName))
                        {
                            byte[] fileContent = File.ReadAllBytes(openFileDialog.FileName);

                            string fileName = openFileDialog.SafeFileName;

                            Guid noteID = actualNoteID;

                            string contentType = MimeUtility.GetMimeMapping(fileName);

                            UploadViewAttach(noteID, openFileDialog.SafeFileName, fileContent, contentType);
                        }
                        else
                        {
                            MessageBox.Show("The attachment file does not exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewClear()
        {
            
        }

        private void ViewCancel_Click_1(object sender, EventArgs e)
        {
            ViewEdit.Visible = true;
            ViewExit.Visible = true;
            ViewATC.Visible = false;
            viewSave.Visible = false;
            ViewCancel.Visible = false;
        }

        private void cancel_Click_1(object sender, EventArgs e)
        {
            if (actualNoteID == Guid.Empty)
            {
                MessageBox.Show("No Note to cancel.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;MultipleActiveResultSets=True;User ID=reflexorigin;Password=waytoGO.1;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Guid noteIDToDelete = actualNoteID;

                string deleteNoteQuery = "DELETE FROM creation WHERE NoteID = @NoteID";

                using (SqlCommand notesCommand = new SqlCommand(deleteNoteQuery, connection))
                {
                    notesCommand.Parameters.Add(new SqlParameter("@NoteID", SqlDbType.UniqueIdentifier) { Value = noteIDToDelete });
                    int rowsAffected = notesCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        actualNoteID = Guid.Empty;
                        Home.PerformClick();
                    }
                    else
                    {
                        Home.PerformClick();
                    }
                }
            }          
        }

        private void FutureCancel_Click(object sender, EventArgs e)
        {
            PanelcalenderVerifySuccess.ResetText();
            Home.PerformClick();
        }

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
    }

    public static class UserManager
    {
        private static Guid _userID = Guid.Empty;

        public static Guid UserID
        {
            get { return _userID; }
        }

        public static void SetUserID(Guid userID)
        {
            _userID = userID;
        }
    }
        /*
    public class Note
    {
        public int NoteID { get; set; }
        public Guid SenderUserID { get; set; }
        public Guid RecipientUserID { get; set; }
        public string NoteContent { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSent { get; set; }
    }

    public class NoteService
    {
        private readonly List<Note> scheduledNotes = new List<Note>();


        public void SaveNoteAndScheduleSending(Guid senderUserId, Guid receiverUserId, string message, DateTime sendDate)
        {
            Note note = new Note
            {
                SenderUserID = senderUserId,
                RecipientUserID = receiverUserId,
                NoteContent = message,
                SendDate = sendDate
            };

            // Save the note to the database
            SaveNoteToDatabase(note);

            // Schedule the note for sending
            ScheduleNoteSending(note);
        }

        private void SaveNoteToDatabase(Note note)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Notes (NoteId, SenderUserId, RecipientUserId, NoteContent, SendDate, IsSent) " +
                                  "VALUES (@NoteId, @SenderUserId, @RecipientUserId, @NoteContent, @SendDate, @IsSent)";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@NoteId", Guid.NewGuid()); // Generate a new GUID
                    insertCommand.Parameters.AddWithValue("@SenderUserId", note.SenderUserID);
                    insertCommand.Parameters.AddWithValue("@RecipientUserId", note.RecipientUserID);
                    insertCommand.Parameters.AddWithValue("@NoteContent", note.NoteContent);
                    insertCommand.Parameters.AddWithValue("@SendDate", note.SendDate);
                    insertCommand.Parameters.AddWithValue("@IsSent", note.IsSent);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        

        private void ScheduleNoteSending(Note note)
        {
            scheduledNotes.Add(note);
        }

        public void StartScheduler()
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("your_connection_string");

            BackgroundJobServer server = new BackgroundJobServer();

            RecurringJob.AddOrUpdate<NoteService>(
                "some_unique_job_id",
                x => x.SendScheduledNotes(null),
                "10 * * * *"); // every 10 minutes
        }



        public void SendScheduledNotes(PerformContext context)
        {
            foreach (var note in scheduledNotes)
            {
                if (!note.IsSent && DateTime.Now >= note.SendDate)
                {
                    SendNoteToRecipient(note);

                    note.IsSent = true;
                }
            }
        }

        private void SendNoteToRecipient(Note note)
        {
            MarkNoteAsSent(note);
        }

        private void MarkNoteAsSent(Note note)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IRO80SN,1433;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1;"))
            {
                connection.Open();

                string updateQuery = "UPDATE Notes SET IsSent = 1 WHERE NoteId = @NoteId";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@NoteId", note.NoteID);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }
        
    }
        */
    public static class AesEncryptionHelper
    {
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            if (key.Length * 8 != 128 && key.Length * 8 != 192 && key.Length * 8 != 256)
            {
                throw new ArgumentException("Invalid key size. Key must be 128, 192, or 256 bits.");
            }

            if (iv.Length * 8 != 128)
            {
                throw new ArgumentException("Invalid IV size. IV must be 128 bits.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            if (key.Length * 8 != 128 && key.Length * 8 != 192 && key.Length * 8 != 256)
            {
                throw new ArgumentException("Invalid key size. Key must be 128, 192, or 256 bits.");
            }

            if (iv.Length * 8 != 128)
            {
                throw new ArgumentException("Invalid IV size. IV must be 128 bits.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}