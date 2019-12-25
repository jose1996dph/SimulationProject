using SimulationProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace SimulationProject.ViewModels
{
    class ResponseViewModel : BaseViewModel
    {

        private int totalCar;
        public int TotalCar
        {
            get { return totalCar; }
            set { SetProperty(ref totalCar, value); }
        }

        private int totalTruck;
        public int TotalTruck
        {
            get { return totalTruck; }
            set { SetProperty(ref totalTruck, value); }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }

        int p;
        List<double> items;
        public List<CumulativeFrequency> Table1 { get; set; }
        public List<CumulativeFrequency> Table2 { get; set; }
        public List<CumulativeFrequency> Table3 { get; set; }
        public List<CumulativeFrequency> Table4 { get; set; }
        public ObservableCollection<Response> TableResponse1 { get; set; }
        public ObservableCollection<Response> TableResponse2 { get; set; }
        public ObservableCollection<Response> TableResponse3 { get; set; }
        public ObservableCollection<Response> TableResponse4 { get; set; }
        public ICommand LoadItemsCommad { get; set; }

        public ResponseViewModel(int p, List<double> items)
        {
            this.p = p;

            this.items = items;

            Table1 = new List<CumulativeFrequency>()
            {
                new CumulativeFrequency{ Description = "Carro"    , value = 0, fi = 0.592, Fi = 0.592, Min = 0.000, Max = 0.591 },
                new CumulativeFrequency{ Description = "Camioneta", value = 0, fi = 0.408, Fi = 1.000, Min = 0.592, Max = 0.999 },
            };
            Table2 = new List<CumulativeFrequency>()
            {
                new CumulativeFrequency{ Description = "1", value = 05800, fi = 0.326, Fi = 0.326, Min = 0.000, Max = 0.325 },
                new CumulativeFrequency{ Description = "2", value = 07300, fi = 0.271, Fi = 0.597, Min = 0.326, Max = 0.596 },
                new CumulativeFrequency{ Description = "3", value = 10000, fi = 0.220, Fi = 0.817, Min = 0.597, Max = 0.816 },
                new CumulativeFrequency{ Description = "4", value = 12000, fi = 0.183, Fi = 1.000, Min = 0.817, Max = 0.999 }
            };
            Table3 = new List<CumulativeFrequency>()
            {
                new CumulativeFrequency{ Description = "1", value = 08100, fi = 0.263, Fi = 0.263, Min = 0.000, Max = 0.262 },
                new CumulativeFrequency{ Description = "2", value = 11500, fi = 0.398, Fi = 0.661, Min = 0.263, Max = 0.660 },
                new CumulativeFrequency{ Description = "3", value = 13700, fi = 0.339, Fi = 1.000, Min = 0.661, Max = 0.999 }
            };
            Table4 = new List<CumulativeFrequency>()
            {
                new CumulativeFrequency{ Description = "0", value = 0, fi = 0.068, Fi = 0.068, Min = 0.000, Max = 0.067 },
                new CumulativeFrequency{ Description = "1", value = 1, fi = 0.125, Fi = 0.193, Min = 0.068, Max = 0.192 },
                new CumulativeFrequency{ Description = "2", value = 2, fi = 0.363, Fi = 0.556, Min = 0.193, Max = 0.555 },
                new CumulativeFrequency{ Description = "3", value = 3, fi = 0.251, Fi = 0.807, Min = 0.556, Max = 0.806 },
                new CumulativeFrequency{ Description = "4", value = 4, fi = 0.140, Fi = 0.947, Min = 0.807, Max = 0.946 },
                new CumulativeFrequency{ Description = "5", value = 5, fi = 0.053, Fi = 1.000, Min = 0.947, Max = 0.999 },
            };

            TableResponse1 = new ObservableCollection<Response>();
            TableResponse2 = new ObservableCollection<Response>();
            TableResponse3 = new ObservableCollection<Response>();
            TableResponse4 = new ObservableCollection<Response>();

            LoadItemsCommad = new RelayCommand(() => ExecuteLoadItemsCommad());
        }

        void ExecuteLoadItemsCommad()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            TableResponse1.Clear();
            TableResponse2.Clear();
            TableResponse3.Clear();
            TableResponse4.Clear();

            for (int i = 0; i < p; i++)
            {
                foreach (var row in Table4)
                {
                    if (items[i] >= row.Min && items[i] <= row.Max)
                    {
                        TableResponse1.Add(new Response
                        { 
                            n = i,
                            Rn1 = items[i],
                            Value = row.value,
                            Description = row.Description,
                        });
                        break;
                    }
                }
            }

            int j = p;

            foreach (var response in TableResponse1)
            {
                for (j = p; j < p + response.Value; j++)
                {
                    foreach (var row in Table1)
                    {
                        if (items[j] >= row.Min && items[j] <= row.Max)
                        {
                            TableResponse2.Add(new Response
                            {
                                n = response.n,
                                Rn1 = items[j],
                                Value = row.value,
                                Description = row.Description,
                            });
                            break;
                        }
                    }
                }
                p = j;
            }

            Total = 0;

            foreach (var response in TableResponse2)
            {
                switch (response.Description)
                {
                    case "Carro":
                        foreach (var row in Table2)
                        {
                            if (items[j] >= row.Min && items[j] <= row.Max)
                            {
                                TableResponse3.Add(new Response
                                {
                                    n = response.n,
                                    Rn1 = items[j],
                                    Value = row.value,
                                    Description = string.Concat(row.Description, " ", response.Description),
                                });
                                Total += row.value;
                                TotalCar++;
                                break;
                            }
                        }
                        break;
                    case "Camioneta":
                        foreach (var row in Table3)
                        {
                            if (items[j] >= row.Min && items[j] <= row.Max)
                            {
                                TableResponse3.Add(new Response
                                {
                                    n = response.n,
                                    Rn1 = items[j],
                                    Value = row.value,
                                    Description = string.Concat(row.Description, " ", response.Description),
                                });
                                Total += row.value;
                                TotalTruck++;
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
                j++;
            }

            IsBusy = false;
        }
    }
}
