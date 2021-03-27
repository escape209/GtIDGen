using System;
using System.Windows.Forms;

namespace GtIDGen {
    public partial class MainForm : Form {
        int convBase = 10;
        string convFormat = string.Empty;

        bool UsingHex {
            get { return convBase == 16; } 
            set {
                if (value) {
                    convBase = 16;
                    convFormat = "X";
                } else {
                    convBase = 10;
                    convFormat = "D";
                }
                
                gtIdTextBox.MaxLength = ulong.MaxValue.ToString(convFormat).Length;
            }
        }

        public MainForm() {
            InitializeComponent();
            UsingHex = hexCheckBox.Checked;
        }

        private void textTextBox_TextChanged(object sender, EventArgs e) {
            if (!textTextBox.Focused) {
                return;
            }
            var comp = GtID.Compress(textTextBox.Text);
            gtIdTextBox.Text = comp == 0 ? string.Empty : comp.ToString();
        }


        private void gtIdTextBox_TextChanged(object sender, EventArgs e) {
            if (!gtIdTextBox.Focused) {
                return;
            }

            textTextBox.Clear();

            try {
                ulong ulConv = Convert.ToUInt64(gtIdTextBox.Text, convBase);
                textTextBox.Text = GtID.ConvertToString(ulConv);
            } catch (Exception) { }
        }

        private void hexCheckBox_CheckedChanged(object sender, EventArgs e) {
            UsingHex = hexCheckBox.Checked;
            gtIdTextBox.Clear();
            textTextBox.Clear();
        }
    }
}
