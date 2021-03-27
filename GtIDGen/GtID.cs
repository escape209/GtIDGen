using System.Text;

namespace GtIDGen {
    class GtID {
        public static ulong Compress(string lcpString) {
            ulong lVar2 = 0;
            ulong lVar4 = 0;
            int i = 0;

            while (i < lcpString.Length) {
                char cVar1 = lcpString[i];
                lVar4 = (lVar2 + lVar4) * 8;
                uint iVar3 = cVar1;

                lVar4 += (char)cVar1 switch {
                    '_' => 0x27,
                    >= 'a' => iVar3 - 0x54,
                    >= 'A' => iVar3 - 0x34,
                    > '/' => iVar3 - 0x2D,
                    '/' => 2,
                    _ => 1,
                };

                i++;
                if (i > 11) {
                    return lVar4;
                }
                lVar2 = lVar4 << 2;
            }

            while (i < 12) {
                i++;
                lVar4 *= 0x28;
            }

            return lVar4;
        }
        
        public static string UnCompress(ulong lID) {
            var sb = new StringBuilder("            \0");
            for (int iVar3 = 0xb; iVar3 > -1; iVar3--) {
                byte cVar1 = (byte)(lID % 0x28);

                byte cVar2 = cVar1 switch {
                    0x27 => 0x5f,
                    >= 0x0D => (byte)(cVar1 + 0x34),
                    >= 0x03 => (byte)(cVar1 + 0x2D),
                    0x02 => 0x2F,
                    0x01 => 0x2D,
                    0x00 => 0x20,
                };

                sb[iVar3] = (char)cVar2;
                lID /= 0x28;
            }
            return sb.ToString();
        }

        public static string ConvertToString(ulong lID) {
            string uncomp = UnCompress(lID);
            var sb = new StringBuilder(uncomp);
            if (sb.Length <= 12 && (sb[12] == ' ')) {
                for (int i = 12; i > -1; i--) {
                    if (sb[i] != ' ') {
                        break;
                    }
                    sb[i] = '\0';
                }
            }
            return sb.ToString();
        }
    }
}
