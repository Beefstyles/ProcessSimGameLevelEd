﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ProcessSimLevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int methaneFrac, ethaneFrac, propaneFrac;
        private int totalFlow = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculatePhase(object sender, RoutedEventArgs e)
        {
             var tb = sender as TextBox;
             totalFlow = methaneFrac + ethaneFrac;
             //MessageBox.Show("Total flow is " + totalFlow);
             DisplayTotalFlow();   
        }

        private void NunberValTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled)
            {
                MessageBox.Show("Enter only numerical values here");
            }
        }

        private void MethaneTBChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox; 
                Int32.TryParse(textBox.Text, out methaneFrac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void EthaneTBChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out ethaneFrac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void PropaneTBChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                Int32.TryParse(textBox.Text, out propaneFrac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void DisplayTotalFlow()
        {
            TotalFlowCalc.Text = totalFlow.ToString();
        }
    }
}
