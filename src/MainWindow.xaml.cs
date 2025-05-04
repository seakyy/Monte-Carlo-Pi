using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace calculate_pi
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ISeries> PiSeries { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PiSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<double> { Values = new List<double>() }
            };
            DataContext = this;
        }

        private async void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            PiText.Text = "";
            ProgressBar.Value = 0;
            (PiSeries[0] as LineSeries<double>).Values = new List<double>();

            int totalPoints = (int)PointSlider.Value;
            int size = 600;
            WriteableBitmap bitmap = new WriteableBitmap(size, size, 96, 96, PixelFormats.Bgra32, null);
            byte[] pixels = new byte[size * size * 4];
            DrawImage.Source = bitmap;

            int insideCircle = 0;
            Random rand = new Random();
            var csv = new List<string> { "x,y,isInside" };
            var piValues = new List<double>();
            (PiSeries[0] as LineSeries<double>).Values = piValues;

            for (int i = 1; i <= totalPoints; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                int px = (int)(x * size);
                int py = (int)(y * size);

                bool isInside = x * x + y * y <= 1;
                if (isInside) insideCircle++;

                int index = (py * size + px) * 4;
                if (index >= 0 && index + 3 < pixels.Length)
                {
                    pixels[index + 0] = isInside ? (byte)0 : (byte)255;     // Blue
                    pixels[index + 1] = isInside ? (byte)255 : (byte)0;     // Green
                    pixels[index + 2] = 0;                                  // Red
                    pixels[index + 3] = 255;                                // Alpha
                }

                csv.Add($"{x},{y},{isInside}");

                if (i % 1000 == 0 || i == totalPoints)
                {
                    double piEstimate = 4.0 * insideCircle / i;
                    PiText.Text = $"Pi ≈ {piEstimate:F6} ({i} points)";
                    ProgressBar.Value = i * 100.0 / totalPoints;
                    piValues.Add(piEstimate);
                    await Task.Delay(1);
                }
            }

            bitmap.WritePixels(new Int32Rect(0, 0, size, size), pixels, size * 4, 0);

            var result = MessageBox.Show(
                "Do you want to save the results as a CSV file?",
                "CSV-Export",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "CSV Dateien (*.csv)|*.csv",
                    FileName = "pi_simulation_results.csv"
                };
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllLines(dialog.FileName, csv);
                    MessageBox.Show("CSV saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}