public void TableSearch(OracleCommand cmd)
{
    try
    {
        string prodId = txt_PRODID.Text;
        if (prodId != "")
        {
            cmd.CommandText = $"SELECT * FROM WORKORDER "
            + $"WHERE PLANDTTM >= ('{workOrderDateTimePicker1.Value.Year}/{workOrderDateTimePicker1.Value.Month}/{workOrderDateTimePicker1.Value.Day}')\n"
            + $"AND PLANDTTM <= to_date('{workOrderDateTimePicker2.Value.Year}/{workOrderDateTimePicker2.Value.Month}/{workOrderDateTimePicker2.Value.Day}') + 1 \n"
            + $"AND PRODID = '{prodId}'"
            + $"ORDER BY WOID";
            cmd.ExecuteNonQuery();//////////////////
        }
        else
        {
            cmd.CommandText = $"SELECT * FROM WORKORDER "
            + $"WHERE PLANDTTM >= ('{workOrderDateTimePicker1.Value.Year}/{workOrderDateTimePicker1.Value.Month}/{workOrderDateTimePicker1.Value.Day}')\n"
            + $"AND PLANDTTM <= to_date('{workOrderDateTimePicker2.Value.Year}/{workOrderDateTimePicker2.Value.Month}/{workOrderDateTimePicker2.Value.Day}') + 1 \n"
            + $"ORDER BY WOID";
            cmd.ExecuteNonQuery();
        }


        OracleDataReader rdr = cmd.ExecuteReader();

        List<workOrder> workOrders = new List<workOrder>();

        while (rdr.Read())
        {
            string woid = rdr["WOID"] as string;
            string wostat = rdr["WOSTAT"] as string;

            // TIMESTAMP 필드를 nullable DateTime으로 처리
            DateTime? plandttm = rdr["PLANDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["PLANDTTM"]);
            DateTime? wostdttm = rdr["WOSTDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["WOSTDTTM"]);
            DateTime? woeddttm = rdr["WOEDDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["WOEDDTTM"]);
            DateTime? insdttm = rdr["INSDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["INSDTTM"]);

            string prodid = rdr["PRODID"] as string;
            string planqty = rdr["PLANQTY"] as string;
            string prodqty = rdr["PRODQTY"] as string;
            string procid = rdr["PROCID"] as string;
            string userid = rdr["USERID"] as string;

            workOrders.Add(new workOrder
            {
                WoId = woid,
                WoStat = wostat,
                PlanDttm = plandttm,
                WoStDttm = wostdttm,
                WoEdDttm = woeddttm,
                ProdId = prodid,
                PlanQty = planqty,
                ProdQty = prodqty,
                ProcId = procid,
                InsDttm = insdttm,
                UserId = userid
            });

        }//end of while (rdr.Read())

        dataGridView1.DataSource = workOrders;

        // 컬럼명 변경
        dataGridView1.Columns[0].HeaderText = "      작업지시ID";
        dataGridView1.Columns[1].HeaderText = "      공정상태";
        dataGridView1.Columns[2].HeaderText = "      계획일자";
        dataGridView1.Columns[3].HeaderText = "      작업시작일";
        dataGridView1.Columns[4].HeaderText = "      작업완료일";
        dataGridView1.Columns[5].HeaderText = "      제품코드";
        dataGridView1.Columns[6].HeaderText = "      계획수량";
        dataGridView1.Columns[7].HeaderText = "      생산한수량";
        dataGridView1.Columns[8].HeaderText = "      공정";
        dataGridView1.Columns[9].HeaderText = "      작업지시일자";
        dataGridView1.Columns[10].HeaderText = "      지시자ID";

        // 헤더 값을 가운데로 정렬
        dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        // 셀 값을 가운데로 정렬
        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }
    catch
    {

    }
}
=========================================
    private void searchButton_Click(object sender, EventArgs e)
    {
        int prodQty = 0;
        string prodCodeText = "";
        string prodNameText = "";
        try
        {
            prodQty = int.Parse(QTYTextBox.Text);   //제품 수량
            prodCodeText = prodCodeComboBox.Text;    //제품 코드
            prodNameText = prodNameComboBox.Text;    //제품 이름
                                                            // 컨트롤 초기화
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb != null)
                    {
                        tb.Text = string.Empty;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox dd = (ComboBox)ctrl;
                    if (dd != null)
                    {
                        dd.Text = string.Empty;
                        dd.SelectedIndex = -1;
                    }
                }
            }

            QTYTextBox.Text = prodQty.ToString();
            prodCodeComboBox.Text = prodCodeText.ToString();
            prodNameComboBox.Text = prodNameText.ToString();

            if ((prodCodeComboBox.Text == "P001" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "NCM양극판 80" || prodNameComboBox.Text == ""))
            {
                int psm002 = prodQty / 3;   //완제품 반제품 비율 3:1
                int psm001 = psm002;        // 반제품 비율 1:1
                NCM믹싱반제품TextBox.Text = $"{psm001}";
                NCM코팅반제품TextBox.Text = $"{psm002}";
            }
            else if ((prodCodeComboBox.Text == "P002" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "LFP양극판 80" || prodNameComboBox.Text == ""))
            {
                int psm004 = prodQty / 3;   //완제품 반제품 비율 3:1
                int psm003 = psm004;        // 반제품 비율 1:1
                LFP믹싱반제품TextBox.Text = $"{psm003}";
                LFP코팅반제품TextBox.Text = $"{psm004}";
            }
            else if ((prodCodeComboBox.Text == "P003" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "음극판 80" || prodNameComboBox.Text == ""))
            {
                int msm002 = prodQty / 3;   //완제품 반제품 비율 3:1
                int msm001 = msm002;        // 반제품 비율 1:1
                음극판믹싱반제품TextBox.Text = $"{msm001}";
                음극판코팅반제품TextBox.Text = $"{msm002}";
            }
            else if ((prodCodeComboBox.Text == "PSM001" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "NCM믹싱반제품" || prodNameComboBox.Text == ""))
            {
                int m001 = prodQty / 10;
                int m002 = prodQty / 10;
                int m003 = prodQty / 5 * 3;
                int m004 = prodQty / 10;
                int m005 = prodQty / 10;
                CarbonBlackTextBox.Text = $"{m001}";
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                NCMTextBox.Text = $"{m005}";
            }
            else if ((prodCodeComboBox.Text == "PSM002" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "NCM코팅반제품 240" || prodNameComboBox.Text == ""))
            {
                int psm001 = prodQty;
                int m008 = prodQty;
                NCM믹싱반제품TextBox.Text = $"{psm001}";
                알루미늄호일TextBox.Text = $"{m008}";
            }
            else if ((prodCodeComboBox.Text == "PSM003" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "LFP믹싱반제품" || prodNameComboBox.Text == ""))
            {
                int m001 = prodQty / 10;
                int m002 = prodQty / 10;
                int m003 = prodQty / 5 * 3;
                int m004 = prodQty / 10;
                int m006 = prodQty / 10;
                CarbonBlackTextBox.Text = $"{m001}";
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                LFPTextBox.Text = $"{m006}";
            }
            else if ((prodCodeComboBox.Text == "PSM004" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "LFP코팅반제품 240" || prodNameComboBox.Text == ""))
            {
                int psm003 = prodQty;
                int m008 = prodQty;
                LFP믹싱반제품TextBox.Text = $"{psm003}";
                알루미늄호일TextBox.Text = $"{m008}";
            }
            else if ((prodCodeComboBox.Text == "MSM001" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "음극판믹싱반제품" || prodNameComboBox.Text == ""))
            {
                int m002 = prodQty / 10;
                int m003 = prodQty / 10 * 7;
                int m004 = prodQty / 10;
                int m007 = prodQty / 10;
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                인조흑연TextBox.Text = $"{m007}";
            }
            else if ((prodCodeComboBox.Text == "MSM002" || prodCodeComboBox.Text == "") && (prodNameComboBox.Text == "음극판코팅반제품 240" || prodNameComboBox.Text == ""))
            {
                int msm001 = prodQty;
                int m009 = prodQty;
                음극판믹싱반제품TextBox.Text = $"{msm001}";
                구리호일TextBox.Text = $"{m009}";
            }
        }
        catch
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb != null)
                    {
                        tb.Text = string.Empty;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox dd = (ComboBox)ctrl;
                    if (dd != null)
                    {
                        dd.Text = string.Empty;
                        dd.SelectedIndex = -1;
                    }
                }
            }
            prodCodeComboBox.Text = prodCodeText.ToString();
            prodNameComboBox.Text = prodNameText.ToString();
        }
    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
        Close();
    }
}
