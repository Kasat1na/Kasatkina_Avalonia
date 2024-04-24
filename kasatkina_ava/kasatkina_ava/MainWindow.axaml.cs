using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace kasatkina_ava
{
    public struct registr
    {
        public string name; // имя
        public string surname; // фамилия
        public string otvestvo; // отчество
        public string pol; // пол
        public string phone; // телефон
        public int dayr; // день р
        public string monther; // месяц р
        public int yearr; // год р

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private List<registr> users = new List<registr>();
        private void WriteFile(object sender, RoutedEventArgs e)
        {
            registr person = new registr();
            person.name = tbName.Text;
            person.surname = tbSurname.Text;
            person.otvestvo = tbotchestvo.Text;
            person.phone = tbphone.Text;


            if (int.TryParse(tbday.Text, out int day))
            {
                person.dayr = day;
            }

            if (int.TryParse(tbyear.Text, out int yearInt))
            {
                person.yearr = yearInt;
            }
            if (rbM.IsChecked == true) person.pol = "Мужской";
            if (rbZH.IsChecked == true) person.pol = "Женский";

            switch (cbContinent.SelectedIndex)
            {
                case 1:
                    person.monther = "Январь";
                    break;
                case 2:
                    person.monther = "Февраль";
                    break;
                case 3:
                    person.monther = "Март";
                    break;
                case 4:
                    person.monther = "Апрель";
                    break;
                case 5:
                    person.monther = "Мая";
                    break;
                case 6:
                    person.monther = "Июнь";
                    break;
                case 7:
                    person.monther = "Июль";
                    break;
                case 8:
                    person.monther = "Август";
                    break;
                case 9:
                    person.monther = "Сентябрь";
                    break;
                case 10:
                    person.monther = "Октябрь";
                    break;
                case 11:
                    person.monther = "Ноябрь";
                    break;
                case 12:
                    person.monther = "Декабрь";
                    break;
            }


            if (!ValidateName(person.name, "Имя") || !ValidateName(person.surname, "Фамилия") ||
            !ValidatePhone(person.phone) || !ValidateDate(person.dayr, person.monther, person.yearr))
            {
                return;
            }
            // получение дня недели и знака древнеславянского гороскопа
            DateTime birthDate = new DateTime(person.yearr, MonthNumber(person.monther), person.dayr);
            string dayOfWeek = birthDate.ToString("dddd", new CultureInfo("ru-RU"));
            string slavicSign = Goroskop(birthDate);

            try
            {
                using (StreamWriter sw = new StreamWriter("users.csv", true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(person.name + ";" + person.surname + ";" + person.otvestvo + ";" + person.pol + ";" + person.phone + ";" + person.dayr + ";" + person.monther + ";" + person.yearr + ";" + dayOfWeek + ";" + slavicSign);
                    ShowMessage.Text = "Запись добавлена";
                }


            }
            catch (Exception ex)
            {
                ShowMessage.Text = $"Ошибка при записи в файл: {ex.Message}";
            }
        }
        private bool ValidateDate(int day, string monthStr, int year)
        {
            if (day < 1 || day > 31 || year > DateTime.Now.Year)
            {
                ShowMessage.Text = "Введите корректную дату рождения.";
                return false;
            }
            return true;
        }
        private int MonthNumber(string monthName)
        {
            switch (monthName)
            {
                case "Январь":
                    return 1;
                case "Февраль":
                    return 2;
                case "Март":
                    return 3;
                case "Апрель":
                    return 4;
                case "Мая":
                    return 5;
                case "Июнь":
                    return 6;
                case "Июль":
                    return 7;
                case "Август":
                    return 8;
                case "Сентябрь":
                    return 9;
                case "Октябрь":
                    return 10;
                case "Ноябрь":
                    return 11;
                case "Декабрь":
                    return 12;
                default:
                    return -1;
            }

        }
        // древнеславянский гороскоп
        private string Goroskop(DateTime birthDate)
        {
            if (birthDate.Month == 1 && birthDate.Day <= 30 || birthDate.Month == 12 && birthDate.Day >= 24)
            {
                return "Мороз";
            }
            if (birthDate.Month == 2 && birthDate.Day <= 28 || birthDate.Month == 1 && birthDate.Day >= 31)
            {
                return "Велес";
            }
            if (birthDate.Month == 3)
            {
                return "Макошь";
            }
            if (birthDate.Month == 4)
            {
                return "Жива";
            }
            if (birthDate.Month == 5 && birthDate.Day <= 14 || birthDate.Month == 5 && birthDate.Day >= 1)
            {
                return "Ярило";
            }
            if (birthDate.Month == 6 && birthDate.Day <= 2 || birthDate.Month == 5 && birthDate.Day >= 15)
            {
                return "Леля";
            }
            if (birthDate.Month == 6 && birthDate.Day <= 12 || birthDate.Month == 6 && birthDate.Day >= 2)
            {
                return "Кострома";
            }
            if (birthDate.Month == 7 && birthDate.Day <= 6 || birthDate.Month == 6 && birthDate.Day >= 13)
            {
                return "Додола";
            }
            if (birthDate.Month == 6 && birthDate.Day >= 24)
            {
                return "Иван Купала";
            }
            if (birthDate.Month == 7 && birthDate.Day <= 31 || birthDate.Month == 7 && birthDate.Day >= 7)
            {
                return "Лада";
            }
            if (birthDate.Month == 8 && birthDate.Day <= 28 || birthDate.Month == 8 && birthDate.Day >= 1)
            {
                return "Перун";
            }
            if (birthDate.Month == 9 && birthDate.Day <= 13 || birthDate.Month == 8 && birthDate.Day >= 29)
            {
                return "Сева";
            }
            if (birthDate.Month == 9 && birthDate.Day <= 27 || birthDate.Month == 9 && birthDate.Day >= 14)
            {
                return "Рожаница";
            }
            if (birthDate.Month == 10 && birthDate.Day <= 15 || birthDate.Month == 9 && birthDate.Day >= 28)
            {
                return "Сварожичи";
            }
            if (birthDate.Month == 11 && birthDate.Day <= 8 || birthDate.Month == 10 && birthDate.Day >= 16)
            {
                return "Морена";
            }
            if (birthDate.Month == 11 && birthDate.Day <= 28 || birthDate.Month == 11 && birthDate.Day >= 9)
            {
                return "Зима";
            }
            if (birthDate.Month == 12 && birthDate.Day <= 23 || birthDate.Month == 11 && birthDate.Day >= 29)
            {
                return "Карачун";
            }
            return "Неизвестный знак";


        }
        private bool ValidateName(string name, string fieldName)
        {
            if (!Regex.IsMatch(name, "^[А-ЯЁ][а-яё]+$") ||
                (fieldName == "Имя" && name.Length < 2) ||
                (fieldName == "Фамилия" && name.Length < 3))
            {
                ShowMessage.Text = $"{fieldName} должно начинаться с заглавной буквы и содержать только русские буквы. " +
                                           $"Минимальная длина для имени - 2 символа, для фамилии - 3 символа.";
                return false;
            }
            return true;
        }
        public bool ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, @"^8\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}$"))
            {
                ShowMessage.Text = "Номер телефона должен быть в формате 8(XXX)XXX-XX-XX";
                return false;
            }
            return true;
        }
        public SolidColorBrush color(string pol)
        {
            switch (pol)
            {
                case "Мужской":
                    return new SolidColorBrush(Color.FromArgb(51, 187, 0, 255));
                case "Женский":
                    return new SolidColorBrush(Color.FromArgb(255, 51, 0, 187));
                default:
                    return new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }
        private void ShowUsersList(object sender, RoutedEventArgs e)
        {
            InputFormGrid.IsVisible = false;
            WritePanel.IsVisible = true;
            users.Sort((user1, user2) =>
            {
                int surnameComparison = user1.surname.CompareTo(user2.surname);
                return surnameComparison != 0 ? surnameComparison : user1.name.CompareTo(user2.name);
            });
            foreach (registr user in users)
            {
                string formattedBirthDate = new DateTime(user.yearr, MonthNumber(user.monther), user.dayr).ToString("dd.MM.yyyy");
            }
            using (StreamReader sr = new StreamReader("users.csv"))
            {
                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] lineArray = line.Split(';');
                    TextBlock tbName = new TextBlock()
                    {
                        Text = "Имя: " + lineArray[0],
                    };
                    TextBlock tbSurname = new TextBlock()
                    {
                        Text = "Фамилия: " + lineArray[1]
                    };
                    TextBlock tbotchestvo = new TextBlock()
                    {
                        Text = "Отчество: " + lineArray[2]
                    };
                    TextBlock tbphone = new TextBlock()
                    {
                        Text = "Телефон: " + lineArray[3]
                    };
                    TextBlock tbday = new TextBlock()
                    {
                        Text = "День: " + lineArray[4]
                    };
                    TextBlock tbyear = new TextBlock()
                    {
                        Text = "Год: " + lineArray[5]
                    };
                    TextBlock cbContinent = new TextBlock()
                    {
                        Text = "Месяц: " + lineArray[6]
                    };

                    StackPanel spCountry = new StackPanel();
                    spCountry.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
                    spCountry.Children.Add(tbName);
                    spCountry.Children.Add(tbSurname);
                    spCountry.Children.Add(tbotchestvo);
                    spCountry.Children.Add(tbday);
                    spCountry.Children.Add(tbyear);
                    spCountry.Children.Add(cbContinent);

                    Border border = new Border();
                    border.Background = color(lineArray[2]);
                    border.BorderBrush = Brushes.Indigo;
                    border.BorderThickness = new Avalonia.Thickness(2);
                    border.CornerRadius = new Avalonia.CornerRadius(50);
                    border.Margin = new Avalonia.Thickness(10);
                    border.Padding = new Avalonia.Thickness(20);

                    border.Child = spCountry;

                    InputFormGrid.Children.Add(border);
                }
            }
        }
        private void ShowInputForm(object sender, RoutedEventArgs e)
        {
            InputFormGrid.IsVisible = true;
            WritePanel.IsVisible = false;
        }
    }
}