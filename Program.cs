using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPapyrus
{
    internal static class Program
    {
        private static bool previousInternetConnection = true;
        private static bool messageSent = false;

        [STAThread]
        static void Main()
        {
            if (!IsInternetAvailable())
            {
                MessageBox.Show("Internet connection is required to use this application. Please check your internet connection and try again.");
                return;
            }
            System.Threading.Timer timer = new System.Threading.Timer(ExecuteScheduledNotesUpdate, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Task.Run(() => MainLoopAsync());

            Application.Run(new Login());
        }

        static void ExecuteScheduledNotesUpdate(object state)
        {
            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;User ID=reflexorigin;Password=waytoGO.1";
            Guid recipientUserID = UserManager.UserID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateIsSentBasedOnScheduledDate", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@recipientUserID", recipientUserID);

                    command.ExecuteNonQuery();
                }
            }
        }


        static bool IsInternetAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        static async Task MainLoopAsync()
        {
            while (true)
            {
                bool currentInternetConnection = await IsInternetAvailableAsync();

                if (!currentInternetConnection && previousInternetConnection)
                {
                    if (!messageSent)
                    {
                        DisplayInternetStatusMessage(false);
                        messageSent = true;
                    }
                }
                else if (currentInternetConnection && !previousInternetConnection)
                {
                    // Internet reconnected
                    DisplayInternetStatusMessage(true);
                    messageSent = false;
                }

                previousInternetConnection = currentInternetConnection;

                await Task.Delay(1000);
            }
        }

        static async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync("https://www.google.com", HttpCompletionOption.ResponseHeadersRead))
                {
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        static void DisplayInternetStatusMessage(bool isConnected)
        {
            string statusMessage = isConnected
                ? "Internet connection is available."
                : "No internet connection. Please check your network.";

            MessageBox.Show(statusMessage, "Internet Status", MessageBoxButtons.OK, isConnected ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }
    }
}
