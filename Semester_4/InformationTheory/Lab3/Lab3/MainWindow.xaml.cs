using System;
using System.IO;
using System.Numerics;
using System.Windows;

namespace RSA_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!BigInteger.TryParse(txtP_Enc.Text, out BigInteger p) || p <= 1)
                    throw new Exception("p должно быть > 1");
                if (!BigInteger.TryParse(txtQ_Enc.Text, out BigInteger q) || q <= 1)
                    throw new Exception("q должно быть > 1");
                if (!BigInteger.TryParse(txtKC_Enc.Text, out BigInteger dPrivate) || dPrivate <= 1)
                    throw new Exception("KC (d) должно быть > 1");

                if (string.IsNullOrWhiteSpace(txtInputFile.Text) || !File.Exists(txtInputFile.Text))
                    throw new Exception("Выберите входной файл");
                if (string.IsNullOrWhiteSpace(txtOutputEncFile.Text))
                    throw new Exception("Укажите выходной файл");

                if (!IsPrime(p) || !IsPrime(q))
                    throw new Exception("p и q должны быть простыми");

                BigInteger n = p * q;
                BigInteger phi = (p - 1) * (q - 1);

                BigInteger ePublic = ModInverse(dPrivate, phi);
                if (ePublic == 0)
                    throw new Exception("d и φ(n) не взаимно просты");

                byte[] plainData = File.ReadAllBytes(txtInputFile.Text);

                var encrypted = RSAHelper.EncryptBytes(plainData, ePublic, n);

                SaveEncryptedToFile(txtOutputEncFile.Text, encrypted);

                txtEncryptedNumbers.Text = string.Join(" ", encrypted);

                lblEncStatus.Content = $"Зашифровано: {txtOutputEncFile.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                lblEncStatus.Content = "Ошибка";
            }
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!BigInteger.TryParse(txtR_Dec.Text, out BigInteger n) || n <= 1)
                    throw new Exception("r (n) должно быть > 1");
                if (!BigInteger.TryParse(txtKC_Dec.Text, out BigInteger dPrivate) || dPrivate <= 1)
                    throw new Exception("KC (d) должно быть > 1");

                if (string.IsNullOrWhiteSpace(txtEncryptedFile.Text) || !File.Exists(txtEncryptedFile.Text))
                    throw new Exception("Выберите зашифрованный файл");
                if (string.IsNullOrWhiteSpace(txtOutputDecFile.Text))
                    throw new Exception("Укажите выходной файл");

                var cipherBlocks = LoadEncryptedFromFile(txtEncryptedFile.Text);

                byte[] decryptedData = RSAHelper.DecryptBlocks(cipherBlocks, dPrivate, n);

                File.WriteAllBytes(txtOutputDecFile.Text, decryptedData);

                lblDecStatus.Content = $"Расшифровано: {txtOutputDecFile.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                lblDecStatus.Content = "Ошибка";
            }
        }

        private void SaveEncryptedToFile(string path, List<BigInteger> numbers)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                foreach (var num in numbers)
                {
                    ushort val = (ushort)(num % 65536);
                    writer.Write(val);
                }
            }
        }

        private List<BigInteger> LoadEncryptedFromFile(string path)
        {
            List<BigInteger> result = new List<BigInteger>();
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    ushort val = reader.ReadUInt16();
                    result.Add(new BigInteger(val));
                }
            }
            return result;
        }

        private BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger x, y;
            BigInteger gcd = RSAHelper.ExtendedGcd(a, m, out x, out y);
            if (gcd != 1) return 0;
            return (x % m + m) % m;
        }

        private bool IsPrime(BigInteger num)
        {
            if (num < 2) return false;
            if (num == 2 || num == 3) return true;
            if (num % 2 == 0) return false;
            for (BigInteger i = 3; i * i <= num; i += 2)
                if (num % i == 0) return false;
            return true;
        }

        private void BtnBrowseInput_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Выберите исходный файл для шифрования";
            if (dialog.ShowDialog() == true)
                txtInputFile.Text = dialog.FileName;
        }

        private void BtnBrowseOutputEnc_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Сохранить зашифрованный файл";
            dialog.Filter = "Бинарные файлы (*.bin)|*.bin|Все файлы (*.*)|*.*";
            dialog.DefaultExt = ".bin";
            dialog.FileName = "encrypted.bin";
            if (dialog.ShowDialog() == true)
                txtOutputEncFile.Text = dialog.FileName;
        }

        private void BtnBrowseEncFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Выберите зашифрованный файл";
            dialog.Filter = "Бинарные файлы (*.bin)|*.bin|Все файлы (*.*)|*.*";
            if (dialog.ShowDialog() == true)
                txtEncryptedFile.Text = dialog.FileName;
        }

        private void BtnBrowseOutputDec_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Сохранить расшифрованный файл";
            dialog.Filter = "Все файлы (*.*)|*.*";
            dialog.FileName = "decrypted";
            if (dialog.ShowDialog() == true)
                txtOutputDecFile.Text = dialog.FileName;
        }
    }
}