﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
namespace Lab6_7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ResourceDictionary dict2 =new ResourceDictionary() { Source = new Uri("light.xaml", UriKind.Relative) };
        private readonly ResourceDictionary dict1= new ResourceDictionary() { Source = new Uri("dark.xaml", UriKind.Relative) };
        private readonly ResourceDictionary dict3= new ResourceDictionary() { Source = new Uri("Resources/lang.xaml", UriKind.Relative) };
        private readonly ResourceDictionary dict4= new ResourceDictionary() { Source = new Uri("Resources/lang.ru_RU.xaml", UriKind.Relative) };
        private List<StockItem> ItemList = new List<StockItem>();
        private readonly string DATApath = "../../database.json";
        public class StockItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Firm { get; set; }
            public string Cost { get; set; }
            public string Stock { get; set; }
            public string ImgPath { get; set; }
        }
       
        public MainWindow()
        {
            InitializeComponent();
            Resources.MergedDictionaries.Add(dict2);
            Resources.MergedDictionaries.Add(dict3);

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ItemList = JsonConvert.DeserializeObject<List<StockItem>>(File.ReadAllText(DATApath));
            Photos.ItemsSource = ItemList;
            Database.ItemsSource = ItemList;
        }

        private void Searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Searcher.Text != "")
            {
                var SearchResult = ItemList.FindAll(t => t.Title.ToLower().Contains(Searcher.Text.ToLower()));
                Photos.ItemsSource = SearchResult;
                Database.ItemsSource = SearchResult;
            }
            else
            {
                Photos.ItemsSource = ItemList;
                Database.ItemsSource = ItemList;
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            ItemList.Add(new StockItem
            {
                Id = ID.Text,
                Title = Title.Text,
                Cost=Cost.Text,
                Stock=Stock.Text,
                Firm=Firm.Text,
                ImgPath = ImgPath.Text
            });
            Database.ItemsSource = null;
            Database.ItemsSource = ItemList;
            Photos.ItemsSource = null;
            Photos.ItemsSource = ItemList;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

            ID.Clear();
            Title.Clear();
            Cost.Clear();
            Stock.Clear();
            Firm.Clear();
            ImgPath.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = InputID.Text;
            var a = ItemList.FindIndex(t => t.Id == id);
            try
            {
                ItemList.RemoveAt(a);
                Database.ItemsSource = null;
                Database.ItemsSource = ItemList;
                Photos.ItemsSource = null;
                Photos.ItemsSource = ItemList;
            }
            catch(Exception)
            {

            }
            finally
            {
                InputID.Clear();
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(DATApath, JsonConvert.SerializeObject(ItemList));
        }

        private void Light_Selected(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries.Remove(dict1);
            Resources.MergedDictionaries.Add(dict2);
        }

        private void Dark_Selected(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries.Remove(dict2);
            Resources.MergedDictionaries.Add(dict1);

        }

        private void Ru_Selected(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries.Remove(dict3);

            Resources.MergedDictionaries.Add(dict4);

        } 
        private void Eng_Selected(object sender, RoutedEventArgs e)
        {
            Resources.MergedDictionaries.Remove(dict4);

            Resources.MergedDictionaries.Add(dict3);

        }

    }
}
