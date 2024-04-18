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
        string connectionString;
        public Login_Sign_Up()
        {
            InitializeComponent();
            _signIn = new SignInControl(LoginLablePressed);
            _logIn = new LogInControl(SignInLablePressed);

            //For now am going to hardcode this
            string dir = Directory.GetCurrentDirectory();
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dir + "\\HotelManangementSystem.mdb;";
            database = new AccessDatabase(connectionString);
        }//ctor main
        private async void Login_Sign_Up_Shown(object sender, System.EventArgs e)
        {
            plnContainer.Controls.Add(_signIn);
            plnContainer.Controls.Add(_logIn);
            _signIn.Visible = false;
            _logIn.Visible = true;
            _logIn.BringToFront();
            await LoadUsersAsync();
        }//Login_Sign_Up_Shown
        private async Task LoadUsersAsync()
        {
            await Task.Run(()=> { database.LoadUsers();Features.GetFeaturesInstance(connectionString); });
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

        private void Login_Sign_Up_Load(object sender, System.EventArgs e)
        {
            SetUpControls();
        }//Login_Sign_Up_Load
        private void BtnSignIn_Click(object sender, System.EventArgs e)
        {
            if (_signIn.IsSignInHandled)//If the registration was not completed
                return;
            //Before adding check if the username is already there
            if (database.Users.UsernameExists(_signIn.txtUsername.Text))
            {
                MessageBox.Show("Username already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }//ens if
            //Extract the data
            string name = _signIn.txtName.Text;
            string surname = _signIn.txtSurname.Text;
            string username = _signIn.txtUsername.Text;
            string password = _signIn.txtxPassword.Text;
            DateTime dob = _signIn.dtDOB.Value;
            IGuest newuser = UsersFactory.CreateGuest(name, surname, dob);
            newuser.SetUsername(username);
            newuser.SetPassword(password);
           //Add the user to the database
            database.Users.Add(newuser);

            //Log user in automatically
            Thread.Sleep(15);
            MessageBox.Show("Logging you in now.");

            //Bring the login page upfront
            LoginLablePressed();

            //Set the details
            _logIn.txtUsername.Text = newuser.UserName;
            _logIn.txtxPassword.Text = newuser.Password;

            //Triger the btnLogin event without any validation
            BtnLogIn_Click(null, null);//no need to send data in the params
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
                window = new CfrmDashboard(guest, database.Bookings, database.Rooms);
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
    }//class
}//namespace
