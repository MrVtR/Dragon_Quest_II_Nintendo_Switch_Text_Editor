using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonQuestEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode("Teste"));
            treeView1.Nodes[0].Nodes.Add(new TreeNode("Teste Node"));
            treeView1.EndUpdate();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodetext = e.Node.Text;
            textBox1.Text = textBox2.Text = nodetext;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(treeView1.SelectedNode == null)
                    throw new ArgumentNullException(paramName: nameof(treeView1), message: "You need to select an node before start editing...");
                treeView1.SelectedNode.Text = textBox2.Text;
            }
            catch(Exception ex)
            {
                textBox2.TextChanged -= textBox2_TextChanged;  // dettach the event handler
                textBox2.Clear(); // update value
                textBox2.TextChanged += textBox2_TextChanged; // reattach the event handler
                MessageBox.Show(ex.Message);
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FontFamily fontfamily = new FontFamily("PRGothIWA-HW-Dm-JReg");
                int fontSize = 16;
                Font font = new Font(fontfamily, fontSize, FontStyle.Regular);
                textBox1.Font = font;
                textBox2.Font = font;
                textBox2.Enabled = true;

                Console.WriteLine("DEBUG:");
                string path = openFileDialog.FileName;
               

                byte[] readText = File.ReadAllBytes(path);
                int i = 2;

                textBox1.Clear();
                textBox2.Clear();
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(openFileDialog.SafeFileName));
                int masterIndex = -1;
                while (i + 1 < readText.Length)
                {

                    string testing = readText[i + 6].ToString("X");
                    // 2 Arrays
                    
                    if (testing.Equals("3C") || testing.Equals("30") || testing.Equals("31"))
                    {
                        var masterArray = new ArrayList();
                        var subArray = new ArrayList();
                        var textsArray = new ArrayList();
                        string master1 = readText[i].ToString("X");
                        string master2 = readText[i + 1].ToString("X");
                        if (master1.Equals("0"))
                            master1 = "00";
                        if (master2.Equals("0"))
                            master2 = "00";
                        string masterArrayLength = master1 + master2;
                        i += 2;
                        //Console.WriteLine("Tamanho do array Mestre: " + Convert.ToInt32(masterArrayLength, 16) + " " + i + " " + testing);
                        for (int j = 0; j < Convert.ToInt32(masterArrayLength, 16); j++)
                        {
                            
                            string sub1 = readText[i].ToString("X");
                            string sub2 = readText[i + 1].ToString("X");
                            if (sub1.Equals("0"))
                                sub1 = "00";
                            if (sub2.Equals("0"))
                                sub2 = "00";
                            string subArrayLength = sub1 + sub2;
                            i += 2;
                            //Console.WriteLine("\tTamanho do sub array: " + Convert.ToInt32(subArrayLength, 16) + " " + i);
                            for (int k = 0; k < Convert.ToInt32(subArrayLength, 16); k++)
                            {
                                
                                string text1 = readText[i].ToString("X");
                                string text2 = readText[i + 1].ToString("X");
                                if (text1.Equals("0"))
                                    text1 = "00";
                                if (text2.Equals("0"))
                                    text2 = "00";
                                string textStringLength = text1 + text2;
                                //Console.WriteLine("\t\tTamanho dos textos: " + textStringLength + " " + i + "\n");
                                int cont = i + 2;
                                var byteText = new ArrayList();
                                for (int l = 0; l < Convert.ToInt32(textStringLength, 16); l++)
                                {
                                    byteText.Add(readText[cont++]);
                                }
                                byte[] text = new byte[byteText.Count];
                                int cont2 = 0;
                                foreach (byte b in byteText)
                                {
                                    text[cont2] = b;
                                    cont2++;
                                }
                                string currentText = System.Text.Encoding.UTF8.GetString(text);
                                textsArray.Add(currentText);
                                //Console.WriteLine(currentText);
                                i += Convert.ToInt32(textStringLength, 16) + 3;
                            }
                            subArray.Add(textsArray);
                            j += Convert.ToInt32(subArrayLength, 16) - 1;
                        }
                        masterArray.Add(subArray);
                        masterIndex++;

                        int subIndex = 0;
                        int textIndex = 0;
                        treeView1.Nodes[0].Nodes.Add(new TreeNode("Master Entry" +masterIndex));
                        foreach (ArrayList sub in masterArray)
                        {
                            treeView1.Nodes[0].Nodes[masterIndex].Nodes.Add(new TreeNode("Sub entry "+subIndex));
                            foreach (ArrayList text in sub)
                            {
                                treeView1.Nodes[0].Nodes[masterIndex].Nodes[subIndex].Nodes.Add(new TreeNode("Text Entry "+textIndex));
                                foreach (String textString in text)
                                {
                                    treeView1.Nodes[0].Nodes[masterIndex].Nodes[subIndex].Nodes[textIndex].Nodes.Add(new TreeNode(textString));
                                    Console.WriteLine(textString + "\n");
                                }
                                textIndex++;
                            }
                            subIndex++;
                        }

                    }
                    // 1 Array
                    else
                    {
                        var masterArray = new ArrayList();
                        var textsArray = new ArrayList();
                        string master1 = readText[i].ToString("X");
                        string master2 = readText[i + 1].ToString("X");
                        if (master1.Equals("0"))
                            master1 = "00";
                        if (master2.Equals("0"))
                            master2 = "00";
                        string masterArrayLength = master1 + master2;
                        i += 2;
                        //Console.WriteLine("Tamanho do array Mestre: " + Convert.ToInt32(masterArrayLength, 16) + " " + i + " " + testing);
                        for (int j = 0; j < Convert.ToInt32(masterArrayLength, 16); j++)
                        {
                            
                            string text1 = readText[i].ToString("X");
                            string text2 = readText[i + 1].ToString("X");
                            if (text1.Equals("0"))
                                text1 = "00";
                            if (text2.Equals("0"))
                                text2 = "00";
                            string textStringLength = text1 + text2;
                            //Console.WriteLine("\t\tTamanho dos textos: " + textStringLength + " " + i + "\n");
                            int cont = i + 2;
                            var byteText = new ArrayList();
                            for (int l = 0; l < Convert.ToInt32(textStringLength, 16); l++)
                            {
                                byteText.Add(readText[cont++]);
                            }
                            byte[] text = new byte[byteText.Count];
                            int cont2 = 0;
                            foreach (byte b in byteText)
                            {
                                text[cont2] = b;
                                cont2++;
                            }
                            string currentText = System.Text.Encoding.UTF8.GetString(text);
                            textsArray.Add(currentText);
                            //Console.WriteLine(currentText);
                            i += Convert.ToInt32(textStringLength, 16) + 3;
                        }
                        masterArray.Add(textsArray);
                        masterIndex++;

                        int textIndex = 0;
                        treeView1.Nodes[0].Nodes.Add(new TreeNode("Master Entry "+masterIndex));
                        foreach (ArrayList text in masterArray)
                        {
                            treeView1.Nodes[0].Nodes[masterIndex].Nodes.Add(new TreeNode("Text Entry "+textIndex));
                            foreach (String textString in text)
                            {
                                treeView1.Nodes[0].Nodes[masterIndex].Nodes[textIndex].Nodes.Add(new TreeNode(textString));
                                Console.WriteLine(textString + "\n");
                            }
                            textIndex++;
                        }
                    }
                }
                treeView1.Nodes[0].Expand();
                treeView1.EndUpdate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://google.com");
        }
    }
}
