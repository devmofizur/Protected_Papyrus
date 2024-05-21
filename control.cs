using System.Drawing;
using System.Windows.Forms;


namespace ProtectedPapyrus
{
    static class control
    {

        static bool isMax = false;
        static Point old_loc, default_loc;
        static Size old_size, default_size;

        public static void SetIntial(Form form) //this method should fire when app starts
        {
            old_loc = form.Location;
            old_size = form.Size;
            default_loc = form.Location;
            default_size = form.Size;
        }

        public static void DoMaximize(Form form, Button Maxbtn)
        {
            if (isMax == false) // app not maximized ; then MAXIMIZED IT!
            {
                old_loc = new Point(form.Location.X, form.Location.Y);
                old_size = new Size(form.Size.Width, form.Size.Height);
                Maximize(form);
                isMax = true;
                Maxbtn.BackgroundImage = ProjectPapyrus.Properties.Resources.normal;
            }

            else // app is currently maximed; then make it normal!
            {
                if (old_size.Width >= SystemInformation.WorkingArea.Width || old_size.Height >= SystemInformation.WorkingArea.Height)
                {
                    form.Location = default_loc;
                    form.Size = default_size;
                }
                else
                {
                    form.Location = old_loc;
                    form.Size = old_size;
                }
                isMax = false;
                Maxbtn.BackgroundImage = ProjectPapyrus.Properties.Resources.maximize;
            }
        }
        static void Maximize(Form form)
        {
            int x = SystemInformation.WorkingArea.Width;
            int y = SystemInformation.WorkingArea.Height;
            form.WindowState = FormWindowState.Normal;
            form.Location = new Point(0, 0);
            form.Size = new Size(x, y);


        }
    }
}
