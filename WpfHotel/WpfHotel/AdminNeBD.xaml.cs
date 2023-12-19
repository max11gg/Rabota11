using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfHotel.Models;

namespace WpfHotel
{
    /// <summary>
    /// Логика взаимодействия для AdminNeBD.xaml
    /// </summary>
    public partial class AdminNeBD : Window
    {
        public AdminNeBD()
        {
            InitializeComponent();
        }
       
            public int count;
            List<Service> data6;
        public async Task LoadData(int count)
        {
            HttpClient client = new HttpClient();
            switch (count)
            {
                
                case 3:
                    try
                    {
                        dgRoom.ItemsSource = null;
                        HttpResponseMessage response4 = await client.GetAsync($"https://192.168.43.59:7071/api/Hotels");
                        string json4 = await response4.Content.ReadAsStringAsync();
                        List<Hotel> data4 = JsonConvert.DeserializeObject<List<Hotel>>(json4);
                      


                        HttpResponseMessage response5 = await client.GetAsync($"https://192.168.43.59:7071/api/Rooms");
                        string json5 = await response5.Content.ReadAsStringAsync();
                        List<Room> data5 = JsonConvert.DeserializeObject<List<Room>>(json5);
                        dgRoom.ItemsSource = data5;

                       

                        var columns5 = dgRoom.Columns;
                        columns5[1].Header = "Количество комнат";
                        columns5[2].Header = "Цена";
                        columns5[3].Header = "Номер комнаты";
                        columns5[4].Header = "Тип комнаты";
                        columns5[5].Header = "Статус комнаты";
                        columns5[6].Header = "Идентификатор отеля";
                        columns5[0].Visibility = Visibility.Collapsed;
                    }
                    catch { await LoadData(3); }
                    break;
                case 4:
                    try
                    {
                        dgService.ItemsSource = null;
                        dgStatus.ItemsSource = null;

                        HttpResponseMessage response6 = await client.GetAsync($"https://192.168.43.59:7071/api/Services");
                        string json6 = await response6.Content.ReadAsStringAsync();
                        data6 = JsonConvert.DeserializeObject<List<Service>>(json6);
                        dgService.ItemsSource = data6;
                        cmbTypeServices.ItemsSource = data6;

                        HttpResponseMessage response7 = await client.GetAsync($"https://192.168.43.59:7071/api/Status");
                        string json7 = await response7.Content.ReadAsStringAsync();
                        List<Status> data7 = JsonConvert.DeserializeObject<List<Status>>(json7);
                        dgStatus.ItemsSource = data7;

                        var columns6 = dgService.Columns;
                        columns6[1].Header = "Название услуги";
                        columns6[2].Header = "Описание услуги";
                        columns6[3].Header = "Цена услуги";
                        columns6[0].Visibility = Visibility.Collapsed;

                        var columns7 = dgStatus.Columns;
                        columns7[1].Header = "Доступность";
                        columns7[0].Visibility = Visibility.Collapsed;
                    }
                    catch { await LoadData(4); }
                    break;

               
            }
        }

        private async void TabItem_Loaded(object sender, RoutedEventArgs e)
            {
                count = 1;
                await LoadData(count);
            }

            private async void TabItem_Loaded_1(object sender, RoutedEventArgs e)
            {
                count = 2;
                await LoadData(count);
            }

            private async void TabItem_Loaded_2(object sender, RoutedEventArgs e)
            {
                count = 3;
                await LoadData(count);
            }

            private async void TabItem_Loaded_3(object sender, RoutedEventArgs e)
            {
                count = 4;
                await LoadData(count);
            }

            private async void TabItem_Loaded_4(object sender, RoutedEventArgs e)
            {
                count = 5;
                await LoadData(count);
            }

            public async void CreateTable<T>(DataGrid name)
            {
                HttpClient client = new HttpClient();
                var selectedItems = name.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    properties[0].SetValue(data, null);
                    string jsonString = JsonConvert.SerializeObject(data);
                    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    string ClassName = typeof(T).Name;
                    switch (ClassName)
                    {
                        case ("Status"):
                            ClassName = "Statu";
                            break;

                    }
                    HttpResponseMessage response = await client.PostAsync($"https://192.168.43.59:7071/api/{ClassName}s", content);
                    await LoadData(count);
                }
                else
                {
                    MessageBox.Show("Выберете строку!");
                }
            }

         

         
            private async void Button_Click_4(object sender, RoutedEventArgs e)
            {
                CreateTable<Room>(dgRoom);
                await LoadData(3);
            }

            private async void Button_Click_5(object sender, RoutedEventArgs e)
            {
                CreateTable<Service>(dgService);
                await LoadData(4);
            }

            private async void Button_Click_6(object sender, RoutedEventArgs e)
            {
                CreateTable<Status>(dgStatus);
                await LoadData(4);
            }

          
            public async void PutTable<T>(DataGrid name)
            {
                HttpClient client = new HttpClient();
                var selectedItems = name.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    int ID = (int)properties[0].GetValue(data);
                    string jsonString = JsonConvert.SerializeObject(data);
                    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    string ClassName = typeof(T).Name;
                    switch (ClassName)
                    {
                        case ("Status"):
                            ClassName = "Statu";
                            break;

                    }
                    HttpResponseMessage response = await client.PutAsync($"https://192.168.43.59:7071/api/{ClassName}s/{ID}", content);
                    await LoadData(count);
                }
                else
                {
                    MessageBox.Show("Выберете строку!");
                }
            }

         

            private async void Button_Click_13(object sender, RoutedEventArgs e)
            {
                PutTable<Room>(dgRoom);
                await LoadData(3);
            }

            private async void Button_Click_14(object sender, RoutedEventArgs e)
            {
                PutTable<Service>(dgService);
                await LoadData(4);
            }

            private async void Button_Click_15(object sender, RoutedEventArgs e)
            {
                PutTable<Status>(dgStatus);
                await LoadData(4);
            }

         

            public async Task DeleteTable<T>(DataGrid name)
            {
                HttpClient client = new HttpClient();
                var selectedItems = name.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    int id = (int)properties[0].GetValue(data);
                    string ClassName = typeof(T).Name;
                    switch (ClassName)
                    {
                        case ("Status"):
                            ClassName = "Statu";
                            break;

                    }
                    HttpResponseMessage response = await client.DeleteAsync($"https://192.168.43.59:7071/api/{ClassName}s/{id}");
                    await LoadData(count);
                }
                else
                {
                    MessageBox.Show("Выберете одну строку для удаления!");
                }
            }

         

            private async void Button_Click_22(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Room>(dgRoom);
                await LoadData(3);
            }

            private async void Button_Click_23(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Service>(dgService);
                await LoadData(4);
            }

            private async void Button_Click_24(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Status>(dgStatus);
                await LoadData(4);
            }

           

           

            private void Button_Click_27(object sender, RoutedEventArgs e)
            {
                string databaseName = "Hotel"; // Замените на имя вашей базы данных

                try
                {
                    // Диалоговое окно выбора файла для сохранения резервной копии
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Backup files (*.bak)|*.bak";
                    saveFileDialog.FileName = $"{databaseName}_Backup.bak";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string backupPath = saveFileDialog.FileName;

                        using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EDQFE2B\\SQL;Initial Catalog=Hotel;User ID=sa;Password=123"))
                        {
                            connection.Open();

                            // Создание резервной копии базы данных
                            string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupPath}'";

                            using (SqlCommand command = new SqlCommand(backupQuery, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show($"Backup created successfully. Path: {backupPath}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating backup: {ex.Message}");
                }
            }

            private void cmbTypeServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // Получаем выбранное значение из комбобокса
                string selectedType = (string)(cmbTypeServices.SelectedItem as Service)?.NameServices;

                // Фильтруем данные в data7 в зависимости от выбранного значения
                List<Service> filteredData = data6.Where(s => s.NameServices == selectedType).ToList();

                // Обновляем источник данных для DataGrid
                dgService.ItemsSource = filteredData;
            }

            private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
            {
                // Получаем текст из TextBox
                string searchText = txtSearch.Text;

                // Фильтруем данные в data7 в зависимости от введенного текста
                List<Service> filteredData = data6.Where(s => s.NameServices.Contains(searchText)).ToList();

                // Обновляем источник данных для DataGrid
                dgService.ItemsSource = filteredData;
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
    }
