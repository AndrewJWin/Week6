using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string newItem = txtToDo.Text.Trim();
            
            if (!string.IsNullOrEmpty(newItem))
            {
                if (itemsIsInList(clsToDo.Items, newItem))
                {
                    MessageBox.Show("You already added that item.", "Error");
                }
                else
                {
                    clsToDo.Items.Add(newItem);
                    txtToDo.Text = "";
                }
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            List<String> doneItems = new List<string>();


            if (clsToDo.CheckedItems.Count.Equals(0))
            {
                MessageBox.Show("Nothing is selected to remove.", "Error");
            } else
            {
                foreach (string Task in clsToDo.CheckedItems)
                {
                    doneItems.Add(Task);
                }
                foreach (string item in doneItems)
                {
                    clsToDo.Items.Remove(item);
                    lstDone.Items.Add(item);
                }
            }
        }
        private bool itemsIsInList(CheckedListBox.ObjectCollection items, string newItem)
        {
            foreach (string item in items)
            {
                if (item.ToUpper() == newItem.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
