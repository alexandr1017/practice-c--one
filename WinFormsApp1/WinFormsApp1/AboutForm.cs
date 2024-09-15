namespace NoteApp
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "О программе";
            this.Size = new Size(400, 200);

            Label aboutLabel = new Label();
            aboutLabel.Text = "NoteApp v1.0\nAuthor: Your Name";
            aboutLabel.AutoSize = true;
            aboutLabel.Location = new Point(10, 10);

            this.Controls.Add(aboutLabel);
        }
    }
}
