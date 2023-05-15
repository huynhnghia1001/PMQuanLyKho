﻿using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList;set { _TonKhoList = value; OnPropertyChanged(); } }
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserWindowCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        

        public MainViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if(loginWindow.DataContext == null) 
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin) {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => {UnitWindow unitWindow = new UnitWindow();unitWindow.ShowDialog();});

            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow suplierWindow = new SuplierWindow(); suplierWindow.ShowDialog(); });

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow customerWindow = new CustomerWindow(); customerWindow.ShowDialog(); });

            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow objectWindow = new ObjectWindow(); objectWindow.ShowDialog(); });

            UserWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow userWindow = new UserWindow(); userWindow.ShowDialog(); });

            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow inputWindow = new InputWindow(); inputWindow.ShowDialog(); });

            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow outputWindow = new OutputWindow(); outputWindow.ShowDialog(); });


            //MessageBox.Show(DataProvider.Ins.DB.Users.First().DisplayName);
        }

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var objectList = DataProvider.Ins.DB.Objects;
            int i = 1;
            foreach(var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if(inputList != null && inputList.Count()>0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonKho = new TonKho();
                tonKho.STT = i;
                tonKho.Count = sumInput - sumOutput;
                tonKho.Object = item;

                TonKhoList.Add(tonKho);

                i++;
            }
            
        }
    }
}