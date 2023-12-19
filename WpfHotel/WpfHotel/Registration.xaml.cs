using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Windows;

namespace WpfHotel
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Назад".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (кнопка).</param>
        /// <param name="e">Информация о событии (RoutedEventArgs).</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр главного окна и отображаем его
            MainWindow main = new MainWindow();
            main.Show();

            // Закрываем текущее окно регистрации
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Зарегистрироваться".
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие (кнопка).</param>
        /// <param name="e">Информация о событии (RoutedEventArgs).</param>
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр HttpClient
            HttpClient client = new HttpClient();

            // Задаем адрес вашего API
            string apiUrl = "https://192.168.43.59:7071/api/Users";

            // Создаем объект, который будет отправлен в теле запроса
            var userData = new
            {
                firstNameUser = FirstName.Text,
                lastNameUser = LastName.Text,
                middleNameUser = MiddleName.Text,
                loginUser = Login.Text,
                passwordUser = Password.Text,

                roleId = 2, // Замените на реальный идентификатор роли
                salt = "string"
            };

            // Сериализуем объект в формат JSON
            var userJson = JsonConvert.SerializeObject(userData);

            // Настройки безопасности для обеспечения поддержки TLS
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (senders, cert, chain, sslPolicyErrors) => true;

            // Отправляем POST-запрос к API
            var response = await client.PostAsync(apiUrl, new StringContent(userJson, Encoding.UTF8, "application/json"));

            // Проверяем успешность запроса
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь зарегистрирован успешно!");
            }
            else
            {
                MessageBox.Show($"Ошибка при регистрации пользователя. Код ошибки: {response.StatusCode}");
            }
        }
    }
}