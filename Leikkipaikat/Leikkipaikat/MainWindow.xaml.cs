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
using LiteDB;

namespace Leikkipaikat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private static string originalAddress = "";
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
                if (txtAddress.Text != null) { 
                //Lisätään uusi kohde, tiedot tekstikentistä osoite ja info. Toisella rivillä päivitetään datagridi.
                string info = Leikkipaikat.DB.AddPlayground(txtAddress.Text, txtInfo.Text);
                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();
                    MessageBox.Show(info);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnDelPlayground_Click(object sender, RoutedEventArgs e)
        {
            
            {
                //Poistetaan datagridistä valittu kohde. Viimeisellä rivillä taas päivitetään.
                Playground playground = (Playground)dgPlaygrounds.SelectedItem;
                Leikkipaikat.DB.DeletePlayground(playground);

                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds();
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
            if (dgPlaygrounds.SelectedItem != null) { 
            Playground chosen = (Playground)dgPlaygrounds.SelectedItem;

            if (chosen != null)
            {
                txtAddress.Text = chosen.Address;
                txtInfo.Text = chosen.Info;


                //Valitun kohteen info näytetään myös txtInfo2-kentässä ja välineet gdEquipment-datagridissä.

                dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(chosen);
                txtInfo2.Text = chosen.Info;
            }
            else //jos ei valintaa ole kentät tyhjenevät
            {
                txtAddress.Text = "";
                txtInfo.Text = "";
                txtInfo2.Text = "";
            }
            }
        }
        private void dgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dgequipment-kentän valitun välineen viat menevät dgFaults-datagridiin.
            if (dgEquipment.SelectedItem != null)
            { Object selected = (Object)dgEquipment.SelectedItem;//tyhjä valinta pistää sekaisin edelleen
                
            Equipment equipment = (Equipment)selected;
                lbFaults.ItemsSource = equipment.Faults;
            }
            
        }

        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään valitulle kohteelle väline, tiedot txtequipment ja txtbrand-kentistä.
            
                Equipment equipment = new Equipment();
            if (txtEquipment.Text != null)
            { 
                equipment.Name = txtEquipment.Text;
                equipment.Brand = txtBrand.Text;
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                string info= Leikkipaikat.DB.AddEquipment(selected, equipment);
                dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(selected);
                MessageBox.Show(info);
            }
            else { MessageBox.Show("Välineen nimi puuttuu"); }


        }

        private void btnDelEquipment_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan valitun kohteen valittu väline.
            
            
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;

                Leikkipaikat.DB.DelEquipment(selected, equipment);
            dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(selected);



        }

        private void btnAddFault_Click(object sender, RoutedEventArgs e)
        {
            //Lisätään vika valitun kohteen valittuun välineeseen. Vika txtFault-kentästä.
            
            
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                string fault = txtFault.Text;
            if (txtFault.Text != null)
            { 
                lbFaults.ItemsSource = null;//Leikkipaikat.DB.AddFault(selected, equipment, fault);
                MessageBox.Show( Leikkipaikat.DB.AddFault(selected, equipment, fault));
                lbFaults.ItemsSource = equipment.Faults;
            }
            else { MessageBox.Show("Vian nimi puuttuu"); }




        }

        private void btnDelFault_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan valittu vika valitun kohteen valitusta välineestä.
            
            
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                string fault = lbFaults.SelectedItem.ToString();
                 lbFaults.ItemsSource = null;
                 Leikkipaikat.DB.DelFault(selected, equipment, fault);
               
                lbFaults.ItemsSource = equipment.Faults; //Ei päivity!



        }
    }
}
