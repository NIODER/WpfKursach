using ClinicWpf.ViewModel;
using ClinicWpf.ViewModel.AddVisitViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicWpf.Model
{
    internal class Locator
    {
        private readonly ClinicModel model = new();

        public LoginViewModel LoginVM { get; }
        public RegistrationViewModel RegistrationViewModelRegistrationVM { get; }
        public MainViewModel MainVM { get; }
        public VisitViewModel VisitViewModel { get; }
        public AddVisitViewModel AddVisitViewModel { get; }

        public Locator()
        {
            LoginVM = new LoginViewModel(model);
            RegistrationViewModelRegistrationVM = new RegistrationViewModel(model);
            MainVM = new MainViewModel(model);
            VisitViewModel = new VisitViewModel(model);
            AddVisitViewModel = new AddVisitViewModel(model);
        }

    }
}
