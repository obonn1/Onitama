namespace Onitama;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        oniBoard1.Game.GameClosed += (_, _) => Close();
    }
}
