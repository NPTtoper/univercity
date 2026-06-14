using System.Collections;

namespace Lab2;

public class StreamCipher
{
    public BitArray BitRegister { get; private set; }
    public BitArray BitKey { get; private set; }
    public BitArray PlainText { get; set; }
    public BitArray CipherBit { get; private set; }

    public void SetRegister(string registerString)
    {
        BitRegister = new BitArray(registerString.Length);
        for (int i = 0; i < registerString.Length; i++)
            BitRegister[i] = registerString[i] == '1';
    }

    public void GenerateKey(int length)
    {
        BitKey = new BitArray(length);

        for (int i = 0; i < length; i++)
        {
            BitKey[i] = BitRegister[0];

            bool newBit = BitRegister[0] ^ BitRegister[25];

            for (int j = 0; j < BitRegister.Length - 1; j++)
                BitRegister[j] = BitRegister[j + 1];

            BitRegister[BitRegister.Length - 1] = newBit;
        }
    }

    public void Encrypt()
    {
        CipherBit = BitKey.Xor(PlainText);
    }
}