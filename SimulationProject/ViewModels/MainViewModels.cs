using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SimulationProject.Models;
using SimulationProject.Views;

namespace SimulationProject.ViewModels
{
    class MainViewModels : BaseViewModel
    {
        private int p;
        public int P
        {
            get { return p; }
            set { SetProperty(ref p, value); }
        }

        private int a;
        public int A
        {
            get { return a; }
            set { SetProperty(ref a, value); }
        }

        private int m;
        public int M
        {
            get { return m; }
            set { SetProperty(ref m, value); }
        }

        private int n;
        public int N
        {
            get { return n; }
            set { SetProperty(ref n, value); }
        }

        private int x0;
        public int X0
        {
            get { return x0; }
            set { SetProperty(ref x0, value); }
        }

        int[] primeNumber { get; } = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                                      73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173,
                                      179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
                                      283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409,
                                      419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541,
                                      547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
                                      661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809,
                                      811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
                                      947, 953, 967, 971, 977, 983, 991, 997 };
        ResponseWindow responseWindow { get; set; }
        public ICommand ChangeValuesCommand { get; set; }
        public ICommand CalculateValuesCommand { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public MainViewModels()
        {
            Items = new ObservableCollection<Item>();

            ChangeValuesCommand = new RelayCommand(() => ExecuteChangeValuesCommand());
            CalculateValuesCommand = new RelayCommand(() => ExecuteCalculateValuesCommand());
        }

        void ExecuteChangeValuesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var random = new Random();

            A = (8* random.Next(1,124)) + (random.Next(0, 1) * 6) - 3;

            M = (int)Math.Pow(2,random.Next(3,9));

            do
            {
                X0 = primeNumber[random.Next(0, primeNumber.Length - 1)];
            } while (mcd(X0, M) != 1);

            IsBusy = false;
        }

        void ExecuteCalculateValuesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            int xn = X0;

            Items.Clear();

            for (int i = 0; i < P+(2*P*5); i++)
            {
                var item = new Item();

                item.n = i;
                item.Xn = xn;
                item.Yn = (A * xn);
                item.Xn1 = xn = (A * xn) % M;
                item.Rn1 = (double)xn / M;

                Items.Add(item);
            }

            responseWindow = new ResponseWindow(P, Items.Select(x => x.Rn1).ToList() );
            responseWindow.Show();

            IsBusy = false;
        }

        int mcd(int x,int y)
        {
            if (x < y)
                return mcd(y, x);
            else if (y == 0)
                return x;
            else
                return mcd(y, x % y);
        }
    }
}
