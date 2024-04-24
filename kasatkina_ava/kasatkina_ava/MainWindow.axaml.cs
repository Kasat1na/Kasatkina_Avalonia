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
        public string name; // ���
        public string surname; // �������
        public string otvestvo; // ��������
        public string pol; // ���
        public string phone; // �������
        public int dayr; // ���� �
        public string monther; // ����� �
        public int yearr; // ��� �

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
            if (rbM.IsChecked == true) person.pol = "�������";
            if (rbZH.IsChecked == true) person.pol = "�������";

            switch (cbContinent.SelectedIndex)
            {
                case 1:
                    person.monther = "������";
                    break;
                case 2:
                    person.monther = "�������";
                    break;
                case 3:
                    person.monther = "����";
                    break;
                case 4:
                    person.monther = "������";
                    break;
                case 5:
                    person.monther = "���";
                    break;
                case 6:
                    person.monther = "����";
                    break;
                case 7:
                    person.monther = "����";
                    break;
                case 8:
                    person.monther = "������";
                    break;
                case 9:
                    person.monther = "��������";
                    break;
                case 10:
                    person.monther = "�������";
                    break;
                case 11:
                    person.monther = "������";
                    break;
                case 12:
                    person.monther = "�������";
                    break;
            }


            if (!ValidateName(person.name, "���") || !ValidateName(person.surname, "�������") ||
            !ValidatePhone(person.phone) || !ValidateDate(person.dayr, person.monther, person.yearr))
            {
                return;
            }
            // ��������� ��� ������ � ����� ����������������� ���������
            DateTime birthDate = new DateTime(person.yearr, MonthNumber(person.monther), person.dayr);
            string dayOfWeek = birthDate.ToString("dddd", new CultureInfo("ru-RU"));
            string slavicSign = Goroskop(birthDate);

            try
            {
                using (StreamWriter sw = new StreamWriter("users.csv", true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(person.name + ";" + person.surname + ";" + person.otvestvo + ";" + person.pol + ";" + person.phone + ";" + person.dayr + ";" + person.monther + ";" + person.yearr + ";" + dayOfWeek + ";" + slavicSign);
                    ShowMessage.Text = "������ ���������";
                }


            }
            catch (Exception ex)
            {
                ShowMessage.Text = $"������ ��� ������ � ����: {ex.Message}";
            }
        }
        private bool ValidateDate(int day, string monthStr, int year)
        {
            if (day < 1 || day > 31 || year > DateTime.Now.Year)
            {
                ShowMessage.Text = "������� ���������� ���� ��������.";
                return false;
            }
            return true;
        }
        private int MonthNumber(string monthName)
        {
            switch (monthName)
            {
                case "������":
                    return 1;
                case "�������":
                    return 2;
                case "����":
                    return 3;
                case "������":
                    return 4;
                case "���":
                    return 5;
                case "����":
                    return 6;
                case "����":
                    return 7;
                case "������":
                    return 8;
                case "��������":
                    return 9;
                case "�������":
                    return 10;
                case "������":
                    return 11;
                case "�������":
                    return 12;
                default:
                    return -1;
            }

        }
        // ���������������� ��������
        private string Goroskop(DateTime birthDate)
        {
            if (birthDate.Month == 1 && birthDate.Day <= 30 || birthDate.Month == 12 && birthDate.Day >= 24)
            {
                return "�����";
            }
            if (birthDate.Month == 2 && birthDate.Day <= 28 || birthDate.Month == 1 && birthDate.Day >= 31)
            {
                return "�����";
            }
            if (birthDate.Month == 3)
            {
                return "������";
            }
            if (birthDate.Month == 4)
            {
                return "����";
            }
            if (birthDate.Month == 5 && birthDate.Day <= 14 || birthDate.Month == 5 && birthDate.Day >= 1)
            {
                return "�����";
            }
            if (birthDate.Month == 6 && birthDate.Day <= 2 || birthDate.Month == 5 && birthDate.Day >= 15)
            {
                return "����";
            }
            if (birthDate.Month == 6 && birthDate.Day <= 12 || birthDate.Month == 6 && birthDate.Day >= 2)
            {
                return "��������";
            }
            if (birthDate.Month == 7 && birthDate.Day <= 6 || birthDate.Month == 6 && birthDate.Day >= 13)
            {
                return "������";
            }
            if (birthDate.Month == 6 && birthDate.Day >= 24)
            {
                return "���� ������";
            }
            if (birthDate.Month == 7 && birthDate.Day <= 31 || birthDate.Month == 7 && birthDate.Day >= 7)
            {
                return "����";
            }
            if (birthDate.Month == 8 && birthDate.Day <= 28 || birthDate.Month == 8 && birthDate.Day >= 1)
            {
                return "�����";
            }
            if (birthDate.Month == 9 && birthDate.Day <= 13 || birthDate.Month == 8 && birthDate.Day >= 29)
            {
                return "����";
            }
            if (birthDate.Month == 9 && birthDate.Day <= 27 || birthDate.Month == 9 && birthDate.Day >= 14)
            {
                return "��������";
            }
            if (birthDate.Month == 10 && birthDate.Day <= 15 || birthDate.Month == 9 && birthDate.Day >= 28)
            {
                return "���������";
            }
            if (birthDate.Month == 11 && birthDate.Day <= 8 || birthDate.Month == 10 && birthDate.Day >= 16)
            {
                return "������";
            }
            if (birthDate.Month == 11 && birthDate.Day <= 28 || birthDate.Month == 11 && birthDate.Day >= 9)
            {
                return "����";
            }
            if (birthDate.Month == 12 && birthDate.Day <= 23 || birthDate.Month == 11 && birthDate.Day >= 29)
            {
                return "�������";
            }
            return "����������� ����";


        }
        private bool ValidateName(string name, string fieldName)
        {
            if (!Regex.IsMatch(name, "^[�-ߨ][�-��]+$") ||
                (fieldName == "���" && name.Length < 2) ||
                (fieldName == "�������" && name.Length < 3))
            {
                ShowMessage.Text = $"{fieldName} ������ ���������� � ��������� ����� � ��������� ������ ������� �����. " +
                                           $"����������� ����� ��� ����� - 2 �������, ��� ������� - 3 �������.";
                return false;
            }
            return true;
        }
        public bool ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, @"^8\([0-9]{3}\)[0-9]{3}-[0-9]{2}-[0-9]{2}$"))
            {
                ShowMessage.Text = "����� �������� ������ ���� � ������� 8(XXX)XXX-XX-XX";
                return false;
            }
            return true;
        }
        public SolidColorBrush color(string pol)
        {
            switch (pol)
            {
                case "�������":
                    return new SolidColorBrush(Color.FromArgb(51, 187, 0, 255));
                case "�������":
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
                        Text = "���: " + lineArray[0],
                    };
                    TextBlock tbSurname = new TextBlock()
                    {
                        Text = "�������: " + lineArray[1]
                    };
                    TextBlock tbotchestvo = new TextBlock()
                    {
                        Text = "��������: " + lineArray[2]
                    };
                    TextBlock tbphone = new TextBlock()
                    {
                        Text = "�������: " + lineArray[3]
                    };
                    TextBlock tbday = new TextBlock()
                    {
                        Text = "����: " + lineArray[4]
                    };
                    TextBlock tbyear = new TextBlock()
                    {
                        Text = "���: " + lineArray[5]
                    };
                    TextBlock cbContinent = new TextBlock()
                    {
                        Text = "�����: " + lineArray[6]
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