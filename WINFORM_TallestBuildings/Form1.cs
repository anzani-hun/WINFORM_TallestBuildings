﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//mysql használata a xampphoz
using MySqlConnector;

namespace WINFORM_TallestBuildings
{
    public partial class Form1 : Form
    {

        MySqlConnection connection;

        public Form1()
        {
            InitializeComponent();


            string connectionString;
            string sqlParancs;

            connectionString = "server=localhost;userid=root;password=;database=tallest_buildings";
            connection = new MySqlConnection(connectionString);

            sqlParancs = "SELECT rank, building_name, floors, city FROM buildings WHERE 1; ";
            
            connection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlParancs, connection);

            DataSet ds = new DataSet();

            adapter.Fill(ds);

            BindingSource bs = new BindingSource();
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;

            connection.Close();
        }

        private void button_orszag_Click(object sender, EventArgs e)
        {
            string adat = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string sqlParancs = "SELECT city FROM buildings WHERE building_name ='" + adat + "';";
            MySqlCommand sqlCommand = new MySqlCommand(sqlParancs, connection);
            string varos = sqlCommand.ExecuteScalar().ToString();
            label1.Text = "Ország: " + varos;
        }

        private void button_osszemeletek_Click(object sender, EventArgs e)
        {
            string adat = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string sqlParancs = "SELECT COUNT(building_name) FROM buildings WHERE height_m > 400 ;";
            MySqlCommand sqlCommand = new MySqlCommand(sqlParancs, connection);
            string varos = sqlCommand.ExecuteScalar().ToString();
            label2.Text = "A 400 m-nél magasabb épületek száma: " + varos;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
