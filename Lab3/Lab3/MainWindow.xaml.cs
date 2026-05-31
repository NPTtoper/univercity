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
                    throw new Exception("p должно быть целым числом > 1");
                if (!BigInteger.TryParse(txtQ_Enc.Text, out BigInteger q) || q <= 1)
                    throw new Exception("q должно быть целым числом > 1");
                if (!BigInteger.TryParse(txtKC_Enc.Text, out BigInteger ePublic) || ePublic <= 1)
                    throw new Exception("KC (e) должно быть > 1");

                if (string.IsNullOrWhiteSpace(txtInputFile.Text) || !File.Exists(txtInputFile.Text))
                    throw new Exception("Выберите существующий входной файл");
                if (string.IsNullOrWhiteSpace(txtOutputEncFile.Text))
                    throw new Exception("Укажите путь для зашифрованного файла");

                if (!IsProbablyPrime(p) || !IsProbablyPrime(q))
                    throw new Exception("p и q должны быть простыми числами");

                BigInteger n = p * q;
                BigInteger phi = (p - 1) * (q - 1);

                if (BigInteger.GreatestCommonDivisor(ePublic, phi) != 1)
                    throw new Exception("KC (e) не взаимно просто с φ(n)");

                BigInteger d = RSAHelper.ComputePrivateKey(ePublic, phi);

                byte[] plainData = FileEncryptionService.ReadFileBytes(txtInputFile.Text);

                var encryptedBlocks = RSAHelper.EncryptBytes(plainData, ePublic, n);

                string outputPath = txtOutputEncFile.Text;
                if (!outputPath.EndsWith(FileEncryptionService.EncryptedExtension))
                    outputPath += FileEncryptionService.EncryptedExtension;

                FileEncryptionService.SaveEncryptedToFile(outputPath, encryptedBlocks);

                string numbers = FileEncryptionService.EncryptedNumbersToString(encryptedBlocks);
                txtEncryptedNumbers.Text = numbers.Length > 500 ? numbers.Substring(0, 500) + "..." : numbers;

                lblEncStatus.Content = $"Успешно зашифровано! Файл: {outputPath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка шифрования", MessageBoxButton.OK, MessageBoxImage.Error);
                lblEncStatus.Content = "Ошибка шифрования";
            }
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!BigInteger.TryParse(txtR_Dec.Text, out BigInteger n) || n <= 1)
                    throw new Exception("r (модуль n) должно быть > 1");
                if (!BigInteger.TryParse(txtKC_Dec.Text, out BigInteger d) || d <= 1)
                    throw new Exception("KC (закрытый ключ d) должно быть > 1");

                if (string.IsNullOrWhiteSpace(txtEncryptedFile.Text) || !File.Exists(txtEncryptedFile.Text))
                    throw new Exception("Выберите зашифрованный файл");
                if (string.IsNullOrWhiteSpace(txtOutputDecFile.Text))
                    throw new Exception("Укажите путь для расшифрованного файла");

                var cipherBlocks = FileEncryptionService.LoadEncryptedFromFile(txtEncryptedFile.Text);

                byte[] decryptedData = RSAHelper.DecryptBlocks(cipherBlocks, d, n);

                FileEncryptionService.WriteFileBytes(txtOutputDecFile.Text, decryptedData);

                lblDecStatus.Content = $"Успешно расшифровано! Файл: {txtOutputDecFile.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка дешифрования", MessageBoxButton.OK, MessageBoxImage.Error);
                lblDecStatus.Content = "Ошибка дешифрования";
            }
        }

        private bool IsProbablyPrime(BigInteger num)
        {
            if (num < 2) return false;
            if (num == 2 || num == 3) return true;
            if (num % 2 == 0) return false;

            for (BigInteger i = 3; i * i <= num; i += 2)
            {
                if (num % i == 0)
                    return false;
                if (i > 1000000) break; 
            }
            return true;
        }

        private void BtnBrowseInput_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
                txtInputFile.Text = dialog.FileName;
        }

        private void BtnBrowseOutputEnc_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "RSA Encrypted|*.rsa";
            if (dialog.ShowDialog() == true)
                txtOutputEncFile.Text = dialog.FileName;
        }

        private void BtnBrowseEncFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "RSA Encrypted|*.rsa|All files|*.*";
            if (dialog.ShowDialog() == true)
                txtEncryptedFile.Text = dialog.FileName;
        }

        private void BtnBrowseOutputDec_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            if (dialog.ShowDialog() == true)
                txtOutputDecFile.Text = dialog.FileName;
        }
    }
}