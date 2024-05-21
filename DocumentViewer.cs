using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PdfiumViewer;

namespace ProjectPapyrus
{
    public partial class DocumentViewer : Form
    {
        string selectedFileName;

        private PdfViewer pdfViewer = new PdfViewer();

        private SqlConnection connection;

        public DocumentViewer(String fileName)
        {
            InitializeComponent();
            
            selectedFileName = fileName;

            string connectionString = "Data Source=DESKTOP-IRO80SN,5126;Initial Catalog=ProtectedPapyrus;Persist Security Info=True;MultipleActiveResultSets=True;User ID=reflexorigin;Password=waytoGO.1;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void HandleDocumentViewerLoad(object sender, EventArgs e)
        {
            byte[] binaryData = GetBinaryDataForAttachment(selectedFileName);
            string contentType = GetContentTypeForAttachment(selectedFileName);

            if (binaryData != null && binaryData.Length > 0)
            {
                try
                {
                    if (contentType == "image/jpeg" || contentType == "image/png")
                    {
                        ViewImage.Visible = true;
                        using (MemoryStream ms = new MemoryStream(binaryData))
                        {
                            Image image = Image.FromStream(ms);
                            ViewImage.Image = image;
                        }
                    }
                    else if (contentType == "application/pdf")
                    {
                        PdfViewer.Visible = true;
                        PdfViewer.Load += PdfViewer_Load;
                    }
                    else if (IsWebPage(contentType))
                    {
                        // Open web pages with default browser
                        OpenWebPageWithDefaultBrowser(binaryData);
                    }
                    else
                    {
                        // Check if it's an image in an unsupported format
                        if (IsImage(contentType))
                        {
                            // Open image with default application
                            OpenImageWithDefaultApp(binaryData, contentType);
                        }
                        else
                        {
                            // Open other file types in WebBrowser
                            ViewWeb.DocumentStream = new MemoryStream(binaryData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading document: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Nothing to Show");
            }
        }


        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/");
        }

        // Helper method to open an image with the default application
        private void OpenImageWithDefaultApp(byte[] imageBytes, string contentType)
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, imageBytes);

            Process.Start(tempFilePath);
        }

        private bool IsWebPage(string contentType)
        {
            return contentType.StartsWith("text/html") || contentType.StartsWith("application/xhtml+xml");
        }

        // Helper method to open a web page with the default browser
        private void OpenWebPageWithDefaultBrowser(byte[] webPageBytes)
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, webPageBytes);

            Process.Start(new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true
            });
        }

        private string GetContentTypeForAttachment(string fileName)
        {
            string contentType = null;

            using (SqlCommand selectContentTypeCommand = new SqlCommand("SELECT ContentType FROM Attachments WHERE FileName = @FileName", connection))
            {
                selectContentTypeCommand.Parameters.AddWithValue("@FileName", fileName);

                using (SqlDataReader contentTypeReader = selectContentTypeCommand.ExecuteReader())
                {
                    if (contentTypeReader.Read())
                    {
                        contentType = contentTypeReader["ContentType"].ToString();
                    }
                }
            }

            return contentType;
        }

        private byte[] GetBinaryDataForAttachment(string fileName)
        {
            byte[] binaryData = null;


             using (SqlCommand command = new SqlCommand("SELECT FileContent FROM Attachments WHERE FileName = @FileName", connection))
             {
                 command.Parameters.AddWithValue("@FileName", fileName);
                 SqlDataReader reader = command.ExecuteReader();
                 if (reader.Read())
                 {
                     binaryData = (byte[])reader["FileContent"];
                 }
             }

            return binaryData;
        }

        private void PdfViewer_Load(object sender, EventArgs e)
        {
            byte[] binaryData = GetBinaryDataForAttachment(selectedFileName);

            var ms = new MemoryStream(binaryData);
            PdfDocument pdfDocument = PdfDocument.Load(ms);
            PdfViewer.Document = pdfDocument;
        }
    }
}
