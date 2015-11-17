using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopSite
{
    public partial class insert : System.Web.UI.Page
    {
        private static readonly Regex phoneNumber = new Regex(@"\d{3}-\d{3}-\d{4}");
        public string port = "54510";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=HbW-Bnm6Ipg");
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void excecuteBtn_Click(object sender, EventArgs e)
        {
            bool custSearch = false;
            bool prodSearch = false;
            bool ordSearch = false;
            bool cartSearch = false;
            string errString = "";
            

        List<Panel> panels = new List<Panel>();
            panels.Add(custPnl);
            panels.Add(prodPnl);
            panels.Add(orderPnl);
            panels.Add(cartPnl);

            foreach (Panel p in panels)
            {

                foreach (Control t in p.Controls)
                {
                    if (t is TextBox)
                    {
                        TextBox tx = (TextBox)t;
                        if (tx.Text != "")
                        {
                            if (p.ID == "custPnl")
                            {
                                custSearch = true;
                            }
                            else if (p.ID == "prodPnl")
                            {
                                prodSearch = true;
                            }
                            else if (p.ID == "orderPnl")
                            {
                                ordSearch = true;
                            }
                            else if (p.ID == "cartPnl")
                            {
                                cartSearch = true;
                            }
                        }
                    }
                }
            }
            if (custSearch)
            {
                if (prodSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    //validate first name
                    if(firstNameTxt.Text == "")
                    {
                        errString += "First name cannot be blank. ";
                    }
                    else if(Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        errString += "First name cannot have numbers. ";
                    }

                    //valivate last name
                    if(lastNameTxt.Text == "")
                    {
                        errString += "Last name cannot be blank. ";
                    }
                    else if(Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        errString += "Last name cannot have numbers. ";
                    }

                    //validate phone number
                    if(phoneTxt.Text == "")
                    {
                        errString += "Phone number cannot be blank. ";
                    }
                    else if(!phoneNumber.IsMatch(phoneTxt.Text))
                    {
                        errString += "Phone number is not XXX-XXX-XXXX";
                    }
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
                    else
                    {
                        try
                        {
                            string content;
                            string Method = "post";
                            string uri = "http://localhost:" + port + "/Customer/" + firstNameTxt.Text + " " + lastNameTxt.Text + " " + phoneTxt.Text; ;

                            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                            req.KeepAlive = false;
                            req.Method = Method.ToUpper();

                            content = firstNameTxt.Text + " " + lastNameTxt.Text + " " + phoneTxt.Text;

                            byte[] buffer = Encoding.ASCII.GetBytes(content);
                            req.ContentLength = buffer.Length;
                            req.ContentType = "text/xml";
                            Stream PostData = req.GetRequestStream();
                            PostData.Write(buffer, 0, buffer.Length);
                            PostData.Close();


                            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                            Encoding enc = System.Text.Encoding.GetEncoding(1252);
                            StreamReader loResponseStream =
                            new StreamReader(resp.GetResponseStream(), enc);

                            string Response = loResponseStream.ReadToEnd();


                            loResponseStream.Close();
                            resp.Close();
                            errLbl.Text = Response.ToString(); //show response

                        }
                        catch (Exception ex)
                        {
                            errLbl.Text = ex.Message.ToString();
                        }
                    }
                }
            }
            else if (prodSearch)
            {
                if (custSearch || ordSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    //validate product name
                    if (prodNameTxt.Text == "")
                    {
                        errString += "Product name cannot be blank. ";
                    }
  
                    //valivate price
                    if (priceTxt.Text == "")
                    {
                        errString += "price cannot be blank. ";
                    }
                    else if (!Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errString += "must be a valid price with numbers and decimal only X.XX. ";
                    }

                    //validate product weight
                    if (prodWeightTxt.Text == "")
                    {
                        errString += "Product weight cannot be blank. ";
                    }
                    else if (!Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errString += "Weight Must be a number. ";
                    }
                    if (errString != "")
                    {
                        errLbl.Text = errString;
                    }
                    else
                    {
                        
                    }
                }
            }
            else if (ordSearch)
            {
                if (prodSearch || custSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table. ";
                }
                else
                {
                    //validate customer id
                    if (ordCustIDTxt.Text == "")
                    {
                        errString += "custID cannot be blank. ";
                    }
                    else if(!Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        errString += "custID can only have numbers. ";
                    }

                    DateTime temp;
                    string[] format = new string[] {"MM-dd-yy" };
                    //valivate order date
                    if (orderDateTxt.Text == "")
                    {
                        errString += "order date cannot be blank. ";
                    }
                    else if (!DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        errString += "date must be in MM-DD-YY format. ";
                    }

                    errLbl.Text = errString;
                }
            }
            else if (cartSearch)
            {
                if (prodSearch || ordSearch || custSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    //validate product id
                    if (cartProdIDTxt.Text == "")
                    {
                        errString += "prodID cannot be blank. ";
                    }
                    else if (!Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        errString += "prodID can only have numbers. ";
                    }


                    //valivate quantity
                    int i = 0;
                    if (quantityTxt.Text == "")
                    {
                        errString += "order date cannot be blank. ";
                    }
                    else if (!int.TryParse(quantityTxt.Text, out i))
                    {
                        errString += "order date must be an integer. ";
                    }

                    errLbl.Text = errString;
                }
            }
        }


    }
}