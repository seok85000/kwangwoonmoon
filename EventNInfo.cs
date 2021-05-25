using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kwangwoonmoon
{
    public partial class EventNInfo : Form
    {
        public enum EventListColumnType
        {
            Title = 0,
            Description,
        }


        public EventNInfo()
        {
            InitializeComponent();
        }

        public void SetEventListView(List<Event> events)
        {
            if (events == null) return;

            eventListView.Items.Clear();
            foreach (Event e in events)
            {
                ListViewItem item = eventListView.Items.Add(new ListViewItem());
                item.Name = EventListColumnType.Title.ToString();
                item.Text = e.EventTitle;

                var description = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                description.Name = EventListColumnType.Description.ToString();
                description.Text = e.EventDescription;

                // Test
                var target = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                target.Name = "target";
                target.Text = e.influenceStock[0].StockName.ToString();
                var ratio = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                ratio.Name = "ratio";
                ratio.Text = e.InfluencePower.ToString();
                // --------------------
            }

            //eventListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Hide();
        }


        private void EventNInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void EventNInfo_Load(object sender, EventArgs e)
        {
            eventListView.View = View.Details;

            eventListView.Columns.Add(EventListColumnType.Title.ToString(), "제목");
            eventListView.Columns[eventListView.Columns.Count - 1].Width = -1;
            eventListView.Columns.Add(EventListColumnType.Description.ToString(), "설명");
            eventListView.Columns[eventListView.Columns.Count - 1].Width = -2;

            // Test
            eventListView.Columns.Add("target", "Stock");
            eventListView.Columns.Add("ratio", "등락률");
            // --------------------
        }
    }
}
