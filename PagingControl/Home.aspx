<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PagingControl.Home" EnableEventValidation="false" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "#C2D69B";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "#C2D69B";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else {
                if (checkbox.checked) {
                    objRef.style.backgroundColor = "#C2D69B";
                }
                else if (evt.type == "mouseout") {
                    if (objRef.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        objRef.style.backgroundColor = "white";
                    }
                    else {
                        objRef.style.backgroundColor = "white";
                    }

                }
            }
        }
//-->
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="Script" runat="server"></asp:ScriptManager>
            <asp:Panel ID="panel" runat="server">

                <asp:TextBox ID="txtOrder" runat="server"></asp:TextBox>
                <asp:Button ID="bth" runat="server" Text="Button" OnClick="bth_Click"></asp:Button>
            </asp:Panel>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panel1" runat="server">

                        <asp:GridView
                            ID="gvuser" runat="server"
                            AutoGenerateColumns="False"
                            OnRowDataBound="gvuser_RowDataBound"
                            OnSorting="gvuser_Sorting"
                            EmptyDataText="------ ไม่พบข้อมูล ------"
                            EmptyDataRowStyle-HorizontalAlign="Center"
                            HeaderStyle-BackColor="#FFECCD"
                            Style="max-width: 100%" AllowPaging="True" OnPageIndexChanging="gvuser_PageIndexChanging"
                            OnSelectedIndexChanged="gvuser_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AgreementNo" HeaderStyle-HorizontalAlign="Center" HeaderText="AgreementNo" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EarlyDocNo" HeaderStyle-HorizontalAlign="Center" HeaderText="EarlyDocNo" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderStyle-HorizontalAlign="Center" HeaderText="Amount" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#FFECCD" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:TextBox ID="txtdata" runat="server" Width="100px"></asp:TextBox>

                </ContentTemplate>
                    
            </asp:UpdatePanel>

            <hr />
            <br />
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panel2" runat="server">

                        <asp:GridView
                            ID="gvdata" runat="server"
                            AutoGenerateColumns="False"
                            OnRowDataBound="gvdata_RowDataBound"
                            OnSorting="gvdata_Sorting"
                            EmptyDataText="------ ไม่พบข้อมูล ------"
                            EmptyDataRowStyle-HorizontalAlign="Center"
                            HeaderStyle-BackColor="#FFECCD"
                            Style="max-width: 100%" AllowPaging="True" OnPageIndexChanging="gvdata_PageIndexChanging"
                            OnSelectedIndexChanged="gvdata_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AgreementNo" HeaderStyle-HorizontalAlign="Center" HeaderText="AgreementNo" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EarlyDocNo" HeaderStyle-HorizontalAlign="Center" HeaderText="EarlyDocNo" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Amount" HeaderStyle-HorizontalAlign="Center" HeaderText="Amount" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Center" HeaderText="Description" ItemStyle-HorizontalAlign="Left" SortExpression="Username">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#FFECCD" />
                        </asp:GridView>
                    </asp:Panel>

                   

                </ContentTemplate>
                    
            </asp:UpdatePanel>
            

        </div>
    </form>
</body>
</html>
