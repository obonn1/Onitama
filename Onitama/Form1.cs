namespace Onitama;

public partial class Form1 : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Form1"/> class.
    /// </summary>
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void OniBoard1_Click(object sender, EventArgs e)
    {
    }

    private void OniBoard1_Paint(object sender, PaintEventArgs e)
    {
        if (oniBoard1.Game.CloseGame)
        {
            Close();
        }
    }
}
