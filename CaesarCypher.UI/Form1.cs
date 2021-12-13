using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using lab1;

namespace CaesarCypher.UI
{
    public partial class Form1 : Form
    {
        private readonly DES _des = new DES();

        private readonly List<string> _weakKeys = new List<string>()
        {
            "0101010101010101",
            "FEFEFEFEFEFEFEFE",
            "E0E0E0E0F1F1F1F1",
            "1F1F1F1F0E0E0E0E",
            "0000000000000000",
            "FFFFFFFFFFFFFFFF",
            "E1E1E1E1F0F0F0F0",
            "1E1E1E1E0F0F0F0F",
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void OnEncryptClicked(object sender, EventArgs e)
        {
            try
            {
                if (IsValidKey(key.Text))
                {
                    output.Text = _des.Encrypt(input.Text, key.Text);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void OnDecryptClicked(object sender, EventArgs e)
        {
            try
            {
                if (IsValidKey(key.Text))
                {
                    output.Text = _des.Decrypt(input.Text, key.Text);
                }
            }
            catch (Exception exception)
            {
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

            if (key.Any(c => (c < '0' || c > '9') && (c < 'A' || c > 'F')))
            {
                error.Text = "Invalid key";
                return false;
            }

            if (_weakKeys.Contains(key))
            {
                error.Text = "Weak key";
                return false;
            }

            return true;
        }
    }
}