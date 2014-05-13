using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PoliHilton_Reloaded
{
    /// <summary>
    /// Interaction logic for Form6.xaml
    /// </summary>
    public partial class Form6 : Window
    {
        Users u1;
        Database db1;

        public Form6(Users u1, Database db1)
        {
            this.db1 = db1;
            InitializeComponent();    
            this.u1 = u1;
            long today = DateTime.Now.Ticks;
            arrivalDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(today)));
            departureDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(today)));
        }
        private void DragForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void arrivalDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            departureDate.SelectedDate = null;
            long lastBlackOut = arrivalDate.SelectedDate.Value.Ticks;
            departureDate.BlackoutDates.Clear();
            departureDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(lastBlackOut)));
        }
        private void Button_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            // ... Set ToolTip on Button before it is shown.
            Button room = sender as Button;
            char[] roomName = room.Name.ToCharArray();
            char[] roomNumber = { roomName[6], roomName[7], roomName[8] };
            String roomNo = new String(roomNumber);
            room.ToolTip = "Status of room " + roomNo + ": \n";

        }
        private void go_button_click(object sender, RoutedEventArgs e)
        {
            Database database = new Database();
            Booking booking = new Booking(database, this);
            booking.getStatus(arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, label1);
        }


       
    }
}
