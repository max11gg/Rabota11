using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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
using DocumentFormat.OpenXml.Math;
using System.Numerics;

namespace WpfHotel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        public static string roleIdString;
        public static string IDUser;


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private readonly string _apiUrl = "https://192.168.43.59:7071/api";

        /// <summary>
        /// Обработчик события нажатия кнопки "Войти".
        /// </summary>
        /// <param name="sender">Объект-инициатор события.</param>
        /// <param name="e">Аргументы события.</param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            string token = await Authorization(login, password);
            if (token != null)
            {
                HttpClient client = new HttpClient();
                string apiUrl = "https://192.168.43.59:7071/api/Users"; // Замените на фактический URL вашего API
                string requestUrl = $"{apiUrl}/GetRoleIdByLogin?login={login}";

                HttpClient clientID = new HttpClient(); // Замените на фактический URL вашего API
                string requestUrlID = $"{apiUrl}/GetIdByLogin?login={login}";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    HttpResponseMessage responseID = await client.GetAsync(requestUrlID);


                    if (response.IsSuccessStatusCode)
                    {
                         roleIdString = await response.Content.ReadAsStringAsync();
                        IDUser = await responseID.Content.ReadAsStringAsync();

                        if (int.TryParse(roleIdString, out int roleId))
                        {
                            switch (roleId)
                            {
                                case 1:
                                    Admin admin = new Admin();
                                    admin.Show();
                                    Close();
                                    break;
                                case 2:
                                    Client doctor = new Client();
                                    doctor.Show();
                                    Close();
                                    break;
                                case 3:
                                    AdminNeBD pacient = new AdminNeBD();
                                    pacient.Show();
                                    Close();
                                    break;
                                case 4:
                                    Manager registrator = new Manager();
                                    registrator.Show();
                                    Close();
                                    break;
                            }
                        }
                        else
                        {
                            // Обработка ошибки преобразования строки в число
                            MessageBox.Show("Невозможно преобразовать RoleId в число.");
                        }
                    }
                    else
                    {
                        // Обработка ошибки, например, вывод сообщения пользователю
                        MessageBox.Show($"Ошибка: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Обработка исключения, например, вывод сообщения об ошибке
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }

        }
        /// <summary>
        /// Метод для выполнения авторизации на сервере.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Токен авторизации.</returns>
        private async Task<string> Authorization(string login, string password)
        {
            using (var httpClient = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                HttpResponseMessage response = await httpClient.GetAsync($"{_apiUrl}/Users/{login}/{password}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    return null;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Зарегистрироваться".
        /// </summary>
        /// <param name="sender">Объект-инициатор события.</param>
        /// <param name="e">Аргументы события.</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }
    }
}
