using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetPlaygrounds_Click(object sender, RoutedEventArgs e)
        {
            
            string polku = txtPath.Text;
            try
            {
                //Haetaan hakunapilla leikkipaikat dgPlaygrounds-datagridiin tietokannasta ja 
                //lähetetään tieto polusta
                if (txtPath.Text != "")
                { 
                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds(polku);
                }
                else { MessageBox.Show("Osoite puuttuu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }

        private void btnAddPlaygrounds_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;
            //Lisätään uusi kohde, tiedot tekstikentistä osoite ja info.
            //Toisella rivillä päivitetään datagridi.
            try
            {
                if (txtAddress.Text != "")
                {
                   
                    string info = Leikkipaikat.DB.AddPlayground(txtAddress.Text, txtInfo.Text, polku);
                    dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds(polku);
                    MessageBox.Show(info);
                }
                else { MessageBox.Show("Osoiterivi on tyhjä"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }
        }
        private void btnDelPlayground_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;
            //Poistetaan datagridistä valittu kohde. Viimeisellä rivillä taas päivitetään.
            try
            {
                if (dgPlaygrounds.SelectedItem != null)
                {
                    Playground playground = (Playground)dgPlaygrounds.SelectedItem;
                    Leikkipaikat.DB.DeletePlayground(playground, polku);

                    dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds(polku);
                    MessageBox.Show("Kohde poistettu");
                }
                else { MessageBox.Show("Valinta on tyhjä"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }
            
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;

            try
            {
                //Muokataan valittua kohdetta, tiedot osoite- ja info kentistä, lopussa päivitetään.
                if (dgPlaygrounds.SelectedItem != null)
                { 
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                string address = txtAddress.Text;
                string info = txtInfo.Text;
                Leikkipaikat.DB.UpdatePlayground(selected, address, info, polku);

                dgPlaygrounds.ItemsSource = Leikkipaikat.DB.GetPlaygrounds(polku);
                    MessageBox.Show("Muokattu");
                }
                else { MessageBox.Show("Valinta puuttuu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }
        }
        private void txtInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Tuplaklikkaamalla infokenttää pääsee infotekstiä muokkaamaan.

            try
            {
                if (dgPlaygrounds.SelectedItem != null)
                {
                    Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                    txtInfo.Text = selected.Info;
                    txtInfo2.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }
        }


        private void dgPlaygrounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string polku = txtPath.Text;
            //Valitun kohteen tiedot menevät osoite- ja info-kenttiin. 

            try
            {
                if (dgPlaygrounds.SelectedItem != null)
                {
                    Object obj = dgPlaygrounds.SelectedItem;
                    Playground chosen = (Playground)obj;

                    if (chosen != null)//Tämä lienee turhaa toistoa?
                    {
                        txtAddress.Text = chosen.Address;
                        txtInfo.Text = "Tuplaklikkaa tätä muokataksesi infoa";

                        dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(chosen, polku);
                        lbFaults.ItemsSource = null;
                        txtInfo2.Text = chosen.Info;
                        txtFault.Text = "";
                        txtFaultClass.Text = "";
                    }
                    else //jos ei valintaa ole kentät tyhjenevät... tämä on itse asiassa turha?
                    {
                        txtAddress.Text = "";
                        txtInfo.Text = "";
                        txtInfo2.Text = "";
                    }
                }
                else
                {
                    txtAddress.Text = "";
                    txtInfo.Text = "";
                    txtInfo2.Text = "";
                }
            }
            catch (Exception  ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }
        private void dgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string polku = txtPath.Text;
            //dgequipment-kentän valitun välineen viat menevät lbFaults-datagridiin. (oli alunperin ListBox)
            //Samalla vian syöttäkentät tyhjenevät
            try
            {
                if (dgPlaygrounds.SelectedItem != null && dgEquipment.SelectedItem != null)
                {
                    Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                    Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                    lbFaults.ItemsSource = Leikkipaikat.DB.GetFaults(selected, equipment, polku);
                    txtFault.Text = "";
                    txtFaultClass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }

        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;
            //Lisätään valitulle kohteelle väline, tiedot txtequipment ja txtbrand-kentistä.

            Equipment equipment = new Equipment();
            try
            {
                if (txtEquipment.Text != "" && dgPlaygrounds.SelectedItem != null)
                {
                    equipment.Name = txtEquipment.Text;
                    equipment.Brand = txtBrand.Text;
                    Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                    string info = Leikkipaikat.DB.AddEquipment(selected, equipment, polku);
                    dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(selected, polku);
                    MessageBox.Show(info);
                    txtEquipment.Text = "";
                    txtBrand.Text = "";
                }
                else { MessageBox.Show("Kohde tai välineen nimi puuttuu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }


        }

        private void btnDelEquipment_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;
            //Poistetaan valitun kohteen valittu väline.

            try
            {
                if (dgPlaygrounds.SelectedItem != null && dgEquipment.SelectedItem != null)
                {
                    Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                    Equipment equipment = (Equipment)dgEquipment.SelectedItem;

                    Leikkipaikat.DB.DelEquipment(selected, equipment, polku);
                    dgEquipment.ItemsSource = Leikkipaikat.DB.GetEquipment(selected, polku);
                    MessageBox.Show("Väline poistettu");
                }
                else { MessageBox.Show("Kohde tai välineen nimi puuttuu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }


        }

        private void btnAddFault_Click(object sender, RoutedEventArgs e)
        {  
            string polku = txtPath.Text;
            //Lisätään vika valitun kohteen valittuun välineeseen. Vika txtFaultClass- ja txtFault-kentistä 
            //Katsotaan ensin että kentät on täytetty. Sitten haetaan viat uudelleen.
            try
            {
                Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                Fault fault = new Fault();
                fault.FaultName = txtFault.Text;             
                char[] input = txtFaultClass.Text.ToCharArray();

                if (txtFault.Text != "" && input.Length > 0 && equipment != null)
                {
                    fault.Category = input[0];
                    MessageBox.Show(Leikkipaikat.DB.AddFault(selected, equipment, fault, polku));
                    lbFaults.ItemsSource = Leikkipaikat.DB.GetFaults(selected, equipment, polku);
                }
                else { MessageBox.Show("Välineen valinta tai vian nimi/luokka puuttuu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }

        private void btnDelFault_Click(object sender, RoutedEventArgs e)
        {
            string polku = txtPath.Text;
            //Poistetaan valittu vika valitun kohteen valitusta välineestä.
            //Tutkitaan ensin että valinnat on tehty ja sitten haetaan viat uusiksi

            try
            {
                if (dgPlaygrounds.SelectedItem != null && dgEquipment.SelectedItem != null && lbFaults.SelectedItem != null)
                {
                    Playground selected = (Playground)dgPlaygrounds.SelectedItem;
                    Equipment equipment = (Equipment)dgEquipment.SelectedItem;
                    Fault fault = (Fault)lbFaults.SelectedItem;
                    //Lähetetään tiedot metodille
                    Leikkipaikat.DB.DelFault(selected, equipment, fault, polku);
                    //Haetaan uudet tiedot
                    lbFaults.ItemsSource = Leikkipaikat.DB.GetFaults(selected, equipment, polku);
                    MessageBox.Show("Vika poistettu");

                }
                else { MessageBox.Show("Vikaa ei valittu"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {//Tyhjennetään vasemman puoleisen palkin tekstikentät käytön helpottamiseksi.
            try
            {
                txtAddress.Text = "";
                txtInfo.Text = "";
                txtInfo2.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ohjelman suorituksen aikana tapahtui seuraava virhe: " + ex.Message);
            }
        }
    }
}
