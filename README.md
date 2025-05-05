# Monte Carlo Pi Estimation (WPF)

# Showcase
![grafik](https://github.com/user-attachments/assets/30f0e50d-49ce-4ceb-b329-79ef3f3b6fb2)
![grafik](https://github.com/user-attachments/assets/a3853e75-7dea-4140-970b-cd1923b0e67e)


## Features

- Monte Carlo simulation to approximate Pi
- Live chart showing the convergence of Pi over time
- Real-time point visualization using pixel-level rendering
- Support for up to 10,000,000 points
- CSV export of simulation data (optional)
- Responsive UI with progress tracking
- Save dialog for controlled export

## How It Works

The Monte Carlo method places random points inside a unit square and counts how many fall within the quarter circle. The ratio approximates Pi using the formula:
```
π ≈ 4 × (points inside circle) / (total points)
```


## Technologies Used

- .NET 8 (WPF)
- LiveCharts2 (for data visualization)
- SkiaSharp (via LiveChartsCore)
- WriteableBitmap (for pixel-based rendering)

## Requirements

- Visual Studio 2022 or newer
- .NET 8 SDK
- NuGet packages:
  - LiveChartsCore.SkiaSharpView.WPF (Pre-Release)
  - SkiaSharp
  - SkiaSharp.Views.WPF


 ## Quick Start

1. Download the latest release from the [Releases](https://github.com/seakyy/Monte-Carlo-Pi/releases) section.
2. Extract the `.zip` file.
3. Run `calculate_pi.exe`.
4. Choose the number of points with the slider.
5. Click **"Start Simulation"** to begin.
6. After the simulation, choose whether to save the results as a CSV file.

> No installation required. The application runs as a standalone `.exe` on Windows.

## Getting Started (for developers)
Clone the repository:
   ```
   git clone https://github.com/seakyy/Monte-Carlo-Pi.git
   ```


```
cd calculate_pi
```
Open in Visual Studio and run!
