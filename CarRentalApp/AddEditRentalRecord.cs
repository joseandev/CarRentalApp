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
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities _db;
    
        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Rental";
            this.Text = "Add New Rental";
            isEditMode = false;
            _db = new CarRentalEntities();
        }

        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
            }
        }
                 
        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.customerName;
            dateRented.Value = (DateTime) recordToEdit.dateRented;
            dateReturned.Value = (DateTime) recordToEdit.dateReturned;
            tbCost.Text = recordToEdit.cost.ToString();
            lblRecordId.Text = recordToEdit.id.ToString();

            var carType = cbTypeOfCar.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dateRented.Value;
                var dateIn = dateReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);

                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrEmpty(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter missing data.\n\r";
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection\n\r";
                }

                if (isValid)
                {
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode)
                    {
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    }

                    rentalRecord.customerName = customerName;
                    rentalRecord.dateRented = dateOut;
                    rentalRecord.dateReturned = dateIn;
                    rentalRecord.cost = (decimal)cost;
                    rentalRecord.typeOfCar = (int)cbTypeOfCar.SelectedValue;
                    
                    if(!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);
                    
                    _db.SaveChanges();

                    MessageBox.Show($"Costumer Name: {customerName}\n\r" +
                        $"Date Rented {dateOut}\n\r" +
                        $"Date Returned {dateIn}\n\r" +
                        $"Cost: {cost}" +
                        $"Car Type: {carType}\n\r" +
                        $"THANKS YOU FOR YOUR BUSINESS");
                    Close();
                } 
                else
                {
                    MessageBox.Show(errorMessage);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void RentalForm_Load(object sender, EventArgs e)
        {
            //Select *from TypesOfCars
            //var cars = carRentalEntities.TypesOfCars.ToList();

            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Id = q.Id,
                    Name = q.Make + " " + q.Model,
                })
                .ToList();

            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "id";
            cbTypeOfCar.DataSource = cars;
        }
    }
}
