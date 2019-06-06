﻿using System.Collections.Generic;
using System.Windows;

namespace SAM
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ImportDelimited : Window
    {
        private string eKey;

        public ImportDelimited(string eKey)
        {
            this.eKey = eKey;
            InitializeComponent();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (DelimitedAccountsTextBox.Text.Length == 0)
            {
                DelimitedAccountsTextBox.Focus();
                MessageBox.Show("No accounts to import!");
                return;
            }

            if (DelimiterCharacterTextBox.Text.Length == 0)
            {
                DelimiterCharacterTextBox.Focus();
                MessageBox.Show("Delimiter character required!");
                return;
            }

            char delimiter = DelimiterCharacterTextBox.Text[0];
            string delimitedAccountsText = DelimitedAccountsTextBox.Text;
            string[] lines = delimitedAccountsText.Split('\n');

            List<Account> accounts = new List<Account>();

            foreach (string line in lines)
            {
                // TODO: collect account info for import.
                string[] info = line.Split(delimiter);

                if (info.Length < 2)
                {
                    MessageBox.Show("Invalid account format!");
                    return;
                }

                accounts.Add(new Account { Name = info[0], Password = StringCipher.Encrypt(info[1], eKey)});
            }

            Utils.ImportAccountsFromList(accounts);

            Close();
        }

        private void DelimiterCharacterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PreviewTextBlock != null && DelimiterCharacterTextBox.Text.Length > 0)
            {
                PreviewTextBlock.Text = "account" + DelimiterCharacterTextBox.Text + "password";
            }
        }
    }
}
