using System.Numerics;

namespace RSA_WPF
{
    public static class RSAHelper
    {
        public static BigInteger ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }
            BigInteger x1, y1;
            BigInteger gcd = ExtendedGcd(b, a % b, out x1, out y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return gcd;
        }

        public static BigInteger ComputePrivateKey(BigInteger e, BigInteger phi)
        {
            BigInteger d, y;
            BigInteger gcd = ExtendedGcd(e, phi, out d, out y);
            if (gcd != 1)
                throw new ArgumentException("e и φ(n) не взаимно просты");
            d = (d % phi + phi) % phi;
            return d;
        }

        public static BigInteger FastPowMod(BigInteger baseVal, BigInteger exp, BigInteger mod)
        {
            if (mod == 1) return 0;
            BigInteger result = 1;
            baseVal %= mod;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    result = (result * baseVal) % mod;
                baseVal = (baseVal * baseVal) % mod;
                exp >>= 1;
            }
            return result;
        }

        public static BigInteger EncryptByte(byte data, BigInteger e, BigInteger n)
        {
            return FastPowMod(new BigInteger(data), e, n);
        }

        public static byte DecryptBlock(BigInteger cipher, BigInteger d, BigInteger n)
        {
            BigInteger m = FastPowMod(cipher, d, n);
            return (byte)(m % 256);
        }

        public static List<BigInteger> EncryptBytes(byte[] data, BigInteger e, BigInteger n)
        {
            List<BigInteger> encrypted = new List<BigInteger>();
            foreach (byte b in data)
            {
                encrypted.Add(EncryptByte(b, e, n));
            }
            return encrypted;
        }

        public static byte[] DecryptBlocks(List<BigInteger> cipherBlocks, BigInteger d, BigInteger n)
        {
            byte[] decrypted = new byte[cipherBlocks.Count];
            for (int i = 0; i < cipherBlocks.Count; i++)
            {
                decrypted[i] = DecryptBlock(cipherBlocks[i], d, n);
            }
            return decrypted;
        }
    }
}