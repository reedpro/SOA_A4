﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;

namespace ShopSite
{
    public partial class search : System.Web.UI.Page
    {
        private static readonly Regex phoneNumber = new Regex(@"\d{3}-\d{3}-\d{4}");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
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
            string errMsg = "";
            string getString = "";

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
                else //then we are doing a customer search. build get string
                {
                    if (custIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "custID:" + custIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "customer ID must be numeric. ";
                    }

                    if (firstNameTxt.Text != "" && !Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "firstName:" + firstNameTxt.Text;
                    }
                    else if (Regex.IsMatch(firstNameTxt.Text, @"\d"))
                    {
                        errMsg += "first name must not be numeric. ";
                    }

                    if (lastNameTxt.Text != "" && !Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "lastName:" + lastNameTxt.Text;
                    }
                    else if (Regex.IsMatch(lastNameTxt.Text, @"\d"))
                    {
                        errMsg += "last name must not be numeric. ";
                    }

                    if (phoneTxt.Text != "" && phoneNumber.IsMatch(phoneTxt.Text))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "phoneNumber:" + phoneTxt.Text;
                    }
                    else if (!phoneNumber.IsMatch(phoneTxt.Text) && phoneTxt.Text != "")
                    {
                        errMsg += "Phone number must be XXX-XXX-XXXX. ";
                    }
                    if (errMsg != "")
                    {
                        errLbl.Text = errMsg;
                    }
                    else
                    {
                        var client = new RestSharp.RestClient("http://10.113.21.144//");
                        var request = new RestSharp.RestRequest("/webservice/shoppService.asmx/HelloWorld", RestSharp.Method.GET);
                        client.Execute(request);
                        RestSharp.RestResponse response = client.Execute(request);
                        var content = response.Content;
                    }
                    //getString;
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
                    if (prodIDTxt.Text != "" && Regex.IsMatch(custIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "product ID must be numeric. ";
                    }

                    if (prodNameTxt.Text != "" && !Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodName:" + prodNameTxt.Text;
                    }
                    else if (Regex.IsMatch(prodNameTxt.Text, @"\d"))
                    {
                        errMsg += "product name must not be numeric. ";
                    }

                    if (priceTxt.Text != "" && !Regex.IsMatch(priceTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "price:" + priceTxt.Text;
                    }
                    else if (Regex.IsMatch(lastNameTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "price be numeric. ";
                    }

                    if (prodWeightTxt.Text != "" && Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodWeight:" + prodWeightTxt.Text;
                    }
                    else if (!Regex.IsMatch(prodWeightTxt.Text, @"^[1-9]\d*(\.\d+)?$"))
                    {
                        errMsg += "Product weight must be a numbe.r ";
                    }
                    errLbl.Text = errMsg + "\n" + getString;
                }
            }
            else if (ordSearch)
            {
                if (prodSearch || custSearch || cartSearch)
                {
                    errLbl.Text = "Error: You cannot search more than one table";
                }
                else
                {
                    if (orderIDTxt.Text != "" && Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(orderIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "order ID must be numeric. ";
                    }

                    if (ordCustIDTxt.Text != "" && Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "custID:" + ordCustIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(ordCustIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "customer ID must be numeric. ";
                    }

                    if (poNumberTxt.Text != "")
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "poNumber:" + poNumberTxt.Text;
                    }

                    DateTime temp;
                    string[] format = new string[] { "MM-dd-yy" };
                    //valivate order date
                    if (orderDateTxt.Text != "" && DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "orderDate:" + orderDateTxt.Text;
                    }
                    else if (!DateTime.TryParseExact(orderDateTxt.Text, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp))
                    {
                        errMsg += "date must be in MM-DD-YY format. ";
                    }
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
                    if (cartOrderIDTxt.Text != "" && Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$"))
                    {
                        getString += "prodID:" + prodIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(cartOrderIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "order ID must be numeric. ";
                    }

                    if (cartProdIDTxt.Text != "" && Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "prodID:" + cartProdIDTxt.Text;
                    }
                    else if (!Regex.IsMatch(cartProdIDTxt.Text, @"^[1-9]?$"))
                    {
                        errMsg += "product ID must be numeric. ";
                    }
                    int i = 0;
                    if (quantityTxt.Text != "" && int.TryParse(quantityTxt.Text, out i))
                    {
                        if (getString != "")
                        {
                            getString += ",";
                        }
                        getString += "quantity:" + quantityTxt.Text;
                    }
                    else if (!int.TryParse(quantityTxt.Text, out i))
                    {
                        errMsg += "quantity must be numeric. ";
                    }
                }
            }
        }
    }
}