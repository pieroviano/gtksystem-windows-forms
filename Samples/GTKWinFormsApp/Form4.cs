using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using GTKWinFormsApp.Properties;

namespace GTKWinFormsApp;

public partial class CommonDialogsForm : Form
{
    public CommonDialogsForm()
    {
        var treeNode1 = new TreeNode(Resources.CommonDialogsForm_CommonDialogsForm_Node_1);
        var treeNode2 = new TreeNode(Resources.CommonDialogsForm_CommonDialogsForm_Node_2, new TreeNode[] { treeNode1 });
        var treeNode3 = new TreeNode(Resources.CommonDialogsForm_CommonDialogsForm_Node_3);
        var treeNode4 = new TreeNode(Resources.CommonDialogsForm_CommonDialogsForm_Node_4, new TreeNode[] { treeNode2, treeNode3 });
        var treeNode5 = new TreeNode(Resources.CommonDialogsForm_CommonDialogsForm_Node_5);
        InitializeComponent();
        treeNode1.Name = Resources.CommonDialogsForm_CommonDialogsForm_Node_1;
        treeNode1.Text = Resources.CommonDialogsForm_CommonDialogsForm_Node_1;
        treeNode2.ImageIndex = 1;
        treeNode2.Name = Resources.CommonDialogsForm_CommonDialogsForm_Node_2;
        treeNode2.Text = Resources.CommonDialogsForm_CommonDialogsForm_Node_2;
        treeNode3.Name = Resources.CommonDialogsForm_CommonDialogsForm_Node_3;
        treeNode3.Text = Resources.CommonDialogsForm_CommonDialogsForm_Node_3;
        treeNode4.Name = Resources.CommonDialogsForm_CommonDialogsForm_Node_4;
        treeNode4.Text = Resources.CommonDialogsForm_CommonDialogsForm_Node_4;
        treeNode5.ImageIndex = 0;
        treeNode5.ImageKey = "img11.jpg";
        treeNode5.Name = Resources.CommonDialogsForm_CommonDialogsForm_Node_5;
        treeNode5.Text = Resources.CommonDialogsForm_CommonDialogsForm_Node_5;
        treeView1.Nodes.AddRange(new TreeNode[] { treeNode4, treeNode5 });
        button1.Text = Resources.CommonDialogsForm_CommonDialogsForm_0;
        button9.Text = Resources.CommonDialogsForm_CommonDialogsForm_1;
        button8.Text = Resources.TestDataForm_TestDataForm_Load_2B;
        button7.Text = Resources.CommonDialogsForm_CommonDialogsForm_3;
        button6.Text = Resources.CommonDialogsForm_CommonDialogsForm_4;
        button3.Text = Resources.CommonDialogsForm_CommonDialogsForm_5;
        label1.Text = Resources.CommonDialogsForm_CommonDialogsForm_6;

        Shown += Form4_Shown;

        button4.Click += Button4_Click;
    }

    private void Button4_Click(object? sender, EventArgs e)
    {
        splitContainer1.Panel1.Controls.Add(new Button() { Location = new Point(200, 100), Size = new Size(160, 30), Text = "testtest", Dock = DockStyle.Fill });
    }

    Point panel1Location = new();
    private void Form4_Shown(object? sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "jpg|*.jpg;png|*.png";
        ofd.Multiselect = true;
        ofd.Title = Resources.CommonDialogsForm_button3_Click_Test_Open_File;

        var dialogResult = ofd.ShowDialog(this);
        Console.WriteLine("dialogResult:" + dialogResult.ToString());
        Console.WriteLine("FileName:" + ofd.FileName);
        foreach (var file in ofd.FileNames)
        {
            Console.WriteLine("FileNames:" + file);
        }
        Console.WriteLine("SafeFileName:" + ofd.SafeFileName);
        foreach (var file in ofd.SafeFileNames)
        {
            Console.WriteLine("SafeFileNames:" + file);
        }
    }

    private void button6_Click(object sender, EventArgs e)
    {
        var ofd = new SaveFileDialog();
        ofd.Filter = "jpg|*.jpg;png|*.png";
        ofd.Title = Resources.CommonDialogsForm_button6_Click_Test_Save_File;

        var dialogResult = ofd.ShowDialog();
        Console.WriteLine("dialogResult:" + dialogResult.ToString());
        Console.WriteLine("FileName:" + ofd.FileName);
        foreach (var file in ofd.FileNames)
        {
            Console.WriteLine("FileNames:" + file);
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        var ofd = new FolderBrowserDialog();
        ofd.Description = Resources.CommonDialogsForm_button7_Click_Browse_Folder_Description;
        var dialogResult = ofd.ShowDialog();
        Console.WriteLine("dialogResult:" + dialogResult.ToString());
        Console.WriteLine("SelectedPath:" + ofd.SelectedPath);
    }

    private void button8_Click(object sender, EventArgs e)
    {
        var colorDialog = new ColorDialog();
        colorDialog.ShowDialog();

        //FontDialog fontDialog = new FontDialog();
        //fontDialog.ShowDialog();

        //Graphics g = CreateGraphics();
        //// g.DrawString("ddddddddd", new Font(FontFamily.GenericSansSerif, 16), new SolidBrush(ColorExtension.Red), 0, 0);
        //g.DrawRectangle(new Pen(new SolidBrush(ColorExtension.Red),2), new Rectangle(110, 110, 200, 200));

    }

    private void button9_Click(object sender, EventArgs e)
    {
        if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith("zh"))
        {
            MessageBox.Show("test message test message test messagetest message test message test message test messagetest message " +
                            "test message test message test messagetest message test message test message test messagetest message test " +
                            "message test message test messagetest message", Resources.CommonDialogsForm_button9_Click_Doubt, 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MessageBox.Show("test message test message \ntest messagetest message", Resources.CommonDialogsForm_button9_Click_Warn, 
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
        }
        else
        {
            // Display a question message box with Yes/No buttons
            MessageBox.Show("test message test message test message test message test message test message test message test message " +
                            "test message test message test message test message test message test message test message test message test message",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Display a warning message box with Retry/Cancel buttons
            MessageBox.Show("test message test message \ntest message test message",
                "Warning",
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Warning);
        }
    }

    private void vScrollBar1_ValueChanged(object sender, EventArgs e)
    {

    }

    private void hScrollBar1_ValueChanged(object sender, EventArgs e)
    {

    }
}