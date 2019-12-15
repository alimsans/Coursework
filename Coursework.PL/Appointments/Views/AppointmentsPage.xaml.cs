using Coursework.PL.Appointments.Models;
using Coursework.PL.Appointments.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Coursework.PL.Appointments.Views
{
    /// <summary>
    /// Interaction logic for AppointmentsPage.xaml
    /// </summary>
    public partial class AppointmentsPage : Page
    {
        private AppointmentsViewModel _appointmentsViewModel;
        public AppointmentsPage()
        {
            InitializeComponent();

            _appointmentsViewModel = new AppointmentsViewModel();
            this.Appointments_DataGrid.ItemsSource = _appointmentsViewModel.AppointmentModels;
            _appointmentsViewModel.UpdateAppointmentsAsync().ConfigureAwait(false);
        }

        private void Appointments_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            this._appointmentsViewModel.SelectedAppointment = dataGrid.SelectedItem as AppointmentModel;
        }

        private async void AddAppointment_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            AddAppointmentView addAppointmentView = new AddAppointmentView();
            addAppointmentView.ShowDialog();

            if (!addAppointmentView.IsConfirmed)
                return;

            try
            {
                await _appointmentsViewModel.AddAppointmentAsync
                    (addAppointmentView.DoctorId, addAppointmentView.PatientId, addAppointmentView.DateTime);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void RemoveAppointment_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_appointmentsViewModel.SelectedAppointment == null)
                return;

            await _appointmentsViewModel.RemoveAppointmentAsync(_appointmentsViewModel.SelectedAppointment);
        }

        private async void RefreshAppointments_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            await _appointmentsViewModel.UpdateAppointmentsAsync();
        }

    }
}
