﻿using System;
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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            populateGrid();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord
            {
                MdiParent = this.MdiParent
            };
            addRentalRecord.Show();
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //get ID of selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                //launch AddEditVehicle window with data
                var addEditRentalRecord = new AddEditRentalRecord(record);
                addEditRentalRecord.MdiParent = this.MdiParent;
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //get ID of selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                //delete vehicle from table
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();

                MessageBox.Show("Deleted");
                populateGrid();
            }
            catch (Exception ex){MessageBox.Show($"Error: {ex.Message}");}
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try{populateGrid();}
            catch (Exception ex){MessageBox.Show($"Error: {ex.Message}");}
        }

        public void populateGrid()
        {
            var records = _db.CarRentalRecords.Select(q => new
            {
                Customer = q.customerName,
                DateOut = q.dateRented,
                DateIn = q.dateReturned,
                Id = q.id,
                q.cost,
                Car = q.TypesOfCar.Make + " " + q.TypesOfCar.Model
            }).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["dateOut"].HeaderText = "Date Out";
            gvRecordList.Columns["dateIn"].HeaderText = "Date In";
            gvRecordList.Columns["id"].Visible = false;
        }
    }
}
