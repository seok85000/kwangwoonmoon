
namespace kwangwoonmoon
{
    partial class KWM
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labe1 = new System.Windows.Forms.Label();
            this.stock_listview = new System.Windows.Forms.ListView();
            this.today_label = new System.Windows.Forms.Label();
            this.gametime_label = new System.Windows.Forms.Label();
            this.nowTurn_label = new System.Windows.Forms.Label();
            this.finish_label = new System.Windows.Forms.Label();
            this.buy_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mystock_listview = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.total_amount_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.total_price_textbox = new System.Windows.Forms.TextBox();
            this.sell_button = new System.Windows.Forms.Button();
            this.news_button = new System.Windows.Forms.Button();
            this.info_shop_button = new System.Windows.Forms.Button();
            this.plus_button = new System.Windows.Forms.Button();
            this.minus_button = new System.Windows.Forms.Button();
            this.gameturn_label = new System.Windows.Forms.Label();
            this.mymoney_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labe1
            // 
            this.labe1.AutoSize = true;
            this.labe1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labe1.Location = new System.Drawing.Point(12, 14);
            this.labe1.Name = "labe1";
            this.labe1.Size = new System.Drawing.Size(66, 27);
            this.labe1.TabIndex = 0;
            this.labe1.Text = "뉴스";
            // 
            // stock_listview
            // 
            this.stock_listview.HideSelection = false;
            this.stock_listview.Location = new System.Drawing.Point(12, 55);
            this.stock_listview.Name = "stock_listview";
            this.stock_listview.Size = new System.Drawing.Size(888, 344);
            this.stock_listview.TabIndex = 1;
            this.stock_listview.UseCompatibleStateImageBehavior = false;
            // 
            // today_label
            // 
            this.today_label.AutoSize = true;
            this.today_label.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.today_label.Font = new System.Drawing.Font("굴림", 20F);
            this.today_label.Location = new System.Drawing.Point(937, 55);
            this.today_label.Name = "today_label";
            this.today_label.Size = new System.Drawing.Size(129, 27);
            this.today_label.TabIndex = 2;
            this.today_label.Text = "현재 날짜";
            // 
            // gametime_label
            // 
            this.gametime_label.AutoSize = true;
            this.gametime_label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gametime_label.Font = new System.Drawing.Font("굴림", 15F);
            this.gametime_label.Location = new System.Drawing.Point(924, 96);
            this.gametime_label.Name = "gametime_label";
            this.gametime_label.Size = new System.Drawing.Size(160, 20);
            this.gametime_label.TabIndex = 3;
            this.gametime_label.Text = "2532년 10월 5일";
            // 
            // nowTurn_label
            // 
            this.nowTurn_label.AutoSize = true;
            this.nowTurn_label.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.nowTurn_label.Font = new System.Drawing.Font("굴림", 20F);
            this.nowTurn_label.Location = new System.Drawing.Point(1118, 55);
            this.nowTurn_label.Name = "nowTurn_label";
            this.nowTurn_label.Size = new System.Drawing.Size(75, 27);
            this.nowTurn_label.TabIndex = 4;
            this.nowTurn_label.Text = "턴 수";
            // 
            // finish_label
            // 
            this.finish_label.AutoSize = true;
            this.finish_label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.finish_label.Font = new System.Drawing.Font("굴림", 15F);
            this.finish_label.Location = new System.Drawing.Point(1146, 96);
            this.finish_label.Name = "finish_label";
            this.finish_label.Size = new System.Drawing.Size(47, 20);
            this.finish_label.TabIndex = 5;
            this.finish_label.Text = "/ 50";
            // 
            // buy_button
            // 
            this.buy_button.BackColor = System.Drawing.Color.DarkRed;
            this.buy_button.Font = new System.Drawing.Font("굴림", 20F);
            this.buy_button.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buy_button.Location = new System.Drawing.Point(922, 605);
            this.buy_button.Name = "buy_button";
            this.buy_button.Size = new System.Drawing.Size(144, 64);
            this.buy_button.TabIndex = 6;
            this.buy_button.Text = "매수";
            this.buy_button.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 15F);
            this.label1.Location = new System.Drawing.Point(13, 437);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "현재 보유 금액";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15F);
            this.label2.Location = new System.Drawing.Point(343, 469);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "현재 보유 주식";
            // 
            // mystock_listview
            // 
            this.mystock_listview.HideSelection = false;
            this.mystock_listview.Location = new System.Drawing.Point(12, 496);
            this.mystock_listview.Name = "mystock_listview";
            this.mystock_listview.Size = new System.Drawing.Size(888, 173);
            this.mystock_listview.TabIndex = 10;
            this.mystock_listview.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 15F);
            this.label3.Location = new System.Drawing.Point(917, 497);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "수량";
            // 
            // total_amount_textbox
            // 
            this.total_amount_textbox.Location = new System.Drawing.Point(983, 496);
            this.total_amount_textbox.Name = "total_amount_textbox";
            this.total_amount_textbox.Size = new System.Drawing.Size(135, 21);
            this.total_amount_textbox.TabIndex = 12;
            this.total_amount_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 15F);
            this.label4.Location = new System.Drawing.Point(917, 539);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "총액";
            // 
            // total_price_textbox
            // 
            this.total_price_textbox.Location = new System.Drawing.Point(983, 538);
            this.total_price_textbox.Name = "total_price_textbox";
            this.total_price_textbox.Size = new System.Drawing.Size(135, 21);
            this.total_price_textbox.TabIndex = 14;
            // 
            // sell_button
            // 
            this.sell_button.BackColor = System.Drawing.Color.DarkBlue;
            this.sell_button.Font = new System.Drawing.Font("굴림", 20F);
            this.sell_button.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.sell_button.Location = new System.Drawing.Point(1072, 605);
            this.sell_button.Name = "sell_button";
            this.sell_button.Size = new System.Drawing.Size(144, 64);
            this.sell_button.TabIndex = 15;
            this.sell_button.Text = "매도";
            this.sell_button.UseVisualStyleBackColor = false;
            // 
            // news_button
            // 
            this.news_button.Location = new System.Drawing.Point(983, 200);
            this.news_button.Name = "news_button";
            this.news_button.Size = new System.Drawing.Size(176, 51);
            this.news_button.TabIndex = 16;
            this.news_button.Text = "뉴스 / 정보";
            this.news_button.UseVisualStyleBackColor = true;
            this.news_button.Click += new System.EventHandler(this.news_button_Click);
            // 
            // info_shop_button
            // 
            this.info_shop_button.Location = new System.Drawing.Point(983, 312);
            this.info_shop_button.Name = "info_shop_button";
            this.info_shop_button.Size = new System.Drawing.Size(176, 51);
            this.info_shop_button.TabIndex = 17;
            this.info_shop_button.Text = "정보 상점";
            this.info_shop_button.UseVisualStyleBackColor = true;
            this.info_shop_button.Click += new System.EventHandler(this.info_shop_button_Click);
            // 
            // plus_button
            // 
            this.plus_button.Location = new System.Drawing.Point(1141, 494);
            this.plus_button.Name = "plus_button";
            this.plus_button.Size = new System.Drawing.Size(75, 23);
            this.plus_button.TabIndex = 18;
            this.plus_button.Text = "+1";
            this.plus_button.UseVisualStyleBackColor = true;
            this.plus_button.Click += new System.EventHandler(this.plus_button_Click);
            // 
            // minus_button
            // 
            this.minus_button.Location = new System.Drawing.Point(1141, 538);
            this.minus_button.Name = "minus_button";
            this.minus_button.Size = new System.Drawing.Size(75, 23);
            this.minus_button.TabIndex = 19;
            this.minus_button.Text = "-1";
            this.minus_button.UseVisualStyleBackColor = true;
            this.minus_button.Click += new System.EventHandler(this.minus_button_Click);
            // 
            // gameturn_label
            // 
            this.gameturn_label.AutoSize = true;
            this.gameturn_label.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gameturn_label.Font = new System.Drawing.Font("굴림", 15F);
            this.gameturn_label.Location = new System.Drawing.Point(1119, 96);
            this.gameturn_label.Name = "gameturn_label";
            this.gameturn_label.Size = new System.Drawing.Size(31, 20);
            this.gameturn_label.TabIndex = 20;
            this.gameturn_label.Text = "01";
            // 
            // mymoney_label
            // 
            this.mymoney_label.AutoSize = true;
            this.mymoney_label.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mymoney_label.Font = new System.Drawing.Font("굴림", 15F);
            this.mymoney_label.Location = new System.Drawing.Point(219, 437);
            this.mymoney_label.Name = "mymoney_label";
            this.mymoney_label.Size = new System.Drawing.Size(122, 20);
            this.mymoney_label.TabIndex = 8;
            this.mymoney_label.Text = "123,456,789";
            this.mymoney_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Font = new System.Drawing.Font("굴림", 15F);
            this.label6.Location = new System.Drawing.Point(177, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(720, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "\\                                                                                " +
    "                원";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KWM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 686);
            this.Controls.Add(this.gameturn_label);
            this.Controls.Add(this.minus_button);
            this.Controls.Add(this.plus_button);
            this.Controls.Add(this.info_shop_button);
            this.Controls.Add(this.news_button);
            this.Controls.Add(this.sell_button);
            this.Controls.Add(this.total_price_textbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.total_amount_textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mystock_listview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mymoney_label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buy_button);
            this.Controls.Add(this.finish_label);
            this.Controls.Add(this.nowTurn_label);
            this.Controls.Add(this.gametime_label);
            this.Controls.Add(this.today_label);
            this.Controls.Add(this.stock_listview);
            this.Controls.Add(this.labe1);
            this.MinimumSize = new System.Drawing.Size(1252, 725);
            this.Name = "KWM";
            this.Text = "Kwang Woon Moon";
            this.Load += new System.EventHandler(this.KWM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labe1;
        private System.Windows.Forms.ListView stock_listview;
        private System.Windows.Forms.Label today_label;
        private System.Windows.Forms.Label gametime_label;
        private System.Windows.Forms.Label nowTurn_label;
        private System.Windows.Forms.Label finish_label;
        private System.Windows.Forms.Button buy_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView mystock_listview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox total_amount_textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox total_price_textbox;
        private System.Windows.Forms.Button sell_button;
        private System.Windows.Forms.Button news_button;
        private System.Windows.Forms.Button info_shop_button;
        private System.Windows.Forms.Button plus_button;
        private System.Windows.Forms.Button minus_button;
        private System.Windows.Forms.Label gameturn_label;
        private System.Windows.Forms.Label mymoney_label;
        private System.Windows.Forms.Label label6;
    }
}

