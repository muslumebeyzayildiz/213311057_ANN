public class NeuralNetwork
{
    private double[] inputLayer; //Giriş katmanı
    private double[] hiddenLayer;
    private double[] outputLayer; 

    private double[,] weightsInputToHidden; // Giriş katmanından gizli katmana olan ağırlıklar
    private double[,] weightsHiddenToOutput; // Gizli katmandan çıkış katmanına olan ağırlıklar

    private double learningRate = 0.1; // Öğrenme oranı

    private int inputNodes; // düğüm sayısı
    private int hiddenNodes;
    private int outputNodes; 

    private Random random;

    // Kurucu metod
    public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes)
    {
        this.inputNodes = inputNodes;
        this.hiddenNodes = hiddenNodes;
        this.outputNodes = outputNodes;

        inputLayer = new double[inputNodes];    
        hiddenLayer = new double[hiddenNodes];
        outputLayer = new double[outputNodes];

        weightsInputToHidden = new double[inputNodes, hiddenNodes];
        weightsHiddenToOutput = new double[hiddenNodes, outputNodes];

        random = new Random();

        InitializeWeights();
    }

    // Ağırlıkları başlatma metodu
    private void InitializeWeights()
    {
        // Giriş katmanından gizli katmana olan ağırlıkları başlatma
        for (int i = 0; i < inputNodes; i++)
        {
            for (int j = 0; j < hiddenNodes; j++)
            {
                weightsInputToHidden[i, j] = random.NextDouble() * 2 - 1; // -1 ile 1 arasında rastgele sayı üretme
            }
        }

        // Gizli katmandan çıkış katmanına olan ağırlıkları başlatma
        for (int i = 0; i < hiddenNodes; i++)
        {
            for (int j = 0; j < outputNodes; j++)
            {
                weightsHiddenToOutput[i, j] = random.NextDouble() * 2 - 1; // -1 ile 1 arasında rastgele sayı üretme
            }
        }
    }

    // Eğitim metod
    public void Train(double[][] inputs, double[][] targets, int epochs)
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] input = inputs[i];
                double[] target = targets[i];

                ForwardPropagation(input); // Feedforward (İleri besleme)

                BackPropagation(target);// Backpropagation (Geriye yayılım)

            }
        }
    }

    // İleri besleme metod
    private void ForwardPropagation(double[] input)
    {
        inputLayer = input;

        for (int i = 0; i < hiddenNodes; i++)
        {
            double sum = 0;
            // girdi katman hesaplama
            for (int j = 0; j < inputNodes; j++)
            {
                sum += inputLayer[j] * weightsInputToHidden[j, i];
            }
            hiddenLayer[i] = Sigmoid(sum);
        }

        // Çıkış katman hesaplama
        for (int i = 0; i < outputNodes; i++)
        {
            double sum = 0;
            for (int j = 0; j < hiddenNodes; j++)
            {
                sum += hiddenLayer[j] * weightsHiddenToOutput[j, i];
            }
            outputLayer[i] = Sigmoid(sum);
        }
        
    }

    // Sigmoid aktivasyon fonksiyonu
    private double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    // Geriye yayılım metod
    private void BackPropagation(double[] target)
    {
        // Hata hesaplama
        double[] outputErrors = new double[outputNodes];
        for (int i = 0; i < outputNodes; i++)
        {
            outputErrors[i] = target[i] - outputLayer[i];
        }

        // Çıkış katmanındaki hata ile gizli katmandaki ağırlıklar arasındaki hata hesaplama
        //yeriye yayılım yapılan yer
        double[] hiddenErrors = new double[hiddenNodes];
        for (int i = 0; i < hiddenNodes; i++)
        {
            double error = 0;
            for (int j = 0; j < outputNodes; j++)
            {
                error += outputErrors[j] * weightsHiddenToOutput[i, j];
            }
            hiddenErrors[i] = error;
        }

        // Ağırlıkları güncelleme
        for (int i = 0; i < inputNodes; i++)
        {
            for (int j = 0; j < hiddenNodes; j++)
            {
                double delta = learningRate * hiddenErrors[j] * hiddenLayer[j] * (1 - hiddenLayer[j]) * inputLayer[i];
                weightsInputToHidden[i, j] += delta;
            }
        }

        for (int i = 0; i < hiddenNodes; i++)
        {
            for (int j = 0; j < outputNodes; j++)
            {
                double delta = learningRate * outputErrors[j] * outputLayer[j] * (1 - outputLayer[j]) * hiddenLayer[i];
                weightsHiddenToOutput[i, j] += delta;
            }
        }
    }
    
    public double[] Predict(double[] input)
    {
        ForwardPropagation(input);
        return outputLayer;
    }
}