using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrensyExchangeProgram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Decimal Exchange()
        {
            XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");
            string ChinaDollar = "";

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "Valute")
                        {
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "ID")
                                    {
                                        if (reader.Value == "R01200")
                                        {
                                            reader.MoveToElement();
                                            ChinaDollar = reader.ReadOuterXml();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            XmlDocument ChinaUSDXml = new XmlDocument();
            ChinaUSDXml.LoadXml(ChinaDollar);
            XmlNode xmlNode = ChinaUSDXml.SelectSingleNode("Valute/Value");
            decimal CDValue = Convert.ToDecimal(xmlNode.InnerText);

            return CDValue;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            decimal value = Exchange() / 10;
            TextBox.Text = $"Курс гонконгского доллара к российскому рублю: 1 к {value}";
        }
    }
}
