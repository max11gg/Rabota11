using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfHotel.Models;

namespace WpfHotel
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
           MilFill();
            MilFills();
         //   SetDataGridColumns();
        }

        private void MilFill()
        {
            DataBaseClass dataBaseClass = new DataBaseClass();
            dataBaseClass.sqlExecute(string.Format("select [ID_Room], [Type_room] from [dbo].[Room] "), DataBaseClass.act.select);
            dataBaseClass.dependency.OnChange += Dependency_OnChange_Mil;
            Action action = () =>
            {
                cbRoom.ItemsSource = dataBaseClass.resultTable.DefaultView;
                cbRoom.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                cbRoom.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }
        private void Dependency_OnChange_Mil(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                MilFill();
        }


        private void MilFills()
        {
            DataBaseClass dataBaseClass = new DataBaseClass();
            dataBaseClass.sqlExecute(string.Format("select [ID_Service], [Name_Services] from [dbo].[Service] "), DataBaseClass.act.select);
            dataBaseClass.dependency.OnChange += Dependency_OnChange_Mils;
            Action action = () =>
            {
                cbService.ItemsSource = dataBaseClass.resultTable.DefaultView;
                cbService.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                cbService.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }
        private void Dependency_OnChange_Mils(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                MilFills();
        }

        public int count;
            List<Service> data6;
            public async Task LoadData(int count)
            {
                HttpClient client = new HttpClient();
                switch (count)
                {
                    case 1:
                    dgBooking.ItemsSource = null;
                  string id = MainWindow.IDUser;
                    HttpResponseMessage response = await client.GetAsync($"https://192.168.43.59:7071/get/{id}");
                    if (response.IsSuccessStatusCode)
                    {
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
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Обработка случая, когда нет бронирований для указанного User_ID
                        dgBooking.ItemsSource = new List<Booking>(); // Установить пустой список
                    }
                    break;

                case 2:
                    try
                    {
                        dgCheckOut.ItemsSource = null;


                        // Получение данных по CheckOut для конкретного пользователя
                        HttpResponseMessage response3 = await client.GetAsync($"https://192.168.43.59:7071/api/CheckOuts/ByUser/{MainWindow.IDUser}");

                        if (response3.IsSuccessStatusCode)
                        {
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
                        else if (response3.StatusCode == HttpStatusCode.NotFound)
                        {
                            // Обработка случая, когда нет данных для указанного User_ID
                            dgCheckOut.ItemsSource = new List<CheckOut>(); // Установить пустой список
                        }
                    }
                    catch { await LoadData(2); }
                    break;
                    case 3:
                    try
                    {
                        dgHotel.ItemsSource = null;
                        dgRoom.ItemsSource = null;
                        HttpResponseMessage response4 = await client.GetAsync($"https://192.168.43.59:7071/api/Hotels");
                        string json4 = await response4.Content.ReadAsStringAsync();
                        List<Hotel> data4 = JsonConvert.DeserializeObject<List<Hotel>>(json4);
                        dgHotel.ItemsSource = data4;


                        HttpResponseMessage response5 = await client.GetAsync($"https://192.168.43.59:7071/api/Rooms");
                        string json5 = await response5.Content.ReadAsStringAsync();
                        List<Room> data5 = JsonConvert.DeserializeObject<List<Room>>(json5);
                        dgRoom.ItemsSource = data5;
                        var columns4 = dgHotel.Columns;
                        columns4[1].Header = "Адрес отеля";
                        columns4[2].Header = "Рейтинг отеля";
                        columns4[3].Header = "Номер телефона отеля";
                        columns4[4].Header = "Email отеля";
                        columns4[0].Visibility = Visibility.Collapsed;

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
        private HttpClient client = new HttpClient();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {


/*
            if (dgBooking.SelectedItems.Count > 0)
            {*/
                // Получаем выбранный объект Booking из DataGrid
             //   Booking selectedBooking = dgBooking.SelectedItem as Booking;

            /*    if (selectedBooking != null)
                {
                    // Чтение значения свойства IsBooking
                    bool isBookingValue = selectedBooking.IsBooking;
*/
                    // Используйте значение isBookingValue по вашему усмотрению
                //    MessageBox.Show($"Значение галочки: {isBookingValue}");


                    // Создаем объект Booking на основе введенных данных в элементы управления
                    Booking newBooking = new Booking
                    {
                        ArrivalDate = datePickerArrival.SelectedDate.GetValueOrDefault(),
                        DepartureDate = datePickerDeparture.SelectedDate.GetValueOrDefault(),
                        UserId = Convert.ToInt32(MainWindow.IDUser),
                        RoomId = (int)cbRoom.SelectedValue,
                        ServiceId = (int)cbService.SelectedValue,
                        IsBooking = false // По умолчанию можно установить false или какой вам нужно
                    };

                    string json = JsonConvert.SerializeObject(newBooking);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("https://192.168.43.59:7071/api/Bookings", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Обработка успешного добавления бронирования
                        MessageBox.Show("Бронирование успешно добавлено!");
               await LoadData(1);
                    }
                    else
                    {
                        // Обработка ошибки добавления бронирования
                        MessageBox.Show("Ошибка при добавлении бронирования. Пожалуйста, проверьте введенные данные.");
                    }
             /*   }*/
         /*   }
            else
            {
                MessageBox.Show("Выберите строку в DataGrid.");
            }
*/



          
        }


        private async void Button_Click_2(object sender, RoutedEventArgs e)
            {
                CreateTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }

            private async void Button_Click_3(object sender, RoutedEventArgs e)
            {
                CreateTable<Hotel>(dgHotel);
                await LoadData(3);
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

            private async void Button_Click_9(object sender, RoutedEventArgs e)
            {
                PutTable<Booking>(dgBooking);
                await LoadData(1);
            }

         

            private async void Button_Click_11(object sender, RoutedEventArgs e)
            {
                PutTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }

            private async void Button_Click_12(object sender, RoutedEventArgs e)
            {
                PutTable<Hotel>(dgHotel);
                await LoadData(3);
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

            private async void Button_Click_18(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Booking>(dgBooking);
                await LoadData(1);
            }

         

            private async void Button_Click_20(object sender, RoutedEventArgs e)
            {
                await DeleteTable<CheckOut>(dgCheckOut);
                await LoadData(2);
            }

            private async void Button_Click_21(object sender, RoutedEventArgs e)
            {
                await DeleteTable<Hotel>(dgHotel);
                await LoadData(3);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
    }
