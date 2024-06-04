using BusStation.DataAccess;
using BusStation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusStation.Forms
{
    public partial class AdminFrom : Form
    {
        public AdminFrom(User user = null)
        {
            currentUser = user;
            if (user == null)
            {
                currentUser = new User(0, "1", "1", "Ivan", "Stefanyshyn", DateTime.Now);
            }
            InitializeComponent();
            accessUser(currentUser);

            EditTabControl.Visible = true;
            EditTabControl.Dock = DockStyle.Fill;
            BookPanel.Visible = false;
            Ticketpanel.Visible = false;
            ProfileAdminTabControl.Visible = false;

            StopDateTimePicker.Format = DateTimePickerFormat.Custom;
            StopDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";

            TripDateStartEditTimePicker.Format = DateTimePickerFormat.Custom;
            TripDateStartEditTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";

            TripDateEndEditDateTimePicker.Format = DateTimePickerFormat.Custom;
            TripDateEndEditDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        private const int ADD_STATION_HEIGHT = 54;
        private const int ADD_BUS_HEIGHT = 54;
        private const int ADD_USER_HEIGHT = 89;
        private const int ADD_TRIP_HEIGHT = 118;

        private string userSearchEditString = "";
        private string stationSearchString = "";
        private string busSearchString = "";
        private string tripSearchString = "";
        private string stopSearchString = "";
        private User currentUser = null;
        private TableLayoutPanel currentDocument;

        private void accessUser(User user)
        {
            isAuthorUser(user);
            isNotAuthorUser(user);
        }
        private void isAuthorUser(User user)
        {
            if (user != null && user.Id != 0)
                this.EditButton.Visible = false;
        }
        private void isNotAuthorUser(User user)
        {
            if (user == null)
            {
                this.EditButton.Visible = false;
                this.ProfileButton.Visible = false;
                this.LogOutButton.Text = "Sign in";
            }
        }
        private DataGridView EditStyleColumn(DataGridView grid)
        {
            if (grid.Columns.Count > 0 && !(grid.Columns[0] is DataGridViewCheckBoxColumn))
                grid.Columns.Insert(0, new DataGridViewCheckBoxColumn());

            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            grid.Columns[0].HeaderText = "Active to edit";
            grid.Columns[0].Width = 145;

            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            grid.Columns[1].Width = 50;
            grid.Columns[1].ReadOnly = true;

            for (int i = 1; i < grid.Columns.Count; ++i)
                grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            return grid;
        }

        private void StationAddSwitcher_Click(object sender, EventArgs e)
        {
            var a = tableLayoutPanel11.RowStyles;
            if (a[1].Height == 0)
                a[1].Height = ADD_STATION_HEIGHT;
            else
                a[1].Height = 0;
        }

        private void UserSwitcherButton_Click(object sender, EventArgs e)
        {
            var a = tableLayoutPanel10.RowStyles;
            if (a[1].Height == 0)
                a[1].Height = ADD_USER_HEIGHT;
            else
                a[1].Height = 0;
        }

        private void BusSwitcherButton_Click(object sender, EventArgs e)
        {
            var a = tableLayoutPanel23.RowStyles;
            if (a[1].Height == 0)
                a[1].Height = ADD_USER_HEIGHT;
            else
                a[1].Height = 0;
        }

        private void TripSwitcherButton_Click(object sender, EventArgs e)
        {
            var a = tableLayoutPanel28.RowStyles;
            if (a[1].Height == 0)
                a[1].Height = ADD_TRIP_HEIGHT;
            else
                a[1].Height = 0;
        }

        private void DocumentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = DocumentComboBox.Text;
            //is current document
            TableLayoutPanel temp = null;
            //hide prev document
            if (currentDocument != null) currentDocument.Visible = false;

            if (s == "Student")
            {
                StudentTableLayoutPanel42.Visible = true;
                temp = StudentTableLayoutPanel42;
            }
            else if (s == "Invalid")
            {
                InvalidTableLayoutPanel43.Visible = true;
                temp = InvalidTableLayoutPanel43;
            }
            else if (s == "None")
            {
                temp = null;
            }

            if (temp != null && temp != currentDocument)
            {
                currentDocument = temp;
            }
        }

        private void TripSearchButton_Click(object sender, EventArgs e)
        {
            tripSearchString = TripEditSearchTextBox.Text;
            List<Trip> trips = new List<Trip>();
            TripAccess db = new TripAccess();
            string from = FromTripTextBox.Text.Trim();
            string to = ToTripTextBox.Text.Trim();
            DateTime dateSearch = dateTimePicker1.Value;
            try
            {
                trips = db.SearchByStation(from, to, dateSearch);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            const int h = 100;
            const int widthSecondColumn = 160;
            const int widthIntervalSecondColumn = 140;
            TripSearchPanel.Controls.Clear();

            for (int i = 0; i < trips.Count; ++i)
            {
                Panel TripPanel = this.InitTicketPanel();
                TripPanel.Location = new Point(5, i == 0 ? 5 : h * i + 5 * (i + 1));

                Label idl = new Label();
                idl.Text = "Id";
                idl.AutoSize = true;
                idl.Size = new Size(50, 25);
                idl.Location = new Point(5, 5);

                Label id = new Label();
                id.Text = Convert.ToString(trips[i].Id);
                id.AutoSize = true;
                id.Size = new Size(100, 25);
                id.Location = new Point(60, 5);

                Label labelF = new Label();
                labelF.Text = "From";
                labelF.AutoSize = true;
                labelF.Size = new Size(50, 25);
                labelF.Location = new Point(5, 37);

                Label stationF = new Label();
                stationF.Text = trips[i].getStation()[0].name;
                stationF.AutoSize = true;
                stationF.Size = new Size(100, 25);
                stationF.Location = new Point(60, 37);

                Label labelT = new Label();
                labelT.Text = "To";
                labelT.AutoSize = true;
                labelT.Size = new Size(50, 25);
                labelT.Location = new Point(5, TripPanel.Height - 30);

                Label stationT = new Label();
                stationT.Text = trips[i].getStation()[trips[i].getStation().Count - 1].name;
                stationT.AutoSize = true;
                stationT.Size = new Size(100, 25);
                stationT.Location = new Point(60, TripPanel.Height - 30);

                Label dateDeparture = new Label();
                dateDeparture.Text = "Date departure";
                dateDeparture.AutoSize = true;
                dateDeparture.Size = new Size(50, 25);
                dateDeparture.Location = new Point(widthSecondColumn, 5);

                Label date = new Label();
                date.Text = trips[i].DateDeparture.ToString();
                date.AutoSize = true;
                date.Size = new Size(100, 25);
                date.Location = new Point(widthSecondColumn + widthIntervalSecondColumn, 5);

                Label dateArrivalLabel = new Label();
                dateArrivalLabel.Text = "Date arrival";
                dateArrivalLabel.AutoSize = true;
                dateArrivalLabel.Size = new Size(50, 25);
                dateArrivalLabel.Location = new Point(widthSecondColumn, 37);

                Label dateArrival = new Label();
                dateArrival.Text = trips[i].DateArrival.ToString();
                dateArrival.AutoSize = true;
                dateArrival.Size = new Size(100, 25);
                dateArrival.Location = new Point(widthSecondColumn + widthIntervalSecondColumn, 37);

                Label seatLabel = new Label();
                seatLabel.Text = "Seats";
                seatLabel.AutoSize = true;
                seatLabel.Size = new Size(50, 25);
                seatLabel.Location = new Point(widthSecondColumn, TripPanel.Height - 30);

                Label seat = new Label();
                seat.Text = Convert.ToString(trips[i].Bus.Seats);
                seat.AutoSize = true;
                seat.Size = new Size(100, 25);
                seat.Location = new Point(widthSecondColumn + widthIntervalSecondColumn, TripPanel.Height - 30);

                TripPanel.Controls.Add(idl);
                TripPanel.Controls.Add(id);
                TripPanel.Controls.Add(labelF);
                TripPanel.Controls.Add(stationF);
                TripPanel.Controls.Add(labelT);
                TripPanel.Controls.Add(stationT);
                TripPanel.Controls.Add(dateDeparture);
                TripPanel.Controls.Add(date);
                TripPanel.Controls.Add(dateArrivalLabel);
                TripPanel.Controls.Add(dateArrival);
                TripPanel.Controls.Add(seatLabel);
                TripPanel.Controls.Add(seat);
                

                TripPanel.MouseClick += new MouseEventHandler(this.Trip_Click);

                TripSearchPanel.Controls.Add(TripPanel);
            }
        }
        private Panel InitTicketPanel()
        {
            Panel panel = new Panel();
            panel.BackColor = Color.LightGray;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Width = 600;
            panel.Height = 100;
            panel.BorderStyle = BorderStyle.FixedSingle;
            return panel;
        }
        private void Document_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox doc = null;
            try
            {
                doc = (ComboBox)selectTicket.Controls[5];
            }
            catch (Exception ex)
            {
                return;
            }
            if (doc.Text == "Student")
            {
                TextBox t = (TextBox)selectTicket.Controls[7];
                t.Enabled = true;
            }
            else if (doc.Text == "Invalid")
            {
                ComboBox c = (ComboBox)selectTicket.Controls[9];
                c.Enabled = true;
                TextBox t = (TextBox)selectTicket.Controls[7];
                t.Enabled = true;
            }
            else
            {
                ComboBox c = (ComboBox)selectTicket.Controls[9];
                c.Enabled = false;
                TextBox t = (TextBox)selectTicket.Controls[7];
                t.Enabled = false;
            }
            return;
        }
        private void Trip_Click(object sender, EventArgs e)
        {
            selectTicket.Dock = DockStyle.Fill;
            selectTicket.BackColor = Color.LightGray;

            var data = ((Panel)sender).Controls;
            string id = ((Label)data[1]).Text;
            string stationFrom = ((Label)data[3]).Text;
            string stationTo = ((Label)data[5]).Text;

            TripAccess dbTrip = new TripAccess();
            var trip = dbTrip.GetOne(Convert.ToInt32((id)));

            Label id_trip = new Label();
            id_trip.Text = id;
            id_trip.Visible = false;

            Label titleLabel = new Label();
            titleLabel.Text = "#" + id + " -|- " + stationFrom + " <===> " + stationTo + " -|- ";
            titleLabel.AutoSize = true;
            titleLabel.Height = 25;
            titleLabel.Location = new Point(5, 5);

            Label seatLabel = new Label();
            seatLabel.Text = "Seat";
            seatLabel.AutoSize = true;
            seatLabel.Size = new Size(50, 25);
            seatLabel.Location = new Point(5, 35);

            ComboBox seats = new ComboBox();
            seats.DropDownStyle = ComboBoxStyle.DropDownList;
            seats.Size = new Size(150, 25);
            seats.Location = new Point(5, 65);
            seats.Click += new EventHandler(this.SeatCombox_Click);


            Label documentLabel = new Label();
            documentLabel.Text = "Document";
            documentLabel.AutoSize = true;
            documentLabel.Size = new Size(50, 25);
            documentLabel.Location = new Point(5, 100);

            ComboBox document = new ComboBox();
            document.DropDownStyle = ComboBoxStyle.DropDownList;
            document.Items.Add("None");
            document.Items.Add("Student");
            document.Items.Add("Invalid");
            document.Size = new Size(150, 25);
            document.Location = new Point(5, 130);
            document.SelectedValueChanged += new EventHandler(this.Document_SelectedValueChanged);
            
            Label documentSeriesLabel = new Label();
            documentSeriesLabel.Text = "Series";
            documentSeriesLabel.AutoSize = true;
            documentSeriesLabel.Size = new Size(50, 25);
            documentSeriesLabel.Location = new Point(5, 165);

            TextBox series = new TextBox();
            series.AutoSize = true;
            series.Size = new Size(130, 25);
            series.Location = new Point(5, 195);
            series.Enabled = false;

            Label documentInvalidLabel = new Label();
            documentInvalidLabel.Text = "Invalid";
            documentInvalidLabel.AutoSize = true;
            documentInvalidLabel.Size = new Size(50, 25);
            documentInvalidLabel.Location = new Point(140, 165);

            ComboBox invalid = new ComboBox();
            invalid.DropDownStyle = ComboBoxStyle.DropDownList;
            invalid.Items.Add("Invalid1");
            invalid.Items.Add("Invalid2");
            invalid.Items.Add("Invalid3");
            invalid.Items.Add("Invalid4");
            invalid.Size = new Size(140, 25);
            invalid.Location = new Point(140, 195);
            invalid.Enabled = false;

            if (currentUser != null && currentUser.Document != null)
            {
                document.Text = currentUser.Document.Type;
                if (document.Text == "Student")
                {
                    series.Enabled = true;
                    invalid.Enabled = false;
                    series.Text = currentUser.Document.Number;
                }
                else if (document.Text == "Invalid")
                {
                    series.Enabled = true;
                    invalid.Enabled = true;
                    series.Text = currentUser.Document.Number;
                    invalid.Text = currentUser.Document.Degree;
                }
                else
                {
                    series.Enabled = false;
                    invalid.Enabled = false;
                }
            }

            Label firstNameLabel = new Label();
            firstNameLabel.Text = "FirstName";
            firstNameLabel.AutoSize = true;
            firstNameLabel.Size = new Size(50, 25);
            firstNameLabel.Location = new Point(5, 230);

            TextBox firstName = new TextBox();
            firstName.AutoSize = true;
            firstName.Size = new Size(130, 25);
            firstName.Location = new Point(110, 230);

            Label lastNameLabel = new Label();
            lastNameLabel.Text = "LastName";
            lastNameLabel.AutoSize = true;
            lastNameLabel.Size = new Size(50, 25);
            lastNameLabel.Location = new Point(5, 265);

            TextBox lastName = new TextBox();
            lastName.AutoSize = true;
            lastName.Size = new Size(130, 25);
            lastName.Location = new Point(110, 265);

            Label costLabel = new Label();
            costLabel.Text = "Cost";
            costLabel.AutoSize = true;
            costLabel.Size = new Size(50, 25);
            costLabel.Location = new Point(5, 295);

            TripAccess db = new TripAccess();

            var costValue = db.SearchDistance(trip, FromTripTextBox.Text.Trim(), ToTripTextBox.Text.Trim()) * 2;

            Label cost = new Label();
            cost.Text = Convert.ToString(costValue*2);
            cost.AutoSize = true;
            cost.Size = new Size(50, 25);
            cost.Location = new Point(65, 295);

            Button buy = new Button();
            buy.Text = "Buy";
            buy.AutoSize = true;
            buy.BackColor = Color.LightSkyBlue;
            buy.Size = new Size(150, 50);
            buy.Location = new Point(5, 325);
            buy.MouseClick += new MouseEventHandler(this.Buy_MouseClick);

            Button cancell = new Button();
            cancell.Text = "Cancell";
            cancell.AutoSize = true;
            cancell.BackColor = Color.LightSkyBlue;
            cancell.Size = new Size(75, 50);
            cancell.Location = new Point(175, 325);
            cancell.MouseClick += new MouseEventHandler(this.Cancell_MouseClick);

            selectTicket.AutoSize = true;
            selectTicket.AutoSizeMode = AutoSizeMode.GrowOnly;

            selectTicket.Controls.Clear();
            selectTicket.Controls.Add(id_trip);
            selectTicket.Controls.Add(titleLabel);
            selectTicket.Controls.Add(seatLabel);
            selectTicket.Controls.Add(seats);
            selectTicket.Controls.Add(documentLabel);
            selectTicket.Controls.Add(document);
            selectTicket.Controls.Add(documentSeriesLabel);
            selectTicket.Controls.Add(series);
            selectTicket.Controls.Add(documentInvalidLabel);
            selectTicket.Controls.Add(invalid);
            selectTicket.Controls.Add(firstNameLabel);
            selectTicket.Controls.Add(firstName);
            selectTicket.Controls.Add(lastNameLabel);
            selectTicket.Controls.Add(lastName);
            selectTicket.Controls.Add(costLabel);
            selectTicket.Controls.Add(cost);
            selectTicket.Controls.Add(buy);
            selectTicket.Controls.Add(cancell);

            TicketSelecttableLayoutPanel46.AutoScroll = true;

            tableLayoutPanel42.Visible = false;
            TicketSelecttableLayoutPanel46.Dock = DockStyle.Fill;
            TicketSelecttableLayoutPanel46.Visible = true;
        }

        private void SeatCombox_Click(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;

            var panel = ((ComboBox)sender).Parent;

            TripAccess tripAccess = new TripAccess();
            var trip = tripAccess.GetOne(Convert.ToInt32(panel.Controls[0].Text));

            BookAccess db = new BookAccess();
            var seats = db.GetSeats(trip.Id);

            for(int i = 0; i < trip.Bus.Seats; ++i)
            {
                combo.Items.Add(i + 1);
            }
            for(int i = 0; i < seats.Count; ++i)
            {
                combo.Items.Remove(seats[i]);
            }

        }
        private void Cancell_MouseClick(object sender, EventArgs e)
        {
            TicketSelecttableLayoutPanel46.Visible = false;
            tableLayoutPanel42.Visible = true;
        }
        private void Buy_MouseClick(object sender, EventArgs e)
        {
            try
            {
                var panel = ((Button)sender).Parent;
                TripAccess tripAccess = new TripAccess();
                var trip = tripAccess.GetOne(Convert.ToInt32(panel.Controls[0].Text));
                var from = FromTripTextBox.Text;
                var to = ToTripTextBox.Text;
                
                //seat
                int seat;
                if(panel.Controls[3].Text == "")
                {
                    throw new Exception("Seat isn't check");
                }
                seat = Convert.ToInt32(panel.Controls[3].Text);

                //document
                Document document = null;
                if (panel.Controls[5].Text.Trim() != "" || panel.Controls[5].Text.Trim() != "None")
                {
                    if(panel.Controls[7].Text.Length < 8)
                    {
                        throw new Exception("Incorrect input. Series document must be more then 8 symbols");
                    }
                    string type = panel.Controls[5].Text.Trim();
                    if (type == "Student")
                    {
                        document = new Document
                        {
                            Id = -1,
                            Type = panel.Controls[5].Text,
                            Number = panel.Controls[7].Text,
                            Degree = panel.Controls[5].Text
                        };
                    }else if (type == "Invalid")
                    {
                        if (panel.Controls[9].Text.Trim() == "")
                            throw new Exception("Please check disiability");
                        document = new Document
                        {
                            Id = -1,
                            Type = panel.Controls[5].Text,
                            Number = panel.Controls[7].Text,
                            Degree = panel.Controls[9].Text
                        };
                    }
                    if(document != null)
                    {
                        DocumentAccess db = new DocumentAccess();
                        try
                        {
                            var doc_id = db.GetOne(document.Number).Id;
                            document.Id = doc_id;
                        }
                        catch (Exception ex)
                        {
                            db.Add(document);
                            var doc_id = db.GetOne(document.Number).Id;
                            document.Id = doc_id;
                        }
                    }
                }

                //firstname and lastname
                var firstname = panel.Controls[11].Text;
                var lastname = panel.Controls[13].Text;
                if (firstname.Trim() == "" || lastname.Trim() == "")
                    throw new Exception("Incorrect date! Firstname or lastname is empty");

                //cost
                var cost = tripAccess.SearchDistance(trip, from, to) * 2;

                if (panel.Controls[6].Text == "Student" || panel.Controls[6].Text == "Invalid")
                    cost *= 0.8;

                Book book = new Book(currentUser.Id, trip.Id, seat, document.Id, from, to, Convert.ToDecimal(cost), firstname, lastname);

                BookAccess dbBook = new BookAccess();
                
                dbBook.Add(book);

                TicketSelecttableLayoutPanel46.Visible = false;
                tableLayoutPanel42.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditTabControl.Dock = DockStyle.Fill;
            EditTabControl.Visible = true;
            Ticketpanel.Visible = false;
            ProfileAdminTabControl.Visible = false;
            BookPanel.Visible = false;
        }

        private void TicketButton_Click(object sender, EventArgs e)
        {
            Ticketpanel.Dock = DockStyle.Fill;
            Ticketpanel.Visible = true;
            EditTabControl.Visible = false;
            BookPanel.Visible = false;
            ProfileAdminTabControl.Visible = false;
            tableLayoutPanel42.Dock = DockStyle.Fill;
            FromTripTextBox.Text = "Kyiv";
            ToTripTextBox.Text = "Ternopil";
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            ProfileAdminTabControl.Dock = DockStyle.Fill;
            ProfileAdminTabControl.Visible = true;
            BookPanel.Visible = false; 
            EditTabControl.Visible = false;
            Ticketpanel.Visible = false;

            fillProfile(currentUser);
        }
        private void fillProfile(User user)
        {
            if (user != null)
            {
                var profile = user.getProfile();
                if (profile != null)
                {
                    FirstnameTextBox.Text = profile.FirstName;
                    LastnameTextBox.Text = profile.LastName;
                }
                UsernameTextBox.Text = user.Username;
                var document = user.Document;
                if(document != null)
                {
                    DocumentComboBox.Text = document.Type;
                    if(document.Type == "Student")
                    {
                        textBox7.Text = document.Number;
                    }else if(document.Type == "Invalid")
                    {
                        textBox3.Text = document.Number;
                        DisabilityComboBox.Text = document.Degree;
                    }
                    DocumentComboBox_SelectedIndexChanged(null, null);
                }
            }
        }
        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authorization form = new Authorization();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void UserSearchButton_Click(object sender, EventArgs e)
        {
            userSearchEditString = UserSearchTextBox.Text.Trim();
            UserAccess db = new UserAccess();
            var a = "olehhh";
            var b = a.Contains("leh");
            List<User> users = userSearchEditString == "" ?
                db.GetAll() :
                db.GetManyBySelector(user =>
                    user.getProfile().FirstName.Contains(userSearchEditString)
                    || user.getProfile().LastName.Contains(userSearchEditString)
                    || user.Username.Contains(userSearchEditString));

            DataGridView grid = UserDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = users;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void StationSearchButton_Click(object sender, EventArgs e)
        {
            stationSearchString = StationSearchTextBox.Text;

            List<Station> stations = new List<Station>();
            StationAccess db = new StationAccess();

            stations = db.GetManyBySelector(station => station.name.Contains(stationSearchString));

            DataGridView grid = StationDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = stations;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void BusSearchButton_Click(object sender, EventArgs e)
        {
            busSearchString = BusSearchTextBox.Text.Trim();
            List<Bus> buses = new List<Bus>();
            int seats = -1;
            try
            {
                seats = Convert.ToInt32(busSearchString);
            }
            catch (Exception ex)
            { }
            BusAccess db = new BusAccess();

            if (seats < 0) buses = db.GetAll();
            else buses = db.GetManyBySelector(bus => bus.Seats == seats);

            DataGridView grid = BusDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = buses;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void TripSearchEditButton_Click(object sender, EventArgs e)
        {
            List<Trip> trips = new List<Trip>();


            DataGridView grid = StopDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = trips;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void UserApplyButton_Click(object sender, EventArgs e)
        {
            var rows = UserDataGridView.Rows;

            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                {
                    int id = Convert.ToInt32(((DataGridViewTextBoxCell)(row.Cells[1])).Value);
                    string username = ((DataGridViewTextBoxCell)(row.Cells[2])).Value.ToString();
                    string password = ((DataGridViewTextBoxCell)(row.Cells[3])).Value.ToString();
                    User user = new User(id, username, password, DateTime.Now);
                    UserAccess db = new UserAccess();
                    db.UpdateUser(user);
                }
            }
        }

        private void UserDeleteButton_Click(object sender, EventArgs e)
        {
            var rows = UserDataGridView.Rows;
            List<int> indexs = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                    indexs.Add(row.Index);
            }
            for (int i = indexs.Count - 1; i >= 0; --i)
            {
                int index = indexs.ElementAt<int>(i);
                long id = Convert.ToInt64(((DataGridViewTextBoxCell)(rows[index].Cells[1])).Value);
                UserAccess db = new UserAccess();
                db.DeleteUser(id);
                rows.RemoveAt(index);
            }
        }

        private void UserRefreshButton_Click(object sender, EventArgs e)
        {
            if (userSearchEditString == null) return;
            UserAccess db = new UserAccess();
            List<User> users = userSearchEditString == "" ?
                db.GetAll() :
                db.GetManyBySelector(user =>
                    user.getProfile().FirstName.Contains(userSearchEditString)
                    || user.getProfile().LastName.Contains(userSearchEditString));

            DataGridView grid = UserDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = users;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void UserAddButton_Click(object sender, EventArgs e)
        {
            string username = UserUsernameTextBox.Text.Trim();
            string password = UserPasswordTextBox.Text.Trim();
            if (username != "" && password != "")
            {
                User user = new User(1, username, password, "", "", DateTime.Now);
                UserAccess db = new UserAccess();
                BindingSource source = (BindingSource)UserDataGridView.DataSource;

                if ((source != null && source.List.Count == 0) || source == null)
                {
                    List<User> users = new List<User>();

                    db.Add(user);
                    users.Add(user);

                    DataGridView grid = UserDataGridView;
                    grid.Rows.Clear();
                    BindingSource bind = new BindingSource();
                    bind.DataSource = users;
                    grid.DataSource = bind;

                    EditStyleColumn(UserDataGridView);
                    return;
                }

                db.Add(user);
                source.List.Add(user);

                UserDataGridView.DataSource = null;
                UserDataGridView.DataSource = source;

                EditStyleColumn(UserDataGridView);
            }

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            resultResetPassword.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstname = FirstnameTextBox.Text;
            string lastname = LastnameTextBox.Text;
            firstname = firstname == null ? "" : firstname.Trim();
            lastname = lastname == null ? "" : lastname.Trim();

            Profile profile = new Profile
            {
                Id = currentUser.Id,
                FirstName = firstname,
                LastName = lastname
            };

            currentUser.setProfile(profile);
        }

        private void StationRefreshButton_Click(object sender, EventArgs e)
        {
            if (stationSearchString == null) return;

            List<Station> stations = new List<Station>();
            StationAccess db = new StationAccess();

            stations = db.GetManyBySelector(station => station.name.Contains(stationSearchString));

            DataGridView grid = StationDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = stations;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void StationAddButton_Click(object sender, EventArgs e)
        {
            string name = StationNameTextBox.Text.Trim();
            if (name != "")
            {
                Station station = new Station { id = 1, name = name };
                StationAccess db = new StationAccess();
                BindingSource source = (BindingSource)StationDataGridView.DataSource;

                if (db.GetManyBySelector(obj => obj.name.Contains(name)).Count > 0)
                {
                    MessageBox.Show("The station name is exist");
                    return;
                }

                if ((source != null && source.List.Count == 0) || source == null)
                {
                    List<Station> stations = new List<Station>();

                    db.Add(station);
                    stations.Add(station);

                    DataGridView grid = StationDataGridView;
                    grid.Rows.Clear();
                    BindingSource bind = new BindingSource();
                    bind.DataSource = stations;
                    grid.DataSource = bind;

                    EditStyleColumn(StationDataGridView);
                    return;
                }

                db.Add(station);
                source.List.Add(station);

                StationDataGridView.DataSource = null;
                StationDataGridView.DataSource = source;

                EditStyleColumn(StationDataGridView);
            }
        }

        private void StationDeleteButton_Click(object sender, EventArgs e)
        {
            var rows = StationDataGridView.Rows;
            List<int> indexs = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                    indexs.Add(row.Index);
            }
            for (int i = indexs.Count - 1; i >= 0; --i)
            {
                int index = indexs.ElementAt<int>(i);
                long id = Convert.ToInt64(((DataGridViewTextBoxCell)(rows[index].Cells[1])).Value);
                StationAccess db = new StationAccess();
                db.Delete(id);
                rows.RemoveAt(index);
            }
        }

        private void StationUpdateButton_Click(object sender, EventArgs e)
        {
            var rows = StationDataGridView.Rows;

            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                {
                    int id = Convert.ToInt32(((DataGridViewTextBoxCell)(row.Cells[1])).Value);
                    string name = ((DataGridViewTextBoxCell)(row.Cells[2])).Value.ToString();
                    Station station = new Station { id = id, name = name };
                    StationAccess db = new StationAccess();
                    db.Update(station);
                }
            }
        }

        private void BusRefreshButton_Click(object sender, EventArgs e)
        {
            if (busSearchString == null) return;

            List<Bus> buses = new List<Bus>();
            int seats = -1;
            try
            {
                seats = Convert.ToInt32(busSearchString);
            }
            catch (Exception ex)
            { }
            BusAccess db = new BusAccess();

            if (seats < 0) buses = db.GetAll();
            else buses = db.GetManyBySelector(bus => bus.Seats == seats);

            DataGridView grid = BusDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = buses;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void BusDeleteButton_Click(object sender, EventArgs e)
        {
            var rows = BusDataGridView.Rows;
            List<int> indexs = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                    indexs.Add(row.Index);
            }
            for (int i = indexs.Count - 1; i >= 0; --i)
            {
                int index = indexs.ElementAt<int>(i);
                long id = Convert.ToInt64(((DataGridViewTextBoxCell)(rows[index].Cells[1])).Value);
                BusAccess db = new BusAccess();
                db.Delete(id);
                rows.RemoveAt(index);
            }
        }

        private void BusUpdateButton_Click(object sender, EventArgs e)
        {
            var rows = BusDataGridView.Rows;

            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                {
                    int id = Convert.ToInt32(((DataGridViewTextBoxCell)(row.Cells[1])).Value);
                    int seats = Convert.ToInt32(((DataGridViewTextBoxCell)(row.Cells[2])).Value);
                    Bus bus = new Bus { Id = id, Seats = seats };
                    BusAccess db = new BusAccess();
                    db.Update(bus);
                }
            }

        }

        private void BusAddButton_Click(object sender, EventArgs e)
        {
            string seatsStr = BusSeatsTextBox.Text.Trim();
            if (seatsStr != "")
            {
                int seats = Convert.ToInt32(seatsStr);
                Bus bus = new Bus { Id = 1, Seats = seats };
                BusAccess db = new BusAccess();
                BindingSource source = (BindingSource)BusDataGridView.DataSource;

                if ((source != null && source.List.Count == 0) || source == null)
                {
                    List<Bus> buses = new List<Bus>();

                    db.Add(bus);
                    buses.Add(bus);

                    DataGridView grid = BusDataGridView;
                    grid.Rows.Clear();
                    BindingSource bind = new BindingSource();
                    bind.DataSource = buses;
                    grid.DataSource = bind;

                    EditStyleColumn(BusDataGridView);
                    return;
                }

                db.Add(bus);
                source.List.Add(bus);

                BusDataGridView.DataSource = null;
                BusDataGridView.DataSource = source;

                EditStyleColumn(BusDataGridView);
            }

        }

        private void StopStationComboBox_Click(object sender, EventArgs e)
        {
            StationAccess db = new StationAccess();
            var stations = db.GetAll();
            this.StopStationComboBox.Items.Clear();
            for (int i = 0; i < stations.Count; ++i)
            {
                this.StopStationComboBox.Items.Add(stations[i].name.ToString());
            }
        }

        private void StopSearchEditButton_Click(object sender, EventArgs e)
        {
            stopSearchString = StopSearchTextBox.Text.Trim();
            List<Stop> stops = new List<Stop>();
            StopAccess db = new StopAccess();

            if (stopSearchString == null) stops = db.GetAll();
            else stops = db.GetManyBySelector(
                stop => stop.trip.Bus.ToString().Contains(stopSearchString)
                || stop.station.name.ToString().Contains(stopSearchString)
                || stop.distance.ToString().Contains(stopSearchString)
                );

            DataGridView grid = StopDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = stops;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void StopAddButton_Click(object sender, EventArgs e)
        {
            string stationName = StopStationComboBox.Text.Trim();
            string tripStr = StopBusComboBox.Text.Trim();
            string distanceStr = StopDistanceTextBox.Text;
            DateTime timestart = StopDateTimePicker.Value;
            if (stationName != "" && tripStr != "" && distanceStr != "")
            {
                int tripId = -1;
                double distance = -1;
                try
                {
                    tripId = Convert.ToInt32(tripStr);
                    distance = Convert.ToDouble(distanceStr);
                }
                catch (Exception ex) { return; }

                StationAccess dbS = new StationAccess();
                int stationId = dbS.GetManyBySelector(station => station.name == stationName)[0].id;

                Stop stop = new Stop(tripId, stationId, stationName, timestart, distance);
                StopAccess db = new StopAccess();
                BindingSource source = (BindingSource)StopDataGridView.DataSource;

                if ((source != null && source.List.Count == 0) || source == null)
                {
                    List<Stop> stops = new List<Stop>();
                    try
                    {
                        db.Add(stop);
                        stops.Add(stop);

                        DataGridView grid = StopDataGridView;
                        grid.Rows.Clear();
                        BindingSource bind = new BindingSource();
                        bind.DataSource = stops;
                        grid.DataSource = bind;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"The Trip is exist    {ex.GetType().FullName}");
                    }

                    EditStyleColumn(StopDataGridView);
                    return;
                }
                try
                {
                    db.Add(stop);
                    source.List.Add(stop);

                    StopDataGridView.DataSource = null;
                    StopDataGridView.DataSource = source;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }

                EditStyleColumn(StopDataGridView);

            }
        }

        private void StopRefreshButton_Click(object sender, EventArgs e)
        {
            if (stopSearchString == null) return;

            List<Stop> stops = new List<Stop>();
            StopAccess db = new StopAccess();

            if (stopSearchString == null) stops = db.GetAll();
            else stops = db.GetManyBySelector(
                stop => stop.trip.Bus.ToString().Contains(stopSearchString)
                || stop.station.name.Contains(stopSearchString)
                || stop.distance.ToString().Contains(stopSearchString)
                );

            DataGridView grid = StopDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = stops;
            grid.DataSource = bind;

            EditStyleColumn(grid);

        }

        private void StopDeleteButton_Click(object sender, EventArgs e)
        {
            var rows = StopDataGridView.Rows;
            List<int> indexs = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                    indexs.Add(row.Index);
            }
            for (int i = indexs.Count - 1; i >= 0; --i)
            {
                int index = indexs.ElementAt<int>(i);
                long id_trip = Convert.ToInt64(((Trip)(((DataGridViewTextBoxCell)(rows[index].Cells[1])).Value)).Id);
                long id_station = Convert.ToInt64(((Station)(((DataGridViewTextBoxCell)(rows[index].Cells[2])).Value)).id);
                StopAccess db = new StopAccess();
                db.Delete(id_trip, id_station);
                rows.RemoveAt(index);
            }
        }

        private void TripEditSearchButton_Click(object sender, EventArgs e)
        {
            tripSearchString = TripEditSearchTextBox.Text.Trim();
            List<Trip> trips = new List<Trip>();

            TripAccess db = new TripAccess();

            if (tripSearchString == "") trips = db.GetAll();
            else trips = db.GetManyBySelector(
                trip =>
                trip.Id.ToString().Contains(tripSearchString)
                || trip.DateArrival.ToString().Contains(tripSearchString)
                || trip.Bus.Id.ToString().Contains(tripSearchString)
                || trip.Bus.Seats.ToString().Contains(tripSearchString)
                );

            DataGridView grid = TripDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = trips;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void TripAddButton_Click(object sender, EventArgs e)
        {
            DateTime datestart = TripDateStartEditTimePicker.Value;
            DateTime dateend = TripDateEndEditDateTimePicker.Value;
            int id_bus = -1;
            try
            {
                id_bus = Convert.ToInt32(TripBusComboBox.Text);
            }
            catch (Exception ex) { return; }
            if (datestart != null && dateend != null && id_bus > 0)
            {
                if (DateTime.Now < datestart && DateTime.Now < dateend && datestart < dateend)
                {
                    MessageBox.Show("Incorrected datetime values");
                    return;
                }
                if (datestart.ToString("yyyy-MM-dd HH:mm") == dateend.ToString("yyyy-MM-dd HH:mm")) return;
                Trip trip = new Trip(-1, id_bus, datestart, dateend);
                TripAccess db = new TripAccess();
                try
                {
                    db.Add(trip);
                    TripRefreshButton_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"The Trip is exist\n{ex.Message}");
                }
            }

        }

        private void TripRefreshButton_Click(object sender, EventArgs e)
        {
            List<Trip> trips = new List<Trip>();

            TripAccess db = new TripAccess();

            if (tripSearchString == "") trips = db.GetAll();
            else trips = db.GetManyBySelector(
                trip =>
                trip.Id.ToString().Contains(tripSearchString)
                || trip.DateArrival.ToString().Contains(tripSearchString)
                || trip.Bus.Id.ToString().Contains(tripSearchString)
                || trip.Bus.Seats.ToString().Contains(tripSearchString)
                );

            DataGridView grid = TripDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = trips;
            grid.DataSource = bind;

            EditStyleColumn(grid);
        }

        private void TripDeleteButton_Click(object sender, EventArgs e)
        {
            var rows = TripDataGridView.Rows;
            List<int> indexs = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                var a = Convert.ToBoolean(((DataGridViewCheckBoxCell)(row.Cells[0])).Value);
                if (a)
                    indexs.Add(row.Index);
            }
            for (int i = indexs.Count - 1; i >= 0; --i)
            {
                int index = indexs.ElementAt<int>(i);
                long id = Convert.ToInt64(((DataGridViewTextBoxCell)(rows[index].Cells[1])).Value);
                TripAccess db = new TripAccess();
                db.Delete(id);
                rows.RemoveAt(index);
            }
        }

        private void TripBusComboBox_Click(object sender, EventArgs e)
        {
            BusAccess db = new BusAccess();
            var buses = db.GetAll();
            ((ComboBox)sender).Items.Clear();
            for (int i = 0; i < buses.Count; ++i)
            {
                ((ComboBox)sender).Items.Add(buses[i].Id);
            }
        }
        private void StopTripComboBox_Click(object sender, EventArgs e)
        {
            TripAccess db = new TripAccess();
            var trips = db.GetAll();
            ((ComboBox)sender).Items.Clear();
            for (int i = 0; i < trips.Count; ++i)
            {
                ((ComboBox)sender).Items.Add(trips[i].Id.ToString());
            }
        }

        private void DocumentComboBox_Click(object sender, EventArgs e)
        {
            DocumentComboBox_SelectedIndexChanged(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string type = DocumentComboBox.Text.Trim();
            string numberStr = "";
            string degreeStr = "Student";
            Document document = null;

            if (type == "Student")
            {
                numberStr = textBox7.Text.Trim();
                if (numberStr.Trim().Length < 8)
                {
                    MessageBox.Show("Number must be more equal then 8 symbols");
                    return;
                }
                document = new Document { Id = currentUser.Id, Type = type, Number = numberStr, Degree = "Student" };
                DocumentAccess db = new DocumentAccess();
                try
                {
                    if (db.GetOne(document.Id) != null)
                        db.Update(document);
                }
                catch (Exception ex)
                {
                    db.Add(document);
                }
            }
            else if (type == "Invalid")
            {
                numberStr = textBox3.Text.Trim();
                if (numberStr.Trim().Length < 8)
                {
                    MessageBox.Show("Number must be more equal then 8 symbols");
                    return;
                }
                degreeStr = DisabilityComboBox.Text;
                if (degreeStr.Trim() == "")
                {
                    MessageBox.Show("Please check disability");
                    return;
                }
                document = new Document { Id = currentUser.Id, Type = type, Number = numberStr, Degree = degreeStr };

            }
            else
            {
                document = null;
                DocumentComboBox.Text = "None";
            }
            currentUser.Document = document;
            DocumentComboBox_SelectedIndexChanged(null, null);
        }

        private void BookButton_Click(object sender, EventArgs e)
        {
            BookPanel.Dock = DockStyle.Fill;
            BookPanel.Visible = true;
            EditTabControl.Visible = false;
            Ticketpanel.Visible = false;
            ProfileAdminTabControl.Visible = false;

            this.refreshBook();
        }
        private void refreshBook()
        {
            BookAccess db = new BookAccess();
            List<Book> books = null;

            if (currentUser.Id == 0)
                books = db.GetAll();
            else
                books = db.GetByUserId(currentUser.Id);

            DataGridView grid = BookDataGridView;
            grid.Rows.Clear();
            BindingSource bind = new BindingSource();
            bind.DataSource = books;
            grid.DataSource = bind;

            EditStyleColumn(grid);

        }
    }
}
