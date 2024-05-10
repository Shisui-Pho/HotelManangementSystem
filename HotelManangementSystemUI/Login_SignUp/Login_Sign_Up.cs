using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManangementControlLibrary.Login_SignUp;
using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.DatabaseService;
using HotelManangementSystemLibrary.Factory;
using HotelManangementSystemUI.Dashboard;
using System.IO;
namespace HotelManangementSystemUI.Login_SignUp
{
    public partial class Login_Sign_Up : Form
    {
        //member to determine if posible data changes were made
        private bool IsDataPossiblyChanged = false;
        private readonly SignInControl _signIn;
        private readonly LogInControl _logIn;
        private readonly IDatabaseService database;
        private string connectionString;
        public Login_Sign_Up()
        {
            InitializeComponent();
            _signIn = new SignInControl(LoginLablePressed);
            _logIn = new LogInControl(SignInLablePressed);

            //For now am going to hardcode this
            string dir = Directory.GetCurrentDirectory();
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dir + "\\HotelManangementSystem.accdb;";
            database = new AccessDatabase(connectionString);
        }//ctor main
        private async void Login_Sign_Up_Shown(object sender, System.EventArgs e)
        {
            plnContainer.Controls.Add(_signIn);
            plnContainer.Controls.Add(_logIn);
            _signIn.Visible = false;
            _logIn.Visible = true;
            _logIn.BringToFront();
            await LoadRooms();//Load this while user attempts to sign in
        }//Login_Sign_Up_Shown
        private async Task LoadRooms()
        {
            await Task.Run(() =>
            {
                Features.GetFeaturesInstance(connectionString);//This will force the database to load the features
                database.LoadRooms();
                database.LoadGuests();
            });
        }//LoadRooms
        private async Task LoadUsersAsync()
        {
            //Load the users and rooms for now
            await Task.Run(()=> 
            { 
                database.LoadUsers();
            });
        }//LoadUsersAsync
        private void LoginLablePressed()
        {
            _signIn.Visible = false;
            _logIn.Visible = true;
            _logIn.BringToFront();
        }//LoginLablePressed
        private void SignInLablePressed()
        {
            _signIn.Visible = true;
            _logIn.Visible = false;
            _signIn.BringToFront();
        }//SignInLablePressed

        private async void Login_Sign_Up_Load(object sender, System.EventArgs e)
        {
            SetUpControls();
            await LoadUsersAsync();
        }//Login_Sign_Up_Load
        private void BtnSignIn_Click(object sender, System.EventArgs e)
        {

        }//BtnSignIn_Click

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            string username = _logIn.txtUsername.Text;
            string password = _logIn.txtxPassword.Text;
            if (!database.Users.LogInUser(username, password, out IUser _logged_in_user))
            {
                MessageBox.Show("Invalid username or password.", "Log in error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logIn.txtxPassword.Text = "";
                _logIn.txtxPassword.Focus();
                return;
            }//end if
            //Successfull
            CfrmDashboard window = default;
            if (_logged_in_user is IAdministrator)
                window = new CfrmDashboard(database, _logged_in_user);
            else if(_logged_in_user is IGuest)
            {
                IGuest guest = database.Guests.FindGuest(_logged_in_user.UserID);
                window = new CfrmDashboard(guest,database.Rooms, LoadBookings);
            }
                
            IsDataPossiblyChanged = true;
            this.Hide();
            if(window.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Logged out!!");
                _logIn.txtxPassword.Text = "";
                _logIn.txtxPassword.Focus();
            }//end if
            this.Show();
        }//BtnLogIn_Click
        private void SetUpControls()
        {
            _logIn.btnLogIn.Click += BtnLogIn_Click;
            _signIn.btnSignIn.Click += BtnSignIn_Click;
        }//SetUpControls
        private void DisposeEvents()
        {
            _logIn.btnLogIn.Click -= BtnLogIn_Click;
            _signIn.btnSignIn.Click -= BtnSignIn_Click;
        }//DisposeEvents

        private async void Login_Sign_Up_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeEvents();
            await SaveData();
        }//Login_Sign_Up_FormClosing
        private async Task SaveData()
        {
            await Task.Run(() =>
            {
                
                if (!IsDataPossiblyChanged)//if data was not changed
                    return;
                database.SaveUsers();
            });
        }//SaveData

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//btnClose_Click
        private async Task<IRoomBookings> LoadBookings(IUser user)
        {
            return await database.LoadBookingsAsync(user);
        }
    }//class
}//namespace
