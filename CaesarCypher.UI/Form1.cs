using System;
using System.Linq;
using System.Windows.Forms;
using lab1;

namespace CaesarCypher.UI
{
    public partial class Form1 : Form
    {
        private readonly DES _des = new DES();

        public Form1()
        {
            InitializeComponent();
        }

        private void OnEncryptClicked(object sender, EventArgs e)
        {
            if (IsValidKey(key.Text))
            {
                output.Text = _des.Encrypt(input.Text, key.Text);
            }
        }

        private void OnDecryptClicked(object sender, EventArgs e)
        {
            if (IsValidKey(key.Text))
            {
                output.Text = _des.Decrypt(input.Text, key.Text);
            }
        }

        private bool IsValidKey(string key)
        {
            error.Text = "";
            if (key.Length != 16)
            {
                error.Text = "Invalid key";
                return false;
            }

            foreach (var c in key)
            {
                if (c > '0' && c < '9' || c > 'A' && c < 'F')
                {
                    return true;
                }
            }

            error.Text = "Invalid key";
            return false;
        }
    }
}