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
                item.Text = e.Title;

                // Test
                var target = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                target.Name = "target";
                string newTargetStr = "";
                foreach (var st in e.influenceStock)
                {
                    newTargetStr += (st.StockName + ", ");
                }
                target.Text = newTargetStr;

                var ratio = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                ratio.Name = "ratio";
                string newRatioStr = "";
                foreach(var isP in e.IsPositiveEvent)
                {
                    newRatioStr += isP.ToString() + ", ";
                }
                ratio.Text = newRatioStr;
                // --------------------
            }
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

            // Test
            eventListView.Columns.Add("target", "Stock");
            eventListView.Columns.Add("ratio", "등락률");
            // --------------------
        }
    }
}
