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
    public partial class ManageVehiculeListing : Form
    {
        private readonly CarRentalEntities _db;
        private bool isOpen;
        public ManageVehiculeListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }
        public void populateGrid()
        {
            //Select *from TypesOfCars
            //var cars = _db.TypesOfCars.ToList();

            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();

            gvVehiculeList.DataSource = cars;
            gvVehiculeList.Columns[4].HeaderText = "License Plate Number";
            gvVehiculeList.Columns["Id"].Visible = false;
        }
        private void ManageVehiculeListing_Load(object sender, EventArgs e)
        {
            try{populateGrid();}
            catch (Exception ex){MessageBox.Show($"Error: {ex.Message}");}
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            if (!Utils.IsFormOpen("AddEditVehicle"))
            {
                var addEditVehicle = new AddEditVehicle(this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            if (!Utils.IsFormOpen("AddEditVehicle"))
            {
                try
                {
                    //get ID of selected row
                    var id = (int)gvVehiculeList.SelectedRows[0].Cells["Id"].Value;

                    //query database for record
                    var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                    //launch AddEditVehicle window with data
                    var addEditVehicle = new AddEditVehicle(car, this);
                    addEditVehicle.MdiParent = this.MdiParent;
                    addEditVehicle.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    populateGrid();
                }
            }
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get ID of selected row
                var id = (int)gvVehiculeList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are you  sure You Want To Delete This Record?", 
                    "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    //delete vehicle from table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                    MessageBox.Show("Deleted");
                    populateGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            populateGrid();
        }
    }
}