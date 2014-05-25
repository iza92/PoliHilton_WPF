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
        Booking booking;
        int[] occupiedRooms=new int[100];
        String[] occupiedUntil = new String[100];
        String[] guests = new String[100];
        int[] discounts = new int[300];
        int selectedRoom, i=0;
        Button selected;

        public Form6(Users u1, Database db1)
        {
            this.db1 = db1;
            InitializeComponent();    
            this.u1 = u1;
            form6_label_user.Content = u1.username;
            long today = DateTime.Now.Ticks;
            arrivalDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(today)));
            departureDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(today)));
            booking = new Booking(db1, this, u1);
            selected = new Button();
        }
        private void DragForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            db1.Close();
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void arrivalDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selection_label.Content = "";
            departureDate.SelectedDate = null;
            long lastBlackOut = arrivalDate.SelectedDate.Value.Ticks;
            departureDate.SelectedDate = null;
            departureDate.BlackoutDates.Clear();
            departureDate.BlackoutDates.Add(new CalendarDateRange(new DateTime(2010, 1, 1), new DateTime(lastBlackOut)));
        }

        private void departureDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selection_label.Content="";
            occupiedRooms = null;
            color_legend.Visibility = Visibility.Visible;
            if(departureDate.SelectedDate!=null)
             occupiedRooms = booking.getStatus(arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, occupiedUntil, guests);
        }

        private void Button_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            // ... Set ToolTip on Button before it is shown.
            Button room = sender as Button;
            char[] roomName = room.Name.ToCharArray();
            char[] roomNumber = { roomName[6], roomName[7], roomName[8] };
            String roomNo = new String(roomNumber);
            if(arrivalDate.SelectedDate!=null && departureDate.SelectedDate!=null)
               room.ToolTip = "Status of room " + roomNo + ": " + booking.toolTipText(arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, Convert.ToInt32(roomNo), occupiedRooms, occupiedUntil, guests);
            else room.ToolTip = "Room " + roomNo + "\n" + booking.toolTipText(Convert.ToInt32(roomNo));

        }

        Button clickedRoom;
        private void form6_roomBtn_Click(object sender, RoutedEventArgs e)
        {
            clickedRoom = sender as Button;       
            if (arrivalDate.SelectedDate != null && departureDate.SelectedDate != null)
            {
                
                Button room = sender as Button;
                char[] roomName = room.Name.ToCharArray();
                char[] roomNumber = { roomName[6], roomName[7], roomName[8] };
                String roomNo = new String(roomNumber);
                selectedRoom = int.Parse(roomNo);
                if (occupiedRooms.Contains(selectedRoom))
                {
                    form6_button_bookNow.Visibility = Visibility.Hidden;
                    selection_label.Content = "Room " + roomNo + " is \ncurrently occupied";
                }
                else
                {   selection_label.Content = "You have selected\n   room " + roomNo;
                    form6_button_bookNow.Visibility = Visibility.Visible;
                    String name = "button" + roomNo;
                    object item = LayoutRoot.FindName(name);
                    if (item is Button)
                    {
                        if (i != 0)
                        { selected.ClearValue(Button.BackgroundProperty); }
                        if (selected != room)
                        {
                            occupiedRooms = booking.getStatus(arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, occupiedUntil, guests);

                            room.Background = new SolidColorBrush(Colors.Green);
                            //path112.Fill = new SolidColorBrush(Colors.AliceBlue);
                            room.Opacity = 0.4;

                        }
                        else { selection_label.Content = "";
                               form6_button_bookNow.Visibility = Visibility.Hidden;
                               occupiedRooms = booking.getStatus(arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, occupiedUntil, guests);

                        }
                        selected = room;
                        i++;
                       
                    }
                }
            }
            else selection_label.Content = "Please select arrival and\n   departure dates";
        }

        private void form6_button_bookNow_Click(object sender, RoutedEventArgs e)
        {   char[] roomName = clickedRoom.Name.ToCharArray();
             char[] roomNumber = { roomName[6], roomName[7], roomName[8] };
             String roomNo = new String(roomNumber);
             Form8 f8 = new Form8(db1, u1, selectedRoom, arrivalDate.SelectedDate.Value, departureDate.SelectedDate.Value, booking.getRoomData(Int32.Parse(roomNo)));
        }


       
    }
}
