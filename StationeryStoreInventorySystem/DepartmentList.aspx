<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentList.aspx.cs" Inherits="DepartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         /*.headerrow {
           color:white;
           background-color:grey;
        }*/
         .middlerow {
           background-color:ButtonHighlight;
        }
        .auto-style1 {
            width: 54px;
        }
        .auto-style2 {
            width: 54px;
            height: 46px;
        }
        .auto-style3 {
            height: 46px;
        }
        .auto-style8 {
            height: 46px;
            width: 393px;
        }
        .auto-style9 {
            width: 133px;
        }
        .auto-style10 {
            height: 46px;
            width: 100px;
        }
        .auto-style11 {
            width: 100px;
        }
        .auto-style13 {
            width: 130px;
        }
        .auto-style14 {
            height: 46px;
            width: 100px;
        }
        .auto-style15 {
            width: 88px;
        }
      .auto-style16 {
            width: 84px;
        }
        .auto-style17 {
            width: 34px;
        }
    	.text2			{ font-family: Arial;  font-size  :10pt; color : #000000; }
		.auto-style18 {
            height: 46px;
            width: 104px;
        }
        .auto-style19 {
            width: 100px;
        }
		</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <h2 class="auto-style1">Department List</h2>
  
        
        <table border="1">
            <tr class="headerrow" >
                <th class="auto-style2">No.</th>
                <th>Department Code</th>
                <th class="auto-style14">Department Name</th>
                <th class="auto-style14">Contact Name</th>
                <th class="auto-style10">Telephone No.</th>
                <th class="auto-style10">Fax No.</th>
                <th class="auto-style16">Head&#39;s Name</th>
                <th class="auto-style16">Delegated Employee</th>
                <th class="auto-style18">Collection Point</th>
                <th class="auto-style14">Representative Name</th>
            </tr>
            <tr>
                <td class="auto-style1">1</td>
                <td class="auto-style17">Department List</td>
                <td class="auto-style15">English Dept</td>
                <td class="auto-style9">Mrs. Pamela Kow</td>
                <td class="auto-style11">8742234</td>
                <td class="auto-style11">8921456</td>
                <td class="auto-style13">Prof. Ezra Pound</td>
                <td class="auto-style17">Michelle </td>
                <td class="auto-style19">University Hospital</td>
                <td>Lim</td>
            </tr>
            <tr class="middlerow">
                <td class="auto-style1">2</td>
                <td class="auto-style17">CPSC</td>
                <td class="auto-style15">Computer Science</td>
                <td class="auto-style9">Mr. Wee Kian Fatt</td>
                <td class="auto-style11">8901235</td>
                <td>8921457</td>
                <td class="auto-style13">Dr. Soh Kian Wee</td>
                <td class="auto-style17">Nicole</td>
                <td class="auto-style19">Stationery Store</td>
                <td>Sean</td>
            </tr>
            <tr>
                <td class="auto-style1">3</td>
                <td class="auto-style17">COMM</td>
                <td class="auto-style15">Commerce Department</td>
                <td class="auto-style9">Mr. Mohd. Azman</td>
                <td class="auto-style11">8741284</td>
                <td>8921256</td>
                <td class="auto-style13">Dr. Chia Leow Bee</td>
                <td class="auto-style17">Jasmine</td>
                <td class="auto-style19">Management School</td>
                <td>Eric</td>
            </tr>
            <tr>
                <td class="auto-style1">4</td>
                <td class="auto-style17">REGR</td>
                <td class="auto-style15">Registar Dept</td>
                <td class="auto-style9">Ms. Helen Ho</td>
                <td class="auto-style11">8901266</td>
                <td>8921465</td>
                <td class="auto-style13">Mrs. Low Kway Boo</td>
                <td class="auto-style17">Jenny Wong Mei Lin</td>
                <td class="auto-style19">Science School</td>
                <td>Roy</td>
            </tr>
            <tr>
                <td class="auto-style1">5</td>
                <td class="auto-style17">ZOOL</td>
                <td class="auto-style15">Zoology Dept</td>
                <td class="auto-style9">Mr. Peter Tan Ah Meng</td>
                <td class="auto-style11">8901266</td>
                <td>8921465</td>
                <td class="auto-style13">Prof. Tan</td>
                <td class="auto-style17">Grace</td>
                <td class="auto-style19">Engineering School</td>
                <td>Alexis</td>
            </tr>
           
        </table>
        
    
    <p>
        
        &nbsp;</p>
  
    <p>
        
        &nbsp;</p>
    <p>
        
    
</asp:Content>

