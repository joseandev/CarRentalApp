using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehiculeListing _manageVehiculeListing;
        private readonly CarRentalEntities _db;

        public AddEditVehicle(ManageVehiculeListing manageVehiculeListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehiculeListing = manageVehiculeListing;
            _db = new CarRentalEntities();
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehiculeListing manageVehiculeListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            if (carToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a velid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(carToEdit);
            }
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVin.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void Limpiar()
        {
            lblId.Text = "";
            tbMake.Text = "";
            tbModel.Text = "";
            tbVin.Text = "";
            tbYear.Text = "";
            tbLicenseNum.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    //Edit Code Here
                    var id = int.Parse(lblId.Text);
                    var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                    car.Model = tbModel.Text;
                    car.Make = tbMake.Text;
                    car.VIN = tbVin.Text;
                    car.Year = int.Parse(tbYear.Text);
                    car.LicensePlateNumber = tbLicenseNum.Text;
                }
                else
                {
                    //Add Code Here
                    var newCar = new TypesOfCar
                    {
                        LicensePlateNumber = tbLicenseNum.Text,
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        VIN = tbVin.Text,
                        Year = int.Parse(tbYear.Text)
                    };

                    _db.TypesOfCars.Add(newCar);
                }
                _db.SaveChanges();
                MessageBox.Show("Operation with Success");

                if (isEditMode){Close();}
                else{Limpiar();}
            } 
            catch (Exception ex)
            {MessageBox.Show($"Error: {ex.Message}");}
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
