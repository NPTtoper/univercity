using System.Collections;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Lab2;

public partial class MainWindow : Window
{
    private readonly StreamCipher streamCipher = new();
    private string originalFileName;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void RegisterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        int count = RegisterTextBox.Text.Count(c => c == '0' || c == '1');
        LengthLabel.Text = $"Введено бит: {count}";
    }

    private void OpenFile_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                originalFileName = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);

                StringBuilder binaryString = new StringBuilder();

                foreach (byte b in fileBytes)
                {
                    binaryString.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }

                string binaryData = binaryString.ToString();
                streamCipher.PlainText = new BitArray(binaryData.Length);

                for (int i = 0; i < binaryData.Length; i++)
                {
                    streamCipher.PlainText[i] = binaryData[i] == '1';
                }

                PlainTextBox.Text = FormatBitArray(streamCipher.PlainText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка");
            }
        }
    }

    private void EncryptButton_Click(object sender, RoutedEventArgs e)
    {
        string filteredInput = new string(RegisterTextBox.Text.Where(c => c == '0' || c == '1').ToArray());

        if (filteredInput.Length != 27)
        {
            MessageBox.Show($"Регистр должен содержать ровно 27 бит (сейчас: {filteredInput.Length})",
                          "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (streamCipher.PlainText == null || streamCipher.PlainText.Length == 0)
        {
            MessageBox.Show("Сначала откройте файл!", "Ошибка",
                          MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        streamCipher.SetRegister(filteredInput);
        streamCipher.GenerateKey(streamCipher.PlainText.Length);

        KeyTextBox.Text = FormatBitArray(streamCipher.BitKey);

        streamCipher.Encrypt();
        CipherTextBox.Text = FormatBitArray(streamCipher.CipherBit);
    }

    private void SaveFile_Click(object sender, RoutedEventArgs e)
    {
        if (streamCipher.CipherBit == null || streamCipher.CipherBit.Length == 0)
        {
            MessageBox.Show("Нечего сохранять. Сначала выполните шифрование!", "Ошибка",
                           MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Все файлы (*.*)|*.*";
        saveFileDialog.DefaultExt = "encrypted"; 
        saveFileDialog.FileName = "encrypted_file.encrypted";

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                byte[] bytes = BitArrayToByteArray(streamCipher.CipherBit);

                string filePath = saveFileDialog.FileName;
                if (!Path.HasExtension(filePath))
                {
                    filePath += ".encrypted";
                }

                File.WriteAllBytes(filePath, bytes);

                MessageBox.Show($"Файл успешно сохранен!\nПуть: {filePath}",
                              "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private byte[] BitArrayToByteArray(BitArray bits)
    {
        int byteCount = (bits.Length + 7) / 8;
        byte[] bytes = new byte[byteCount];

        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i])
            {
                int byteIndex = i / 8;
                int bitIndex = 7 - (i % 8);
                bytes[byteIndex] |= (byte)(1 << bitIndex);
            }
        }

        return bytes;
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        RegisterTextBox.Clear();
        KeyTextBox.Clear();
        PlainTextBox.Clear();
        CipherTextBox.Clear();
    }

    private string FormatBitArray(BitArray array)
    {
        if (array.Length == 0)
            return string.Empty;

        StringBuilder result = new StringBuilder();

        if (array.Length <= 240)
        {
            for (int i = 0; i < array.Length; i++)
                result.Append(array[i] ? '1' : '0');
        }
        else
        {
            result.AppendLine("Первые 120 бит (15 байт):");
            for (int i = 0; i < 120; i++)
                result.Append(array[i] ? '1' : '0');

            result.AppendLine();
            result.AppendLine("Последние 120 бит (15 байт):");
            for (int i = 120; i > 0; i--)
                result.Append(array[array.Length - i] ? '1' : '0');
        }

        return result.ToString();
    }
}
/*private string FormatBytes(byte[] data)
{
    if (data == null || data.Length == 0)
        return string.Empty;

    StringBuilder result = new StringBuilder();

    if (data.Length <= 30)  
    {
        result.Append(BitConverter.ToString(data).Replace("-", " "));
    }
    else
    {
        byte[] first15 = data.Take(15).ToArray();
        result.AppendLine("Первые 15 байт (120 бит):");
        result.AppendLine(BitConverter.ToString(first15).Replace("-", " "));

        result.AppendLine();

        byte[] last15 = data.Skip(data.Length - 15).Take(15).ToArray();
        result.AppendLine("Последние 15 байт (120 бит):");
        result.AppendLine(BitConverter.ToString(last15).Replace("-", " "));
        
        result.AppendLine();
        result.AppendLine($"Всего байт: {data.Length} ({(data.Length * 8)} бит)");
    }

    return result.ToString();
}*/