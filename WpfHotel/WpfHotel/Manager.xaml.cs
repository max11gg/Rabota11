using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
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
                case 1:
                    try
                    {
                        dgBooking.ItemsSource = null;

                        HttpResponseMessage response = await client.GetAsync($"https://192.168.43.59:7071/api/Bookings");
                        string json1 = await response.Content.ReadAsStringAsync();
                        List<Booking> data = JsonConvert.DeserializeObject<List<Booking>>(json1);
                        dgBooking.ItemsSource = data;

                        var columns = dgBooking.Columns;
                        columns[1].Header = "Дата прибытия";
                        columns[2].Header = "Дата отбытия";
                        columns[3].Header = "Идентификатор пользователя";
                        columns[4].Header = "Идентификатор услуги";
                        columns[5].Header = "Идентификатор номера";
                        columns[6].Header = "Бронирование";

                        columns[0].Visibility = Visibility.Collapsed;
                    }
                    catch { await LoadData(1); }
                    break;
                case 2:
                    try
                    {
                        dgCheckIn.ItemsSource = null;
                        dgCheckOut.ItemsSource = null;

                        HttpResponseMessage response2 = await client.GetAsync($"https://192.168.43.59:7071/api/CheckIns");
                        string json2 = await response2.Content.ReadAsStringAsync();
                        List<CheckIn> data2 = JsonConvert.DeserializeObject<List<CheckIn>>(json2);
                        dgCheckIn.ItemsSource = data2;

                        var columns2 = dgCheckIn.Columns;
                        columns2[1].Header = "Статус заселения";
                        columns2[2].Header = "Идентификатор пользователя";
                        columns2[3].Header = "Идентификатор бронирования";
                        columns2[0].Visibility = Visibility.Collapsed;
                        HttpResponseMessage response3 = await client.GetAsync($"https://192.168.43.59:7071/api/CheckOuts");
                        string json3 = await response3.Content.ReadAsStringAsync();
                        List<CheckOut> data3 = JsonConvert.DeserializeObject<List<CheckOut>>(json3);
                        dgCheckOut.ItemsSource = data3;

                        var columns3 = dgCheckOut.Columns;
                        columns3[0].Visibility = Visibility.Collapsed;
                        columns3[1].Header = "Дата оплаты";
                        columns3[2].Header = "Общая стоимость";
                        columns3[3].Header = "Идентификатор заселения";
                        columns3[4].Header = "Идентификатор пользователя";

                    }
                    catch { await LoadData(2); }
                    break;
          

                case 5:
                    try
                    {
                        dgUser.ItemsSource = null;
                        HttpResponseMessage response8 = await client.GetAsync($"https://192.168.43.59:7071/api/Users");
                        string json8 = await response8.Content.ReadAsStringAsync();
                        List<User> data8 = JsonConvert.DeserializeObject<List<User>>(json8);
                        dgUser.ItemsSource = data8;

                        HttpResponseMessage response9 = await client.GetAsync($"https://192.168.43.59:7071/api/Roles");
                        string json9 = await response9.Content.ReadAsStringAsync();
                        List<Role> data9 = JsonConvert.DeserializeObject<List<Role>>(json9);
                      

                        var columns8 = dgUser.Columns;
                        columns8[1].Header = "Имя";
                        columns8[2].Header = "Фамилия";
                        columns8[3].Header = "Отчество";
                        columns8[4].Header = "Email";
                        columns8[5].Header = "Номер телефона";
                        columns8[6].Header = "Серия паспорта";
                        columns8[7].Header = "Номер паспорта";
                        columns8[8].Header = "Логин";
                        columns8[9].Header = "Пароль";
                        columns8[10].Header = "Роль";
                        columns8[0].Visibility = Visibility.Collapsed;

                    }
                    catch { await LoadData(5); }
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

            private async void Button_Click(object sender, RoutedEventArgs e)
            {
                CreateTable<Booking>(dgBooking);
                await LoadData(1);
            }

            private async void Button_Click_1(object sender, RoutedEventArgs e)
            {
                CreateTable<CheckIn>(dgCheckIn);
                await LoadData(2);
            }

            private async void Button_Click_2(object sender, RoutedEventArgs e)
            {
                CreateTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }

         

            private async void Button_Click_7(object sender, RoutedEventArgs e)
            {
                CreateTable<User>(dgUser);
                await LoadData(5);
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

            private async void Button_Click_9(object sender, RoutedEventArgs e)
            {
                PutTable<Booking>(dgBooking);
                await LoadData(1);
            }

            private async void Button_Click_10(object sender, RoutedEventArgs e)
            {
                PutTable<CheckIn>(dgCheckIn);
                await LoadData(2);
            }

            private async void Button_Click_11(object sender, RoutedEventArgs e)
            {
                PutTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }



            private async void Button_Click_16(object sender, RoutedEventArgs e)
            {
                PutTable<User>(dgUser);
                await LoadData(5);
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

            private async void Button_Click_18(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Booking>(dgBooking);
                await LoadData(1);
            }

            private async void Button_Click_19(object sender, RoutedEventArgs e)
            {
                await DeleteTable<CheckIn>(dgCheckIn);
                await LoadData(2);
            }

            private async void Button_Click_20(object sender, RoutedEventArgs e)
            {
                await DeleteTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }

      

            private async void Button_Click_25(object sender, RoutedEventArgs e)
            {
                await DeleteTable<User>(dgUser);
                await LoadData(5);
            }

        private void ExportToSql_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EDQFE2B\\SQL;Initial Catalog=Hotel;User ID=sa;Password=123"))
            {
                connection.Open();

                List<Booking> data = dgBooking.ItemsSource.Cast<Booking>().ToList();

                foreach (var item in data)
                {
                    string insertQuery = $"INSERT INTO Booking (Arrival_date, Departure_date, User_ID, Service_ID, Room_ID) " +
                                         $"VALUES ('{item.ArrivalDate}', '{item.DepartureDate}', {item.UserId}, {item.ServiceId}, {item.RoomId})";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Export to SQL completed.");
        }

        private void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            List<Booking> data = dgBooking.ItemsSource.Cast<Booking>().ToList();

            StringBuilder csv = new StringBuilder();
            csv.AppendLine("IdBooking,ArrivalDate,DepartureDate,UserId,ServiceId,RoomId");

            foreach (var item in data)
            {
                csv.AppendLine($"{item.IdBooking},{item.ArrivalDate},{item.DepartureDate},{item.UserId},{item.ServiceId},{item.RoomId}");
            }

            File.WriteAllText("Booking.csv", csv.ToString());

            MessageBox.Show("Export to CSV completed.");
        }

        private void ImportFromSql_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EDQFE2B\\SQL;Initial Catalog=Hotel;User ID=sa;Password=123"))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Booking";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    List<Booking> importedData = new List<Booking>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                IdBooking = Convert.ToInt32(reader["ID_Booking"]),
                                ArrivalDate = Convert.ToDateTime(reader["Arrival_date"]),
                                DepartureDate = Convert.ToDateTime(reader["Departure_date"]),
                                UserId = Convert.ToInt32(reader["User_ID"]),
                                ServiceId = Convert.ToInt32(reader["Service_ID"]),
                                RoomId = Convert.ToInt32(reader["Room_ID"])
                            };

                            importedData.Add(booking);
                        }
                    }

                    dgBooking.ItemsSource = importedData;
                }
            }

            MessageBox.Show("Import from SQL completed.");
        }

        private void ImportFromCsv_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                List<Booking> importedData = new List<Booking>();

                foreach (var line in lines.Skip(1)) // Skip header
                {
                    var values = line.Split(',');
                    if (values.Length == 6)
                    {
                        Booking booking = new Booking
                        {
                            IdBooking = int.Parse(values[0]),
                            ArrivalDate = DateTime.Parse(values[1]),
                            DepartureDate = DateTime.Parse(values[2]),
                            UserId = int.Parse(values[3]),
                            ServiceId = int.Parse(values[4]),
                            RoomId = int.Parse(values[5])
                        };

                        importedData.Add(booking);
                    }
                }

                dgBooking.ItemsSource = importedData;
                MessageBox.Show("Import from CSV completed.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}