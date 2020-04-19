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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Leikkipaikat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string originalAddress = "";
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnGetPlaygrounds_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Haetaan hakunapilla leikkipaikat dgPlaygrounds-datagridiin tietokannasta
                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }

        private void btnAddPlaygrounds_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Lisätään uusi kohde, tiedot tekstikentistä osoite ja info. Toisella rivillä päivitetään datagridi.
                Leikkipaikat.DB.AddPlayground(txtAddress.Text, txtInfo.Text);
                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnDelPlayground_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Poistetaan datagridistä valittu kohde. Viimeisellä rivillä taas päivitetään.
                string chosen = dgPlaygrounds.SelectedItem.ToString();
                Leikkipaikat.DB.DeletePlayground(chosen);

                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //Muokataan valittua kohdetta, tiedot osoite ja info kentistä, lopussa päivitetään.
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                string address = txtAddress.Text;
                string info = txtInfo.Text;
                Leikkipaikat.DB.UpdatePlayground(selected, address, info);

                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgPlaygrounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Valitun kohteen tiedot menevät osoite- ja info-kenttiin. Näin kohdetta voi muokata halutessa.
            int i = dgPlaygrounds.SelectedIndex;
            if (i > -1)
            {
                var dt = (DataTable)dgPlaygrounds.DataContext;
                DataRow dr = dt.Rows[i];
                originalAddress = dr[0].ToString();
                txtAddress.Text = dr[0].ToString();
                txtInfo.Text = dr[1].ToString();

                //Valitun kohteen info näytetään dgInfo-datagridissä ja välineet gdEquipment-datagridissä.
                Playground playground = (Playground)dgPlaygrounds.SelectedItem;
                dgEquipment.DataContext = playground.Equipment;
                dgInfo.DataContext = playground.Info;
            }
        }
        private void dgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgequipment-kentän valitun välineen viat menevät dgFaults-datagridiin.
            Equipment equipment = (Equipment)dgEquipment.SelectedItem;
            dgFaults.DataContext = equipment.Faults;
        }

        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään valitulle kohteelle väline, tiedot txtequipment ja txtbrand-kentistä.
            try
            {
                Equipment equipment = new Equipment();
                equipment.Name = txtEquipment.Text;
                equipment.Brand = txtBrand.Text;
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Leikkipaikat.DB.AddEquipment(selected, equipment);

               
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnDelEquipment_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan valitun kohteen valittu väline.
            try
            {
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;

                Leikkipaikat.DB.DelEquipment(selected, equipment);

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddFault_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään vika valitun kohteen valittuun välineeseen. Vika txtFault-kentästä.
            try
            {
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                string fault = txtFault.Text;
                Leikkipaikat.DB.AddFault(selected, equipment, fault);

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnDelFault_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan valittu vika valitun kohteen valitusta välineestä.
            try
            {
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                string fault = dgFaults.SelectedItem.ToString();
                Leikkipaikat.DB.DelFault(selected, equipment, fault);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
