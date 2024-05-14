namespace ANN
{
    public partial class Form1 : Form
    {
        static double[][] inputs = {
            // A harfi
            new double[] {0,0,1,0,0,
                          0,1,0,1,0,
                          1,0,0,0,1,
                          1,0,0,0,1,
                          1,1,1,1,1,
                          1,0,0,0,1,
                          1,0,0,0,1},

            // B harfi
            new double[] {1,1,1,1,0,
                          1,0,0,0,1,
                          1,0,0,0,1,
                          1,1,1,1,0,
                          1,0,0,0,1,
                          1,0,0,0,1,
                          1,1,1,1,0},

            // C harfi
            new double[] {0,0,1,1,1,
                          0,1,0,0,0,
                          1,0,0,0,0,
                          1,0,0,0,0,
                          1,0,0,0,0,
                          0,1,0,0,0,
                          0,0,1,1,1},

            // D harfi
            new double[] {1,1,1,0,0,
                          1,0,0,1,0,
                          1,0,0,0,1,
                          1,0,0,0,1,
                          1,0,0,0,1,
                          1,0,0,1,0,
                          1,1,1,0,0},

            // E harfi
            new double[] {1,1,1,1,1,
                          1,0,0,0,0,
                          1,0,0,0,0,
                          1,1,1,1,1,
                          1,0,0,0,0,
                          1,0,0,0,0,
                          1,1,1,1,1}
        };

        // A, B, C, D, E harflerinin sýrasýyla 1, 2, 3, 4, 5 olarak etiketlenmiþ çýkýþ deðerleri
        static double[][] targets = {
            new double[] {1, 0, 0, 0, 0}, // A
            new double[] {0, 1, 0, 0, 0}, // B
            new double[] {0, 0, 1, 0, 0}, // C
            new double[] {0, 0, 0, 1, 0}, // D
            new double[] {0, 0, 0, 0, 1}  // E
        };

        public Form1()
        {
            InitializeComponent();
        }

        int[] butonGirisDegerleri = new int[35]
        {
          0,0,0,0,0,
          0,0,0,0,0,
          0,0,0,0,0,
          0,0,0,0,0,
          0,0,0,0,0,
          0,0,0,0,0,
          0,0,0,0,0
        };

        public NeuralNetwork nn = new NeuralNetwork(inputs[0].Length, 10, targets[0].Length);


        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int deger = int.Parse(btn.Name.Substring(6));
            if (btn.BackColor == Color.White)
            {
                butonGirisDegerleri[deger - 1] = 1;
                btn.BackColor = Color.Black; // Butonun arka plan rengini siyaha dönüþtür
            }
            else
            {
                butonGirisDegerleri[deger - 1] = 0;
                btn.BackColor = Color.White; // Butonun arka plan rengini beyaza dönüþtür
            }
        }

        private void buttonEgitim_Click(object sender, EventArgs e)
        {
            nn.Train(inputs, targets, 1000);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            /*
             double[] inputToPredict = {0,0,1,0,0,
                                        0,1,0,1,0,
                                        1,0,0,0,1,
                                        1,0,0,0,1,
                                        1,1,1,1,1,
                                        1,0,0,0,1,
                                        1,0,0,0,1};*/
            double[] inputToPredict = new double[35];
            for (int i = 0; i < 35; i++)
            {
                inputToPredict[i] = butonGirisDegerleri[i];
            }


            double[] prediction = nn.Predict(inputToPredict);

            lblASonuc.Text = prediction[0].ToString();
            lblBSonuc.Text = prediction[1].ToString();
            lblCSonuc.Text = prediction[2].ToString();
            lblDSonuc.Text = prediction[3].ToString();
            lbl6ESonuc.Text = prediction[4].ToString(); 
        }
    }
}
