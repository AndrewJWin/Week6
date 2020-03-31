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

        // Button Add task method, checks if the new item is not null or empty then if not, adds it to the list box.
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string newItem = txtToDo.Text.Trim();
            
            if (!string.IsNullOrEmpty(newItem))
            {
                // Checking if the item is already in the list, if so - Then don't add it.
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

        // Remove item button method, goes through all the checked items and moved them to the done items and removes them from to be done.
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            List<String> doneItems = new List<string>();

            // If there is nothing checked - We can't remove anything, respond accordingly.
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

        // Bool method, checking the items in the list if the new item is the same, return true.
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
